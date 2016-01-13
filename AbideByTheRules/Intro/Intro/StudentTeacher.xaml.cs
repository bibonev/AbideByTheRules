/**
 * Boyan Bonev @2011/2012
 **/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AbideByTheRules;
using System.Diagnostics;
using System.Xml;

namespace AbideByTheRules
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class StudentTeacher : Window
	{
        public StudentTeacher()
		{
			this.InitializeComponent();
            this.WindowState = WindowState.Maximized;
            helpTxtBox.IsReadOnly = true;
            aboutTxtBox.IsReadOnly = true;
			// Insert code required on object creation below this point.
		}

        #region Fields
        string outPutFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Microsoft\Windows\Start Menu\Programs\Startup\AbideTheRules\Teachers.xml"; //
        List<Teacher> teachers = new List<Teacher>();
        bool passCheck = false, userCheck = false, userExicst = true;
        #endregion

        #region Login

        private void go_btn_Click(object sender, RoutedEventArgs e)
        {
            string user = username_enter_txtbox.Text;
            string pass = pass_enter_txtbox.Password;

            using (XmlReader read = XmlReader.Create(outPutFile))
            {
                while (read.Read())
                {
                    if (read.IsStartElement())
                    {
                        if (read.Name == "Username" || read.Name == "Password")
                        {
                            if (read.Read())
                            {
                                if (user == read.Value)
                                {
                                    userCheck = true;
                                }

                                if (pass == read.Value)
                                {
                                    passCheck = true;
                                }
                            }
                        }
                    }
                }
            }

            if (userCheck == true && passCheck == true)
            {
                var work = new Work();
                work.Show();
                userCheck = false;
                passCheck = false;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Грешно потребителско име/парола");
            } 
        }

        #endregion

        #region Register

        private void username_reg_txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            username_enter_txtbox.MaxLength = 20;
        }

        private void pass_reg_txtbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pass_reg_txtbox.MaxLength = 20;
        }

        private void Read()
        {
            string user = username_reg_txtbox.Text;
            using (XmlReader read = XmlReader.Create(outPutFile))
            {
                while (read.Read())
                {
                    if (read.IsStartElement())
                    {
                        if (read.Name == "Username")
                        {
                            if (read.Read())
                            {
                                if (user != read.Value.Trim())
                                {
                                    userExicst = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Write()
        {
            if (!userExicst)
            {
                using (XmlWriter writer = XmlWriter.Create(outPutFile))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Teachers");

                    foreach (Teacher teacher in teachers)
                    {
                        writer.WriteStartElement("Teacher");

                        writer.WriteElementString("FirstName", teacher.FName.ToString());
                        writer.WriteElementString("LastName", teacher.LName.ToString());
                        writer.WriteElementString("Username", teacher.Username.ToString());
                        writer.WriteElementString("Password", teacher.Password.ToString());

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            else { MessageBox.Show("Потребителското име е заето!"); }
        }

        private void parametersDB()
        {
            Teacher newTeacher = new Teacher(username_reg_txtbox.Text, pass_reg_txtbox.Password, name_reg_txtbox.Text, family_reg_txtbox.Text);
            teachers.Add(newTeacher);
            Read();
            Write();
            MessageBox.Show("Вие се регистрирахте успешно");
            
            username_reg_txtbox.Text = "";
            pass_reg_txtbox.Password = "";
            name_reg_txtbox.Text = "";
            family_reg_txtbox.Text = "";
            code_reg_txtbox.Text = "";
        }

        private void registration_reg_btn_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            string[] checkCode = { "AIOO2", "D24WE", "12DW0", "QW22P" };
            for (int index = 0; index < 4; index++)
            {
                if (checkCode[index] == code_reg_txtbox.Text)
                {
                    check = true;
                }
            }

            if ((username_reg_txtbox.Text.Length >= 6) &&
                (pass_reg_txtbox.Password.Length >= 6) &&
                (check == true))
            {
                parametersDB();
            }
 
            else
            {
                MessageBox.Show("Грешни данни!");
                username_reg_txtbox.Text = "";
                pass_reg_txtbox.Password = "";
                name_reg_txtbox.Text = "";
                family_reg_txtbox.Text = "";
                notCorrect_reg_label.Visibility = Visibility;
            }
        }

        #endregion


        //If the user is student:
        private void studentUser_Click(object sender, RoutedEventArgs e)
        {
            TypeOfUser.ChekUser = false;
            TypeOfUser.NameUser = "Ученик";
            var work = new Work();
            work.Show();
            this.Hide();
        }

        private void bicycleBtn_Click(object sender, RoutedEventArgs e)
        {
            var game = new BicycleLevels();
            game.Show();
            this.Hide();
        }

        private void backToMenu_Click(object sender, RoutedEventArgs e)
        {
            var menu = new SkipIntro();
            menu.Show();
            this.Hide();
        }

        
	}
}
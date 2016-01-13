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
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace Intro
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        
		public MainWindow()
		{
			this.InitializeComponent();
            this.WindowState = System.Windows.WindowState.Maximized;
			// Insert code required on object creation below this point.
		}

        SqlConnection connDB = new SqlConnection("Server=.\\SQLEXPRESS; Database=PanelsProject2011; Integrated Security=true");

        bool checkPointFirst = false, checkPointSecond = false, checkPointThird = false;
        //string drawingLines = "DrawingLines";
        string userText, passText;
        bool check;
        

        private void go_btn_Click(object sender, RoutedEventArgs e)
        {
            connDB.Open();
            SqlCommand cmdCheck = new SqlCommand("SELECT Username, Password FROM Users WHERE Username = '" + username_enter_txtbox.Text + "' AND Password = '" + pass_enter_txtbox.Password + "'", connDB);
            SqlDataReader read = cmdCheck.ExecuteReader();
            userText = username_enter_txtbox.Text;
            passText = pass_enter_txtbox.Password;

            read.Read();
            if (read.HasRows)
            {
                name_label_main.Content = userText;
                Storyboard animation = (Storyboard)FindResource("Enter_to_the_system");
                animation.Begin();
            }
            else
            {
                MessageBox.Show("not ok");
            }
            read.Close();
            connDB.Close();
            
        }

        private void parametersDB(string usertype)
        {
            connDB.Open();
            SqlCommand cmdInsert = new SqlCommand
            ("INSERT INTO Users (Username, Password, FirstName, LastName, UserType) VALUES ('" + username_reg_txtbox.Text + "','" + pass_reg_txtbox.Password + "','" + name_reg_txtbox.Text + "','" + family_reg_txtbox.Text + "', '" + usertype + "')", connDB);
            cmdInsert.ExecuteNonQuery();
            connDB.Close();

            MessageBox.Show("Регистрацията премина успешно!");

            username_reg_txtbox.Text = "";
            pass_reg_txtbox.Password = "";
            name_reg_txtbox.Text = "";
            family_reg_txtbox.Text = "";
            comboBox.SelectedValue = null;
            notCorrect_reg_label.Visibility = Visibility.Collapsed;
            comboBox.SelectedValue = "";

        }

        private void registration_reg_btn_Click(object sender, RoutedEventArgs e)
        {
            string[] checkCode = { "AIOO2", "D24WE", "12DW0", "QW22P" };
            for (int index = 0; index < 4; index++)
            {
                if (checkCode[index] == code_reg_txtbox.Text)
                {
                    check = true;
                }
            }


            if ((username_reg_txtbox.Text.Length > 6) &&
                (pass_reg_txtbox.Password.Length > 6) &&
                (comboBox.SelectedValue != null))
            {
                if (teacher.IsSelected == true && check == true)
                {
                    string teacherSTR = "Teacher";
                    parametersDB(teacherSTR);
                }
                else if (student.IsSelected == true)
                {
                    string studentSTR = "Student";
                    parametersDB(studentSTR);
                }
            }

            else 
            {
                username_reg_txtbox.Text = "";
                pass_reg_txtbox.Password = "";
                name_reg_txtbox.Text = "";
                family_reg_txtbox.Text = "";
                comboBox.SelectedValue = null;

                notCorrect_reg_label.Visibility = Visibility;
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (teacher.IsSelected == true)
            {
                code_reg_label.Visibility = Visibility;
                code_reg_txtbox.Visibility = Visibility;
            }
            if (student.IsSelected == true)
            {
                code_reg_txtbox.Visibility = Visibility.Hidden;
                code_reg_label.Visibility = Visibility.Hidden;
            }
        }

        private void username_reg_txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            username_enter_txtbox.MaxLength = 20;
        }

        private void pass_reg_txtbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pass_reg_txtbox.MaxLength = 20;
        }

        private void back_reg_btn_Click(object sender, RoutedEventArgs e)
        {
            code_reg_label.Visibility = Visibility.Collapsed;
            code_reg_txtbox.Visibility = Visibility.Collapsed;
            notCorrect_reg_label.Visibility = Visibility.Collapsed;
            comboBox.SelectedValue = "";

        }

        private void Animations(string animationName)
        {
            Storyboard animation = (Storyboard)FindResource(animationName);
            animation.Begin();
        }

        private void Enabling()
        {
            CheckPoint_firstmap_1.IsEnabled = false;
            CheckPoint_firstmap_2.IsEnabled = false;
            CheckPoint_firstmap_3.IsEnabled = false;
            CheckPoint_firstmap_4.IsEnabled = false;
            CheckPoint_firstmap_5.IsEnabled = false;
            CheckPoint_firstmap_6.IsEnabled = false;
            CheckPoint_firstmap_7.IsEnabled = false;
        }

        private void CheckPoint_firstmap_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            checkPointFirst = true;
            MessageBox.Show("aaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }

        private void CheckPoint_firstmap_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            checkPointSecond = true;
            MessageBox.Show("adsasaaa");
            
        }

        private void CheckPoint_firstmap_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            checkPointThird = true;
            MessageBox.Show("asdasd");

            correct_result_firstGrade_map1.Visibility = Visibility.Hidden;
            notCorrect_result_FirstGrade_map1.Visibility = Visibility.Hidden;
        }

        private void CheckPoint_firstmap_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            while (true)
            {
                if (checkPointFirst == true && checkPointSecond == true && checkPointThird == true)
                {
                    MessageBox.Show("Браво това е верния път!");
                    // Start Animation
                 //   Animations(drawingLines);
                    // Enable the check points
                    Enabling();
                }
                else
                {
                    MessageBox.Show("Решение: Съжалявам, но това е грешният път!");
                 //   Animations(drawingLines);
                    Enabling();
                }
                break;
            }
        }









         
	}
}
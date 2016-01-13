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
        string userText, passText;

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
                MessageBox.Show("ok");
            }
            else
            {
                MessageBox.Show("not ok");
            }
            read.Close();
            connDB.Close();
            
        }
	}
}
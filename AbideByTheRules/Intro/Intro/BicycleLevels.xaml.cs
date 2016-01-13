/**
 * Boyan Bonev @2011/2012
 **/
using System;
using System.Windows;
using System.Diagnostics;

namespace AbideByTheRules
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class BicycleLevels : Window
	{
        public BicycleLevels()
		{
			this.InitializeComponent();
            this.WindowState = WindowState.Maximized;
			// Insert code required on object creation below this point.
		}

        //If the user is student:
        private void studentUser_Click(object sender, RoutedEventArgs e)
        {
            String startupPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            Process.Start(startupPath + @"\Game\WindowsGameN.exe");
        }

        private void backToMenu_Click(object sender, RoutedEventArgs e)
        {
            var menu = new SkipIntro();
            menu.Show();
            this.Hide();
        }

        private void teacher_Click(object sender, RoutedEventArgs e)
        {
            String startupPath = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            Process.Start(startupPath + @"\Game\Map2\WindowsGameN.exe");
        }
        
	}
}
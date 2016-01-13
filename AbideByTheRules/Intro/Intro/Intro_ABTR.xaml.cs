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
using System.Diagnostics;
using System.Xml;

namespace AbideByTheRules
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class IntroABTR : Window
	{
		public IntroABTR()
		{
			this.InitializeComponent();
            this.WindowState = WindowState.Maximized;
            helpTxtBox.IsReadOnly = true;
            aboutTxtBox.IsReadOnly = true;
			// Insert code required on object creation below this point.
		}
		
        private void bicycleBtn_Click(object sender, RoutedEventArgs e)
        {
            var game = new BicycleLevels();
            game.Show();
            this.Hide();
        }

        private void safetyRoadBtn_Click(object sender, RoutedEventArgs e)
        {
            var studentTeacher = new StudentTeacher();
            studentTeacher.Show();
            this.Hide();
        }

	}
}
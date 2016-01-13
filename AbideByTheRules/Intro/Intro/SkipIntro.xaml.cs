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
	public partial class SkipIntro : Window
	{
		public SkipIntro()
		{
			this.InitializeComponent();
            this.WindowState = WindowState.Maximized;
            helpTxtBox.IsReadOnly = true;
            aboutTxtBox.IsReadOnly = true;
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
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Collections;
using System.Diagnostics;

namespace AbideByTheRules
{
    public partial class EditMap : Window
    {
        public EditMap()
        {
            this.InitializeComponent();
            this.WindowState = WindowState.Maximized;
            walkways.Add(path1Walkway1);
            walkways.Add(path2Walkway1);
            walkways.Add(path3Walkway1);
            walkways.Add(path4Walkway1);
            walkways.Add(path5Walkway1);
            walkways.Add(path1Walkway2);
            walkways.Add(path2Walkway2);
            walkways.Add(path3Walkway2);
            walkways.Add(path4Walkway2);
            walkways.Add(path5Walkway2);
            walkways.Add(path6Walkway2);
            walkways.Add(path1Walkway3);
            walkways.Add(path2Walkway3);
            walkways.Add(path3Walkway3);
            walkways.Add(path4Walkway3);
            walkways.Add(path5Walkway3);
            walkways.Add(path1Walkway4);
            walkways.Add(path2Walkway4);
            walkways.Add(path3Walkway4);
            walkways.Add(path4Walkway4);
            walkways.Add(path5Walkway4);
            walkways.Add(path6Walkway4);

            for (int element = 1; element <= 7; element++)
            {
                checkPoints.Add(element, false);
            }

            ellipses.Add(1, checkElipse1);
            ellipses.Add(2, checkElipse2);
            ellipses.Add(3, checkElipse3);
            ellipses.Add(4, checkElipse4);
            ellipses.Add(5, checkElipse5);
            ellipses.Add(6, checkElipse6);
            ellipses.Add(7, checkElipse7);
            // Insert code required on object creation below this point.
        }

        private bool checkEditMapLoad = false;

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            checkEditMapLoad = true;

            for (int index = 1; index <= 7; index++) 
            {
                GoToWhite(ellipses[index]);
            }
        }

        #region FirstStep

        private List<Path> walkways = new List<Path>(23);

        private void ColorEnable(Path path)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromRgb(255, 255, 255);
            path.Fill = mySolidColorBrush;
            path.IsEnabled = false;
        }

        private void walkway1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorEnable(path1Walkway1);
            ColorEnable(path2Walkway1);
            ColorEnable(path3Walkway1);
            ColorEnable(path4Walkway1);
            ColorEnable(path5Walkway1);
        }

        private void walkway2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorEnable(path1Walkway2);
            ColorEnable(path2Walkway2);
            ColorEnable(path3Walkway2);
            ColorEnable(path4Walkway2);
            ColorEnable(path5Walkway2);
            ColorEnable(path6Walkway2);
        }

        private void walkway3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorEnable(path1Walkway3);
            ColorEnable(path2Walkway3);
            ColorEnable(path3Walkway3);
            ColorEnable(path4Walkway3);
            ColorEnable(path5Walkway3);
        }

        private void walkway4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorEnable(path1Walkway4);
            ColorEnable(path2Walkway4);
            ColorEnable(path3Walkway4);
            ColorEnable(path4Walkway4);
            ColorEnable(path5Walkway4);
            ColorEnable(path6Walkway4);
        }

        private void forwardToNextStep_Click(object sender, RoutedEventArgs e)
        {

            for (int index = 0; index <= 21; index++)
            {
                if (walkways[index].IsEnabled == true)
                {
                    walkways[index].Visibility = Visibility.Hidden;
                }
            }
     
            secondStepLabel.Visibility = Visibility.Visible;
            choosePlaceKidLabel.Visibility = Visibility.Visible;
            theKidImage.Visibility = Visibility.Visible;
            hereLabel1.Visibility = Visibility.Visible;
            hereLabel2.Visibility = Visibility.Visible;
            hereLabel3.Visibility = Visibility.Visible;
            canvasHere1.Visibility = Visibility.Visible;
            canvasHere2.Visibility = Visibility.Visible;
            canvasHere3.Visibility = Visibility.Visible;
            forwardToNextStep.Visibility = Visibility.Hidden;
            forwardToNextStep.IsEnabled = false;
            forwardToLastStep.Visibility = Visibility.Visible;
        }
        #endregion
         
        #region SecondStep
        //For moving
        Point anotherPoint;
        Point currentPoint;
        bool isInDrag = false;
            // For labels
        bool checkVisibilityLabel1 = false;
        bool checkVisibilityLabel2 = false;
        bool checkVisibilityLabel3 = false;
        bool notCorrect = false;

        private void theKidImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            anotherPoint = e.GetPosition(null);
            element.CaptureMouse();
            isInDrag = true;
            e.Handled = true;
        }

        private void theKidImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (isInDrag)
            {
                FrameworkElement element = sender as FrameworkElement;
                currentPoint = e.GetPosition(null);

                imageTransform.X += (currentPoint.X - anotherPoint.X);
                imageTransform.Y += (currentPoint.Y - anotherPoint.Y);

                anotherPoint = currentPoint;
            }
        }

        private void ImageInside(FrameworkElement image, FrameworkElement rectangle, Visual root, 
                                    Label labVisible, Label labVisible2, Label labHidden)
        {
            Rect imageRect = image.TransformToVisual(root).TransformBounds(new Rect(new Size(theKidImage.Width, theKidImage.Height)));
            Rect canvasRect = rectangle.TransformToVisual(root).TransformBounds(new Rect(new Size(rectangle.Width, rectangle.Height)));

            if (imageRect.Left >= canvasRect.Left && 
                imageRect.Right <= canvasRect.Right && 
                imageRect.Bottom <= canvasRect.Bottom &&
                imageRect.Top >= canvasRect.Top)
            {
                labHidden.Visibility = Visibility.Hidden;
                labVisible.Visibility = Visibility.Visible;
                labVisible2.Visibility = Visibility.Visible;
            }
            else if (imageRect.Left >= canvasRect.Left &&
                     imageRect.Right <= canvasRect.Right &&
                     imageRect.Bottom <= canvasRect.Bottom &&
                     imageRect.Top <= canvasRect.Top)
            {
                labHidden.Visibility = Visibility.Visible;
                labVisible.Visibility = Visibility.Hidden;
                labVisible2.Visibility = Visibility.Visible;
            }
            
        }

        private void theKidImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isInDrag)
            {
                FrameworkElement element = sender as FrameworkElement;
                element.ReleaseMouseCapture();
                isInDrag = false;
                e.Handled = true;

                ImageInside(element, canvasHere3, this, hereLabel1, hereLabel2, hereLabel3);
                ImageInside(element, canvasHere2, this, hereLabel1, hereLabel3, hereLabel2);
                ImageInside(element, canvasHere1, this, hereLabel2, hereLabel3, hereLabel1);

                if (hereLabel1.Visibility == Visibility.Hidden)
                {
                    checkVisibilityLabel1 = true;
                }
                else if (hereLabel2.Visibility == Visibility.Hidden)
                {
                    checkVisibilityLabel2 = true;
                }
                else if (hereLabel3.Visibility == Visibility.Hidden)
                {
                    checkVisibilityLabel3 = true;
                }
                else
                {
                    notCorrect = true;
                }
            }
        }
#endregion

        #region ThirdStep

        private Dictionary<int, Ellipse> ellipses = new Dictionary<int, Ellipse>(7);

        private Dictionary<int, bool> checkPoints = new Dictionary<int, bool>(7);

        private void GoToBlack(Ellipse checkPoint)
        {
            SolidColorBrush black = new SolidColorBrush();
            black.Color = Color.FromRgb(0, 0, 0);
            checkPoint.Fill = black;    
        }

        private void GoToWhite(Ellipse checkPoint)
        {
            SolidColorBrush white = new SolidColorBrush();
            white.Color = Color.FromRgb(255, 255, 255);
            checkPoint.Fill = white;
        }

        private void VisibilityCheck()
        {
            checkElipse1.Visibility = Visibility.Visible;
            checkElipse2.Visibility = Visibility.Visible;
            checkElipse3.Visibility = Visibility.Visible;
            checkElipse4.Visibility = Visibility.Visible;
            checkElipse5.Visibility = Visibility.Visible;
            checkElipse6.Visibility = Visibility.Visible;
            checkElipse7.Visibility = Visibility.Visible;
            checkElipseOutside1.Visibility = Visibility.Visible;
            checkElipseOutside2.Visibility = Visibility.Visible;
            checkElipseOutside3.Visibility = Visibility.Visible;
            checkElipseOutside4.Visibility = Visibility.Visible;
            checkElipseOutside5.Visibility = Visibility.Visible;
            checkElipseOutside6.Visibility = Visibility.Visible;
            checkElipseOutside7.Visibility = Visibility.Visible;
        }

        private void forwardToLastStep_Click(object sender, RoutedEventArgs e)
        {
            if (notCorrect == true)
            {
                MessageBox.Show("Моля изберете място на детето!", "Грешка!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            else
            {
                theKidImage.IsEnabled = false;
                canvasHere1.Visibility = Visibility.Hidden;
                canvasHere2.Visibility = Visibility.Hidden;
                canvasHere3.Visibility = Visibility.Hidden;
                hereLabel1.Visibility = Visibility.Hidden;
                hereLabel2.Visibility = Visibility.Hidden;
                hereLabel3.Visibility = Visibility.Hidden;
                thirdStepLabel.Visibility = Visibility.Visible;
                chooseRightWayLabel.Visibility = Visibility.Visible;
                forwardToLastStep.Visibility = Visibility.Hidden;
                saveFileBtn.Visibility = Visibility.Visible;
                theKidImage.Visibility = Visibility.Visible;
                VisibilityCheck();
            }
        }
            // Click Ellipse

        private void CheckPoint_firstmap_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GoToBlack(ellipses[1]);
            checkPoints[1] = true;
        }

        private void CheckPoint_firstmap_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GoToBlack(ellipses[2]);
            checkPoints[2] = true;
        }

        private void CheckPoint_firstmap_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GoToBlack(ellipses[3]);
            checkPoints[3] = true;
        }

        private void CheckPoint_firstmap_4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GoToBlack(ellipses[4]);
            checkPoints[4] = true;
        }

        private void CheckPoint_firstmap_5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GoToBlack(ellipses[5]);
            checkPoints[5] = true;
        }

        private void CheckPoint_firstmap_6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GoToBlack(ellipses[6]);
            checkPoints[6] = true;
        }

        private void CheckPoint_firstmap_7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GoToBlack(ellipses[7]);
            checkPoints[7] = true;
        }

        private void Result()
        {
            for (int id = 1; id <= 7; id++)
            {
                GoToWhite(ellipses[id]);
            }

            for (int checkPoint = 1; checkPoint <= 7; checkPoint++)
            {
                checkPoints[checkPoint] = false;
            }
        }

        #endregion

        #region SaveFile
        private void saveFileBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!checkPoints[4])
            {
                MessageBox.Show("Моля изберете крайната точка - тази до училището!", "Грешка!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            else if (checkPoints[4])
            {
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.Filter = "AbideByTheRules Files|*.txt";
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = true;

                Nullable<bool> result = dialog.ShowDialog();
                
                if (result == true)
                {
                    System.IO.FileStream fs = null;

                    if (!System.IO.File.Exists(dialog.FileName))
                    {
                        using (fs = System.IO.File.Create(dialog.FileName))
                        {
                            MessageBox.Show("Вие създадахте вашия файл успешно!", "Готово!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                            var work = new Work();
                            work.Show();
                            this.Hide();
                        }
                    }

                    if (System.IO.File.Exists(dialog.FileName))
                    {
                        using (System.IO.StreamWriter writeInfo = new System.IO.StreamWriter(dialog.FileName, true))
                        {
                            try
                            {
                                // WRITE CORRECT WAY
                                writeInfo.WriteLine("CheckPoints:");
                                for (int i = 1; i <= 7; i++)
                                {
                                    if (checkPoints[i] == true)
                                    {
                                        writeInfo.WriteLine("cs{0}", i);
                                    }
                                }
                                //WRITE CROSSZEBRA
                                writeInfo.WriteLine("Paths:");
                                for (int index = 0; index <= 21; index++)
                                {
                                    if (walkways[index].Visibility == Visibility.Visible)
                                    {
                                        writeInfo.WriteLine("p{0}", index);
                                    }
                                }
                                //WRITE LOCATION OF KID
                                writeInfo.WriteLine("Location of kid:");
                                writeInfo.WriteLine("l{0}", imageTransform.X.ToString());
                                writeInfo.WriteLine("l{0}", imageTransform.Y.ToString());
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Информацията не може да бъде записана!", "Грешка!", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                            }
                        }
                    }
                }
            }
                     
        }
        #endregion

        #region OpenFile

        private String[] readCheckPoints = new String[7];
        private bool checkLoadMap = false;
        private void openBtn_Click(object sender, RoutedEventArgs e)
        {
            
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
            openFile.Filter = "AbideByTheRules Files|*.txt";
            openFile.Title = @"Select a '.txt' file";
            openFile.ShowDialog();
            

            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
               try
               {
                    using (System.IO.StreamReader read = new System.IO.StreamReader(openFile.FileName))
                    {
                        // GENERAL SETTINGS

                        // Set Settings

                        for (int index = 1; index <= 7; index++)
                        {
                            GoToWhite(ellipses[index]);
                        }

                        String readLines;
                        // Indexes of reading files
                        int indexCheckPoint = 0, indexPath = 0, indexLoc = 0;
                        // Arrays of elements, which are going to read
                        String[] readPaths = new String[22];
                        String[] readLoc = new String[2];
                        
                        while ((readLines = read.ReadLine()) != null)
                        {
                            if (readLines.StartsWith("cs"))
                            {
                                readCheckPoints[indexCheckPoint] = readLines;
                                indexCheckPoint++;
                            }

                            if (readLines.Contains("p"))
                            {
                                readPaths[indexPath] = readLines;
                                indexPath++;
                            }
                            if (readLines.Contains("l"))
                            {
                                readLoc[indexLoc] = readLines;
                                indexLoc++;
                            }
                        }

                        //REMOVE LETTER FROM LINES
                        for (int i = 0; i <= 6; i++) 
                        {
                            if (readCheckPoints[i] != null)
                            {
                                readCheckPoints[i] = readCheckPoints[i].Remove(0, 2);
                            }
                        }

                        for (int i = 0; i <= 21; i++)
                        {
                            if (readPaths[i] != null)
                            {
                                readPaths[i] = readPaths[i].Remove(0, 1);
                            }
                        }

                        for (int i = 0; i <= 1; i++) 
                        {
                            readLoc[i] = readLoc[i].Remove(0, 1);
                        }

                        //GIVE PARAMETARS ...
                            // First - location of kid
                        imageTransform.X = double.Parse(readLoc[0]) - 30;
                        imageTransform.Y = double.Parse(readLoc[1]) - 50;
                        theKidImage.Visibility = Visibility.Visible;
                        theKidImage.IsEnabled = false;

                            // Second - location of zebras
                        for (int i = 0; i < readPaths.Length; i++)
                        {
                            if (readPaths[i] == null)
                            {
                                walkways[i].Visibility = Visibility.Hidden;
                            }
                        }
                            //Third - checking for way
                        checkLoadMap = true;
                        checkEditMapLoad = true;
                    }
                }
                 
                catch (System.Exception ex)
                {
                   MessageBox.Show("Програмата не може да отвори този файл!", "Грешка!", MessageBoxButton.OK, MessageBoxImage.Error , MessageBoxResult.OK);
                } 
            }
        }
        #endregion

        private void bicycleBtn_Click(object sender, RoutedEventArgs e)
        {
            var game = new BicycleLevels();
            game.Show();
            this.Hide();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;

namespace ImageViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer Animator = new DispatcherTimer(DispatcherPriority.Send);
        ScaleTransform Rotator = new ScaleTransform();
        BitmapImage NextImage;
        bool Grow = false;
        Queue<string> ImageList = new Queue<string>();

        double Tracker = 0;

        public MainWindow()
        {
            InitializeComponent();

            Animator.Interval = TimeSpan.FromMilliseconds(20);
            Animator.Tick += Animator_Tick;
        }

        void Animator_Tick(object sender, EventArgs e)
        {
            if(Grow == true)
            {
                Tracker += 0.05;
                if(Tracker > 1.5)
                {
                    Grow = false;
                }
                if(Tracker > 1)
                {
                    if (ImageList.Count != 0)
                    {
                        string ImageName = ImageList.Dequeue();
                        ImageList.Enqueue(ImageName);
                        NextImage = new BitmapImage(new Uri(@"" + ImageName));
                    }
                    GC.Collect();
                }
            }
            else
            {
                Tracker -= 0.05;

                if(Tracker < 0)
                {
                    Grow = true;
                    Picture.Source = NextImage;
                    GC.Collect();
                }
            }

            if (Tracker < 1)
            {
                Rotator.ScaleX = Tracker;
                Picture.Opacity = Tracker;
                Picture.RenderTransform = Rotator;
            }
            //if (Tracker < 2)
            //{
            //    Tracker += 0.05;
            //    Rotator.ScaleX = Tracker - 1;
            //    Picture.RenderTransform = Rotator;

            //    if (Tracker > 1.1)
            //    {
            //        Picture.Opacity += 0.05;
            //    }
            //    else if(Tracker < 1.0)
            //    {
            //        Picture.Opacity -= 0.05;
            //    }
            //    else
            //    {
            //        Picture.Source = NextImage;
            //        GC.Collect();
            //    }
            //}
            //else
            //{
            //    GoToNextImage();
            //}
        }

        private void Picture_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //GoToNextImage();
        }

        void GoToNextImage()
        {
            if (ImageList.Count != 0)
            {
                string ImageName = ImageList.Dequeue();
                ImageList.Enqueue(ImageName);
                NextImage = new BitmapImage(new Uri(@"" + ImageName));
                Tracker = 0;
                Animator.Start();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Open = new OpenFileDialog();
            Open.Title = "Select The Images for the SildeShow";
            Open.DefaultExt = ".jpg";
            Open.Multiselect = true;
            Open.ShowDialog();
            ImageList.Clear();
            foreach(var FileName in Open.FileNames)
            {
                if (FileName.Substring(FileName.Length - 4) == ".jpg")
                {
                    ImageList.Enqueue(FileName);
                }
            }

            GoToNextImage();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}

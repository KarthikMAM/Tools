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
using System.Windows.Ink;
using System.IO;

namespace WpfApplication6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int i;
        public MainWindow()
        {
            i = 0;
            InitializeComponent();
        }
     
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.ink.CopySelection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ink.Strokes.Clear();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "inkstrokes";
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "Image (.jpg)|*.jpg";
            dlg.InitialDirectory = "C:\\Users\\KARTHIK.M.A.M\\Documents\\4A Games\\";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                int width = (int)this.ink.ActualWidth+20;
                int height = (int)this.ink.ActualHeight+15;
                RenderTargetBitmap rtb = new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
                rtb.Render(ink);

                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));
                    encoder.Save(fs);
                }
            }
        }

        private void Eraser_Click(object sender, RoutedEventArgs e)
        {
            this.ink.EditingMode = InkCanvasEditingMode.EraseByPoint;
            i++;
        }

        private void Pen_Click(object sender, RoutedEventArgs e)
        {
            this.ink.EditingMode = InkCanvasEditingMode.Ink;
        }

    }
}

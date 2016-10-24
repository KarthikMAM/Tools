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
using System.Collections;
using System.IO;

namespace WpfApplication19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Initialization of the global variables
        string EncodedMenuTemplate;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuBuffer.Text != "" && MenuBuffer.Text != "Add Menu")
            {
                Content.Items.Add(MenuBuffer.Text);
                MenuBuffer.Clear();
                MenuBuffer.Focus();
            }
        }

        private void CodeGenerator_Click(object sender, RoutedEventArgs e)
        {
            if (Content.Items.IsEmpty == false)
            {
                EncodedMenuTemplate = "#include<iostream>";
                EncodedMenuTemplate += "\n\n" + "using namespace std;";
                EncodedMenuTemplate += "\n\n" + "int main()";
                EncodedMenuTemplate += "\n" + "{";
                EncodedMenuTemplate += "\n\n\t" + "int SwitchOption;";
                EncodedMenuTemplate += "\n\t" + "bool IsNextIterationAllowed = true;";
                EncodedMenuTemplate += "\n\n\n\t" + "do";
                EncodedMenuTemplate += "\n\t" + "{";
                EncodedMenuTemplate += "\n\t\t" + "cout<<\"\\n\\n--------------------------------------------------------------------------------\";";
                EncodedMenuTemplate += "\n\t\t" + "cout<<\"\\n\\n\\n\\tOPTIONS\\n\";";
                int CaseOptionIndicator = 0;
                foreach (string Menu in Content.Items)
                {
                    EncodedMenuTemplate += "\n\t\t" + "cout<<\"\\n\\t   " + ++CaseOptionIndicator + ". " + Menu.ToUpper() + "\";";
                }
                EncodedMenuTemplate += "\n\t\t" + "cout<<\"\\n\\t   " + ++CaseOptionIndicator + ". " + "EXIT" + "\";";
                EncodedMenuTemplate += "\n\t\t" + "cout<<\"\\n\\n\\t\\t\\tENTER YOUR OPTION : \";";
                EncodedMenuTemplate += "\n\t\t" + "cin>>SwitchOption;";
                EncodedMenuTemplate += "\n\t\t" + "cout<<\"\\n\\n--------------------------------------------------------------------------------\";";
                EncodedMenuTemplate += "\n\t\t" + "switch(SwitchOption)";
                EncodedMenuTemplate += "\n\t\t" + "{";

                CaseOptionIndicator = 0;
                foreach (string Menu in Content.Items)
                {
                    EncodedMenuTemplate += "\n\t\t" + "case " + ++CaseOptionIndicator + ":";
                    EncodedMenuTemplate += "\n\t\t\t" + "{";
                    EncodedMenuTemplate += "\n\t\t\t\t" + "//This Block Executes the Option : " + Menu.ToUpper();
                    EncodedMenuTemplate += "\n\t\t\t" + "}";
                    EncodedMenuTemplate += "\n\t\t\t" + "break;";
                }
                EncodedMenuTemplate += "\n\t\t" + "case " + ++CaseOptionIndicator + " : ";
                EncodedMenuTemplate += "\n\t\t\t" + "{";
                EncodedMenuTemplate += "\n\t\t\t\t" + "//This Block Executes the Option : " + "Exit";
                EncodedMenuTemplate += "\n\t\t\t\t" + "IsNextIterationAllowed=false;";
                EncodedMenuTemplate += "\n\t\t\t" + "}";
                EncodedMenuTemplate += "\n\t\t\t" + "break;";
                EncodedMenuTemplate += "\n\t\t" + "default : ";
                EncodedMenuTemplate += "\n\t\t\t" + "{";
                EncodedMenuTemplate += "\n\t\t\t\t" + "cout<<\"\\n\\n\\t\\t\\tINVALID OPTION\";";
                EncodedMenuTemplate += "\n\t\t\t" + "}";
                EncodedMenuTemplate += "\n\t\t\t" + "break;";
                EncodedMenuTemplate += "\n\t\t\t" + "}";
                EncodedMenuTemplate += "\n\t" + "}while(IsNextIterationAllowed == true);";
                EncodedMenuTemplate += "\n\t" + "cout<<\"\\n\\n--------------------------------------------------------------------------------\";";
                EncodedMenuTemplate += "\n\t" + "return 0;";
                EncodedMenuTemplate += "\n" + "}";
                Code.Text = EncodedMenuTemplate;
            }
            else
            {
                MessageBox.Show("Please Create Some Menus and Then Build the Template Code", "© Kappspot");
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Code.Text != "")
            {
                Microsoft.Win32.SaveFileDialog SaveAsDialogBox = new Microsoft.Win32.SaveFileDialog();
                SaveAsDialogBox.DefaultExt = ".cpp";
                SaveAsDialogBox.Filter = "C++ (*.cpp) | *.cpp";
                try
                {
                    if (SaveAsDialogBox.ShowDialog() == true)
                    {
                        FileStream CppFormatFile = new FileStream(SaveAsDialogBox.FileName, FileMode.CreateNew, FileAccess.Write);
                        StreamWriter Writer = new StreamWriter(CppFormatFile);
                        Writer.WriteLine(EncodedMenuTemplate);
                        Writer.Flush();
                        CppFormatFile.Close();
                    }
                }
                catch
                {
                    FileStream CppFormatFile = new FileStream(SaveAsDialogBox.FileName, FileMode.Truncate, FileAccess.Write);
                    StreamWriter Writer = new StreamWriter(CppFormatFile);
                    Writer.WriteLine(EncodedMenuTemplate);
                    Writer.Flush();
                    CppFormatFile.Close();
                }
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Content.Items.Clear();
            Code.Clear();
        }

        private void MenuBuffer_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MenuBuffer.Text == "Add Menu")
            {
                MenuBuffer.Clear();
            }
            MenuBuffer.IsUndoEnabled = true;
            MenuBuffer.PreviewKeyDown += MenuNames_PreviewKeyDown;
        }

        void MenuNames_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (MenuBuffer.Text != "Add Menu" && MenuBuffer.Text != "")
                {
                    Content.Items.Add(MenuBuffer.Text);
                    MenuBuffer.Clear();
                    MenuBuffer.Focus();
                }
            }
        }

        private void Content_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Content.SelectedIndex != -1)
            {
                Content.Items.Remove(Content.SelectedItem);
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FontSizeSlider.Maximum = 500;
            FontSizeSlider.Minimum = 100;
            Content.FontSize = FontSizeSlider.Value / 10 + 5;
            Code.FontSize = FontSizeSlider.Value / 10 - 5;
        }

        private void Instructions_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\nInstructions : " + "\n\n\t1. Type the menus that you wnat in your Lab Exercise" + "\n\t2. Click the Add Button or the Enter key" + "\n\t3. Double Click on an item to Delete that item" + "\n\t4. Click the build button to build the template code" + "\n\t5. Click the Save Button to SaveAsDialogBox it as a .cpp file" + "\n\t6. Build your code over it", "Kappspot Support");
        }
    }
}

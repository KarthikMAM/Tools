using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace vCardSplitter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Global Variables
        /// Values updated during the split process
        /// </summary>
        private string filePath = null;
        private List<string> contactData = null;
        private string savePath = null;

        /// <summary>
        /// Static Variables
        /// </summary>
        private static string[] SPLIT_TEXT = { "BEGIN:VCARD" };
        private static string APPEND_TEXT = "BEGIN:VCARD";
        private static string FINAL_MSG = "Split {0} Contacts";
        private static string CONTACT_FILE_NAME = "Contact{0}.vcf";
        private static string PATH_SEPERATOR = "\\";

        /// <summary>
        /// Initializes the main window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens the file, 
        /// Reads all its contents and returns the same
        /// </summary>
        /// <param name="path">The full path of the file</param>
        /// <returns>The data within the file</returns>
        public string ReadFromFile(string path)
        {
            //Create the container to store the data
            string contents = null;

            try
            {
                //Create the stream objects
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader fileReader = new StreamReader(file);

                //Read the data from the stream
                contents = fileReader.ReadToEnd();

                //Close the stream
                fileReader.Close();
                file.Close();
            }
            catch (Exception e) { MessageBox.Show(e.Message, "Error"); }

            //Return the data read
            return contents;
        }

        /// <summary>
        /// Writes the given data to the path specified
        /// </summary>
        /// <param name="data">The data to be written</param>
        /// <param name="path">The path to write</param>
        public void WriteToFile(string data, string path)
        {
            try
            {
                //Create the stream objects
                FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter fileWriter = new StreamWriter(file);

                //Write the data to the file
                fileWriter.Write(data);

                //Flush the stream and close it
                fileWriter.Flush();
                file.Close();
            }
            catch (Exception e) { MessageBox.Show(e.Message, "Error"); }
        }

        /// <summary>
        /// The logic for the vCard Splitting is implemented here
        /// </summary>
        /// <param name="sender">The select button</param>
        /// <param name="e">The event args</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Get the file path from the user
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Select";
            openDialog.Multiselect = false;
            openDialog.Filter = "vCard | *.vcf";

            //Show the dialog
            //Do the following if the user selects a vCard file
            if (openDialog.ShowDialog(this) == true)
            {
                //Save the file path
                filePath = openDialog.FileName;

                //Read the contents and if it is not empty
                //Split it
                string contents = ReadFromFile(filePath);
                if (contents != null)
                {
                    //Create the target path 
                    savePath = Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath));
                    Directory.CreateDirectory(savePath);

                    //Split the contents and add the redundant data
                    //And write the data to the corresponding files
                    contactData = new List<string>(contents.Split(SPLIT_TEXT, StringSplitOptions.RemoveEmptyEntries));
                    for (int i = 0; i < contactData.Count; i++)
                    {
                        contactData[i] = APPEND_TEXT + contactData[i]; 
                        WriteToFile(contactData[i], savePath + PATH_SEPERATOR + String.Format(CONTACT_FILE_NAME, i + 1)); 
                    }

                    //Show the confirmation message
                    //Open the save path location
                    MessageBox.Show(String.Format(FINAL_MSG, contactData.Count), "Split Complete!");
                    Process.Start(savePath);
                }
            }
        }
    }
}

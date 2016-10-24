using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Text_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Static constants for the menu
        private const string NEW = "New";
        private const string OPEN = "Open";
        private const string SAVE = "Save";
        private const string SAVEAS = "Save As";
        private const string EXIT = "Exit";

        //Static constants for other messages
        private const string TITLE = "TextEditor | {0}{1}";
        private const string UNTITLED = "Untitled";
        private const string CHANGE = "*";
        private const string NO_CHANGE = "";

        //Properties that define the state of the editor
        public string FilePath { get; private set; }
        public string FileName { get; private set; }
        public bool NeedSave { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //Reset the fields
            ResetEditor();
        }

        /// <summary>
        /// Resets the editor to the default state
        /// </summary>
        public void ResetEditor()
        {
            //Reset all fields which might change
            //during the runtime of the program
            Title = String.Format(TITLE, UNTITLED, NO_CHANGE);
            TextArea.Text = "";
            FilePath = "";
            FileName = UNTITLED;
            NeedSave = false;
        }

        /// <summary>
        /// Sets the state of the text editor
        /// </summary>
        /// <param name="filePath">The path where the contents are to be stored</param>
        public void SetState(string filePath)
        {
            //Set the state variables for this program
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
            Title = String.Format(TITLE, FileName, NO_CHANGE);
            NeedSave = false;
        }

        /// <summary>
        /// Opens the file from the filePath and sets the content of the text area to that path
        /// </summary>
        /// <param name="filePath">The path where the file must be loaded from</param>
        /// <returns>Whether the file was successfully loaded or not</returns>
        public bool ReadFile(string filePath)
        {
            //Object referances to be instantiated later
            FileStream file = null;
            StreamReader reader = null;
            bool result = false;

            try
            {
                //Create the stream and 
                //Use the streamreader to read the full content of the file
                file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(file);
                TextArea.Text = reader.ReadToEnd();

                //Success so set true
                result = true;
            }
            catch(Exception e)
            {
                //Failure so show the message
                //Set false to result
                MessageBox.Show(e.Message, "Unable to open");
                result = false;
            }
            finally
            {
                //Release the resources
                if (reader != null) { reader.Close(); }
                if (file != null) { file.Close(); }
            }

            //Return the result
            return result;
        }

        /// <summary>
        /// Saves the contents in the textarea to the required file path
        /// </summary>
        /// <param name="filePath">The path where the file must be saved</param>
        /// <returns>Whether the file is successfully saved or not</returns>
        public bool SaveFile(string filePath)
        {
            //Object referances to be instantiated later
            FileStream file = null;
            StreamWriter writer = null;
            bool result = false;

            try
            {
                //Open the stream
                //use the stream writer to write the data to the file
                if (File.Exists(filePath)) { file = new FileStream(filePath, FileMode.Truncate, FileAccess.Write); }
                else { file = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write); }
                writer = new StreamWriter(file);
                writer.Write(TextArea.Text);

                //Success so set true
                result = true;
            } 
            catch(Exception e)
            {
                //Failure so show the message
                //Set false to result
                MessageBox.Show(e.Message, "Unable to save");
                result = false;
            }
            finally
            {
                //Release the resources
                if (writer != null) { writer.Close(); }
                if (file != null) { file.Close(); }
            }

            //Return the results
            return result;
        }

        /// <summary>
        /// Event handler for when a menu is clicked
        /// </summary>
        /// <param name="sender">The menu which triggers this event</param>
        /// <param name="e">The parameters associated with it</param>
        private void MenuItem_Click(object sender, EventArgs e)
        {
            //Get the menu which triggered this event
            MenuItem menu = sender as MenuItem;

            //Do operation based on the menu the user selects
            switch (menu.Header.ToString()) 
            {
                case NEW:
                    //Reset the text editor
                    ResetEditor();
                    break;
                case OPEN:
                    //Create the open dialog
                    OpenFileDialog openDialog = new OpenFileDialog();
                    openDialog.Title = OPEN;
                    openDialog.CheckFileExists = true;
                       
                    //Display the dialog and get the user preferance
                    if (openDialog.ShowDialog() == true)
                    {
                        //If the user wants to open the file and
                        //If the file is successfully read change the state
                        if (ReadFile(openDialog.FileName)) { SetState(openDialog.FileName); }
                    }
                    break;
                case SAVE:
                    if (FilePath != "")
                    {
                        //If it is an existing file and
                        //If the file is successfully saved change state
                        if (SaveFile(FilePath)) { SetState(FilePath); }
                    }
                    else { goto case SAVEAS; } //If it is a new file which is not yeet saved even once
                    break;
                case SAVEAS:
                    //Create a save dialog
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Title = NEW;
                    saveDialog.OverwritePrompt = true;
                    
                    //Display the dialog and get the user preferance
                    if (saveDialog.ShowDialog() == true)
                    {
                        //If the user wants to save the file and
                        //If the file is successfully saved change the state
                        if (SaveFile(saveDialog.FileName)) { SetState(saveDialog.FileName); }
                    }
                    break;
                case EXIT:
                    Close();
                    break;
            }
        }

        /// <summary>
        /// The event handler callback for when the textarea changes its text
        /// </summary>
        /// <param name="sender">The textbox that triggers this event</param>
        /// <param name="e"></param>
        private void TextArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Change the state of the texteditor
            Title = String.Format(TITLE, FileName, CHANGE);
            NeedSave = true;
        }

        /// <summary>
        /// The event handler callback for when the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (NeedSave)
            {
                //If the file needs saving and
                //If the user decides to save the file save the file
                if (MessageBox.Show("Do you want to save the file?", "Unsaved Changes", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    MenuItem_Click(Save, new EventArgs());
                }
            }
        }


    }
}

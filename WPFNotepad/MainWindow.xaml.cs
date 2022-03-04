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
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;

/* FILE             :   MainWindow.xaml.cs
 * PROJECT          :   PROG2121 - Assignment 2
 * PROGRAMMER       :   Devin Graham
 * FIRST VERSION    :   2021-09-27
*/



namespace WPFNotepad
{
    /*
     * NAME    : MainWindow
     * PURPOSE : The MainWindow class has been created to model the behaviour of Microsoft Notedpad. The class contains
     *          event handlers and functions for the interaction logic for MainWindow.xaml. The interactions that this
     *          class models are new, open, save as, close, and about. This class also has an event handler for determining
     *          when changes have been made to the document.
     */
    public partial class MainWindow : Window
    {
        private bool unsavedText { get; set; }
        private string currentFilePath { get; set; }

        

        public MainWindow()
        {
            InitializeComponent();
            unsavedText = false;
            currentFilePath = "Untitled";
          
        }



        /*
         * FUNCTION     :   MenuNew_Click
         * DESCRIPTION  :   The event handler contains the logic to create a new work area in the Notepad application.
         *                  If there is unsaved work the user is given the oppurtunity to save their file.
         * PARAMETERS   :
         *      object sender - The instance of the object that raised the event
         *      RoutedEventArgs - Contains data associated with the event
         * RETURNS      :
         *      void
         */
        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            //Check if there is unsaved text in the workarea
            if(unsavedText)
            {
                string messageBoxText = "Do you want to save changes to " + currentFilePath + "?";
                string caption = "Notepad";
                MessageBoxButton buttons = MessageBoxButton.YesNoCancel;
                MessageBoxResult result = System.Windows.MessageBox.Show(messageBoxText, caption, buttons);

                //Check if the file already exists
                if (currentFilePath == "Untitled")
                {
                    //Retrieve user selection from save box and perform proper operation
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            saveFile(currentFilePath);
                            InputBox.Text = String.Empty;
                            unsavedText = false;
                            break;
                        case MessageBoxResult.No:
                            InputBox.Text = String.Empty;
                            unsavedText = false;
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }
                }
                else
                {
                    //Retrieve user selection from save box and perform proper operation
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            using (StreamWriter writer = File.CreateText(currentFilePath))
                            {
                                writer.WriteLine(InputBox.Text);
                            }
                            InputBox.Text = String.Empty;
                            currentFilePath = "Untitled";
                            unsavedText = false;
                            break;
                        case MessageBoxResult.No:
                            InputBox.Text = String.Empty;
                            currentFilePath = "Untitled";
                            unsavedText = false;
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }
                }
            }
            else
            {
                InputBox.Text = String.Empty;
                currentFilePath = "Untitled";
                unsavedText = false;
            }
        }



        /*
        * FUNCTION     :   MenuOpen_Click
        * DESCRIPTION  :   The event handler contains the logic to open and load a text file into the work area.
        *                  If there is unsaved work the user is given the oppurtunity to save their file.
        * PARAMETERS   :
        *      object sender - The instance of the object that raised the event
        *      RoutedEventArgs - Contains data associated with the event
        * RETURNS      :
        *      void
        */
        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            //Check if there is unsaved text in the workarea
            if (unsavedText)
            {
                string messageBoxText = "Do you want to save changes to " + currentFilePath + "?";
                string caption = "Notepad";
                MessageBoxButton buttons = MessageBoxButton.YesNoCancel;
                MessageBoxResult result = System.Windows.MessageBox.Show(messageBoxText, caption, buttons);

                //Check if the file already exists
                if (currentFilePath == "Untitled")
                {
                    //Retrieve user selection from save box and perform proper operation
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            saveFile(currentFilePath);
                            openFile();
                            break;
                        case MessageBoxResult.No:
                            openFile();
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }
                }
                else
                {
                    //Retrieve user selection from save box and perform proper operation
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            using (StreamWriter writer = File.CreateText(currentFilePath))
                            {
                                writer.WriteLine(InputBox.Text);
                            }
                            openFile();
                            break;
                        case MessageBoxResult.No:
                            openFile();
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }
                }
            }
            else
            {
                openFile();
            }
        }



        /*
        * FUNCTION     :   MenuSaveAs_Click
        * DESCRIPTION  :   The event handler calls the function responsible for invoking the save dialog.
        * PARAMETERS   :
        *      object sender - The instance of the object that raised the event
        *      RoutedEventArgs - Contains data associated with the event
        * RETURNS      :
        *      void
        */
        private void MenuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            saveFile(currentFilePath);
        }



        /*
        * FUNCTION     :   MenuClose_Click
        * DESCRIPTION  :   The event handler contains the logic to close the application.
        *                  If there is unsaved work the user is given the oppurtunity to save their file.
        * PARAMETERS   :
        *      object sender - The instance of the object that raised the event
        *      RoutedEventArgs - Contains data associated with the event
        * RETURNS      :
        *      void
        */
        private void MenuClose_Click(object sender, RoutedEventArgs e)
        {
            //Check if there is unsaved text in the workarea
            if (unsavedText)
            {
                string messageBoxText = "Do you want to save changes to " + currentFilePath + "?";
                string caption = "Notepad";
                MessageBoxButton buttons = MessageBoxButton.YesNoCancel;
                MessageBoxResult result = System.Windows.MessageBox.Show(messageBoxText, caption, buttons);

                //Check if the file already exists
                if (currentFilePath == "Untitled")
                {
                    //Retrieve user selection from save box and perform proper operation
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            saveFile(currentFilePath);
                            System.Windows.Application.Current.Shutdown();
                            break;
                        case MessageBoxResult.No:
                            System.Windows.Application.Current.Shutdown();
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }
                }
                else
                {
                    //Retrieve user selection from save box and perform proper operation
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            using (StreamWriter writer = File.CreateText(currentFilePath))
                            {
                                writer.WriteLine(InputBox.Text);
                            }
                            System.Windows.Application.Current.Shutdown();
                            break;
                        case MessageBoxResult.No:
                            System.Windows.Application.Current.Shutdown();
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }
                }
            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }
        }



        /*
        * FUNCTION     :   MenuAbout_Click
        * DESCRIPTION  :   The event handler contains the logic to open and load an about box from the about box class
        * PARAMETERS   :
        *      object sender - The instance of the object that raised the event
        *      RoutedEventArgs - Contains data associated with the event
        * RETURNS      :
        *      void
        */
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.Show();
        }



        /*
        * FUNCTION     :   MenuWordWrap_Click
        * DESCRIPTION  :   The event handler contains the logic to change the text wrapping of the textbox from wrapped to not wrapped
        * PARAMETERS   :
        *      object sender - The instance of the object that raised the event
        *      RoutedEventArgs - Contains data associated with the event
        * RETURNS      :
        *      void
        */
        private void MenuWordWrap_Click(object sender, RoutedEventArgs e)
        {
            //Change the textwrapping of the textbox
            if(InputBox.TextWrapping == System.Windows.TextWrapping.Wrap)
            {
                InputBox.TextWrapping = System.Windows.TextWrapping.NoWrap;
            }
            else
            {
                InputBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            }
        }



        /*
        * FUNCTION     :   MenuFont_Click
        * DESCRIPTION  :   The event handler contains the logic to change the font style of the text in the workarea
        * PARAMETERS   :
        *      object sender - The instance of the object that raised the event
        *      RoutedEventArgs - Contains data associated with the event
        * RETURNS      :
        *      void
        */
        private void MenuFont_Click(object sender, RoutedEventArgs e)
        {
            FontDialog fd = new FontDialog();
            DialogResult result = fd.ShowDialog();

            //Change the styling of the text
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                InputBox.FontFamily = new FontFamily(fd.Font.Name);
                InputBox.FontSize = fd.Font.Size * 98.0 / 72.0;
                InputBox.FontWeight = fd.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                InputBox.FontStyle = fd.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
            }
        }



        /*
        * FUNCTION     :   TextChangedEventHandler
        * DESCRIPTION  :   The event handler contains the logic to change the character count on the bottom status bar
        *                  and updates the unsavedText variable to true
        * PARAMETERS   :
        *      object sender - The instance of the object that raised the event
        *      TextChangedEventArgs - Contains data associated with the event
        * RETURNS      :
        *      void
        */
        private void TextChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            unsavedText = true;

            //Update character count on the bottom status bar
            if(InputBox != null)
            {
                CharacterCount.Content = "Character Count: " + InputBox.Text.Length.ToString();
            }
        }



        /*
        * FUNCTION     :   saveFile
        * DESCRIPTION  :   The event handler contains the logic to save a file by opening a save file dialog box
        *                  and allows the content from the text area to be saved to a file
        * PARAMETERS   :
        *      none
        * RETURNS      :
        *      void
        */
        public void saveFile(string filepath)
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.FileName = filepath;
            sfd.DefaultExt = ".txt";
            sfd.Filter = "Text documents (.txt)|*.txt";

            bool? result = sfd.ShowDialog();

            //Write all text from the inputbox to the file
            if (result == true)
            {
                string filename = sfd.FileName;
                File.WriteAllText(filename, InputBox.Text);
                unsavedText = false;
            }
        }



        /*
        * FUNCTION     :   saveFile
        * DESCRIPTION  :   The event handler contains the logic to open a file by opening a open file dialog box
        *                  and allows the content from the file to be written to the text area
        * PARAMETERS   :
        *      none
        * RETURNS      :
        *      void
        */
        public void openFile()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

            ofd.Filter = "Text documents (.txt)|*.txt";
            bool? result = ofd.ShowDialog();

            //Read all text from the file and put it in the work area
            if (result == true)
            {
                string filename = ofd.FileName;
                string readText = File.ReadAllText(filename);
                InputBox.Text = readText;
                currentFilePath = filename;
                unsavedText = false;
            }
        }
    }
}

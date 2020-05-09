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
using System.IO;
using Microsoft.Win32;

namespace WPFPortfolio.TextEditor
{
    /// <summary>
    /// Interaction logic for TextEditor.xaml
    /// </summary>
    public partial class SimpleTextEditor : UserControl
    {
        //private string pickedFolder;
        private string fileToOpen;
        private string fileToSave;
        private string currentNameFile;
        private bool workSaved = false;
        private bool workOpened = false;
        private OpenCloseFile openCloseFile = new OpenCloseFile();

        public SimpleTextEditor()
        {
            InitializeComponent();
            lblLastEdited.Visibility = Visibility.Hidden;
            dateLastEdited.Visibility = Visibility.Hidden;
        }

        private void menuNew_Click(object sender, RoutedEventArgs e)
        {
            SaveQuit();

            lblLastEdited.Visibility = Visibility.Hidden;
            fileToOpen = "";
            fileToSave = "";
            currentNameFile = "";
            workSaved = false;
            workOpened = false;
            textWriteArea.Text = "";
            savedStatus.Content = "";
            dateLastEdited.Content = "";
        }

        private void openMenu_Click_1(object sender, RoutedEventArgs e)
        {
            lblLastEdited.Visibility = Visibility.Visible;
            OpenFileDialog openFile = new OpenFileDialog();
            //openFile.InitialDirectory = pickedFolder;
            openFile.Filter = "Text files |*.txt";
            if (openFile.ShowDialog() == true)
            {
                savedStatus.Content = "";
                workOpened = true;
                currentNameFile = openFile.SafeFileName;
                fileToOpen = openFile.FileName;
                if (File.Exists(fileToOpen))
                {
                    textWriteArea.Text = openCloseFile.OpenFile(fileToOpen);
                    dateLastEdited.Visibility = Visibility.Visible;
                    dateLastEdited.Content = File.GetLastWriteTime(fileToOpen).ToString();
                    //dateAccessed.Text = File.GetLastAccessTime(fileToOpen).ToString();
                }
                else
                {
                    MessageBox.Show("Invalid file");
                }
            }
        }
        private void saveMenu_Click(object sender, RoutedEventArgs e)
        {
            if (!workSaved && !workOpened)
            {
                SaveContinue();
            }

            else
            {
                if (workOpened)
                {
                    openCloseFile.SaveFile(fileToOpen, textWriteArea.Text);
                    workSaved = true;
                    savedStatus.Content = "Changes have been saved at " + DateTime.Now.ToShortTimeString();
                }
                else if (workSaved)
                {
                    openCloseFile.SaveFile(fileToSave, textWriteArea.Text);
                    workSaved = true;
                    savedStatus.Content = "Changes have been saved at " + DateTime.Now.ToShortTimeString();
                }
            }
        }

        private void saveAsMenu_Click(object sender, RoutedEventArgs e)
        {
            SaveContinue();
        }


        private void hideStatusBar_Click(object sender, RoutedEventArgs e)
        {
            if (hideStatusBar.Header.ToString() == "Hide Status Bar")
            {
                statusBar.Visibility = Visibility.Hidden;
                hideStatusBar.Header = "Show Status Bar";
            }
            else
            {
                statusBar.Visibility = Visibility.Visible;
                hideStatusBar.Header = "Hide Status Bar";
            }
        }
        private void saveAndQuitMenu_Click(object sender, RoutedEventArgs e)
        {
            SaveQuit();
            this.Visibility = Visibility.Hidden;
        }

        private void quitWithoutSavingMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void SaveQuit()
        {
            MessageBoxResult wantToSave = MessageBox.Show("Would you like to save your work?",
                "Unsaved changes", MessageBoxButton.YesNo);
            if (wantToSave == MessageBoxResult.Yes)
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                //saveFile.InitialDirectory = pickedFolder;
                saveFile.Filter = "Text files |*.txt";
                saveFile.FileName = currentNameFile;
                if (!String.IsNullOrEmpty(fileToOpen))
                    saveFile.FileName = fileToOpen;
                if (saveFile.ShowDialog() == true)
                {
                    currentNameFile = saveFile.FileName;
                    fileToSave = saveFile.FileName;
                    openCloseFile.SaveFile(fileToSave, textWriteArea.Text);
                    workSaved = false;
                }
            }
        }

        private void SaveContinue()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            //saveFile.InitialDirectory = pickedFolder;
            saveFile.Filter = "Text files |*.txt";
            saveFile.FileName = currentNameFile;
            if (!String.IsNullOrEmpty(fileToOpen))
                saveFile.FileName = fileToOpen;
            if (saveFile.ShowDialog() == true)
            {
                workSaved = true;
                currentNameFile = saveFile.FileName;
                fileToSave = saveFile.FileName;
                openCloseFile.SaveFile(fileToSave, textWriteArea.Text);
                savedStatus.Content = "Changes have been saved at " + DateTime.Now.ToShortTimeString();
            }
        }
    }
}

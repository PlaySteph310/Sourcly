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
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Sourcly
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string filter = "HTML|*.html|TXT|*.txt|CSS|*.css";
        public MainWindow()
        {
            if (Directory.Exists(Environment.ExpandEnvironmentVariables(ParsePath(@"%localappdata%\\Sourcly\"))))
            {
                if (Directory.Exists(Environment.ExpandEnvironmentVariables(ParsePath(@"%localappdata%\\Sourcly\webfiles\"))))
                {

                }
            }
            else
            {
                Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ParsePath(@"%localappdata%\\Sourcly\")));
                Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ParsePath(@"%localappdata%\\Sourcly\webfiles\")));
            }
            InitializeComponent();
        }
        public static string ParsePath(string path) //function by dr4yyee
        {
            var newPath = new StringBuilder();
            var folders = path.Split(System.IO.Path.DirectorySeparatorChar);
            foreach (var folder in folders) newPath.Append((Regex.IsMatch(folder, "%.+%")) ? Environment.GetEnvironmentVariable(Regex.Match(folder, "(?:%)(.+)(?:%)").Groups[1].Value) : folder).Append((folders[folders.Length - 1] == folder) ? string.Empty : new string(System.IO.Path.DirectorySeparatorChar, 1));
            return newPath.ToString();
        }
        private async void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string textbox = text.Text;
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = "unbenannt";
            savefile.Filter = filter;
            if (savefile.ShowDialog() == true)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(savefile.FileName);
                await file.WriteLineAsync(textbox);
                file.Close();
            }
        }

        private async void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            if (openfile.ShowDialog() == true)
            {
                string path = openfile.FileName;
                StreamReader sr = new StreamReader(path);
                string file = await sr.ReadToEndAsync();
                text.Text = file;
            }
        }
        private void MenuItem_Click2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult newfile = MessageBox.Show("Möchten Sie die Datei speichern oder verwerfen?","Bestätigen", MessageBoxButton.YesNoCancel);
            switch(newfile)
            {
                case MessageBoxResult.Yes:
                    SaveFiles();
                    break;
                case MessageBoxResult.No:
                    text.Text = "";
                    break;
                case MessageBoxResult.Cancel:
                    break;
                default:
                    break;
            }
        }
        public bool SaveFiles()
        {
            string textbox = text.Text;
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = "unbenannt";
            savefile.Filter = filter;
            if (savefile.ShowDialog() == true)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(savefile.FileName);
                file.WriteLine(textbox);
                file.Close();
                //lol = true;
                return true;
            }
            else 
            {
                return false;
            }
        }

        private void datei_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItem_print(object sender, RoutedEventArgs e)
        {
            /*PrintDialog print = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            doc.DocumentName = text.Text;
            print.PrintDocument(doc.DocumentName);
            if (print.ShowDialog() == true)
            {
                //print.PrintDocument(text.Text);
            }*/
            MessageBox.Show("Bald verfügbar");
        }
        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
        }
        private void textblock_header_1(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
        }

        private void MenuItem_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void MenuItem_beenden(object sender, RoutedEventArgs e)
        {
            if (text.Text == "")
            {
                Application.Current.Shutdown();
            }
            else
            {
                switch(MessageBox.Show("Möchten Sie den Text speichern?", "Bestätigen", MessageBoxButton.YesNoCancel))
                {
                    case MessageBoxResult.Yes:
                        if (SaveFiles() == true)
                        {
                            Application.Current.Shutdown();
                        }
                        break;
                    case MessageBoxResult.No:
                        Application.Current.Shutdown();
                        break;
                    case MessageBoxResult.Cancel:
                        break;

                }
            }
        }
        private void start_standartbrowser(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ParsePath(@"%localappdata%\\Sourcly\webfiles\file_1.html")), text.Text);
            string path = ParsePath(@"%localappdata%\\Sourcly\webfiles\");
            Process.Start("file://" + path + "file_1.html");
        }
        private void MenuItem_internet_explorer(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ParsePath(@"%localappdata%\\Sourcly\webfiles\file_1.html")), text.Text);
            string path = ParsePath(@"%localappdata%\\Sourcly\webfiles\");
            Process.Start("iexplore.exe", "file://" + path + "file_1.html");
        }
        private void MenuItem_reserved(object sender, RoutedEventArgs e)
        {
            /*var e1 = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, Key.LeftCtrl) { RoutedEvent = Keyboard. };
            InputManager.Current.ProcessInput(e1);*/
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            NewFile File = new NewFile();
            File.Show();
        }
    }
}

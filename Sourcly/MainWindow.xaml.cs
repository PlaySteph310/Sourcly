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
            InitializeComponent();
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
        private async void MenuItem_Click2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult newfile = MessageBox.Show("Möchten Sie die Datei speichern oder verwerfen?","Bestätigen", MessageBoxButton.YesNoCancel);
            switch(newfile)
            {
                case MessageBoxResult.Yes:
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

        private void datei_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

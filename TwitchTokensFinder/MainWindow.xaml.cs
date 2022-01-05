using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace TwitchTokensFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
            if (sfd.ShowDialog() == true)
            {
                TextRange doc = new TextRange(docBox.Document.ContentStart, docBox.Document.ContentEnd);
                using (FileStream fs = File.Create(sfd.FileName))
                {
                    if (Path.GetExtension(sfd.FileName).ToLower() == ".txt")
                        doc.Save(fs, DataFormats.Text);
                }
            }
        }

        private string GetToken(string filename)
        {
            var text = File.ReadAllLines(filename);//Содержимое файла cookies
            var tokenString = text.FirstOrDefault(x => x.Contains(".twitch.tv") && x.Contains("auth-token"));//Поиск подходящей строки
            if (!(tokenString is null))
            {
                string token = tokenString.Split().Last();//Получение токена
                return token;
            }
            else
            {
                return null;
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            TextRange doc = new TextRange(docBox.Document.ContentStart, docBox.Document.ContentEnd);
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                string[] allfiles = Directory.GetFiles(dialog.SelectedPath, "*.txt", SearchOption.AllDirectories);
                string resultTokens = "";
                foreach (string filename in allfiles)
                {
                    var token = GetToken(filename);
                    if (!string.IsNullOrEmpty(token))
                    {
                        resultTokens += $"{token}\n";
                    }
                }
                doc.Text = resultTokens;
            }

        }
    }
}

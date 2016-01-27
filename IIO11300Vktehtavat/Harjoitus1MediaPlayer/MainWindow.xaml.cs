using Microsoft.Win32;
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

namespace Harjoitus1MediaPlayer
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

        private void LoadMediaFile()
        {
            try
            {
                // soitetaan käyttäjän valitsemaa mediatiedostoa
                // string filu = @"D:\H8374\test.mp4";
                string filu = txtFileName.Text;
                // tutkitaan onko tiedosto olemassa
                if (System.IO.File.Exists(filu))
                {
                    // MessageBox.Show("soitetaan tiedosto: " + filu);
                    mediaElement.Source = new Uri(filu);
                }
                else {
                    MessageBox.Show("Tiedostoa " + filu + " ei löydy.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            LoadMediaFile();
            mediaElement.Play();
            ChangeButtonState();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
            ChangeButtonState();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            ChangeButtonState();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            // avataan vakio open-dialogi jotta käyttäjä voi valita tiedoston
            OpenFileDialog dlg = new OpenFileDialog(); // käytä tarjottua importtia
            dlg.InitialDirectory = "D:\\"; // oletuskansio josta tiedoston haku lähtee
            dlg.Filter = "Rock files(*.mp3)|*.mp3|Media files (*.wmv, *.mp4)|*.wmv,*.mp4,*.mpeg4|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                txtFileName.Text = dlg.FileName;
                btnPlay.IsEnabled = true;
                btnPause.IsEnabled = false;
                btnStop.IsEnabled = false;
            }
        }

        private void ChangeButtonState()
        {
            btnPause.IsEnabled = !btnPause.IsEnabled;
            btnStop.IsEnabled = !btnStop.IsEnabled;
            btnPlay.IsEnabled = !btnPlay.IsEnabled;
        }
    }
}

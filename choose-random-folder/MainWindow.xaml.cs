using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using choose_random_folder.Classes;
using Ookii.Dialogs.Wpf;

namespace choose_random_folder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : INotifyPropertyChanged
    {
        private Folder _selectedFolder;
        
        private bool _running;

        private StartingFolder _startingFolder;

        public StartingFolder StartingFolder
        {
            get { return _startingFolder; }
            set
            {
                _startingFolder = value;
                OnPropertyChanged();
            }
        }

        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }
        
        // 

        public MainWindow()
        {
            Text = "Roll";
            InitializeComponent();
        }

        private async Task SleepSelectLoop(int loopAmount, int sleepAmount)
        {
            for (var i = 0; i < loopAmount; i++)
            {
                Listbox.SelectedItem = StartingFolder.RandomFolder;
                await Task.Delay(sleepAmount);
            }
        }

        // 

        private async void MainButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartingFolder.Count <= 0 || _running)
                return;

            if (_selectedFolder == null)
            {
                _running = true;
                Text = "Rolling ...";

                await SleepSelectLoop(30, 100);
                await SleepSelectLoop(10, 150);
                await SleepSelectLoop(5, 250);
                await SleepSelectLoop(5, 500);
                await SleepSelectLoop(4, 650);
                await SleepSelectLoop(3, 800);

                _running = false;
                _selectedFolder = Listbox.SelectedItem as Folder;
                Text = "Open";
            }


            else
            {
                Process.Start(_selectedFolder.Path);
            }

        }

        private void FolderBrowser_Click(object sender, RoutedEventArgs e)
        {
            if (_running)
                return;

            _selectedFolder = null;
            
            var dialog = new VistaFolderBrowserDialog();

            if (dialog.ShowDialog() == true)
            {
                StartingFolder = new StartingFolder(dialog.SelectedPath);
                Text = "Roll";
            }
        }
        
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.R)
            {
                if (!_running)
                {
                    Listbox.SelectedItem = null;
                    _selectedFolder = null;
                    Text = "Roll";
                }
            }
        }

        // 

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}

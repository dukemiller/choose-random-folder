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
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : INotifyPropertyChanged
    {

        public MainWindow()
        {
            Text = "Roll";
            InitializeComponent();
        }

        private Folder SelectedFolder => Listbox.SelectedItem as Folder;

        private bool Running { get; set; }

        private LoadedFolder _loadedFolder;

        public LoadedFolder LoadedFolder
        {
            get { return _loadedFolder; }
            set
            {
                _loadedFolder = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        private async Task SleepSelectLoop(int loopAmount, int sleepAmount)
        {
            for (var i = 0; i < loopAmount; i++)
            {
                Listbox.SelectedItem = LoadedFolder.RandomFolder;
                await Task.Delay(sleepAmount);
            }
        }

        // 

        private async void MainButton_Click(object sender, RoutedEventArgs e)
        {
            if ((LoadedFolder.Count <= 0) || Running)
                return;

            if (Text.Equals("Roll"))
            {
                Running = true;
                Text = "Rolling ...";

                await SleepSelectLoop(30, 100);
                await SleepSelectLoop(10, 150);
                await SleepSelectLoop(5, 250);
                await SleepSelectLoop(5, 500);
                await SleepSelectLoop(4, 650);
                await SleepSelectLoop(3, 800);

                Running = false;
                Text = "Open";
            }


            else
                Process.Start(SelectedFolder.Path);
        }

        private void FolderBrowser_Click(object sender, RoutedEventArgs e)
        {
            if (Running)
                return;

            var dialog = new VistaFolderBrowserDialog();

            if (dialog.ShowDialog() == true)
            {
                LoadedFolder = new LoadedFolder(dialog.SelectedPath);
                Text = "Roll";
                Keyboard.ClearFocus();
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.R)
                if (!Running)
                {
                    Listbox.SelectedItem = null;
                    Text = "Roll";
                }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
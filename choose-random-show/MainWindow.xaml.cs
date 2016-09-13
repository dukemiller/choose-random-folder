using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ookii.Dialogs.Wpf;

namespace choose_random_show
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : INotifyPropertyChanged
    {
        
        private Folder _selected;
        
        private bool _running;

        private BaseFolder _folder;

        public BaseFolder Folder
        {
            get { return _folder; }
            set
            {
                _folder = value;
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
                Listbox.SelectedItem = Folder.RandomFolder;
                await Task.Delay(sleepAmount);
            }
        }

        // 

        private async void MainButton_Click(object sender, RoutedEventArgs e)
        {
            if (Folder.Count <= 0 || _running)
                return;

            if (_selected != null)
            {
                Process.Start(_selected.Path);
                return;
            }

            _running = true;

            Text = "Rolling ...";

            await SleepSelectLoop(30, 100);
            await SleepSelectLoop(10, 150);
            await SleepSelectLoop(5, 250);
            await SleepSelectLoop(5, 500);
            await SleepSelectLoop(4, 650);
            await SleepSelectLoop(3, 800);

            _running = false;
            _selected = Listbox.SelectedItem as Folder;
            Text = "Open";

        }

        private void FolderBrowser_Click(object sender, RoutedEventArgs e)
        {
            if (_running)
                return;

            _selected = null;
            
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                Folder = new BaseFolder(dialog.SelectedPath);
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
                    _selected = null;
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

    public sealed class BaseFolder : INotifyPropertyChanged
    {

        private static readonly Random Random = new Random();

        public Folder RandomFolder => Contents.ElementAt(Random.Next(0, Contents.Count - 1));

        public BaseFolder(string path)
        {
            _path = path;

            try
            {
                _contents = Directory.GetDirectories(path)
                    .Select(s => new Folder(s))
                    .OrderBy(s => s.Path.Length)
                    .ToList();
            }

            catch
            {
                _contents = new List<Folder>();
            }
        }

        public int Count => Contents.Count;

        private List<Folder> _contents;

        public List<Folder> Contents
        {
            get { return _contents; }
            set
            {
                _contents = value;
                OnPropertyChanged();
            }
        }

        private string _path;

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public sealed class Folder : INotifyPropertyChanged
    {
        public Folder(string path)
        {
            Path = path;
        }

        private string _path;

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged();
            }
        }

        public string Name => System.IO.Path.GetFileNameWithoutExtension(Path);

        public int Size => Contents.Count();

        public IEnumerable<Folder> Contents => Directory.GetDirectories(Path)
            .Select(s => new Folder(s))
            .OrderBy(s => s.Path.Length);

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

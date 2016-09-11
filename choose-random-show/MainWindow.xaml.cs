using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Ookii.Dialogs.Wpf;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace choose_random_show
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : INotifyPropertyChanged
    {
        private static readonly Random Random = new Random();

        private int RandomChoice => Random.Next(0, Folders.Count - 1);

        private bool _running;

        private string _folder;

        public string Folder
        {
            get { return _folder; }
            set
            {
                _folder = value;
                OnPropertyChanged();
            }
        }

        private List<ShowFolder> _folders;

        public List<ShowFolder> Folders
        {
            get { return _folders; }
            set
            {
                _folders = value;
                OnPropertyChanged();
            }
        }

        // 

        public MainWindow()
        {
            _folder = "";
            _folders = new List<ShowFolder>();
            InitializeComponent();
        }

        private async Task SleepSelectLoop(int amount, int sleepTime)
        {
            for (var i = 0; i < amount; i++)
            {
                Listbox.SelectedItem = Listbox.Items.GetItemAt(RandomChoice);
                await Task.Delay(sleepTime);
            }
        }

        // 

        private async void Choose_Click(object sender, RoutedEventArgs e)
        {
            if (Folders.Count <= 0 || _running)
                return;

            _running = true;

            await SleepSelectLoop(30, 100);
            await SleepSelectLoop(10, 150);
            await SleepSelectLoop(5, 250);
            await SleepSelectLoop(5, 500);
            await SleepSelectLoop(5, 650);
            await SleepSelectLoop(3, 800);

            _running = false;

        }

        private void SelectFolder_Click(object sender, RoutedEventArgs e)
        {
            if (_running)
                return;

            var dialog = new VistaFolderBrowserDialog();

            if (dialog.ShowDialog() == true)
            {
                Folder = dialog.SelectedPath;
                Folders = Directory.GetDirectories(dialog.SelectedPath).Select(s => new ShowFolder(s)).OrderBy(s => s.FullPath.Length).ToList();
            }
        }

        private void Listbox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_running)
                return;

            var selected = (sender as ListBox)?.SelectedItem as ShowFolder;
            if (selected != null)
                Process.Start(selected.FullPath);
        }

        // 

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class ShowFolder
    {

        public ShowFolder(string baseDirectory)
        {
            FullPath = baseDirectory;
        }

        public string FullPath { get; }

        public string BaseFolder => Path.GetFileNameWithoutExtension(FullPath);

    }
}

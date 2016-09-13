using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace choose_random_folder.Classes
{
    public class Folder : INotifyPropertyChanged
    {
        private string _path;

        public Folder(string path)
        {
            Path = path;
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
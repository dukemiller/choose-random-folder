using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace choose_random_show.Classes
{
    public class Folder : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
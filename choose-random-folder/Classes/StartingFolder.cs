using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace choose_random_folder.Classes
{
    public class StartingFolder : Folder
    {
        private static readonly Random Random = new Random();

        public Folder RandomFolder => Contents.ElementAt(Random.Next(0, Count - 1));

        public StartingFolder(string path) : base(path)
        {
            try
            {
                Contents = Directory.GetDirectories(path)
                    .Select(s => new Folder(s))
                    .OrderBy(s => s.Path.Length)
                    .ToList();
            }

            catch
            {
                Contents = new List<Folder>();
            }
        }

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

        public int Count => Contents.Count;
    }
}
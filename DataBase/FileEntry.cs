using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.DataBase
{
    public class FileEntry
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public FileEntry(string name , string path)
        {
            Name = name;
            Path = path; 
        }

        public FileEntry()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingProject.Models
{
    public class FileSystemItem
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public DateTime LastTimeWrite { get; set; }

        public long? Lenght { get; set; }

        public FileType FileType { get; set; }

        public string Url { get; set; }

        public FileSystemItem(string name, string path, DateTime lastTimeWrite, long? lenght, FileType fileType, string url)
        {
            Name = name;
            Path = path;
            LastTimeWrite = lastTimeWrite;
            Lenght = lenght;
            FileType = fileType;
            Url = url;
        }
    }
}
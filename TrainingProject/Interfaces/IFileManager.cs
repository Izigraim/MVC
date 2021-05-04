using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingProject.Models;

namespace TrainingProject.Interfaces
{
    public interface IFileManager
    {
        IEnumerable<FileSystemItem> GetFiles(string path);

        IEnumerable<FileSystemItem> GetDrives();

        void CreateFile(string path);

        void CreateFolder(string path);

        void DeleteFile(string path);
    }
}
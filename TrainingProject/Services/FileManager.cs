using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TrainingProject.Interfaces;
using TrainingProject.Models;

namespace TrainingProject.Services
{
    public class FileManager : IFileManager
    {

        public IEnumerable<FileSystemItem> GetFiles(string path)
        {
            List<FileSystemItem> files = new List<FileSystemItem>();
            DirectoryInfo dir = new DirectoryInfo(path);

            try
            {
                foreach (DirectoryInfo item in dir.GetDirectories())
                {
                    FileSystemItem file = new FileSystemItem(item.Name, item.FullName, item.LastWriteTime, null, FileType.Folder, UrlPathHelper.PathToUrl(item.FullName));
                    files.Add(file);
                }

                foreach (FileInfo item in dir.GetFiles())
                {
                    FileSystemItem file = new FileSystemItem(item.Name, item.FullName, item.LastWriteTime, item.Length / 1024, FileType.File, UrlPathHelper.PathToUrl(item.FullName));
                    files.Add(file);
                }

            }
            catch (Exception)
            {
                return files.DefaultIfEmpty();
            }

            return files;
        }

        public IEnumerable<FileSystemItem> GetDrives()
        {
            List<FileSystemItem> drivesList = new List<FileSystemItem>();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                drivesList.Add(new FileSystemItem(drive.Name, drive.Name, DateTime.Now, null, FileType.Drive, UrlPathHelper.PathToUrl(drive.Name)));
            }

            return drivesList;
        }

        public void CreateFile(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            if (!fileInfo.Exists)
            {
                FileStream fs = fileInfo.Create();
                fs.Close();
            }              
        }

        public void CreateFolder(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }

        public void DeleteFile(string path)
        {

            if (this.IsFolder(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                directoryInfo.Delete(true);
            }
            else
            {
                FileInfo fileInfo = new FileInfo(path);

                fileInfo.Delete();
            }

        }

        private bool IsFolder(string path)
        {
            if (File.Exists(path))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
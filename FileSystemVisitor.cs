using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitor
{
    public class FileSystemVisitor : IFileSystemVisitor
    {
        private readonly Func<string, bool>? filterFolderAndFile = null;

        private event Action<string> NotifyEvent;

        public FileSystemVisitor(Func<string, bool> filterFolderAndFile)
            : this()
        {
            this.filterFolderAndFile = filterFolderAndFile;
        }

        public FileSystemVisitor()
        {
            InitializeNotifyEvent();
        }

        private void InitializeNotifyEvent()
        {
            NotifyEvent += message =>
            {
                Console.WriteLine();
                Console.WriteLine(message);
                Console.WriteLine("...........................");
            };

            NotifyEvent("Program started...");
        }

        public IEnumerable<string> GetFoldersAndFiles(string path)
        {
            ValidatePathForExistance(path);

            return GetFoldersAndFilesWithin(path);
        }

        private void ValidatePathForExistance(string path)
        {
            if (IsPathNotFound(path))
            {
                NotifyEvent("Path not found...");
                throw new DirectoryNotFoundException();
            }

            NotifyEvent("Path found...");
        }

        private IEnumerable<string> GetFoldersAndFilesWithin(string path)
        {
            return Enumerable.Concat(GetFolders(path), GetFiles(path));
        }

        private IEnumerable<string> GetFolders(string path)
        {
            foreach (string folderFullName in Directory.EnumerateDirectories(path))
            {
                var folder = RemovePathFromSource(path, folderFullName);
                if (CheckForFilter(folder))
                {
                    yield return folder;
                }
            }
        }

        private IEnumerable<string> GetFiles(string path)
        {
            foreach (string fileFullName in Directory.EnumerateFiles(path))
            {
                var file = RemovePathFromSource(path, fileFullName);
                if (CheckForFilter(file))
                {
                    yield return file;
                }
            }
        }

        private bool CheckForFilter(string folderOrFileName) => filterFolderAndFile == null || filterFolderAndFile(folderOrFileName);

        private bool IsPathNotFound(string path) => !Directory.Exists(path);

        private string RemovePathFromSource(string path, string folderOrFileName) => folderOrFileName.Substring(path.Length + 1);
    }
}

namespace FileSystemVisitor
{
    public interface IFileSystemVisitor
    {
        /// <summary>
        /// returns all files and folders by path
        /// </summary>
        /// <param name="pathName">C:\projects\MyTrash</param>
        /// <returns>
        /// a.txt
        /// a1.txt
        /// ab
        /// file2
        /// </returns>
        IEnumerable<string> GetFoldersAndFiles(string pathName);
    }
}

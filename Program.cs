using FileSystemVisitor;


var path = @"C:\Projects\FMI";

GetFoldersAndFilesWithoutFilter(path);

GetFoldersAndFilesWithFilter(path);


void GetFoldersAndFilesWithoutFilter(string path)
{
    var fileSystemVisitor = new FileSystemVisitor.FileSystemVisitor();
    var results = fileSystemVisitor.GetFoldersAndFiles(path);
    foreach (var item in results)
    {
        Console.WriteLine(item);
    }
}

static void GetFoldersAndFilesWithFilter(string path)
{
    var fileSystemVisitor = new FileSystemVisitor.FileSystemVisitor(source => source.StartsWith("crm"));
    var results = fileSystemVisitor.GetFoldersAndFiles(path);

    foreach (var item in results)
    {
        Console.WriteLine(item);
    }
}
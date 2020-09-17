using System;

namespace Desktop.Classes
{
    public static class IOOperations
    {
        public static class Directory
        {
            public static bool Exists(string Path)
            {
                return System.IO.Directory.Exists(Path);
            }

            public static void Create(string Path, string FolderName)
            {
                System.IO.Directory.CreateDirectory($@"{Path}\{FolderName}");
            }
        }

        public static class File
        {
            public static bool Exists(string Path)
            {
                return System.IO.File.Exists(Path);
            }

            public static void Create(string Path, string FileName)
            {
                System.IO.File.Create($@"{Path}\\{FileName}");
            }
        }

        public static void ImageFolderControl()
        {
            if (Directory.Exists(Environment.CurrentDirectory + @"/img") == false)
            {
                Directory.Create(Environment.CurrentDirectory, "img");
            }
        }
    }
}
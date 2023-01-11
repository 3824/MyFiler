using MyApp.file;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    public class FileInfo : ItemInfo
    {
        public FileInfo(string path, string name)
        {
            Name = name;
            Path = path;
            // https://learn.microsoft.com/ja-jp/dotnet/api/system.io.fileattributes?view=net-7.0
            

        }

        public void setAttribute(FileAttributes attributes)
        {
            IsReadOnly = (attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
        }
            
        public string Name { get; set; }

        public string Path { get; set; }
        public bool IsFolder { get; set; }

        public bool IsReadOnly { get; set; }

    }
}

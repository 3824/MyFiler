using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.file
{
    public class FolderInfo : ItemInfo
    {
        public FolderInfo(string path, string name)
        {
            Name = name;
            Path = path;
        }

        public string Name { get; set; }

        public string Path { get; set; }
        public bool IsFolder { get; set; }

        public bool IsReadOnly { get; set; }
        public List<FolderInfo> FolderInfoList { get; set; } = new List<FolderInfo>();
    }
}

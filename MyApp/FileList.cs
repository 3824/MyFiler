using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp
{
    public class FileList
    {
        public ObservableCollection<FileInfo> Data { get; }

        public FileList()
        {
            Data = new ObservableCollection<FileInfo>
            {
               // new FileInfo{Name="file1", Attribute="r"}
            };
        }
    }
}

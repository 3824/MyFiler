using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.file
{
    public interface ItemInfo
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public bool IsFolder { get; set; }

        public bool IsReadOnly { get; set; }
    }
}

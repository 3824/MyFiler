using MyApp.file;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.util
{
    class FileUtil
    {
        public static List<FolderInfo> walkSubFolder(string rootPath, int maxDepth = 2, int currentDepth = 0)
        {
            List<FolderInfo> children = new List<FolderInfo>();
            if (currentDepth >= maxDepth) return children;

            DirectoryInfo di = new DirectoryInfo(rootPath);
            try
            {
                DirectoryInfo[] subFolders = di.GetDirectories();
                foreach (DirectoryInfo subFolder in subFolders)
                {
                    FolderInfo fi = new FolderInfo(subFolder.FullName, subFolder.Name);
                    fi.FolderInfoList = walkSubFolder(fi.Path);
                    children.Add(fi);
                }
            }
            catch (UnauthorizedAccessException uae)
            {
                return children;
            }

            return children;
        }

    }
}

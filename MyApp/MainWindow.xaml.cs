using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics; // For debugging.
using System.Collections.ObjectModel;
using MyApp.util;
using MyApp.file;

namespace MyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<FolderInfo> _folderInfoList = new ObservableCollection<FolderInfo>();
        private ObservableCollection<ItemInfo> _fileInfoList = new ObservableCollection<ItemInfo>();

        public MainWindow()
        {
            InitializeComponent();
            Trace.WriteLine("start");
            SetDriveLabelBar();
        }

        private void SetDriveLabelBar()
        {
            string[] drives = Directory.GetLogicalDrives();
            foreach (string drive in drives)
            {
                Button b = new Button();
                b.Content = drive;
                b.Name = drive.Substring(0, 1);
                b.Click += (sender, e) => DriveClickEvent(sender);
                driveLabelBar.Items.Add(b);
                Trace.WriteLine(drive);
            }
        }

        private void DriveClickEvent(object sender)
        {
            Trace.WriteLine(((Button)sender).Name);

            string path = ((Button)sender).Name + @":\";

            DirectoryInfo di = new DirectoryInfo(path);

            DirectoryInfo[] subFolders = di.GetDirectories();

            _folderInfoList.Clear();
            foreach (string f in Directory.EnumerateDirectories(path))
            {
                FolderInfo fi = new FolderInfo(f, f);
                fi.FolderInfoList = FileUtil.walkSubFolder(fi.Path);
                _folderInfoList.Add(fi);
            }
            FolderTreeView.ItemsSource = _folderInfoList;

            _fileInfoList.Clear();
            System.IO.FileInfo[] files = di.GetFiles();
            foreach (string file in Directory.EnumerateFiles(path))
            {
                FileInfo fi = new FileInfo(file, file);
                fi.IsFolder = true;
                _fileInfoList.Add(fi);
            }
            FileDataGrid.ItemsSource = _fileInfoList;
        }

        private void FolderLeftMouseUp(object sender, MouseButtonEventArgs e)
        {
            //Trace.WriteLine("click:"+e.GetPosition(sender as UIElement));
            if (sender == null) return;
            TreeViewItem item = sender as TreeViewItem;

            ((FolderInfo)item.DataContext).FolderInfoList = 
                FileUtil.walkSubFolder(((FolderInfo)item.DataContext).Path);

            Trace.WriteLine(((FolderInfo)item.DataContext).Path);
            _fileInfoList.Clear();
            DirectoryInfo di = new DirectoryInfo(((FolderInfo)item.DataContext).Path);

            try
            {
                DirectoryInfo[] dirs = di.GetDirectories();
                foreach (DirectoryInfo d in dirs)
                {
                    FolderInfo fi = new FolderInfo(d.FullName, d.Name);
                    _fileInfoList.Add(fi);
                }

                System.IO.FileInfo[] files = di.GetFiles();
                foreach (System.IO.FileInfo file in files)
                {
                    FileInfo fi = new FileInfo(file.FullName, file.Name);
                    fi.setAttribute(file.Attributes);

                    _fileInfoList.Add(fi);
                }
                FileDataGrid.ItemsSource = _fileInfoList;
            }
            catch(UnauthorizedAccessException ae)
            {
                Trace.WriteLine(((FolderInfo)item.DataContext).Path + "を開けません");
            }
        }

        // https://todosoft.net/blog/?p=816

       /* private List<FolderInfo> walkSubFolder(FolderInfo rootFolder)
        {
            List<FolderInfo> children = new List<FolderInfo>();
            DirectoryInfo di = new DirectoryInfo(rootFolder.FolderPath);
            try
            {
                DirectoryInfo[] subFolders = di.GetDirectories();
                foreach (DirectoryInfo subFolder in subFolders)
                {
                    FolderInfo fi = new FolderInfo(subFolder.FullName, subFolder.Name);
                    fi.FolderInfoList = walkSubFolder(fi);
                    children.Add(fi);
                }
            }
            catch(UnauthorizedAccessException uae)
            {
                return children;
            }
            
            return children;
        }
*/
    }


    /*public sealed class FolderInfo
    {
        public FolderInfo(string path, string name)
        {
            FolderPath = path;
            FolderName = name;
        }
        public string FolderName { get; set; }
        public string FolderPath { get; set; }
        public List<FolderInfo> FolderInfoList { get; set; } = new List<FolderInfo>();
    }*/
}

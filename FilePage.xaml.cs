using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public partial class FilePage : ContentPage
    {
        int count = 0;

        public ObservableCollection<FileData> YourCollection { get; set; }
        public void InitializeDocuments()
        {
            try
            {
                if (!Directory.Exists(FileManager.ROOT_DIR + FileManager.DOCUMENT_DIR))
                {
                    Directory.CreateDirectory(FileManager.ROOT_DIR + FileManager.DOCUMENT_DIR);   // 若存放所有markdown文档的文件夹不存在，则先创建一个。
                }
            }
            catch(Exception err)
            {
                Console.WriteLine("Current Work Route:" + Directory.GetCurrentDirectory());
                Console.WriteLine("Error:" + err.ToString()); 
                this.DisplayAlert("Error",err.ToString() , "OK"); 
                return;
            }

            DirectoryInfo documentInfo = new DirectoryInfo(FileManager.ROOT_DIR + FileManager.DOCUMENT_DIR);
            FileInfo[] files = documentInfo.GetFiles("*.md", SearchOption.TopDirectoryOnly);
            FileData[] fileDatas = new FileData[files.Length];
            int cnt = 0; 
            foreach (FileInfo file in files)    // 遍历文件夹中的Files 并且转化为FIleData
            {
                fileDatas[cnt] = new FileData{ Name = file.Name } ;
                cnt++; 
            }
            YourCollection = new ObservableCollection<FileData>(fileDatas);
            BindingContext = this;
        }
        public FilePage()
        {
            InitializeComponent();
            InitializeDocuments(); 
        }
    }
    public class FileData
    {
        public string Name { get; set; }
    }
}

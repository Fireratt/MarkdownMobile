using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml.Linq;
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
            if(YourCollection!=null)
            {
                YourCollection.Clear();
                foreach (var item in fileDatas)
                {
                    YourCollection.Add(item);
                }
                return; 
            }
            YourCollection = new ObservableCollection<FileData>(fileDatas);
            BindingContext = this;
        }
        public FilePage()
        {
            InitializeComponent();
            InitializeDocuments(); 
        }
        public void OnRefresh(object sender, EventArgs e)
        {
            InitializeDocuments(); 
        }
    }
    public class FileData : INotifyPropertyChanged  
    {
        private string _name; 
        public string Name
        {
            get => _name; set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

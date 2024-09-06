using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage; 
using System.IO;
namespace MauiApp1
{
	public class FileManager
	{
        public const string ROOT_DIR = "/data/data/com.Firerat.mauiapp1";
		public const string DOCUMENT_DIR = "/Document";
        public FileManager()
		{
		}
		public static async Task<bool> SaveFile(String content , String fileName)
		{
            if (!Directory.Exists(ROOT_DIR + DOCUMENT_DIR))
            {
                Directory.CreateDirectory(ROOT_DIR + DOCUMENT_DIR);   // 若存放所有markdown文档的文件夹不存在，则先创建一个。
            }
            if (fileName != null && fileName != "")
			{
				var filePath = ROOT_DIR + DOCUMENT_DIR + "/" +  fileName + ".md"; 
				Console.WriteLine(filePath); 
				File.WriteAllText(filePath, content);
				return true; 
            }
			else
			{
				return false; 
            }
		}

	}
}
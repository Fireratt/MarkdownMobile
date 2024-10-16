using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage; 
using System.IO;
using MauiApp1.System;
namespace MauiApp1
{
	public class FileManager
	{
        public const string ROOT_DIR = "/data/data/com.Firerat.mauiapp1";
		public const string DOCUMENT_DIR = "/Document";
		public const string ANDROID_PREFIX = "/data/data/"; 
        public FileManager()
		{
		}
		public static bool SaveFile(string content , string fileName)
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
		public static async Task<string> ReadFile(string fileName)
		{
			string fullname = ROOT_DIR + DOCUMENT_DIR + "/" + fileName;
			string result = ""; 
			try 
			{ 
				using (StreamReader reader = new StreamReader(fullname))
				{
					result = await reader.ReadToEndAsync(); 
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString()); 
			}
			return result; 
		}
		public static async Task<string> ReadRawFile(string fullName)
		{
			//fullName = ANDROID_PREFIX + fullName; 
            string result = "";
            try
            {
                using (StreamReader reader = new StreamReader(fullName))
                {
                    result = await reader.ReadToEndAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return result;
        }
		public static async void ShareFile(string fileName)
		{
            string fullname = ROOT_DIR + DOCUMENT_DIR + "/" + fileName;
			await Share.Default.RequestAsync(new ShareFileRequest
			{
				File = new ShareFile(fullname) , 
				Title = "Share Your markdown file" 
			}); 

        }

		public static bool DeleteFile(string fileName)
		{
            string fullname = ROOT_DIR + DOCUMENT_DIR + "/" + fileName;
			try
			{
                File.Delete(fullname);

            }
			catch(Exception e)
			{
				Console.WriteLine(e.ToString());
				return false; 
			}
			return true; 
        }
		public static async Task<string> SelectFile()
		{
			try
			{
				var result = await FilePicker.PickAsync(null);
				if(result == null)
				{
					return ""; 
				}
				return result.FullPath; 
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message); 
			}
			return ""; 
		}
    }
}
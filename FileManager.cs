using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage; 
using System.IO;
using MauiApp1.System;
namespace MauiApp1
{
	public class FileManager
	{
        public const string ROOT_DIR = "/data/data/com.Firerat.MarkdownMobile";
		public const string DOCUMENT_DIR = "/Document";
		public const string ANDROID_PREFIX = "/data/data/"; 
        public FileManager()
		{
		}
		public static bool SaveFile(string content , string fileName)
		{
			return SaveFile(content, DOCUMENT_DIR, fileName + ".md"); 
		}
		// 将Content保存到根目录下自定义的文件夹中的指定文件名中去
		public static bool SaveFile(string content , string dir , string fileName)
		{
            if (!Directory.Exists(ROOT_DIR + dir))
            {
                Directory.CreateDirectory(ROOT_DIR + dir);   // 若存放文件夹不存在，则先创建一个。
            }
            if (fileName != null && fileName != "")
            {
                var filePath = ROOT_DIR + dir + "/" + fileName;
                Console.WriteLine(filePath);
                File.WriteAllText(filePath, content);
                return true;
            }
            else
            {
                return false;
            }
        }
		public static bool SaveFile(byte[] content , string dir , string fileName)
		{
            if (!Directory.Exists(ROOT_DIR + dir))
            {
                Directory.CreateDirectory(ROOT_DIR + dir);   // 若存放文件夹不存在，则先创建一个。
            }
            if (fileName != null && fileName != "")
            {
                var filePath = ROOT_DIR + dir + "/" + fileName;
                Console.WriteLine(filePath);
                File.WriteAllBytes(filePath, content);
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
		public static async Task<bool> RequestReadAndWrite()
		{
            PermissionStatus status = await Permissions.RequestAsync<Permissions.StorageRead>();
            PermissionStatus status2 = await Permissions.RequestAsync<Permissions.StorageWrite>();
			return status.Equals(PermissionStatus.Granted) || status.Equals(PermissionStatus.Restricted) &&
				status2.Equals(PermissionStatus.Granted) || status2.Equals(PermissionStatus.Restricted); 
        }
        public static async void ShareFile(string fileName)
		{
            string fullname = ROOT_DIR + DOCUMENT_DIR + "/" + fileName;

			await ShareRawFile(fullname); 

        }
		public static async Task ShareRawFile(string fullname)
		{
            await Share.Default.RequestAsync(new ShareFileRequest
            {
                File = new ShareFile(fullname),
                Title = "Share Your markdown file"
            });
        }
		public static async Task<bool> DeleteFile(string fileName)
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
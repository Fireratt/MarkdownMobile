using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage; 
using System.IO;
namespace MauiApp1
{
	public class FileManager
	{
		public FileManager()
		{
		}
		public static async Task<bool> SaveFile(String content)
		{
			var saveFileResult = await FilePicker.PickAsync(new PickOptions
			{
				PickerTitle = "Save as New File"
            });
			if (saveFileResult != null)
			{
				var filePath = saveFileResult.FullPath;
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
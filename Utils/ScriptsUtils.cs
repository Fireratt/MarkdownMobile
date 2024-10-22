using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage; 
namespace MarkdownMobile.Utils
{
    public class ScriptsUtils
    {
        // the path is the relative path . the working dir is set to MauiApp1's directory .
        public static async Task<string> loadScripts(string path)
        {

            Stream stream = await Microsoft.Maui.Storage.FileSystem.OpenAppPackageFileAsync(path);
            StreamReader reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
    }
}

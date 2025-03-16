using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Utils
{
    public class StringUtils
    {
        public static string GetFileName(string path)
        {
            string[] strings = path.Split("/");
            return strings[strings.Length - 1]; 
        }
    }
}

using System;
using System.IO; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop; 
using MauiApp1;
using System.Runtime.Serialization;
using Microsoft.JSInterop.Implementation;
namespace MarkdownMobile.Utils
{
    public class PdfUtils
    {
        public const string PDF_SCRIPT = "getPdfString()"; 
        public const string PDF_DIR = "/tem/pdf";
        public const string PDF_NAME = "/tem_pdf.pdf"; 
        public static async Task<string> ToPdf(WebView webview)
        {
            try
            {
                // encoded in base64 
                var result = await webview.EvaluateJavaScriptAsync(PDF_SCRIPT);
                // need to parse the base 64 ; first need to get the last of the base64 string
                string[] base64Array = result.Split(",");
                string base64String = base64Array[base64Array.Length - 1];
                byte[] content = Convert.FromBase64String(base64String); 
                //Console.WriteLine(result);
                if(result == "{}")
                {
                    return ""; 
                }
                FileManager.SaveFile(content, PDF_DIR, PDF_NAME); 
                return PDF_DIR + PDF_NAME; 
            }
            catch(Exception e)
            {
                Console.WriteLine("Error Occurred When Processing the PDF"); 
                Console.Write(e.Message); 
                Console.WriteLine(e.StackTrace) ;
                return ""; 
            }
        }

        private static Stream StringToStream(string html)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(html);
            Stream stream = new MemoryStream(byteArray);
            return stream; 
        }
    }
}

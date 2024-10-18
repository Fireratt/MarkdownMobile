using System;
using System.IO; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;
using MauiApp1;
namespace MarkdownMobile.Utils
{
    public class PdfUtils
    {
        public const string PDF_DIR = "/tem/pdf";
        public const string PDF_NAME = "/tem_pdf.pdf"; 
        public static string toPdf(string html)
        {
            HtmlLoadOptions options = new HtmlLoadOptions();
            try
            {
                Document pdfDocument = new Document(StringToStream(html), options);
                pdfDocument.Save(FileManager.ROOT_DIR + PDF_DIR + PDF_NAME);
                return FileManager.ROOT_DIR + PDF_DIR + PDF_NAME; 
            }
            catch(Exception e)
            {
                Console.Write(e.Message); 
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSoftware.KatexSharpRunner;
using Jint; 
namespace MauiApp1
{
    public class LatexRunner
    {
        static readonly KatexJsLatexProcessor preprocessor = new KatexJsLatexProcessor(jintEngineCount: 1, jintEngineLockTimeout: 2000);
        static readonly MarkdownLatexPreprocessor markdownPreprocessor = new MarkdownLatexPreprocessor(preprocessor); 
        public static string RenderMarkdown(string markdown)
        {
            
            return markdownPreprocessor.ProcessLatex(markdown);
        }

    }
}


using Markdig;
using System.Diagnostics.Metrics;
using System.Text.Unicode;
using System.Xml.Linq;
namespace MauiApp1
{
    public partial class MainPage : ContentPage , IQueryAttributable
    {
        int count = 0;
        readonly string[] fastInputs = { "`", "```", "#", "$", "^", "_", "*" };

        public MainPage()
        {
            InitializeComponent();
            InitializeFastInputs(); 
        }
        private void InitializeFastInputs()
        {
            foreach (string i in fastInputs)
            {
                var button = new FastInputButton(this, i);
                MainButtons.Add(button); 
            }
        }
        public void Input(string append)    // append a string in the text ; it will be used in the FastInput
        {
            if (MarkdownEditor.Text == null)
            {
                MarkdownEditor.Text = ""; 
            }
            Console.WriteLine(MarkdownEditor.CursorPosition);
            int originalCursor = MarkdownEditor.CursorPosition; 
            MarkdownEditor.Text = MarkdownEditor.Text.Insert(MarkdownEditor.CursorPosition, append);
            // Set the cursor the the position write behind the append
            MarkdownEditor.CursorPosition = originalCursor + append.Length; 
            Console.WriteLine(MarkdownEditor.CursorPosition);


        }
        public String Read()    // transform the markdown to html
        {
            var mdPipeLine = new MarkdownPipelineBuilder().UseMathematics().UseAutoLinks().Build();
            Console.WriteLine(Markdig.Markdown.ToHtml(MarkdownEditor.Text, mdPipeLine)); 
            return Markdig.Markdown.ToHtml(MarkdownEditor.Text , mdPipeLine); 
        }
        public void OnChange(object sender, TextChangedEventArgs e)
        {
            string result = Read(); 
            Console.WriteLine(result);
            string header = "    <meta charset=\"UTF-8\">" + 
    "<meta name = \"viewport\" content = \"width=device-width, initial-scale=1.0\">\n" + 
    "<link rel = \"stylesheet\" href = \"https://cdn.jsdelivr.net/npm/katex@0.15.1/dist/katex.min.css\">\n" + 
    "<script defer src = \"https://cdn.jsdelivr.net/npm/katex@0.15.1/dist/katex.min.js\"></script>\n" + 
    "<script defer src = \"https://cdn.jsdelivr.net/npm/katex@0.15.1/dist/contrib/auto-render.min.js\"" + 
        "onload = \"renderMathInElement(document.body);\"></script>" 
            ;
            MarkdownView.Html = $"<!DOCTYPE html><html><head>{header}</head><body><div class=\"results\">{result}</div></body></html>";

        }

        public async void OnSave(object sender, EventArgs e)
        {
            string fileName = await this.DisplayPromptAsync("SaveFile", "请输入保存文件名");
            Console.WriteLine(fileName); 
            if(fileName == "" || fileName == null)
            {
                this.DisplayAlert("", "save stopped due to use input error", "OK");
                return; 
            }
            if (await FileManager.SaveFile(MarkdownEditor.Text , fileName))
            {
                this.DisplayAlert("", "save success" , "OK");
            }
            else
            {
                this.DisplayAlert("", "save stopped due to unknowned problem", "OK");

            }
        }
        public void onSaveAs(object sender, TextChangedEventArgs e)
        {

        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("filename", out object filename)) // receive the filename from filepage
            {
                MarkdownEditor.Text = await FileManager.ReadFile(filename as string);

            }
        }
    }
    

}

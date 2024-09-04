
using Markdig;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public MainPage()
        {
            InitializeComponent();
        }
        public String Read()    // transform the markdown to html
        {
            var mdPipeLine = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

            return Markdig.Markdown.ToHtml(Canvas.Text , mdPipeLine); 
        }
        public void OnChange(object sender, TextChangedEventArgs e)
        {
            Canvas.Text = MarkdownEditor.Text;
            MarkdownView.Html = Read(); 
        }

        public void onSave()
        {

        }
        public void onSaveAs()
        {

        }
    }
    

}

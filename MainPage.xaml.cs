
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

        public async void OnSave(object sender, EventArgs e)
        {
            if (await FileManager.SaveFile(Canvas.Text))
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

    }
    

}

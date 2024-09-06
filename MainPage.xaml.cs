
using Markdig;
namespace MauiApp1
{
    public partial class MainPage : ContentPage , IQueryAttributable
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
            string fileName = await this.DisplayPromptAsync("SaveFile", "请输入保存文件名");
            Console.WriteLine(fileName); 
            if(fileName == "" || fileName == null)
            {
                this.DisplayAlert("", "save stopped due to use input error", "OK");
                return; 
            }
            if (await FileManager.SaveFile(Canvas.Text , fileName))
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

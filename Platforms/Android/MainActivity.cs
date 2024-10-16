using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using MauiApp1.DataBase;
using MauiApp1.Utils; 
namespace MauiApp1
{
    [IntentFilter(new[] { Android.Content.Intent.ActionSend, Android.Content.Intent.ActionView }
    , Categories = new[] { Android.Content.Intent.CategoryDefault }, DataMimeType = @"text/markdown")]//.md
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true,
        LaunchMode = LaunchMode.SingleTask, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
        | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
        protected override async void OnNewIntent(Intent? intent)
        {
            base.OnNewIntent(intent);
            if (intent!=null && intent.Action == Android.Content.Intent.ActionSend)
            {
                var fileUri = intent.GetParcelableExtra(Android.Content.Intent.ExtraStream) as Android.Net.Uri;
                if (fileUri != null)
                {
                    await Shell.Current.GoToAsync("//MainPage");
                    (Shell.Current.CurrentPage as MainPage)?.Open(await ReadFileAndroid(fileUri));
                }
            }
            else if (intent!=null && intent.Action == Android.Content.Intent.ActionView)
            {
                var fileUri = intent.Data;
                if (fileUri != null)
                {
                    await Shell.Current.GoToAsync("//MainPage");
                    (Shell.Current.CurrentPage as MainPage)?.Open(await ReadFileAndroid(fileUri));
                }
            }
        }
        public async Task<string> ReadFileAndroid(Android.Net.Uri fileUri)
        {
            using var inputStream = ContentResolver?.OpenInputStream(fileUri);
            if(inputStream == null)
            {
                throw new Exception("Error Occur When Open Uri"); 
            }
            using var streamReader = new StreamReader(inputStream);
            await RecordFileEntryAndroid(fileUri); 
            return streamReader.ReadToEnd();
        }
        // the static version for other class to call 
        public string ReadFileAndroid(string uri)
        {
            Android.Net.Uri readingUri = Android.Net.Uri.Parse(uri);
            if(readingUri == null)
            {
                throw new Exception("Error When Read File From Android System");
            }
            using var inputStream = ContentResolver?.OpenInputStream(readingUri);
            if (inputStream != null)
            {
                using var streamReader = new StreamReader(inputStream);
                return streamReader.ReadToEnd();
            }
            throw new Exception("Error When Read File From Android System");
        }
        private async Task RecordFileEntryAndroid(Android.Net.Uri fileUri)
        {
            if(fileUri.ToString() == null)
            {
                return;
            }
            var filePath = fileUri.ToString()??"";  // check if it is empty ; 
            string filename = StringUtils.GetFileName(filePath); 
            await DatabaseDao.getDataBase().InsertEntriesAsync(new FileEntry {Name=filename , Path = filePath }); 
        }
    }
    
}

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;

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
        protected override void OnNewIntent(Intent? intent)
        {
            base.OnNewIntent(intent);
            if (intent.Action == Android.Content.Intent.ActionSend)
            {
                var fileUri = intent.GetParcelableExtra(Android.Content.Intent.ExtraStream) as Android.Net.Uri;
                if (fileUri != null)
                {
                    (App.Current.MainPage as MainPage).Open(ReadFileAndroid(fileUri));
                }
            }
            else if (intent.Action == Android.Content.Intent.ActionView)
            {
                var fileUri = intent.Data;
                if (fileUri != null)
                {
                    (Shell.Current.CurrentPage as MainPage).Open(ReadFileAndroid(fileUri)); 
                }
            }
        }
        private string ReadFileAndroid(Android.Net.Uri fileUri)
        {
            using var inputStream = ContentResolver.OpenInputStream(fileUri);
            using var streamReader = new StreamReader(inputStream);
            return streamReader.ReadToEnd();

        }
    }
    
}

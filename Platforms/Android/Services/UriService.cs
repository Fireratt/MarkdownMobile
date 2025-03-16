using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public partial class UriService
    {
        public static partial string ReadUriData(string uri)
        {
            Android.Net.Uri readingUri = Android.Net.Uri.Parse(uri);
            using var inputStream = Platform.CurrentActivity.ContentResolver.OpenInputStream(readingUri);
            using var streamReader = new StreamReader(inputStream);
            return streamReader.ReadToEnd();
        }
    }
    //public partial interface IUriService
    //{
    //    public string ReadUriData(string uri); 
    //}
}

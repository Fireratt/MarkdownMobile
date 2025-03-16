using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.System
{
    public class SystemInfo
    {
        public static bool IsAndroid()
        {
            return DeviceInfo.Current.Platform == DevicePlatform.Android; 
        }
    }
}

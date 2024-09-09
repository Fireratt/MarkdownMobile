using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MauiApp1
{
    public class FastInputButton:Button
    {
        readonly string text;
        readonly MainPage parent; 
        private void onClicked(object sender, EventArgs e)
        {
            parent.Input(text); 
        }
        public FastInputButton(MainPage parent , string text)
        {
            this.Clicked += onClicked;
            this.text = text;
            this.parent = parent;
            this.Text = text;
            this.Margin = new Thickness(10,0,10,0); 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MauiApp1
{
    public class FileUnit: VerticalStackLayout
    {
        private HorizontalStackLayout line1;
        private Label route; 
        private Label name; 
        private Button deleteButton;
        private Button openButton;
        private Button shareButton; 
        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FileUnit)bindable;
            control.name.Text = newValue as string;
        }
        private static void OnRouteChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FileUnit)bindable;
            control.route.Text = newValue as string;
        }
        // bind the property 
        public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(FileUnit), default(string) , propertyChanged:OnTextChanged);
        public String Text
        {
            get => (string)GetValue(TextProperty); 
            set
            {
                SetValue(TextProperty, value); 
            }
        }
        public static readonly BindableProperty RouteProperty =
        BindableProperty.Create(nameof(Route), typeof(string), typeof(FileUnit), default(string), propertyChanged: OnRouteChanged);
        public String Route
        {
            get => (string)GetValue(RouteProperty);
            set
            {
                SetValue(RouteProperty, value);
            }
        }
        public FileUnit()
        {
            name = new Label
            {
                Text = Text,
            };

            deleteButton = new Button
            {
                Text = "删除",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            openButton = new Button
            {
                Text = "打开"
            };
            shareButton = new Button
            {
                Text = "分享"
            };
            line1 = new HorizontalStackLayout();
            line1.Padding = 10;
            line1.Margin = 24; 
            line1.Add(name);
            line1.Add(deleteButton);
            line1.Add(shareButton); 
            openButton.Clicked += onOpen;   // bind the function for button
            shareButton.Clicked += onShare;
            deleteButton.Clicked += onDelete;
            line1.Add(openButton);
            Console.WriteLine("Route:" + Route); 
            route = new Label
            {

                Text = Route
            };
            Add(line1); 
            Add(route); 
            Padding = 10;
            Margin = 24; 
        }
        public void onOpen(object sender,EventArgs e)
        {
            var shell = Shell.Current;
            shell.GoToAsync($"//MainPage?filename={Text}"); 
        }

        public void onShare(object sender , EventArgs e)
        {
            FileManager.ShareFile(Text); 
        }
        public void onDelete(object sender, EventArgs e)
        {
            if (FileManager.DeleteFile(Text))
            {
                this.IsVisible = false; 
            }

        }
    }
}

using System;
using Markdig;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Text;
using Microsoft.Maui.Graphics.Text.Renderer;
namespace MauiApp1
{
    public class MarkdownCanvas : GraphicsView
    {
        public MarkdownCanvas()
        {
            WidthRequest = 300;
            HeightRequest = 300;
            HorizontalOptions = Microsoft.Maui.Controls.LayoutOptions.Fill;
            VerticalOptions = Microsoft.Maui.Controls.LayoutOptions.Fill; 
            Drawable = new MarkdownDrawable(this);
        }
        // bind the property 
        public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(MarkdownCanvas), default(string));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set {
                SetValue(TextProperty, value);
                // trigger the re-draw event 
                this.Invalidate(); 
            }

        }
        public class MarkdownDrawable : IDrawable
        {
            private MarkdownCanvas parent;
            public MarkdownDrawable(MarkdownCanvas outCanvas)
            {
                this.parent = outCanvas;
            }
            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                //canvas.DrawText(Read(), 0, 0, (float)parent.WidthRequest, (float)parent.HeightRequest);
            }
            public String Read()    // Transform markdown to html . 
            {
                Console.WriteLine(MarkdownAttributedTextReader.Read(parent.Text).Text);
                return parent.Text; 
                //return MarkdownAttributedTextReader.Read(parent.Text);
            }
        }

    }

}

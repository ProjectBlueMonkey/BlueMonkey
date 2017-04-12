using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueMonkey.iOS.Renderers;
using BlueMonkey.Views.Controls;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NoneBorderEntry), typeof(NoneBorderEntryRenderer))]
namespace BlueMonkey.iOS.Renderers
{
    public class NoneBorderEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}
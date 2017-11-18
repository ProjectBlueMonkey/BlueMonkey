using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueMonkey.iOS.Effects;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("BlueMonkey")]
[assembly: ExportEffect(typeof(RemoveBorderEffect), "RemoveBorderEffect")]
namespace BlueMonkey.iOS.Effects
{
    public class RemoveBorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var textField = Control as UITextField;
            if (textField != null)
            {
                textField.BorderStyle = UITextBorderStyle.None;
            }
        }

        protected override void OnDetached()
        {
        }
    }
}
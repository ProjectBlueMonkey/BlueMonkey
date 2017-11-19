using Android.Content;
using Android.Graphics;
using Android.Views;
using BlueMonkey.Droid.Renderers;
using BlueMonkey.Views.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ButtonRenderer = Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer;

[assembly: ExportRenderer(typeof(FlatButton), typeof(FlatButtonRenderer))]
namespace BlueMonkey.Droid.Renderers
{
    public class FlatButtonRenderer : ButtonRenderer
    {
        public FlatButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            var button = Control as Android.Widget.Button;
            button.StateListAnimator = null;
            button.Gravity = GravityFlags.Left;
        }
    }
}
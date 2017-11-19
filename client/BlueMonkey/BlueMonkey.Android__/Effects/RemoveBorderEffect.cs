using BlueMonkey.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("BlueMonkey")]
[assembly: ExportEffect(typeof(RemoveBorderEffect), "RemoveBorderEffect")]
namespace BlueMonkey.Droid.Effects
{
    public class RemoveBorderEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
        }

        protected override void OnDetached()
        {
        }
    }
}
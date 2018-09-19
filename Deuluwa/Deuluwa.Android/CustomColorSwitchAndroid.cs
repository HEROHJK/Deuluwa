using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;

[assembly: ExportRenderer(typeof(Deuluwa.CustomColorSwitch), typeof(Deuluwa.Droid.CustomColorSwitchAndroid))]
namespace Deuluwa.Droid
{
    public class CustomColorSwitchAndroid : SwitchRenderer
    {
        private Android.Graphics.Color greyColor = new Android.Graphics.Color(215, 218, 220);
        private Android.Graphics.Color greenColor = new Android.Graphics.Color(32, 156, 68);
        private Android.Graphics.Color whiteColor = new Android.Graphics.Color(255, 255, 255);

        public CustomColorSwitchAndroid(Android.Content.Context context) : base(context)
        {

        }

        protected override void Dispose(bool disposing)
        {
            this.Control.CheckedChange -= this.OnCheckedChange;
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                if (this.Control.Checked)
                {
                    this.Control.ThumbDrawable.SetColorFilter(greenColor, PorterDuff.Mode.SrcAtop);
                    this.Control.TrackDrawable.SetColorFilter(greyColor, PorterDuff.Mode.SrcAtop);
                }
                else
                {
                    this.Control.ThumbDrawable.SetColorFilter(whiteColor, PorterDuff.Mode.SrcAtop);
                    this.Control.TrackDrawable.SetColorFilter(greyColor, PorterDuff.Mode.SrcAtop);
                }

                this.Control.CheckedChange += this.OnCheckedChange;
            }
        }

        private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (this.Control.Checked)
            {
                this.Control.ThumbDrawable.SetColorFilter(greenColor, PorterDuff.Mode.SrcAtop);
            }
            else
            {
                this.Control.ThumbDrawable.SetColorFilter(greyColor, PorterDuff.Mode.SrcAtop);
            }
        }
    }
}
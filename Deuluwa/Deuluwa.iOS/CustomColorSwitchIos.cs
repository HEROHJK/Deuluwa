using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Deuluwa.CustomColorSwitch), typeof(Deuluwa.iOS.CustomColorSwitchIos))]
namespace Deuluwa.iOS
{
    public class CustomColorSwitchIos : SwitchRenderer
    {
        private Color greyColor = new Color(215, 218, 220);
        private Color greenColor = new Color(32, 156, 68);
        private Color whiteColor = new Color(255, 255, 255);

        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            Element.Toggled += ElementToggled;

            base.OnElementChanged(e);

            if (Control != null)
            {
                UpdateUiSwitchColor();
            }
        }

        private void ElementToggled(object sender, ToggledEventArgs e)
        {
            UpdateUiSwitchColor();
        }

        private void UpdateUiSwitchColor()
        {
            var temp = Element as Switch;

            if (temp.IsToggled)
            {
                Control.ThumbTintColor = greenColor.ToUIColor();
                Control.OnTintColor = greyColor.ToUIColor();
            }
            else
            {
                Control.ThumbTintColor = whiteColor.ToUIColor();
            }
        }
    }
}
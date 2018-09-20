using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof(Deuluwa.WhiteRoundEntry), typeof(Deuluwa.iOS.WhiteRoundEntryIos))]
namespace Deuluwa.iOS
{
    public class WhiteRoundEntryIos : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Layer.CornerRadius = 10;
                Control.Layer.BorderWidth = 3f;
                Control.Layer.BorderColor = Color.White.ToCGColor();

                Control.LeftView = new UIView(new CGRect(0, 0, 10, 0));
                Control.LeftViewMode = UITextFieldViewMode.Always;
            }
        }
    }
}
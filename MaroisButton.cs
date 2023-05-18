using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Marois.Framework.Core.WPF.Controls
{
    public class MaroisButton : Button
    {
        #region CTOR
        static MaroisButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaroisButton), new FrameworkPropertyMetadata(typeof(MaroisButton)));
        }
        #endregion

        #region Dependency Properties
        #region DP Caption
        public static readonly DependencyProperty CaptionProperty =
                    DependencyProperty.Register("Caption",
                    typeof(string),
                    typeof(MaroisButton),
                    new PropertyMetadata(""));

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }
        #endregion
        #endregion
    }
}

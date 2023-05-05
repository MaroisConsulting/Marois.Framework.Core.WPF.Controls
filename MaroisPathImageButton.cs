using System.Windows;
using System.Windows.Controls;

namespace Marois.Framework.Core.WPF.Controls
{
public class MaroisPathImageButton : Button
{
    #region CTOR
    static MaroisPathImageButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MaroisPathImageButton), new FrameworkPropertyMetadata(typeof(MaroisPathImageButton)));
    }
    #endregion

    #region Dependency Properties
    #region DP Caption
    public static readonly DependencyProperty CaptionProperty =
                DependencyProperty.Register("Caption",
                typeof(string),
                typeof(MaroisPathImageButton),
                new PropertyMetadata(""));

    public string Caption
    {
        get { return (string)GetValue(CaptionProperty); }
        set { SetValue(CaptionProperty, value); }
    }
    #endregion

    #region DP PathData
    public static readonly DependencyProperty PathDataProperty =
                DependencyProperty.Register("PathData",
                typeof(System.Windows.Media.Geometry),
                typeof(MaroisPathImageButton),
                new PropertyMetadata(null, new PropertyChangedCallback(OnPathDataChanged)));

    public System.Windows.Media.Geometry PathData
    {
        get { return (System.Windows.Media.Geometry)GetValue(PathDataProperty); }
        set { SetValue(PathDataProperty, value); }
    }

    private static void OnPathDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        MaroisPathImageButton control = (MaroisPathImageButton)d;
    }
    #endregion

    #endregion
}
}

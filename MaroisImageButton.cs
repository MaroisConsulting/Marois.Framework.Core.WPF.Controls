using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Application = System.Windows.Application;

namespace Marois.Framework.Core.WPF.Controls
{
    public class MaroisImageButton : Button
    {
        public enum LayoutMode
        {
            Horizontal,
            Verticle
        }

        #region CTOR
        static MaroisImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaroisImageButton), 
                new FrameworkPropertyMetadata(typeof(MaroisImageButton)));
        }
        #endregion

        #region Dependency Properties
        #region DP HoverBrush
        public static readonly DependencyProperty HoverBrushProperty =
                    DependencyProperty.Register("HoverBrush",
                    typeof(SolidColorBrush),
                    typeof(MaroisImageButton),
                    new PropertyMetadata(new SolidColorBrush(Colors.Black), new PropertyChangedCallback(OnHoverBrushChanged)));

        public SolidColorBrush HoverBrush
        {
            get { return (SolidColorBrush)GetValue(HoverBrushProperty); }
            set { SetValue(HoverBrushProperty, value); }
        }

        private static void OnHoverBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var control = (MaroisImageButton)d;
        }
        #endregion

        #region DP PressedBrush
        public static readonly DependencyProperty PressedBrushProperty =
                    DependencyProperty.Register("PressedBrush",
                    typeof(SolidColorBrush),
                    typeof(MaroisImageButton),
                    new PropertyMetadata(new SolidColorBrush(Colors.Black), new PropertyChangedCallback(OnPressedBrushChanged)));

        public SolidColorBrush PressedBrush
        {
            get { return (SolidColorBrush)GetValue(PressedBrushProperty); }
            set { SetValue(PressedBrushProperty, value); }
        }


        private static void OnPressedBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var control = (MaroisImageButton)d;
        }
        #endregion

        #region DP DisabledBrush
        public static readonly DependencyProperty DisabledBrushProperty =
                    DependencyProperty.Register("DisabledBrush",
                    typeof(SolidColorBrush),
                    typeof(MaroisImageButton),
                    new PropertyMetadata(new SolidColorBrush(Colors.Black), new PropertyChangedCallback(OnDisabledBrushChanged)));

        public SolidColorBrush DisabledBrush
        {
            get { return (SolidColorBrush)GetValue(DisabledBrushProperty); }
            set { SetValue(DisabledBrushProperty, value); }
        }

        private static void OnDisabledBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var control = (MaroisImageButton)d;
        }
        #endregion

        #region DP Caption
        public static readonly DependencyProperty CaptionProperty =
                    DependencyProperty.Register("Caption",
                    typeof(string),
                    typeof(MaroisImageButton),
                    new PropertyMetadata("", new PropertyChangedCallback(OnCaptionChanged)));

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        private static void OnCaptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var control = (MaroisImageButton)d;
        }
        #endregion

        #region DP ImageSize
        public double ImageSize
        {
            get { return (double)GetValue(ImageSizeProperty); }
            set { SetValue(ImageSizeProperty, value); }
        }
        public static readonly DependencyProperty ImageSizeProperty =
            DependencyProperty.Register("ImageSize", typeof(double), typeof(MaroisImageButton),
            new FrameworkPropertyMetadata(30.0, FrameworkPropertyMetadataOptions.AffectsRender));
        #endregion

        #region DP NormalImage
        public string NormalImage
        {
            get { return (string)GetValue(NormalImageProperty); }
            set { SetValue(NormalImageProperty, value); }
        }
        public static readonly DependencyProperty NormalImageProperty =
            DependencyProperty.Register("NormalImage", typeof(string), typeof(MaroisImageButton),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender, ImageSourceChanged));

        private static void ImageSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var uri = new Uri((string)e.NewValue, UriKind.Relative);
            Application.GetResourceStream(uri);
        }
        #endregion

        #region DP HoverImage
        public static readonly DependencyProperty HoverImageProperty =
                    DependencyProperty.Register("HoverImage",
                    typeof(string),
                    typeof(MaroisImageButton),
                    new PropertyMetadata(null, new PropertyChangedCallback(OnHoverImageChanged)));

        public string HoverImage
        {
            get { return (string)GetValue(HoverImageProperty); }
            set { SetValue(HoverImageProperty, value); }
        }

        private static void OnHoverImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MaroisImageButton control = (MaroisImageButton)d;
        }
        #endregion

        #region DP PressedImage
        public string PressedImage
        {
            get { return (string)GetValue(PressedImageProperty); }
            set { SetValue(PressedImageProperty, value); }
        }
        public static readonly DependencyProperty PressedImageProperty =
            DependencyProperty.Register("PressedImage", typeof(string), typeof(MaroisImageButton),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender, ImageSourceChanged));
        #endregion

        #region DP DisabledImage
        public string DisabledImage
        {
            get { return (string)GetValue(DisabledImageProperty); }
            set { SetValue(DisabledImageProperty, value); }
        }
        public static readonly DependencyProperty DisabledImageProperty =
            DependencyProperty.Register("DisabledImage", typeof(string), typeof(MaroisImageButton),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender, ImageSourceChanged));

        //private static void ImageSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        //{
        //    //var uri = new Uri("pack://application:,,," + (string)e.NewValue);
        //    var uri = new Uri((string)e.NewValue);
        //    Application.GetResourceStream(uri);
        //}
        #endregion

        #region DP Orientation
        public static readonly DependencyProperty OrientationProperty =
                    DependencyProperty.Register("Orientation",
                    typeof(LayoutMode),
                    typeof(MaroisImageButton),
                    new PropertyMetadata(LayoutMode.Verticle, new PropertyChangedCallback(OnOrientationChanged)));

        public LayoutMode Orientation
        {
            get { return (LayoutMode)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ////var control = (MaroisImageButton)d;
        }
        #endregion

        #region DP CornerRadius
        public static readonly DependencyProperty CornerRadiusProperty =
                    DependencyProperty.Register("CornerRadius",
                    typeof(double),
                    typeof(MaroisImageButton),
                    new PropertyMetadata(0.00, new PropertyChangedCallback(OnCornerRadiusChanged)));

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }


        private static void OnCornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var control = (MaroisImageButton)d;
        }
        #endregion
        #endregion
    }
}

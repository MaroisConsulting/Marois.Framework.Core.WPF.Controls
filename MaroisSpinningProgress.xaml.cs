using System.Windows;
using System.Windows.Media;

namespace Marois.Framework.Core.WPF.Controls
{
    /// <summary>
    /// Interaction logic for MaroisSpinningProgress.xaml
    /// </summary>
    public partial class MaroisSpinningProgress : UserControlBase
    {
        #region DP IndicatorColor
        public static readonly DependencyProperty IndicatorColorProperty =
                    DependencyProperty.Register("IndicatorColor",
                    typeof(SolidColorBrush),
                    typeof(MaroisSpinningProgress),
                    new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public SolidColorBrush IndicatorColor
        {
            get { return (SolidColorBrush)GetValue(IndicatorColorProperty); }
            set { SetValue(IndicatorColorProperty, value); }
        }
        #endregion

        #region DP TextColor
        public static readonly DependencyProperty TextColorProperty =
                    DependencyProperty.Register("TextColor",
                    typeof(SolidColorBrush),
                    typeof(MaroisSpinningProgress),
                    new PropertyMetadata(new SolidColorBrush(Colors.Black), new PropertyChangedCallback(OnTextColorChanged)));

        public SolidColorBrush TextColor
        {
            get { return (SolidColorBrush)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }


        private static void OnTextColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MaroisSpinningProgress control = (MaroisSpinningProgress)d;
        }
        #endregion


        #region DP Message
        public static readonly DependencyProperty MessageProperty =
                    DependencyProperty.Register("Message",
                    typeof(string),
                    typeof(MaroisSpinningProgress),
                    new PropertyMetadata("", new PropertyChangedCallback(OnMessageChanged)));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }


        private static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MaroisSpinningProgress)d;
        }
        #endregion

        #region CTOR
        public MaroisSpinningProgress()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion
    }
}

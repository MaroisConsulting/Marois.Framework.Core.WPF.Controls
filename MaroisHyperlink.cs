using Marois.Framework.Core.Utilities;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Marois.Framework.Core.WPF.Controls
{
    public class MaroisHyperlink : ControlBase
    {
        #region Routed Events
        #region LinkClickedEvent
        public static readonly RoutedEvent LinkClickedEvent =
                    EventManager.RegisterRoutedEvent("LinkClicked",
                    RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler),
                    typeof(MaroisHyperlink));


        public event RoutedEventHandler LinkClicked
        {
            add { AddHandler(LinkClickedEvent, value); }
            remove { RemoveHandler(LinkClickedEvent, value); }
        }

        private void RaiseLinkClickedEvent()
        {
            var args = new RoutedEventArgs(LinkClickedEvent);
            RaiseEvent(args);
        }
        #endregion
        #endregion

        #region DP's
        #region DP TextDecorations
        public static readonly DependencyProperty TextDecorationsProperty =
            Inline.TextDecorationsProperty.AddOwner(typeof(MaroisHyperlink));

        public TextDecorationCollection TextDecorations
        {
            get { return (TextDecorationCollection)GetValue(TextDecorationsProperty); }
            set { SetValue(TextDecorationsProperty, value); }
        }
        #endregion

        #region DP HoverBrush
        public static readonly DependencyProperty HoverBrushProperty =
                    DependencyProperty.Register("HoverBrush",
                    typeof(SolidColorBrush),
                    typeof(MaroisHyperlink),
                    new PropertyMetadata(new SolidColorBrush(Colors.Green)));

        public SolidColorBrush HoverBrush
        {
            get { return (SolidColorBrush)GetValue(HoverBrushProperty); }
            set { SetValue(HoverBrushProperty, value); }
        }
        #endregion

        #region DP LinkText
        public static readonly DependencyProperty LinkTextProperty =
                    DependencyProperty.Register("LinkText",
                    typeof(string),
                    typeof(MaroisHyperlink),
                    new PropertyMetadata("MaroisHyperlink"));

        public string LinkText
        {
            get { return (string)GetValue(LinkTextProperty); }
            set { SetValue(LinkTextProperty, value); }
        }
        #endregion
        #endregion

        #region Commands
        private ICommand? _LinkClickedCommand;
        public ICommand LinkClickedCommand
        {
            get
            {
                if (_LinkClickedCommand == null)
                    _LinkClickedCommand = new RelayCommand(LinkClickedExecuted);
                return _LinkClickedCommand;
            }
        }
        #endregion

        #region CTOR
        static MaroisHyperlink()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaroisHyperlink),
                new FrameworkPropertyMetadata(typeof(MaroisHyperlink)));
        }
        #endregion

        #region Private Methods
        private void LinkClickedExecuted()
        {
            RaiseLinkClickedEvent();
        }
        #endregion
    }

    //public class LinkClickedEventArgs : RoutedEventArgs
    //{
    //    public string NewPath { get; private set; }

    //    public LinkClickedEventArgs(RoutedEvent routedEvent, string newPath) :
    //        base(routedEvent)
    //    {
    //        NewPath = newPath;
    //    }
    //}
}

using CommunityToolkit.Mvvm.Input;
using Marois.Framework.Core.Controls.Shared.Entities;
using Marois.Framework.Core.Controls.Shared.Interfaces;
using Marois.Framework.Core.WPF.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Marois.Framework.Core.WPF.Controls
{
    public class MaroisNavigationContainer : UserControlBase
    {
        #region Properties
        private string? _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                if (_SearchText != value)
                {
                    _SearchText = value;
                    RaisePropertyChanged(nameof(SearchText));
                }
            }
        }
        #endregion

        #region Commands
        #endregion

        #region DP's
        #region DP ContainerItems
        public static readonly DependencyProperty ContainerItemsProperty =
                    DependencyProperty.Register("ContainerItems",
                    typeof(List<MaroisNavigationPane>),
                    typeof(MaroisNavigationContainer),
                    new PropertyMetadata(null, new PropertyChangedCallback(OnContainerItemsChanged)));

        public List<MaroisNavigationPane>? ContainerItems
        {
            get { return (List<MaroisNavigationPane>?)GetValue(ContainerItemsProperty); }
            set { SetValue(ContainerItemsProperty, value); }
        }

        private static void OnContainerItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var control = (NavigationContainer)d;
        }
        #endregion

        #region NavigationPanes DP
        public static readonly DependencyProperty NavigationPanesProperty =
                    DependencyProperty.Register("NavigationPanes",
                    typeof(List<MaroisNavigationPaneModel>),
                    typeof(MaroisNavigationContainer),
                    new PropertyMetadata(null, new PropertyChangedCallback(OnNavigationPanesChanged)));

        public List<MaroisNavigationPaneModel> NavigationPanes
        {
            get { return (List<MaroisNavigationPaneModel>)GetValue(NavigationPanesProperty); }
            set { SetValue(NavigationPanesProperty, value); }
        }

        private static void OnNavigationPanesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MaroisNavigationContainer)d;
            control.Load();
        }
        #endregion

        #region DP NavigationLoadArguments
        public static readonly DependencyProperty NavigationLoadArgumentsProperty =
                    DependencyProperty.Register("NavigationLoadArguments",
                    typeof(INavigationLoadArguments),
                    typeof(MaroisNavigationPane),
                    new PropertyMetadata(null, new PropertyChangedCallback(OnNavigationLoadArgumentsChanged)));

        public INavigationLoadArguments NavigationLoadArguments
        {
            get { return (INavigationLoadArguments)GetValue(NavigationLoadArgumentsProperty); }
            set { SetValue(NavigationLoadArgumentsProperty, value); }
        }


        private static void OnNavigationLoadArgumentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MaroisNavigationPane)d;
        }
        #endregion
        #endregion

        #region Routed Events
        #region NavigationItemSelectedEvent
        public static readonly RoutedEvent NavigationItemSelectedEvent =
                    EventManager.RegisterRoutedEvent("NavigationItemSelected",
                    RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler),
                    typeof(MaroisNavigationContainer));


        public event RoutedEventHandler NavigationItemSelected
        {
            add { AddHandler(NavigationItemSelectedEvent, value); }
            remove { RemoveHandler(NavigationItemSelectedEvent, value); }
        }

        private void RaiseNavigationItemSelectedEvent()
        {
            var args = new RoutedEventArgs(NavigationItemSelectedEvent);
            RaiseEvent(args);
        }
        #endregion
        #endregion

        #region CTOR
        static MaroisNavigationContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaroisNavigationContainer),
                new FrameworkPropertyMetadata(typeof(MaroisNavigationContainer)));
        }
        #endregion

        #region Private Methods
        private void Load()
        {
            if (NavigationPanes != null)
            {
                ContainerItems = new List<MaroisNavigationPane>();

                foreach (var navigationPaneModel in NavigationPanes)
                {
                    var navigationPane = new MaroisNavigationPane
                    { 
                        Header = navigationPaneModel.Header ?? "",
                        NavigationPaneModel = navigationPaneModel,
                        LoadArguments = NavigationLoadArguments,
                        IsPaneExpanded = navigationPaneModel.IsExpanded
                    };
                    ContainerItems.Add(navigationPane);
                }
            }
        }
        #endregion
    }
}

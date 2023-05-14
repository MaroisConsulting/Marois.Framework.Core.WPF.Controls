using CommunityToolkit.Mvvm.Input;
using Marois.Framework.Core.Controls.Shared.Entities;
using Marois.Framework.Core.Controls.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Marois.Framework.Core.WPF.Controls
{
    public class MaroisNavigationPane : UserControlBase
    {
        #region Private Fields
        // This is set in OnInitialize to signal that init has finished. Then
        // during runtime when the user expands the pane it's checked to see
        // if the control has finished initializin before allowing Load() to
        // run.
        private bool _isInitialized = false;

        // This is set from the OnIsPaneExpandedChanged BEFORE OnInitialize
        // fires.
        //
        // If the MaroisNavigationPaneModel.IsExpanded is set true from the parent,
        // then the panel should not load until AFTER OnInitialize. 
        //
        // When OnInitialize is done, it checks to see if _isExpandedOnLoad
        // is True, and if so, calls Load();
        private bool _isPaneExpandedOnLoad = false;
        #endregion

        #region Properties
        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                if (_IsLoading != value)
                {
                    _IsLoading = value;
                    RaisePropertyChanged(nameof(IsLoading));

                    ToggleLoadingIndicator();
                }
            }
        }
        #endregion

        #region Commands
        private ICommand? _ExpanderHeaderTemplateLoadedCommand;
        public ICommand ExpanderHeaderTemplateLoadedCommand
        {
            get
            {
                if (_ExpanderHeaderTemplateLoadedCommand == null)
                    _ExpanderHeaderTemplateLoadedCommand = new RelayCommand<RoutedEventArgs>(p => ExpanderHeaderTemplateLoadedExecuted(p), p => ExpanderHeaderTemplateLoadedCanExecute(p));
                return _ExpanderHeaderTemplateLoadedCommand;
            }
        }

        private ICommand? _ItemClickedCommand;
        public ICommand ItemClickedCommand
        {
            get
            {
                if (_ItemClickedCommand == null)
                    _ItemClickedCommand = new RelayCommand<MaroisNavigationEntity>(p => ItemClickedExecuted(p), p => ItemClickedCanExecute(p));
                return _ItemClickedCommand;
            }
        }
        #endregion

        #region DP's
        #region DP Header
        public static readonly DependencyProperty HeaderProperty =
                    DependencyProperty.Register("Header",
                    typeof(string),
                    typeof(MaroisNavigationPane),
                    new PropertyMetadata("", new PropertyChangedCallback(OnHeaderChanged)));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        private static void OnHeaderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var control = (NavigationContainer)d;
        }
        #endregion

        #region DP Items
        public static readonly DependencyProperty ItemsProperty =
                    DependencyProperty.Register("Items",
                    typeof(ObservableCollection<MaroisNavigationEntity>),
                    typeof(MaroisNavigationPane),
                    new PropertyMetadata(null, new PropertyChangedCallback(OnItemsChanged)));

        public ObservableCollection<MaroisNavigationEntity> Items
        {
            get { return (ObservableCollection<MaroisNavigationEntity>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MaroisNavigationPane)d;
        }
        #endregion

        #region DP NavigationPaneModel
        public static readonly DependencyProperty NavigationPaneModelProperty =
                    DependencyProperty.Register("NavigationPaneModel",
                    typeof(MaroisNavigationPaneModel),
                    typeof(MaroisNavigationPane),
                    new PropertyMetadata(null, new PropertyChangedCallback(OnNavigationPaneModelChanged)));

        public MaroisNavigationPaneModel NavigationPaneModel
        {
            get { return (MaroisNavigationPaneModel)GetValue(NavigationPaneModelProperty); }
            set { SetValue(NavigationPaneModelProperty, value); }
        }
        private static void OnNavigationPaneModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MaroisNavigationPane)d;

            if (control._isInitialized && control.NavigationPaneModel.IsExpanded)
            {
                control._isPaneExpandedOnLoad = true;
            }
        }
        #endregion

        #region DP IsPaneExpanded
        public static readonly DependencyProperty IsPaneExpandedProperty =
                    DependencyProperty.Register("IsPaneExpanded",
                    typeof(bool),
                    typeof(MaroisNavigationPane),
                    new PropertyMetadata(false, new PropertyChangedCallback(OnIsPaneExpandedChanged)));

        public bool IsPaneExpanded
        {
            get { return (bool)GetValue(IsPaneExpandedProperty); }
            set { SetValue(IsPaneExpandedProperty, value); }
        }

        private static async void OnIsPaneExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MaroisNavigationPane)d;
            control._isPaneExpandedOnLoad = control.IsPaneExpanded;

            if (control._isInitialized && control.IsPaneExpanded && control.NavigationPaneModel != null)
            {
                await control.Load();
            }
        }
        #endregion
        
        #region DP LoadArguments
        public static readonly DependencyProperty LoadArgumentsProperty =
                    DependencyProperty.Register("LoadArguments",
                    typeof(INavigationLoadArguments),
                    typeof(MaroisNavigationPane),
                    new PropertyMetadata(null, new PropertyChangedCallback(OnLoadArgumentsChanged)));

        public INavigationLoadArguments LoadArguments
        {
            get { return (INavigationLoadArguments)GetValue(LoadArgumentsProperty); }
            set { SetValue(LoadArgumentsProperty, value); }
        }

        private static void OnLoadArgumentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MaroisNavigationPane)d;
        }
        #endregion
        #endregion

        #region Routed Events
        #region ItemClickedEvent
        public static readonly RoutedEvent ItemClickedEvent =
                    EventManager.RegisterRoutedEvent("ItemClicked",
                    RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler),
                    typeof(MaroisNavigationPane));


        public event RoutedEventHandler ItemClicked
        {
            add { AddHandler(ItemClickedEvent, value); }
            remove { RemoveHandler(ItemClickedEvent, value); }
        }

        private void RaiseItemClickedEvent(MaroisNavigationEntity entity)
        {
            var args = new MaroisNavigationEventArgs(ItemClickedEvent, entity);
            RaiseEvent(args);
        }
        #endregion
        #endregion

        public MaroisNavigationPane()
        {
            Loaded += MaroisNavigationPane_Loaded;
        }

        private void MaroisNavigationPane_Loaded(object sender, RoutedEventArgs e)
        {
            _isInitialized = true;

            if (_isPaneExpandedOnLoad)
            {
                IsPaneExpanded = true;
            }

        }
        #region CTOR
        static MaroisNavigationPane()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaroisNavigationPane),
                new FrameworkPropertyMetadata(typeof(MaroisNavigationPane)));
        }
        #endregion

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            _isInitialized = true;

            if (_isPaneExpandedOnLoad)
            {
                IsPaneExpanded = true;
            }
        }

        #region Private Methods
        private bool ExpanderHeaderTemplateLoadedCanExecute(RoutedEventArgs args)
        {
            return true;
        }

        private void ExpanderHeaderTemplateLoadedExecuted(RoutedEventArgs args)
        {
            var border = args.Source as Border;

            var presenter = border.TemplatedParent as ContentPresenter;
            presenter.HorizontalAlignment  = HorizontalAlignment.Stretch;
        }

        private bool ItemClickedCanExecute(MaroisNavigationEntity entity)
        {
            return IsEnabled;
        }

        private void ItemClickedExecuted(MaroisNavigationEntity entity)
        {
            RaiseItemClickedEvent(entity);
        }

        public async Task Load()
        {
            if (NavigationPaneModel != null && NavigationPaneModel.DataSource != null)
            {
                var dataSource = NavigationPaneModel.DataSource();

                List<MaroisNavigationEntity>? data = null;

                if (dataSource != null)
                {
                    IsLoading = true;

                    data = await Task.Run(() => dataSource);

                    IsLoading = false;
                }

                if (data != null)
                {
                    Items = new ObservableCollection<MaroisNavigationEntity>(data);
                }
            }
        }

        private void ToggleLoadingIndicator()
        { 
        }
        #endregion
    }
}

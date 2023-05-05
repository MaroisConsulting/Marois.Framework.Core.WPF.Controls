//SOURCE https://www.codeproject.com/Articles/21248/A-Simple-WPF-Explorer-Tree

using Marois.Framework.Core.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Marois.Framework.Core.WPF.Controls
{
    public class MaroisExplorerTree : ControlBase
    {
        #region Private Fields
        private object _dummyNode = null;
        #endregion

        #region Properties
        private List<TreeViewItem>? _TreeItems;
        public List<TreeViewItem> TreeItems
        {
            get { return _TreeItems; }
            set
            {
                if (_TreeItems != value)
                {
                    _TreeItems = value;
                    RaisePropertyChanged(nameof(TreeItems));
                }
            }
        }

        private string? _SelectedImagePath;
        public string SelectedImagePath
        {
            get { return _SelectedImagePath; }
            set
            {
                if (_SelectedImagePath != value)
                {
                    _SelectedImagePath = value;
                    RaisePropertyChanged(nameof(SelectedImagePath));
                }
            }
        }
        #endregion

        #region PathChangedEvent
        public static readonly RoutedEvent PathChangedEvent =
                    EventManager.RegisterRoutedEvent("PathChanged",
                    RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler),
                    typeof(MaroisExplorerTree));

        public event RoutedEventHandler PathChanged
        {
            add { AddHandler(PathChangedEvent, value); }
            remove { RemoveHandler(PathChangedEvent, value); }
        }

        private void RaisePathChangedEvent(string newPath)
        {
            var args = new PathChangedEventArgs(PathChangedEvent, newPath);
            RaiseEvent(args);
        }
        #endregion

        #region Commands
        private ICommand? _SelectedItemChangedCommand;
        public ICommand SelectedItemChangedCommand
        {
            get
            {
                if (_SelectedItemChangedCommand == null)
                    _SelectedItemChangedCommand = new RelayCommand<RoutedPropertyChangedEventArgs<object>>(x => SelectedItemChangedExecuted(x), x => SelectedItemChangedCanExecute(x));
                return _SelectedItemChangedCommand;
            }
        }
        #endregion

        #region CTOR
        public MaroisExplorerTree()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaroisExplorerTree),
                new FrameworkPropertyMetadata(typeof(MaroisExplorerTree)));

            TreeItems = new List<TreeViewItem>();
        }
        #endregion

        #region Overrides
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            foreach (string s in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Items.Add(_dummyNode);
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                TreeItems.Add(item);
            }
        }
        #endregion

        #region Private Methods
        private bool SelectedItemChangedCanExecute(RoutedPropertyChangedEventArgs<object> args)
        {
            return IsEnabled;
        }

        private void SelectedItemChangedExecuted(RoutedPropertyChangedEventArgs<object> args)
        {
            var newVal = args.NewValue as TreeViewItem;
            if (newVal != null)
            {
                var selectedPath = newVal.Tag as string;

                RaisePathChangedEvent(selectedPath ?? "");
            }
        }
        #endregion

        #region Event Handlers
        private void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == _dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Items.Add(_dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception) { }
            }
        }

        private void foldersItem_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tree = (TreeView)sender;
            TreeViewItem temp = ((TreeViewItem)tree.SelectedItem);

            if (temp == null)
                return;
            SelectedImagePath = "";
            string temp1 = "";
            string temp2 = "";
            while (true)
            {
                temp1 = temp.Header.ToString();
                if (temp1.Contains(@"\"))
                {
                    temp2 = "";
                }
                SelectedImagePath = temp1 + temp2 + SelectedImagePath;
                if (temp.Parent.GetType().Equals(typeof(TreeView)))
                {
                    break;
                }
                temp = ((TreeViewItem)temp.Parent);
                temp2 = @"\";
            }
            //show user selected path
            MessageBox.Show(SelectedImagePath);
        }
        #endregion
    }

    public class PathChangedEventArgs : RoutedEventArgs
    {
        public string NewPath { get; private set; }

        public PathChangedEventArgs(RoutedEvent routedEvent, string newPath):
            base(routedEvent)
        {
            NewPath = newPath;
        }
    }
}

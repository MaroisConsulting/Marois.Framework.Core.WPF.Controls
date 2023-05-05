using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows;

namespace Marois.Framework.Core.WPF.Controls
{
    public class MaroisTreeView : TreeView
    {
        public static readonly DependencyProperty SelectedItemExProperty = DependencyProperty.Register("SelectedItemEx", typeof(object),
             typeof(MaroisTreeView), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnSelectedItemExChanged)));

        public MaroisTreeView()
            : base()
        {
            SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(TreeViewEx_SelectedItemChanged);
        }

        public object SelectedItemEx
        {
            get { return (object)GetValue(SelectedItemExProperty); }
            set { SetValue(SelectedItemExProperty, value); }
        }

        public bool SelectItem(object item)
        {
            if ((SelectedItem != null) && (SelectedItem.Equals(item)))
                return true;

            if (!ExpandAndSelectItem(this, item))
            {
                UpdateLayout();
                return ExpandAndSelectItem(this, item);
            }

            return true;
        }

        private static void OnSelectedItemExChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MaroisTreeView treeView = d as MaroisTreeView;

            if ((object)treeView != null)
                treeView.SelectItem(e.NewValue);
        }

        private void TreeViewEx_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SetValue(SelectedItemExProperty, e.NewValue);
        }

        private bool ExpandAndSelectItem(ItemsControl parentContainer, object itemToSelect)
        {
            TreeViewItem tvi = parentContainer.ItemContainerGenerator.ContainerFromItem(itemToSelect) as TreeViewItem;

            if ((object)tvi != null)
            {
                tvi.IsSelected = true;
                tvi.BringIntoView();
                tvi.Focus();
                return true;
            }

            for (int nIndex = 0; nIndex < parentContainer.Items.Count; nIndex++)
            {
                TreeViewItem currentContainer = parentContainer.ItemContainerGenerator.ContainerFromIndex(nIndex) as TreeViewItem;

                if (((object)currentContainer != null) && (currentContainer.Items.Count > 0))
                {
                    bool bIsExpanded = currentContainer.IsExpanded;

                    if (currentContainer.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                    {
                        EventHandler handler = null;

                        handler = new EventHandler(delegate
                        {
                            if (currentContainer.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                            {
                                if (ExpandAndSelectItem(currentContainer, itemToSelect) == false)
                                    currentContainer.IsExpanded = false;

                                currentContainer.ItemContainerGenerator.StatusChanged -= handler;
                            }
                        });

                        currentContainer.ItemContainerGenerator.StatusChanged += handler;

                        if (currentContainer.IsExpanded == false)
                            currentContainer.IsExpanded = true;
                    }
                    else
                    {
                        if (ExpandAndSelectItem(currentContainer, itemToSelect) == false)
                            currentContainer.IsExpanded = bIsExpanded;
                        else
                            return true;
                    }
                }
            }

            return false;
        }
    }

}

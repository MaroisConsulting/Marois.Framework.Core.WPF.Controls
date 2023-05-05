using Marois.Frameworo.Core.Controls.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Marois.Framework.Core.WPF.Controls
{
    public class MaroisTaskList : ControlBase
    {
        #region DP's
        #region DP Tasks
        public static readonly DependencyProperty TasksProperty =
                    DependencyProperty.Register("Tasks",
                    typeof(ObservableCollection<TaskEntity>),
                    typeof(MaroisTaskList),
                    new PropertyMetadata(null, new PropertyChangedCallback(OnTasksChanged)));

        public ObservableCollection<TaskEntity> Tasks
        {
            get { return (ObservableCollection<TaskEntity>)GetValue(TasksProperty); }
            set { SetValue(TasksProperty, value); }
        }


        private static void OnTasksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MaroisTaskList)d;
        }
        #endregion

        #region DP ShowToolbar
        public static readonly DependencyProperty ShowToolbarProperty =
                    DependencyProperty.Register("ShowToolbar",
                    typeof(bool),
                    typeof(MaroisTaskList),
                    new PropertyMetadata(true, new PropertyChangedCallback(OnShowToolbarChanged)));

        public bool ShowToolbar
        {
            get { return (bool)GetValue(ShowToolbarProperty); }
            set { SetValue(ShowToolbarProperty, value); }
        }


        private static void OnShowToolbarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MaroisTaskList)d;
        }
        #endregion

        #region DP Headers
        public static readonly DependencyProperty HeadersProperty =
                    DependencyProperty.Register("Headers",
                    typeof(List<TaskListHeadersEnum>),
                    typeof(MaroisTaskList),
                    new PropertyMetadata(null, new PropertyChangedCallback(OnHeadersChanged)));

        public List<TaskListHeadersEnum> Headers
        {
            get { return (List<TaskListHeadersEnum>)GetValue(HeadersProperty); }
            set { SetValue(HeadersProperty, value); }
        }


        private static void OnHeadersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MaroisTaskList)d;
        }
        #endregion
        #endregion

        #region CTOR
        public MaroisTaskList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaroisTaskList),
                new FrameworkPropertyMetadata(typeof(MaroisTaskList)));

        }
        #endregion
    }
}

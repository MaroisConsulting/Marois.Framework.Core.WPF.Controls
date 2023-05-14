using log4net.Core;
using Marois.Framework.Core.Controls.Shared.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Marois.Framework.Core.WPF.Controls
{
    public class MaroisDropDownTree : ComboBox, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<DropdownTreeItemEntity>? _TreeData;
        public ObservableCollection<DropdownTreeItemEntity> TreeData
        {
            get { return _TreeData; }
            set
            {
                if (_TreeData != value)
                {
                    _TreeData = value;
                    RaisePropertyChanged("TreeData");
                }
            }
        }

        private DropdownTreeItemEntity? _SelectedTreeData;
        public DropdownTreeItemEntity SelectedTreeData
        {
            get { return _SelectedTreeData; }
            set
            {
                if (_SelectedTreeData != value)
                {
                    _SelectedTreeData = value;
                    RaisePropertyChanged("SelectedTreeData");
                }
            }
        }

        public MaroisDropDownTree()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaroisDropDownTree),
                new FrameworkPropertyMetadata(typeof(MaroisDropDownTree)));

            TreeData = new ObservableCollection<DropdownTreeItemEntity>
            {
                new DropdownTreeItemEntity
                {
                    Caption = "Level 1",

                    Items = new ObservableCollection<DropdownTreeItemEntity>
                    {
                        new DropdownTreeItemEntity 
                        { 
                            Caption = "Level 1A"
                        },
                        new DropdownTreeItemEntity
                        {
                            Caption = "Level 1B"
                        },
                        new DropdownTreeItemEntity
                        {
                            Caption = "Level 1C"
                        },
                    }
                },

                new DropdownTreeItemEntity
                {
                    Caption = "Level 2",

                    Items = new ObservableCollection<DropdownTreeItemEntity>
                    {
                        new DropdownTreeItemEntity
                        {
                            Caption = "Level 2A"
                        },
                        new DropdownTreeItemEntity
                        {
                            Caption = "Level 2B"
                        },
                        new DropdownTreeItemEntity
                        {
                            Caption = "Level 2C"
                        },
                    }
                }
            };
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System.ComponentModel;
using System.Windows.Controls;

namespace Marois.Framework.Core.WPF.Controls
{
    public class UserControlBase : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

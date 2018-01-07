using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace ClipRetain
{
    /// <summary>
    /// Credit: @author amaca - https://stackoverflow.com/a/662232/7019742
    /// </summary>
    class IndexConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)value;
            ListView listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
            int index = listView.ItemContainerGenerator.IndexFromContainer(item);
            return index.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

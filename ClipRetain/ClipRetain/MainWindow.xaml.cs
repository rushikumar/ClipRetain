using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClipRetain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PasteOnLoad();
        }

        public void PasteOnLoad()
        {
            IDataObject clipBoardData = Clipboard.GetDataObject();
            crCurrentContent.Content = Clipboard.GetText();
            //crCurrentContent.Document = new FlowDocument(new Paragraph(new Run(Clipboard.GetDataObject)));

            var cntr = 1;
            //var newHistoryListItem = new ListViewItem(Clipboard.GetText());
            crClipHistoryList.Items.Add(cntr.ToString() + " - " + Clipboard.GetText()); cntr++;
            crClipHistoryList.Items.Add(cntr.ToString() + " - " + Clipboard.GetText()); cntr++;
            crClipHistoryList.Items.Add(cntr.ToString() + " - " + Clipboard.GetText()); cntr++;
            crClipHistoryList.Items.Add(cntr.ToString() + " - " + Clipboard.GetText()); cntr++;
            crClipHistoryList.Items.Add(cntr.ToString() + " - " + Clipboard.GetText()); cntr++;
            crClipHistoryList.Items.Add(cntr.ToString() + " - " + Clipboard.GetText()); cntr++;

            var totalCharacters = 0;
            totalCharacters = Clipboard.GetText().Count();

            crCurrentStats.Content = "Stats: Total Characters: " + totalCharacters;

            return;
        }

        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (crClipHistoryList.SelectedIndex < 0)
            {
                MessageBox.Show("No item selected!", "Select Item First!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Clipboard.SetText(crClipHistoryList.SelectedItem.ToString());
                Notifications showNotification = new Notifications("Item copied!");
                showNotification.Show();
            }
        }

        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (crClipHistoryList.SelectedIndex < 0)
            {
                //ShowDialog("Select an item first!");
                MessageBox.Show("No item selected!", "Select Item First!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                crClipHistoryList.Items.RemoveAt(crClipHistoryList.SelectedIndex);
            }
            /*foreach(ListViewItem item in crClipHistoryList.SelectedItems)
            {
                crClipHistoryList.Items.Remove(item);
            }*/
        }

        private void Button_Remove_All_Click(object sender, RoutedEventArgs e)
        {
            crClipHistoryList.Items.Clear();
        }
    }
}

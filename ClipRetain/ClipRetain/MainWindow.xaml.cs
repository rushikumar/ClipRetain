﻿//----------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="CodingRush.com">
// Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>Rushikumar J. Bhatt</author>
// <summary>This is the main (partial) class for the ClipRetain project, which 
// embodies the core functions/features</summary>
//----------------------------------------------------------------------------------

namespace ClipRetain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB" }; // For the history list, for when we populate it
        private string loggedOnUser = string.Empty;

        public MainWindow()
        {
            this.InitializeComponent();
            
            // Set the main window's title
            this.loggedOnUser = WindowsIdentity.GetCurrent().Name.Substring(WindowsIdentity.GetCurrent().Name.IndexOf(@"\") + 1);
            ClipRetainMainWindow.Title = "ClipRetain - Welcome " + this.loggedOnUser;

            /// This works too
            /// Console.WriteLine(Environment.UserName);
            /// Console.WriteLine(Environment.MachineName);
            /// 
            this.Loaded += new RoutedEventHandler(this.Window_Loaded);

            this.PasteOnLoad();
        }

        public void PasteOnLoad()
        {
            /// if clipboard is empty, we simply return...
            if (Clipboard.ContainsData(DataFormats.Text) == false) return;

            IDataObject clipBoardData = Clipboard.GetDataObject();
            crCurrentContent.Content = Clipboard.GetText(); // set the label to what's currently in the clipboard

            var cntr = 1;

            ClipboardRecords currentClipboardItem = new ClipboardRecords();
            currentClipboardItem.Counter = cntr;
            currentClipboardItem.Content = Clipboard.GetText();
            currentClipboardItem.Size = SizeSuffix(GetSize(Clipboard.GetText()));
            crClipHistoryList.Items.Add(currentClipboardItem);

            cntr++;

            string[] seedData = { "A string of Data", "Another string of data", "Why not? lets have another string", "A string of Data", "Another string of data", "Why not? lets have another string", "A string of Data", "Another string of data", "Why not? lets have another string", "A string of Data", "Another string of data", "Why not? lets have another string" };
            foreach (string str in seedData)
            {
                ClipboardRecords newRecord = new ClipboardRecords();
                newRecord.Counter = cntr;
                newRecord.Content = str;
                newRecord.Size = SizeSuffix(GetSize(str));
                crClipHistoryList.Items.Add(newRecord);
                cntr++;
            }

            var totalCharacters = 0;
            totalCharacters = Clipboard.GetText().Count();

            crCurrentStats.Content = "Stats: Total Characters: " + totalCharacters;

            return;
        }

        /// <summary>
        /// Calculate the size of the string that is passed and return it as an int
        /// </summary>
        /// <param name="value">Method accepts a string</param>
        /// <returns>returns (numerical) size</returns>
        private static int GetSize(string value)
        {
            // int contentSize = System.Text.ASCIIEncoding.Unicode.GetByteCount(Clipboard.GetText()); // This gives double the size of "https://stackoverflow.com/a/14488941/7019742" 88 bytes
            // var contentSize = Clipboard.GetText().Length * sizeof(Char); // This too gives double the size of "https://stackoverflow.com/a/14488941/7019742" - 88 bytes
            // int contentSize = System.Text.ASCIIEncoding.ASCII.GetByteCount(Clipboard.GetText()); // This gives the true size of "https://stackoverflow.com/a/14488941/7019742" - 44 bytes
            return System.Text.ASCIIEncoding.ASCII.GetByteCount(value);
        }

        /// <summary>
        /// Depending on the value sent, we will return value with the proper suffix (bytes, kb, etc.)
        /// Adapted from an answer by JLRishe on StackOverflow @ https://stackoverflow.com/a/14488941/7019742
        /// Assumption: value will always be >= 0
        /// </summary>
        /// <param name="value">Numerical value to be calculated</param>
        /// <param name="decimalPlaces">Number of decimal places (optional, as default is 1)</param>
        /// <returns>returns a formatted string (#.# suffix) </returns>
        private static string SizeSuffix(long value, int decimalPlaces = 1)
        {
            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", adjustedSize, SizeSuffixes[mag]);
        }

        /// <summary>
        /// Method that copies item from the history list to the clipboard 
        /// (if anything is in the clipboard, it will be removed and be replaced with the selected item)
        /// </summary>
        private void Button_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (crClipHistoryList.SelectedIndex < 0)
            {
                MessageBox.Show("No item selected!", "Select Item First!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                /// string currentlySelected = crClipHistoryList.SelectedItem.ToString();
                /// Dictionary<string,string> keyValuePairs = currentlySelected.Split("=")

                /// The following works too!
                /// var selectedClipboardRecord = crClipHistoryList.SelectedItems[0] as ClipboardRecords;
                /// Clipboard.SetText(selectedClipboardRecord.Content);

                /// The following works too!
                /// ClipboardRecords selectedHistoryRecord = (ClipboardRecords)crClipHistoryList.SelectedItems[0];
                /// Clipboard.SetText(selectedHistoryRecord.Content);

                /// The following works too!
                ClipboardRecords selectedHistoryRecord = crClipHistoryList.SelectedItem as ClipboardRecords;
                Clipboard.SetText(selectedHistoryRecord.Content);
                
                Notifications showNotification = new Notifications("Item copied!");
                showNotification.Show();
            }
        }

        /// <summary>
        /// Method that copies the selected item from the history list and replaces it with what is in the clipboard
        /// if clipboard is empty, it merely copies the item to clipboard
        /// </summary>
        private void Button_Copy_And_Replace_Click(object sender, RoutedEventArgs e)
        {
            /// Get the current data from clipboard
            ClipboardRecords selectedHistoryRecord = crClipHistoryList.SelectedItem as ClipboardRecords;
            string currentlySelected = selectedHistoryRecord.Content;
            
            /// setting the selected item's content to what is in the clipboard
            selectedHistoryRecord.Content = Clipboard.GetText();

            /// lets copy the selected item to the clipboard
            Clipboard.SetText(currentlySelected);

            /// let's refresh our listview control...
            crClipHistoryList.Items.Refresh();
        }

        /// <summary>
        /// Method for swapping two items in the history list
        /// ToDo! This is an incomplete implementation... Might not be part of v1.0
        /// </summary>
        private void Button_Swap_Items_Click(object sender, RoutedEventArgs e)
        {
            if (crClipHistoryList.SelectedIndex < 0)
            {
                MessageBox.Show("No item selected!", "Select Item First!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (crClipHistoryList.SelectedItems.Count == 1)
            {
                MessageBox.Show("Please select one more item with which you would like to swap", "Select One More...", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (crClipHistoryList.SelectedItems.Count > 2)
            {
                MessageBox.Show("You can only select two items", "Invalid Selection!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                /// List<int> myIndexes = new List<int>();
                /// var selected = crClipHistoryList.SelectedItems.Cast<ClipboardRecords>().ToArray();
                string itemOne = "";
                string itemTwo = "";
                var firstSelectedClipboardRecord = crClipHistoryList.SelectedItems[0] as ClipboardRecords;
                itemOne = firstSelectedClipboardRecord.Content;

                var secondSelectedClipboardRecord = crClipHistoryList.SelectedItems[1] as ClipboardRecords;
                itemTwo = secondSelectedClipboardRecord.Content;

                Console.WriteLine("Two Items:");
                Console.WriteLine(itemOne);
                Console.WriteLine(itemTwo);

                /*foreach (ClipboardRecords swappableRecords in selected)
                {
                    swappableRecords.Content = itemTwo;
                }*/

                crClipHistoryList.Items.Refresh();
            }
        }

        /// <summary>
        /// Method that handles the removal of selected item(s)
        /// </summary>
        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (crClipHistoryList.SelectedIndex < 0)
            {
                MessageBox.Show("No item selected!", "Select Item First!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var selected = crClipHistoryList.SelectedItems.Cast<object>().ToArray();
                foreach (var item in selected)
                {
                    crClipHistoryList.Items.Remove(item);
                }
            }
        }

        /// <summary>
        /// Method that clears out the history list (removes from view as well as from the database) 
        /// </summary>
        private void Button_Remove_All_Click(object sender, RoutedEventArgs e)
        {
            // if we have nothing in the history list, we need to let the user know
            if (crClipHistoryList.Items.Count < 1)
            {
                MessageBox.Show("Your clipboard history is empty!", "Clipboard History Empty!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                crClipHistoryList.Items.Clear();
            }
        }

        /// <summary>
        /// Open the main window on bottom right of the screen
        /// This endeavor is part of a future plan of showing the ClipRetain only as an icon
        /// in the lower right area of the screen; upon clicking or hovering, we will do other fancy things...
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var getWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = getWorkingArea.Right - this.Width;
            this.Top = getWorkingArea.Bottom - this.Height;
        }
    }
}

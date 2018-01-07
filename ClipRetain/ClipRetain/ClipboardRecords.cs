//----------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="CodingRush.com">
// Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author>Rushikumar J. Bhatt</author>
// <summary>This is the main (partial) class for the ClipRetain project, which 
// embodies the core functions/features</summary>
//----------------------------------------------------------------------------------

namespace ClipRetain
{
    internal class ClipboardRecords
    {
        public int Counter { get; internal set; }
        public string Content { get; internal set; }
        public string Size { get; internal set; }
    }
}
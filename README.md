# ClipRetain
## Description
Clipboard (copy history) retainer/manager

## Motivation
I haven't touched C# in a LONG time; the last major (i.e. publicly released) C# project I worked on was eons ago, on a project called "[RoboDown](https://sourceforge.net/projects/rapiddown/)", in 2009. I have been itching to get into C# again and what better way than to start with a WPF application?
 
Right off bat, there were two goals to achieve by creating this project:
 1. Get practical experience in C#
 2. Create something meaningful, while at it

## Initial work (v1.0 goals)
1. Text only operations/abilities for this tool
2. Save the history list in some database (SQLite, most likely)
3. Add some sort of a "Toast" like notification --- instead of the boring "alert" like (i.e. `MessageBox.Show("...")`) dialog boxes!

## ToDo (in no particular order)
1. Capability of swapping contents
   - i.e. item #4 with #8 etc., in the ClipRetain history list

~~2. Add notifications functionality so that we can notify users on certain events (copy/remove/etc.)~~

3. Removal of multiple items from the ClipRetain history list
4. Monitor clipboard; on new copied item, add to ClipRetain history
5. Export to popular places such as DropBox etc.
6. Allow the setting of `max` records (5, 10, 15, 25, 50, 75, 100, unlimited)
7. Saving of ClipRetain history
   - SQLite db?
   - Option for encrypting the entire db
8. Stash ClipRetain into the tray
9. Auto-start option
10. Turn it into a service? (might have to, if copied item cannot be added whilst ClipRetain is minimized/in tray)
11. Come up with, and subsequently add, an icon for this app!
12. Remove seed/dummy data and populate with actual data from the saved history list
13. In the listview, add the counter column
14. Add settings / configuration window
15. Add copy and swap button which:
    - will copy the item from the history list and (either add to the end of history list? or replace the item, with what's in the clipboard, in history list?... hmmmm... think think!)
16. Splash screen? (or is that too much... hmmmm... think think think!)
17. When you double click one of the items, it opens a window showing full contents
18. Add three columns to the history list view:
    - `#`
    - `Content`
    - `Size` (bytes / kb / etc.)
19. `Save to disk` option...


## Credits / Attributions
- [StackOverflow](https://stackoverflow.com)
- [Google](https://google.com)
- [Imagination](http://rushikumar.com)
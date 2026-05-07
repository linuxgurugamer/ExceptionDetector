Exception Detector Enhanced
===========================

This add-on creates a log file by hooking into the logger callbacks in Unity and is able to extract more value from the available information. Assists in finding issues that you might not even be aware of and might be able to find any pesky add-on(s) that might be causing your issues. For Kerbal Space Program.

What does this mod do? This mod (currently) creates a log file at Logs/ExceptionDetectorUpdated/edu.log which you will (probably) find more helpful than the other logs available to you.

The mod will make use of the ClickThroughBlocker if it is installed.
The mod will make use of the ToolbarController if it is installed.

This mod hooks into the logger callbacks in Unity, and is able to extract a little more value from the available information.

For modders: Find what issues you may be causing, perhaps more quickly

For non-modders: You might be able to find that one pesky mod that is causing you problems.

Usage
=====

Window is shown at game startup.  Once closed, it can be opened by clicking the toolbar button.
Toolbar button 

Available Toggles
	Word Wrap			If enabled, log lines will wrap in the window
	Bold				If enabled, log lines will be bold text
	Use Whitelist		use the Whitelist if enabled
	use Alwayslist		Use the Alwayslist if enabled
	Use Alt Skin		Uses an alternate skin if enabled

Available Button
	Edit lists			Opens a window where the whitelist and alwayslist can be edited

Resizable Window
	The window is resizable, using the right edge, bottom right corner or bottom edge


The whitelist is used to ignore various error messages. The whitelist is stored in the settings.cfg
the alwaysList is used to specify strings which should always be found, also stored in the settings.cfg


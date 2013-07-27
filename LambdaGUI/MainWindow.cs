using System;
using Gtk;
using Mono.TextEditor;


public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		TextEditor text = new TextEditor ();
		this.Add (text);

		int lineNumber = 1;
		var segment = new TextSegment (0, 2);
		var marker = new TextSegmentMarker (segment);
		text.Document.AddMarker (marker);
		text.QueueDraw();
		
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}

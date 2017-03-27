using System;
using System.Collections.Generic;


namespace PurpleRadiant
{
	/// <summary>
	/// A window widget
	/// </summary>
	/// <description>
	/// A window similar to the GUI windows.
	/// It is a canvas widget.
	/// </description>
	public class Window : IWidgetCanvas
	{
		int width;
		int height;
		string title;
		bool focused;
		List<WidgetContainer> liw;
		LinkedList<WidgetContainer> focus;
		LinkedListNode<WidgetContainer> currentFocus;
		CloseButton cb;

		public Window (int W, int H, string Title)
		{
			width = W;
			height = H;
			title = Title;
			cb = new CloseButton ();
			liw = new List<WidgetContainer>();
			WidgetContainer wan = new WidgetContainer ();
			wan.iw = cb;
			wan.x = width;
			wan.y = 1;
			liw.Add (wan);
			focus = new LinkedList<WidgetContainer> ();
			focus.AddFirst (wan);
			currentFocus = focus.First;
			currentFocus.Value.iw.Focused ();
		}

		public void AddWidget(IWidget iw, int x, int y)
		{
			WidgetContainer wc = new WidgetContainer ();
			wc.iw = iw;
			wc.x = x+1;
			wc.y = y+3;
			liw.Add (wc);
			focus.AddBefore (focus.Last, wc);
		}

		public void RemoveWidget(IWidget iw)
		{
			liw.Find ((WidgetContainer obj) => (obj.iw == iw));
		}

		public Tuple<int, int> GetSize ()
		{
			return new Tuple<int, int> (width + 2, height + 4);
		}

		#region IWidget implementation

		public TextBuf DrawTextBuf ()
		{
			TextBuf tb;
			lock (this)
			{
				tb = new TextBuf (width + 2, height + 4);
				tb.FG = DrawColors.edit_text;
				tb.BG = DrawColors.wind_bg;
				int i, j;

				tb.Append (BoxGraphics.DoubleULCorner);
				for (i = 0; i < width; i++)
					tb.Append (BoxGraphics.DoubleHorizLine);
				tb.Append (BoxGraphics.DoubleURCorner);
				if (focused)
					tb[1, 0] = new Tuple<char, ConsoleColor, ConsoleColor> (BoxGraphics.Square, ConsoleColor.Green, DrawColors.wind_bg);
				tb.Append (BoxGraphics.DoubleVertLine);
				if (title.Length > width)
				{
					for (i = 0; i < width - 3; i++)
						tb.Append (title[i]);
					tb.Append ('.');
					tb.Append ('.');
					tb.Append ('.');
				}
				else
				{
					for (i = 0; i < title.Length; i++)
						tb.Append (title[i]);
					for (i = 0; i < width - title.Length; i++)
						tb.Append (' ');
				}
				tb.Append (BoxGraphics.DoubleVertLine);
				tb.Append (BoxGraphics.DoubleTLeft);
				for (i = 0; i < width; i++)
					tb.Append (BoxGraphics.DoubleHorizLine);
				tb.Append (BoxGraphics.DoubleTRight);
				for (i = 0; i < height; i++)
				{
					tb.Append (BoxGraphics.DoubleVertLine);
					for (j = 0; j < width; j++)
					{
						tb.Append (' ');
					}
					tb.Append (BoxGraphics.DoubleVertLine);
				}
				tb.Append (BoxGraphics.DoubleLLCorner);
				for (i = 0; i < width; i++)
					tb.Append (BoxGraphics.DoubleHorizLine);
				tb.Append (BoxGraphics.DoubleLRCorner);
			}


			foreach (WidgetContainer wc in liw)
			{
				wc.iw.DrawTextBuf ().DrawOver (tb, wc.x, wc.y);
			}
			return tb;
		}

		public void InputKey (ConsoleKeyInfo cki)
		{
			if (cki.Key == ConsoleKey.Tab)
			{
				if (cki.Modifiers == ConsoleModifiers.Shift)
				{
					if (currentFocus.Previous == null)
					{
						currentFocus.Value.iw.UnFocused ();
						currentFocus = focus.Last;
						currentFocus.Value.iw.Focused ();
					}
					else
					{
						currentFocus.Value.iw.UnFocused ();
						currentFocus = currentFocus.Previous;
						currentFocus.Value.iw.Focused ();
					}
				}
				else
				{
					if (currentFocus.Next == null)
					{
						currentFocus.Value.iw.UnFocused ();
						currentFocus = focus.First;
						currentFocus.Value.iw.Focused ();
					}
					else
					{
						currentFocus.Value.iw.UnFocused ();
						currentFocus = currentFocus.Next;
						currentFocus.Value.iw.Focused ();
					}
				}
				return;
			}
			if ((cki.Key == ConsoleKey.F4) && (cki.Modifiers == ConsoleModifiers.Alt))
				;
			if (currentFocus.Value.iw == this)
				return;
			currentFocus.Value.iw.InputKey (cki);
		}


		public void Focused ()
		{
			focused = true;
		}

		public void UnFocused ()
		{
			focused = false;
		}
		#endregion
	}

	class CloseButton : IWidget
	{
		bool focused;
		#region IWidget implementation
		public TextBuf DrawTextBuf ()
		{
			lock (this)
			{
				TextBuf tb = new TextBuf (1, 1);
				if (focused)
					tb[0, 0] = new Tuple<char, ConsoleColor, ConsoleColor> ('X', ConsoleColor.Green, ConsoleColor.Blue);
				else
					tb[0, 0] = new Tuple<char, ConsoleColor, ConsoleColor> ('X', ConsoleColor.Red, ConsoleColor.Black);
				return tb;
			}
		}
		public void InputKey (ConsoleKeyInfo cki)
		{
			if (cki.Key == ConsoleKey.Enter)
				Environment.Exit (0);
		}

		public void Focused ()
		{
			focused = true;
		}

		public void UnFocused ()
		{
			focused = false;
		}

		public Tuple<int, int> GetSize ()
		{
			return new Tuple<int, int> (1, 1);
		}
		#endregion
		
	}
}


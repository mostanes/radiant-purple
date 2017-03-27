using System;
using System.Collections.Generic;

namespace PurpleRadiant
{
	public class Screen : IWidgetCanvas
	{
		List<WidgetContainer> liw;
		LinkedList<WidgetContainer> focus;
		LinkedListNode<WidgetContainer> currentFocus;

		public Screen ()
		{
			liw = new List<WidgetContainer>();
			focus = new LinkedList<WidgetContainer> ();
		}

		public void AddWidget(IWidget iw, int x, int y)
		{
			WidgetContainer wc = new WidgetContainer ();
			wc.iw = iw;
			wc.x = x;
			wc.y = y;
			liw.Add (wc);
			focus.AddLast (wc);
			iw.UnFocused ();
			if (currentFocus == null)
				currentFocus = focus.First;
			currentFocus.Value.iw.Focused ();
		}

		public void RemoveWidget(IWidget iw)
		{
			liw.Find ((WidgetContainer obj) => (obj.iw == iw));
		}

		public Tuple<int, int> GetSize ()
		{
			return new Tuple<int, int> (Console.WindowWidth, Console.WindowHeight);
		}

		#region IWidget implementation

		public TextBuf DrawTextBuf ()
		{
			TextBuf tb;
			lock (this)
			{
				tb = new TextBuf (Console.WindowWidth, Console.WindowHeight);
				tb.FG = DrawColors.edit_text;
				tb.BG = DrawColors.scr_bg;
				foreach (WidgetContainer wc in liw)
				{
					wc.iw.DrawTextBuf ().DrawOver (tb, wc.x, wc.y);
				}
			}
			return tb;
		}

		public void InputKey (ConsoleKeyInfo cki)
		{

			if (focus.First == null)
				return;
			if (cki.Modifiers == ConsoleModifiers.Control)
			{
				if (cki.Key == ConsoleKey.Tab)
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
					return;
				}
				WidgetContainer wan = currentFocus.Value;
				if (cki.Key == ConsoleKey.UpArrow)
				{
					if (wan.y > 0)
						wan.y--;
				}
				if (cki.Key == ConsoleKey.DownArrow)
				{
					if (wan.y < Console.WindowHeight - wan.iw.GetSize().Item2)
						wan.y++;
				}
				if (cki.Key == ConsoleKey.LeftArrow)
				{
					if (wan.x > 0)
						wan.x--;
				}
				if (cki.Key == ConsoleKey.RightArrow)
				{
					if (wan.x < Console.WindowWidth - wan.iw.GetSize().Item1)
						wan.x++;
				}
				currentFocus.Value = wan;
			}
			if (currentFocus.Value.iw == this)
				return;
			currentFocus.Value.iw.InputKey (cki);
		}


		public void Focused ()
		{
			return;
		}

		public void UnFocused ()
		{
			return;
		}

		#endregion
	}
}


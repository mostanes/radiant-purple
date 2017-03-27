using System;

namespace PurpleRadiant
{
	public class Button : IWidget
	{
		PressedReturn OnEnter;
		string text;
		int width;
		bool focused;
		bool active;

		bool Active
		{
			get
			{
				lock (this)
				{
					return active;
				}
			}
			set
			{
				lock (this)
				{
					active = value;
				}
			}
		}

		public Button (string label, int width, PressedReturn ret)
		{
			if (width < 5)
				throw new ArgumentOutOfRangeException ("width", "Button width must be larger than 4.");
			if (label.Length + 4 > width)
				text = label.Substring (width - 4);
			else
			{
				int w = width - 4 - label.Length;
				text = new string (' ', w / 2) + label + new string (' ', (w + 1) / 2);
			}
			this.width = width;
			OnEnter = ret;
			active = true;
		}

		#region IWidget implementation

		public TextBuf DrawTextBuf ()
		{
			TextBuf tb = new TextBuf (width, 1);
			if (!active)
			{
				tb.BG = DrawColors.button_inactive_bg;
				tb.FG = DrawColors.button_inactive_fg;
			}
			else
			{
				if (focused)
				{
					tb.BG = DrawColors.button_selected_bg;
					tb.FG = DrawColors.button_selected_fg;
				}
				else
				{
					tb.BG = DrawColors.button_simple_bg;
					tb.FG = DrawColors.button_simple_fg;
				}
			}
			if (focused)
				tb.Append ('>');
			else
				tb.Append (' ');
			tb.Append (' ');
			foreach (char c in text)
				tb.Append (c);
			tb.Append (' ');
			if (focused)
				tb.Append ('<');
			else
				tb.Append (' ');
			return tb;
		}

		public void InputKey (ConsoleKeyInfo cki)
		{
			if (cki.Key == ConsoleKey.Enter)
				OnEnter ();
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
			return new Tuple<int, int> (width, 1);
		}

		#endregion
	}
}


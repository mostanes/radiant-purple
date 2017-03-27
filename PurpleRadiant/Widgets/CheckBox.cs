using System;

namespace PurpleRadiant
{
	public class CheckBox : IWidget
	{
		bool selected;
		bool focused;
		bool active;
		string text;

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

		public CheckBox (string text)
		{
			this.text = text;
			active = true;
		}

		#region IWidget implementation

		public TextBuf DrawTextBuf ()
		{
			TextBuf tb = new TextBuf (text.Length + 4, 1);
			if (active)
			{
				if (focused)
				{
					tb.BG = DrawColors.select_bg;
					tb.FG = DrawColors.select_text;
				}
				else
				{
					tb.BG = DrawColors.wind_bg;
					tb.FG = DrawColors.edit_text;
				}
			}
			else
			{
				tb.BG = DrawColors.wind_bg;
				tb.FG = DrawColors.inactive_text;
			}
			tb.Append ('[');
			if (selected)
				tb.Append ('X');
			else
				tb.Append (' ');
			tb.Append (']');
			if (active)
			{
				tb.BG = DrawColors.wind_bg;
				tb.FG = DrawColors.edit_text;
			}
			else
			{
				tb.BG = DrawColors.wind_bg;
				tb.FG = DrawColors.inactive_text;
			}
			tb.Append (' ');
			foreach (char c in text)
				tb.Append (c);
			return tb;
		}

		public void InputKey (ConsoleKeyInfo cki)
		{
			if (cki.Key == ConsoleKey.Spacebar)
				selected = !selected;
			if (cki.Key == ConsoleKey.Enter)
				selected = !selected;
			if (cki.Key == ConsoleKey.Add)
				selected = true;
			if (cki.Key == ConsoleKey.Subtract)
				selected = false;
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
			return new Tuple<int, int> (text.Length + 4, 1);
		}

		#endregion
	}
}


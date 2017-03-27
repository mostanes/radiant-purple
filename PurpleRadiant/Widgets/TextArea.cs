using System;
using System.Collections.Generic;
using System.Text;

namespace PurpleRadiant
{
	public class TextArea : IWidget
	{
		IList<string> lines = new List<string>();
		int pc_x, pc_y;
		int width, height;
		StringBuilder linesb;
		bool active;
		bool focused;
		int view0_x, view0_y;

		public TextArea (int Width, int Height)
		{
			width = Width;
			height = Height;
			active = true;
			pc_x = 0;
			pc_y = 0;
			view0_x = 0;
			view0_y = 0;
			lines = new List<string> ();
			lines.Add ("");
			linesb = new StringBuilder ();
		}

		#region IWidget implementation

		public TextBuf DrawTextBuf ()
		{
			TextBuf tb = new TextBuf (width, height);
			if (active)
				tb.BG = DrawColors.edit_bg;
			else
				tb.BG = DrawColors.wind_bg;
			tb.FG = DrawColors.edit_text;
			lines[pc_y] = linesb.ToString ();
			int i,j;
			string s;
			for (i = view0_y; i < view0_y + height; i++)
			{
				if (i >= lines.Count)
				{
					for (j = 0; j < width; j++)
						tb.Append (' ');
					continue;
				}
				if (view0_x >= lines[i].Length)
				{
					for (j = 0; j < width; j++)
						tb.Append (' ');
					continue;
				}
				if (view0_x + width >= lines[i].Length)
				{
					s = lines[i].Substring (view0_x);
					s += new string (' ', width - s.Length);
				}
				else
					s = lines[i].Substring (view0_x, width);
				foreach (char c in s)
					tb.Append (c);
			}
			if (active)
			{
				var van = tb[pc_x-view0_x, pc_y-view0_y];
				tb[pc_x - view0_x, pc_y - view0_y] = new Tuple<char, ConsoleColor, ConsoleColor> (van.Item1, ConsoleColor.Black, ConsoleColor.White);
			}
			return tb;
		}

		public void InputKey (ConsoleKeyInfo cki)
		{
			if (cki.Key == ConsoleKey.LeftArrow)
			{
				if (pc_x > 0)
				{
					pc_x--;
					if (pc_x < view0_x)
						view0_x = pc_x;
				}
				else
				{
					if (pc_y > 0)
					{
						linesb.Clear ();
						pc_y--;
						if (pc_y < view0_y)
							view0_y = pc_y;
						linesb.Append (lines[pc_y]);
					}
					pc_x = linesb.Length;
					if (pc_x > view0_x + width - 1)
						view0_x = pc_x - width + 1;
				}
				return;
			}
			if (cki.Key == ConsoleKey.RightArrow)
			{
				if (pc_x < linesb.Length - 1)
				{
					pc_x++;
					if (pc_x > view0_x + width - 1)
						view0_x = pc_x - width + 1;
				}
				else
				{
					pc_x = 0;
					view0_x = 0;
					lines[pc_y] = linesb.ToString ();
					linesb.Clear ();
					if (pc_y < lines.Count - 1)
					{
						pc_y++;
						if (pc_y > view0_y + height - 1)
							view0_y = pc_y - height + 1;
						linesb.Clear ();
						linesb.Append (lines[pc_y]);
					}
					linesb.Append (lines[pc_y]);
				}
				return;
			}
			if (cki.Key == ConsoleKey.UpArrow)
			{
				if (pc_y > 0)
				{
					linesb.Clear ();
					pc_y--;
					if (pc_y < view0_y)
						view0_y = pc_y;
					linesb.Append (lines[pc_y]);
				}
				return;
			}
			if (cki.Key == ConsoleKey.DownArrow)
			{
				if (pc_y < lines.Count - 1)
				{
					pc_y++;
					if (pc_y > view0_y + height - 1)
						view0_y = pc_y - height + 1;
					linesb.Clear ();
					linesb.Append (lines[pc_y]);
				}
				return;
			}
			if (cki.Key == ConsoleKey.Delete)
			{
				if (pc_x < linesb.Length)
					linesb.Remove (pc_x, 1);
				else
				{
					linesb.Append (lines[pc_y + 1]);
					lines.RemoveAt (pc_y + 1);
				}
				return;
			}
			if (cki.Key == ConsoleKey.Backspace)
			{
				if (pc_x > 0)
				{
					pc_x--;
					if (pc_x < view0_x)
						view0_x = pc_x;
					linesb.Remove (pc_x, 1);
				}
				else
				{
					if (pc_y > 0)
					{
						pc_y--;
						if (pc_y < view0_y)
							view0_y = pc_y;
						linesb.Clear ();
						linesb.Append (lines[pc_y]);
						lines.RemoveAt (pc_y + 1);
						pc_x = linesb.Length;
						if (pc_x < view0_x)
							view0_x = pc_x;
						if (pc_x > view0_x + width - 1)
							view0_x = pc_x - width + 1;
					}
				}
				return;
			}
			if (cki.Key == ConsoleKey.Enter)
			{
				linesb = new StringBuilder ();
				lines.Add (linesb.ToString ());
				pc_x = 0;
				view0_x = 0;
				pc_y++;
				if (pc_y > view0_y + height - 1)
					view0_y = pc_y - height + 1;
				return;
			}
			if (cki.KeyChar == '\u0000')
				return;
			linesb.Insert (pc_x, cki.KeyChar);
			pc_x++;
			if (pc_x > view0_x + width - 1)
				view0_x = pc_x - width + 1;
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
			return new Tuple<int, int> (width, height);
		}

		#endregion
	}
}


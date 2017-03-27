using System;
using System.Collections.Generic;

namespace PurpleRadiant
{
	public class ListBox : IWidget
	{
		int width;
		int height;
		public List<string> elm;
		int vst;
		InputBoxMini searchBox;
		bool searching;
		bool focused;
		bool active;
		PressedReturn OnEnter;

		public int SelectedElement
		{
			get;
			set;
		}

		public ListBox (int width, int height, PressedReturn ret)
		{
			this.width = width;
			this.height = height;
			elm = new List<string> ();
			active = true;
			SelectedElement = 0;
			vst = 0;
			OnEnter = ret;
		}

		#region IWidget implementation

		public Tuple<int, int> GetSize ()
		{
			return new Tuple<int, int> (width+2, height+2);
		}

		public TextBuf DrawTextBuf ()
		{
			TextBuf tb = new TextBuf (width+2, height+2);
			int i, j;
			if (active)
				tb.FG = DrawColors.active_fence;
			else
				tb.FG = DrawColors.inactive_fence;
			tb.BG = DrawColors.wind_bg;
			tb.Append (BoxGraphics.ULCorner);
			for (i = 0; i < width; i++)
				tb.Append (BoxGraphics.HorizLine);
			tb.Append (BoxGraphics.URCorner);
			tb.BG = DrawColors.edit_bg;
			tb.FG = DrawColors.edit_text;
			for (i = 0; i < elm.Count; i++)
			{
				if (i == SelectedElement)
				{
					tb.BG = DrawColors.select_bg;
					tb.FG = DrawColors.select_ar;
					tb.Append ('>');
					tb.FG = DrawColors.select_text;
				}
				else
					tb.Append (' ');
				if (elm[i].Length > width)
				{
					for (j = 0; j < width - 3; j++)
						tb.Append (elm[i][j]);
					tb.Append ('.');
					tb.Append ('.');
					tb.Append ('.');
				}
				else
				{
					for (j = 0; j < elm[i].Length; j++)
						tb.Append (elm[i][j]);
					for (j = 0; j < width - elm[i].Length; j++)
						tb.Append (' ');
				}
				if (i == SelectedElement)
				{
					tb.FG = DrawColors.select_ar;
					tb.Append ('<');
					tb.BG = DrawColors.edit_bg;
					tb.FG = DrawColors.edit_text;
				}
				else
					tb.Append (' ');
			}
			for (; i < height; i++)
				for (j = 0; j < width + 2; j++)
					tb.Append (' ');
			if (active)
				tb.FG = DrawColors.active_fence;
			else
				tb.FG = DrawColors.inactive_fence;
			tb.BG = DrawColors.wind_bg;
			tb.Append (BoxGraphics.LLCorner);
			for (i = 0; i < width; i++)
				tb.Append (BoxGraphics.HorizLine);
			tb.Append (BoxGraphics.LRCorner);
			if (focused)
			{
				tb[1, 0] = new Tuple<char, ConsoleColor, ConsoleColor> (BoxGraphics.Square, ConsoleColor.Green, DrawColors.wind_bg);
			}
			if (searching)
			{
				searchBox.DrawTextBuf ().DrawOver (tb, 0, height);
			}
			return tb;
		}

		public void InputKey (ConsoleKeyInfo cki)
		{
			if (elm.Count == 0)
				return;
			if (cki.Key == ConsoleKey.UpArrow)
			{
				if (SelectedElement > 0)
					SelectedElement--;
			}
			if (cki.Key == ConsoleKey.DownArrow)
			{
				SelectedElement++;
				if (SelectedElement >= elm.Count)
					SelectedElement--;
			}
			if (cki.Key == ConsoleKey.Enter)
				OnEnter ();
			if (cki.KeyChar == '\u0000')
				return;
			if (searching)
			{
				searchBox.InputKey (cki);
				int kap;
				kap = elm.FindIndex (delegate(string obj)
				{
					if (obj.Contains (searchBox.Text))
						return true;
					else
						return false;
				});
				SelectedElement = kap;
			}
			else
			{
				searching = true;
				searchBox = new InputBoxMini (width + 2, new PressedReturn(delegate(){searching = false;}));  /*TODO: Implement search */
				searchBox.InputKey (cki);
				int kap;
				kap = elm.FindIndex (delegate(string obj)
				{
					if (obj.Contains (searchBox.Text))
						return true;
					else
						return false;
				});
				SelectedElement = kap;
			}
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
}


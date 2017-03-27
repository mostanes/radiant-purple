using System;
using System.Text;

namespace PurpleRadiant
{
	public delegate void PressedReturn();

	[Obsolete]
	public class InputBoxMultiLine : IWidget
	{
		//string input;
		StringBuilder inp;
		int length;
		int width, height;
		int pos_c;
		int pc_x, pc_y, lines;
		int v_x, v_y;
		bool focused;
		bool active;
		PressedReturn returnMethod;

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

		public InputBoxMultiLine (int width, int height, PressedReturn returns)
		{
			this.width = width;
			this.height = height;
			returnMethod = returns;
			pos_c = 0;
			inp = new StringBuilder ();
			active = true;
			v_x = 0;
			v_y = 0;
			pc_x = 0;
			pc_y = 0;
		}

		public void inputKey(ConsoleKeyInfo k)
		{
			if (k.Key == ConsoleKey.LeftArrow)
			{
				if (pos_c > 0)
				{
					pos_c--;
					pc_x--;
				}
				return;
			}
			if (k.Key == ConsoleKey.RightArrow)
			{
				if (pos_c < inp.Length)
				{
					pos_c++;
					pc_x++;
				}
				return;
			}
			if (k.Key == ConsoleKey.UpArrow)
			{
				int i, j;
				for (i = pos_c; i > 0; i++)
				{
					if (inp[i] == '\n')
						break;
				}
				for (; i > 0; i++)
				{
					if (inp[i] == '\n')
						break;
				}
				for (j=0; j < pc_x; j++)
				{
					if (inp[i] == '\n')
						break;
					i++;
				}
				pos_c = i;
				if (pc_y > 0)
					pc_y--;
				pc_x = j;
			}
			if (k.Key == ConsoleKey.DownArrow)
			{
				int i, j;
				for (i = pos_c; i < inp.Length; i++)
				{
					if (inp[i] == '\n')
						break;
				}
				for (j=0; j < pc_x; j++)
				{
					if (inp[i] == '\n')
						break;
					i++;
				}
				pos_c = i;
				if (pc_y < lines)
					pc_y++;
				pc_x = j;
			}
			if (k.Key == ConsoleKey.Delete)
			{
				if (inp.Length == 0)
					return;
				if (pos_c == inp.Length)
					return;
				inp.Remove (pos_c, 1);
				return;
			}
			if (k.Key == ConsoleKey.Backspace)
			{
				if (inp.Length == 0)
					return;
				if (pos_c < 1)
					return;
				pos_c--;
				if (pc_x == 0)
					pc_y--;
				inp.Remove (pos_c, 1);
				return;
			}
			if (k.Key == ConsoleKey.Enter)
			{
				//returnMethod.Invoke ();
				//return;
				inp.Insert(pos_c, '\n');
				pos_c++;
				pc_x = 0;
				pc_y++;
				lines++;
				return;
			}
			if (k.KeyChar == '\u0000')
				return;
			inp.Insert (pos_c, k.KeyChar);
			pos_c++;
			pc_x++;
		}

		public string PrintInput()
		{
			StringBuilder sb = new StringBuilder ((length + 3) * 3);
			int i, k;
			for (i = 0; i < length+2; i++)
				sb.Append (BoxGraphics.HorizLine);
			sb.Append ("\n");
			sb.Append ('|');
			k = sb.Length;
			if (inp.Length > length)
			{
				for (i = 0; i < length - 3; i++)
				{
					sb[k] = inp[i];
					k++;
				}
				
				sb.Append ("...");
			}
			else
			{
				sb.Append (inp.ToString());
				for (i = 0; i < length - inp.Length; i++)
					sb.Append (' ');
			}
			sb.Append ('|');
			sb.Append ("\n");
			for (i = 0; i < length+2; i++)
				sb.Append (BoxGraphics.HorizLine);
			return sb.ToString ();
		}

		public TextBuf GetOut()
		{
			TextBuf tb = new TextBuf (width, height);
			int i, j;
			if (active)
				tb.BG = DrawColors.edit_bg;
			else
				tb.BG = DrawColors.wind_bg;
			tb.FG = DrawColors.edit_text;
			/*tb.Append (BoxGraphics.ULCorner);
			for (i = 0; i < length; i++)
				tb.Append (BoxGraphics.HorizLine);
			tb.Append (BoxGraphics.URCorner);
			tb.Append (BoxGraphics.VertLine);
			tb.FG = DrawColors.text;
			*/
			int pc = 0;
			for (i = 0; i < v_y; i++)
			{
				for (; pc < inp.Length; pc++)
				{
					if (inp[pc] == '\n')
						continue;
				}
			}
			for (i = 0; i < height; i++)
			{
				pc += v_x;
				if (pc >= inp.Length)
				{
					for (j = 0; j < width; j++)
						tb.Append (' ');
					break;
				}
				for (j = 0; j < width; j++)
				{
					tb.Append (inp[pc]);
					pc++;
					if (pc == inp.Length)
						break;
					if (inp[pc] == '\n')
						break;
				}
				for (j++; j < width; j++)
					tb.Append (' ');
				if (pc >= inp.Length)
					break;
				if (inp[pc] == '\n')
				{
					pc++;
					continue;
				}
				for (; pc < inp.Length; pc++)
					if (inp[pc] == '\n')
						break;
			}
			for (i++; i < height; i++)
			{
				for (j = 0; j < width; j++)
					tb.Append (' ');
			}
			/*if (inp.Length > length)
			{
				for (i = 0; i < length - 3; i++)
					tb.Append (inp[i]);
				tb.Append ('.');
				tb.Append ('.');
				tb.Append ('.');
			}
			else
			{
				for (i = 0; i < inp.Length; i++)
					tb.Append (inp[i]);
				for (i = 0; i < length - inp.Length; i++)
					tb.Append (' ');
			}*/
			/*if (focused)
			{
				var van = tb[pos_c, 0];
				tb[pos_c, 0] = new Tuple<char, ConsoleColor, ConsoleColor> (van.Item1, ConsoleColor.Black, ConsoleColor.White);
			}*/
			/*if (active)
				tb.FG = DrawColors.active_fence;
			else
				tb.FG = DrawColors.inactive_fence;
			tb.Append (BoxGraphics.VertLine);
			tb.Append (BoxGraphics.LLCorner);
			for (i = 0; i < length; i++)
				tb.Append (BoxGraphics.HorizLine);
			tb.Append (BoxGraphics.LRCorner);
     */
			if (active)
			{
				var van = tb[pc_x, pc_y];
				tb[pc_x, pc_y] = new Tuple<char, ConsoleColor, ConsoleColor> (van.Item1, ConsoleColor.Black, ConsoleColor.White);
			}
			return tb;
		}


		#region IWidget implementation
		public TextBuf DrawTextBuf ()
		{
			lock (this)
			{
				return GetOut ();
			}
		}
		public void InputKey (ConsoleKeyInfo cki)
		{
			inputKey (cki);
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
			return new Tuple<int, int> (length, 1);
		}
		#endregion
	}
}


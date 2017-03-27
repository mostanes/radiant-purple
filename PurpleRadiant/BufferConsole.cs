using System;
using System.Collections.Generic;

namespace PurpleRadiant
{
	public class BufferConsole : TextBuf
	{
		//internal List<Tuple<int,int>> regenpos;
		public BufferConsole () : base(Console.WindowWidth, Console.WindowHeight)
		{
		//	regenpos = new List<Tuple<int, int>> ();
		}

		public override Tuple<char,ConsoleColor,ConsoleColor> this[int x, int y]
		{
			get
			{
				return new Tuple<char,ConsoleColor,ConsoleColor> (buf[y, x], cc[y, x, 0], cc[y, x, 1]);
			}
			set
			{
				buf[y, x] = value.Item1;
				cc[y, x, 0] = value.Item2;
				cc[y, x, 1] = value.Item3;
				//regenpos.Add (new Tuple<int, int> (x, y));
			}
		}

		/*public void Redraw()
		{
			foreach (var van in regenpos)
			{
				Console.SetCursorPosition (van.Item1, van.Item2);
				Console.BackgroundColor = cc[van.Item2, van.Item1, 1];
				Console.ForegroundColor = cc[van.Item2, van.Item1, 0];
				Console.Write (buf[van.Item2, van.Item1]);
			}
		}*/

		public void Redraw(BufferConsole older)
		{
			int i, j;
			for (i = 0; i < m_height; i++)
			{
				for (j = 0; j < m_width; j++)
				{
					if ((cc[i, j, 0] != older.cc[i, j, 0]) || (cc[i, j, 1] != older.cc[i, j, 1]) || (buf[i, j] != older.buf[i, j]))
					{
						Console.ForegroundColor = cc[i, j, 0];
						Console.BackgroundColor = cc[i, j, 1];
						Console.SetCursorPosition (j, i);
						Console.Write (buf[i, j]);
					}
				}
				//Console.WriteLine ();
			}
		}

		public void Seek(int x, int y)
		{
			pos_x = x;
			pos_y = y;
		}

		public void AppendMore(char c, ConsoleColor bg, ConsoleColor fg)
		{
			/*if ((buf[pos_y, pos_x] == c) && (cc[pos_y, pos_x, 0] == csc[0]) && (cc[pos_y, pos_x, 1] == csc[1]))
				return;*/
			buf[pos_y, pos_x] = c;
			cc[pos_y, pos_x, 0] = fg;
			cc[pos_y, pos_x, 1] = bg;
			//regenpos.Add (new Tuple<int, int> (pos_x, pos_y));
			pos_x++;
			if (pos_x >= m_width)
			{
				pos_y++;
				pos_x = 0;
			}
		}
	}

}


using System;

namespace PurpleRadiant
{
	/// <summary>
	/// Console Text Buffer
	/// </summary>
	/// <description>
	/// A buffer for the console text-graphics output.
	/// Contains color information.
	/// </description>
	public class TextBuf
	{
		internal char[,] buf;
		internal ConsoleColor[,,] cc;
		internal int m_height;
		internal int m_width;

		internal ConsoleColor[] csc = new ConsoleColor[2];
		internal ConsoleColor lastbg, lastfg;
		internal int pos_x, pos_y;

		internal int Width { get { return m_width; } }
		internal int Height { get { return m_height; } }

		public ConsoleColor FG { get { return csc[0]; } set { csc[0] = value; } }
		public ConsoleColor BG { get { return csc[1]; } set { csc[1] = value; } }

		public TextBuf (int Width, int Height)
		{
			m_width = Width;
			m_height = Height;
			buf = new char[m_height, m_width];
			cc = new ConsoleColor[m_height, m_width, 2];
			csc[0] = ConsoleColor.White;
			csc[1] = ConsoleColor.Black;
			lastbg = ConsoleColor.Black;
			lastfg = ConsoleColor.White;
			pos_x = 0;
			pos_y = 0;
		}

		/// <summary>
		/// Gets or sets the text and colors at the specified x y coordinates.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public virtual Tuple<char,ConsoleColor,ConsoleColor> this[int x, int y]
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
			}
		}

		/// <summary>
		/// Seek the specified new positon.
		/// </summary>
		/// <param name="new_pos">The new position</param>
		public void Seek(int new_pos)
		{
			pos_x = new_pos % m_width;
			pos_y = new_pos / m_width;
		}
		/// <summary>
		/// Append the specified character with the preset colors.
		/// </summary>
		/// <param name="c">character</param>
		public virtual void Append(char c)
		{
			buf[pos_y, pos_x] = c;
			cc[pos_y, pos_x, 0] = csc[0];
			cc[pos_y, pos_x, 1] = csc[1];
			pos_x++;
			if (pos_x >= m_width)
			{
				pos_y++;
				pos_x = 0;
			}
		}

		[Obsolete]
		public void Draw()
		{
			int i, j;
			for (i = 0; i < m_height; i++)
			{
				for (j = 0; j < m_width; j++)
				{
					if (cc[i, j, 0] != lastfg)
					{
						Console.ForegroundColor = cc[i, j, 0];
						lastfg = cc[i, j, 0];
					}
					if (cc[i, j, 1] != lastbg)
					{
						Console.BackgroundColor = cc[i, j, 1];
						lastbg = cc[i, j, 1];
					}
					Console.Write (buf[i, j]);
				}
				//Console.WriteLine ();
			}
		}

		/// <summary>
		/// Draws the on the Virtual Console
		/// <seealso cref="T:PurpleRadiant.VirtualConsole"/>
		/// </summary>
		public void DrawOnVC()
		{
			int i, j;
			for (i = 0; i < m_height; i++)
			{
				for (j = 0; j < m_width; j++)
				{
					VirtualConsole.ForegroundColor = cc[i, j, 0];
					VirtualConsole.BackgroundColor = cc[i, j, 1];
					VirtualConsole.Write (buf[i, j]);
				}
				//VirtualConsole.WriteLine ();
			}
		}

		/// <summary>
		/// Draws this TextBuf over another TextBuf
		/// </summary>
		/// <param name="tb">Textbuf to be drawn over.</param>
		/// <param name="start_x">The x coordinate where the TextBuf should start</param>
		/// <param name="start_y">The y coordinate where the TextBuf should start</param>
		public void DrawOver(TextBuf tb, int start_x, int start_y)
		{
			int i, j;
			for (i = 0; i < m_height; i++)
			{
				for (j = 0; j < m_width; j++)
				{
					tb.cc[i + start_y, j + start_x, 0] = cc[i, j, 0];
					tb.cc[i + start_y, j + start_x, 1] = cc[i, j, 1];
					tb.buf[i + start_y, j + start_x] = buf[i, j];
				}
			}
		}
	}
}


using System;

namespace PurpleRadiant
{
	static class BoxGraphics
	{
		public const char HorizLine = '─';
		public const char VertLine = '│';
		public const char ULCorner = '┌';
		public const char URCorner = '┐';
		public const char LLCorner = '└';
		public const char LRCorner = '┘';
		public const char DoubleHorizLine = '═';
		public const char DoubleVertLine = '║';
		public const char DoubleULCorner = '╔';
		public const char DoubleURCorner = '╗';
		public const char DoubleLLCorner = '╚';
		public const char DoubleLRCorner = '╝';
		public const char DoubleTLeft = '╠';
		public const char DoubleTRight = '╣';
		public const char Square = '■';
		public const char LeftArrow = '◄';
		public const char RightArrow = '►';
		public const char UpArrow = '▲';
		public const char DownArrow = '▼';
		public const char ScrollWhite = '▒';
		public const char ScrollBlack = '▓';
	}

	static class DrawColors
	{
		public static ConsoleColor scr_bg = ConsoleColor.Black;
		public static ConsoleColor wind_bg = ConsoleColor.DarkGray;
		public static ConsoleColor active_fence = ConsoleColor.White;
		public static ConsoleColor inactive_fence = ConsoleColor.DarkGray;
		public static ConsoleColor edit_text = ConsoleColor.White;
		public static ConsoleColor edit_bg = ConsoleColor.Blue;
		public static ConsoleColor select_bg = ConsoleColor.Red;
		public static ConsoleColor select_ar = ConsoleColor.Black;
		public static ConsoleColor select_text = ConsoleColor.Black;
		public static ConsoleColor overlay_bg = ConsoleColor.Black;
		public static ConsoleColor button_simple_bg = ConsoleColor.DarkRed;
		public static ConsoleColor button_simple_fg = ConsoleColor.White;
		public static ConsoleColor button_inactive_bg = ConsoleColor.Gray;
		public static ConsoleColor button_inactive_fg = ConsoleColor.White;
		public static ConsoleColor button_selected_bg = ConsoleColor.Red;
		public static ConsoleColor button_selected_fg = ConsoleColor.Black;
		public static ConsoleColor inactive_text = ConsoleColor.Gray;
		public static ConsoleColor scroll_bar = ConsoleColor.Green;
	}
}


using System;
using System.Text;

namespace PurpleRadiant
{
	public class InputBoxMini : IWidget
	{
		//string input;
		StringBuilder inp;
		int length;
		int pos_c;
		int veda;
		bool focused;
		PressedReturn returnMethod;

		public string Text { get { return inp.ToString(); } set { inp.Clear(); inp.Append(value);
			}}

		public InputBoxMini (int inputLength, PressedReturn returns)
		{
			length = inputLength; returnMethod = returns;
			pos_c = 0;
			inp = new StringBuilder ();
		}

		public void inputKey(ConsoleKeyInfo k)
		{
			if (k.Key == ConsoleKey.LeftArrow)
			{
				if (pos_c > 0)
					pos_c--;
				return;
			}
			if (k.Key == ConsoleKey.RightArrow)
			{
				if (pos_c < inp.Length)
					pos_c++;
				return;
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
				inp.Remove (pos_c, 1);
				return;
			}
			if (k.Key == ConsoleKey.Enter)
			{
				returnMethod.Invoke ();
				return;
			}
			inp.Insert (pos_c, k.KeyChar);
			pos_c++;
		}

		public TextBuf GetOut()
		{
			TextBuf tb = new TextBuf (length, 1);
			tb.BG = DrawColors.overlay_bg;
			tb.FG = DrawColors.edit_text;
			int i;
			if (inp.Length > length)
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


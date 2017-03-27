using System;
using System.Text;

namespace PurpleRadiant
{
	public class PasswordField : IWidget
	{
		bool focused;
		StringBuilder inp;
		int pos_c;
		PressedReturn OnEnter;

		const long a = 88463;
		const long b = 867463;
		const long c = 4169718233;
		//const string subo = "■□▲►▼◄○◌●◘◙☺☻☼♀♂♠♣♥♦♪♫✶";
		//const string subo = "αﬡερτουιωπ«σδφγηθκλζξχψβνμ";
		const string subo = "`~!@#$%^&*(){}[]:;|<>,.-_+=";
		long seph=21;
		StringBuilder outBuild;

		public PasswordField ()
		{
			focused = false;
			OnEnter = new PressedReturn (delegate()
			{
				return;
			});
			outBuild = new StringBuilder (8);
			int jar;
			for (jar = 0; jar < 8; jar++)
			{
				LCGIter (0);
				outBuild.Append (subo[(int)(seph % subo.Length)]);
			}
			inp = new StringBuilder ();
			pos_c = 0;
		}

		public PasswordField(PressedReturn onEnter)
		{
			focused = false;
			OnEnter = onEnter;
			outBuild = new StringBuilder (8);
			int jar;
			for (jar = 0; jar < 8; jar++)
			{
				LCGIter (0);
				outBuild.Append (subo[(int)(seph % subo.Length)]);
			}
			inp = new StringBuilder ();
			pos_c = 0;
		}

		private void LCGIter (int iter)
		{
			for (; iter > 0; iter--)
			{
				seph = (a * seph + b) % c;
			}
		}

		#region IWidget implementation

		public TextBuf DrawTextBuf ()
		{
			TextBuf tb = new TextBuf (8, 1);
			tb.BG = DrawColors.edit_bg;
			tb.FG = DrawColors.edit_text;
			int i;
			for (i = 0; i < outBuild.Length; i++)
			{
				tb.Append (outBuild[i]);
			}
			return tb;
		}

		public void InputKey (ConsoleKeyInfo cki)
		{
			bool f = false;
			if (cki.Key == ConsoleKey.LeftArrow)
			{
				if (pos_c > 0)
					pos_c--;
				f = true;
			}
			if (cki.Key == ConsoleKey.RightArrow)
			{
				if (pos_c < inp.Length)
					pos_c++;
				f = true;
			}
			if (cki.Key == ConsoleKey.Delete)
			{
				if (inp.Length == 0)
					return;
				if (pos_c == inp.Length)
					return;
				inp.Remove (pos_c, 1);
				f = true;
			}
			if (cki.Key == ConsoleKey.Backspace)
			{
				if (inp.Length == 0)
					return;
				if (pos_c < 1)
					return;
				pos_c--;
				inp.Remove (pos_c, 1);
				f = true;
			}
			if (cki.Key == ConsoleKey.Enter)
			{
				OnEnter ();
				return;
			}
			if (!f)
			{
				inp.Insert (pos_c, cki.KeyChar);
				pos_c++;
			}

			int jar = 0;
			int i;
			seph = (inp.Length == 0 ? 21 : inp[0]);
			int fyur;
			if (inp.Length == 0)
				fyur = 0;
			else
			{
				fyur = inp[inp.Length - 1];
				for (i = 0; i < inp.Length; i++)
				{
					fyur += inp[i];
					fyur ^= inp[i];
					if (i % 2 == 0)
						fyur ^= (inp[i] * 32);
					if (i % 4 == 0)
						fyur ^= (inp[i] / 4);
				}
			}
			for(i=0; i<8; i++)
			{
				LCGIter ((int) fyur%4);
				fyur /= 4;
				jar++;
				jar %= 8;
				outBuild[jar] = subo[(int)(seph % subo.Length)];
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

		public Tuple<int, int> GetSize ()
		{
			return new Tuple<int, int> (8, 1);
		}

		#endregion
	}
}


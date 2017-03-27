using System;
using System.Collections.Generic;

namespace PurpleRadiant
{
	public class ScrollableArea : IWidget
	{
		int a_width;
		int a_height;
		int width;
		int height;
		bool focused;
		IWidget wid;
		/*List<WidgetContainer> liw;
		LinkedList<WidgetContainer> focus;
		LinkedListNode<WidgetContainer> currentFocus;*/

		public ScrollableArea (int W, int H, int AW, int AH, IWidget widget)
		{
			width = W;
			height = H;
			a_width = AW;
			a_height = AH;
			wid = widget;
		}

		public Tuple<int, int> GetSize ()
		{
			return new Tuple<int, int> (width + 1, height + 1);
		}

		#region IWidget implementation

		public TextBuf DrawTextBuf ()
		{
			TextBuf tb, taa;
			tb = new TextBuf (width + 1, height + 1);
			taa = new TextBuf (a_width, a_height);
			TextBuf vertScroll = new TextBuf(1, height);
			TextBuf horizScroll = new TextBuf(width, 1);
			TextBuf corner = new TextBuf (1, 1);
			corner.Append (BoxGraphics.Square);
			wid.DrawTextBuf ().DrawOver (taa, 0, 0);
			vertScroll.DrawOver (tb, width+1, 0);
			horizScroll.DrawOver (tb, 0, height+1);
			corner.DrawOver (tb, width + 1, height + 1);
			return tb;
		}

		public void InputKey (ConsoleKeyInfo cki)
		{
			wid.InputKey (cki);
		}


		public void Focused ()
		{
			wid.Focused ();
		}

		public void UnFocused ()
		{
			wid.UnFocused ();
		}
		#endregion
	}
}


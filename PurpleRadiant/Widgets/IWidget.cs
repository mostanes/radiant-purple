using System;

namespace PurpleRadiant
{
	/// <summary>
	/// Widget interface. All displayable widgets should inherit from it.
	/// </summary>
	/// <description>
	/// The interface that implements widgets.
	/// </description>
	public interface IWidget
	{
		/// <summary>
		/// Draws its own TextBuf
		/// </summary>
		/// <returns>The TextBuf</returns>
		TextBuf DrawTextBuf();
		/// <summary>
		/// Send an input key to the widget.
		/// </summary>
		/// <param name="cki">The key to be sent</param>
		void InputKey (ConsoleKeyInfo cki);
		/// <summary>
		/// Inform the widget that it is focused (and display accordingly)
		/// </summary>
		void Focused();
		/// <summary>
		/// Inform the widget that it is unfocused (and display accordingly)
		/// </summary>
		void UnFocused();
		/// <summary>
		/// Get the display size of the widget
		/// </summary>
		/// <returns>A tuple with the width in the first element and the height in the second element</returns>
		Tuple<int,int> GetSize();
	}

	/// <summary>
	/// A canvas widget
	/// </summary>
	/// <description>
	/// A widget that is a canvas
	/// A canvas widget can display other widgets on it.
	/// </description>
	public interface IWidgetCanvas : IWidget
	{
		/// <summary>
		/// Add a widget to the canvas
		/// </summary>
		/// <param name="iw">The widget</param>
		/// <param name="x">The x coordinate</param>
		/// <param name="y">The y coordinate</param>
		void AddWidget(IWidget iw, int x, int y);
		/// <summary>
		/// Removes a widget.
		/// </summary>
		/// <param name="iw">The widget</param>
		void RemoveWidget(IWidget iw);
	}
}


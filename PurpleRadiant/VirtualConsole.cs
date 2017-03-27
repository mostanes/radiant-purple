using System;

namespace PurpleRadiant
{
	public static class VirtualConsole
	{
		static BufferConsole bc = new BufferConsole();
		static BufferConsole oldbc = new BufferConsole();
		static ConsoleColor bg = ConsoleColor.Black;
		static ConsoleColor fg = ConsoleColor.White;

		public static void Redraw()
		{
			bc.Redraw (oldbc);
			oldbc = bc;
			bc = new BufferConsole ();
			Console.SetCursorPosition (0, 0);
		}

		public static void Draw()
		{
			bc.Draw ();
			oldbc = bc;
			bc = new BufferConsole ();
			Console.SetCursorPosition (0, 0);
		}

		public static void Refresh()
		{
			bc.pos_x = 0;
			bc.pos_y = 0;
		}

		//
		// Static Fields
		//
		private const int MaxBeepFrequency = 32767;

		private const int MinBeepFrequency = 37;

		private const int CapsLockVKCode = 20;

		private const int NumberLockVKCode = 144;

		private const short AltVKCode = 18;

		private const int DefaultConsoleBufferSize = 256;

		private const int MaxConsoleTitleLength = 24500;

		//
		// Static Properties
		//
		public static ConsoleColor BackgroundColor
		{
			get
			{
				return bg;
			}
			set
			{
				bg = value;
				//bc.regenpos.Add (new Tuple<int, int> (bc.pos_x, bc.pos_y));
			}
		}

		/*public static int BufferHeight
		{
			[SecuritySafeCritical]
			get
			{
				return (int) Console.GetBufferInfo ().dwSize.Y;
			}
			set
			{
				Console.SetBufferSize (Console.BufferWidth, value);
			}
		}

		public static int BufferWidth
		{
			[SecuritySafeCritical]
			get
			{
				return (int) Console.GetBufferInfo ().dwSize.X;
			}
			set
			{
				Console.SetBufferSize (value, Console.BufferHeight);
			}
		}*/

		public static bool CapsLock
		{
			get
			{
				return Console.CapsLock;
			}
		}

		/*private static IntPtr ConsoleInputHandle
		{
			[SecurityCritical]
			get
			{
				if (Console._consoleInputHandle == IntPtr.Zero)
				{
					Console._consoleInputHandle = Win32Native.GetStdHandle (-10);
				}
				return Console._consoleInputHandle;
			}
		}

		private static IntPtr ConsoleOutputHandle
		{
			[SecurityCritical]
			get
			{
				if (Console._consoleOutputHandle == IntPtr.Zero)
				{
					Console._consoleOutputHandle = Win32Native.GetStdHandle (-11);
				}
				return Console._consoleOutputHandle;
			}
		}

		public static int CursorLeft
		{
			[SecuritySafeCritical]
			get
			{
				return (int) Console.GetBufferInfo ().dwCursorPosition.X;
			}
			set
			{
				Console.SetCursorPosition (value, Console.CursorTop);
			}
		}

		public static int CursorSize
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
				Win32Native.CONSOLE_CURSOR_INFO cONSOLE_CURSOR_INFO;
				if (!Win32Native.GetConsoleCursorInfo (consoleOutputHandle, out cONSOLE_CURSOR_INFO))
				{
					__Error.WinIOError ();
				}
				return cONSOLE_CURSOR_INFO.dwSize;
			}
			[SecuritySafeCritical]
			set
			{
				if (value < 1 || value > 100)
				{
					throw new ArgumentOutOfRangeException ("value", value, Environment.GetResourceString ("ArgumentOutOfRange_CursorSize"));
				}
				new UIPermission (UIPermissionWindow.SafeTopLevelWindows).Demand ();
				IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
				Win32Native.CONSOLE_CURSOR_INFO cONSOLE_CURSOR_INFO;
				if (!Win32Native.GetConsoleCursorInfo (consoleOutputHandle, out cONSOLE_CURSOR_INFO))
				{
					__Error.WinIOError ();
				}
				cONSOLE_CURSOR_INFO.dwSize = value;
				if (!Win32Native.SetConsoleCursorInfo (consoleOutputHandle, ref cONSOLE_CURSOR_INFO))
				{
					__Error.WinIOError ();
				}
			}
		}

		public static int CursorTop
		{
			[SecuritySafeCritical]
			get
			{
				return (int) Console.GetBufferInfo ().dwCursorPosition.Y;
			}
			set
			{
				Console.SetCursorPosition (Console.CursorLeft, value);
			}
		}

		public static bool CursorVisible
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
				Win32Native.CONSOLE_CURSOR_INFO cONSOLE_CURSOR_INFO;
				if (!Win32Native.GetConsoleCursorInfo (consoleOutputHandle, out cONSOLE_CURSOR_INFO))
				{
					__Error.WinIOError ();
				}
				return cONSOLE_CURSOR_INFO.bVisible;
			}
			[SecuritySafeCritical]
			set
			{
				new UIPermission (UIPermissionWindow.SafeTopLevelWindows).Demand ();
				IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
				Win32Native.CONSOLE_CURSOR_INFO cONSOLE_CURSOR_INFO;
				if (!Win32Native.GetConsoleCursorInfo (consoleOutputHandle, out cONSOLE_CURSOR_INFO))
				{
					__Error.WinIOError ();
				}
				cONSOLE_CURSOR_INFO.bVisible = value;
				if (!Win32Native.SetConsoleCursorInfo (consoleOutputHandle, ref cONSOLE_CURSOR_INFO))
				{
					__Error.WinIOError ();
				}
			}
		}

		public static TextWriter Error
		{
			[HostProtection (SecurityAction.LinkDemand, UI = true)]
			get
			{
				if (Console._error == null)
				{
					Console.InitializeStdOutError (false);
				}
				return Console._error;
			}
		}*/

		public static ConsoleColor ForegroundColor
		{
			get
			{
				return fg;
			}
			set
			{
				fg = value;
				//bc.regenpos.Add (new Tuple<int, int> (bc.pos_x, bc.pos_y));
			}
		}

		/*public static TextReader In
		{
			[SecuritySafeCritical]
			[HostProtection (SecurityAction.LinkDemand, UI = true)]
			get
			{
				if (Console._in == null)
				{
					lock (Console.InternalSyncObject)
					{
						if (Console._in == null)
						{
							Stream stream = Console.OpenStandardInput (256);
							TextReader in;
							if (stream == Stream.Null)
							{
								in = StreamReader.Null;
							}
							else
							{
								Encoding inputEncoding = Console.InputEncoding;
								in = TextReader.Synchronized (new StreamReader (stream, inputEncoding, false, 256, true));
							}
							Thread.MemoryBarrier ();
							Console._in = in;
						}
					}
				}
				return Console._in;
			}
		}

		public static Encoding InputEncoding
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._inputEncoding != null)
				{
					return Console._inputEncoding;
				}
				Encoding inputEncoding;
				lock (Console.InternalSyncObject)
				{
					if (Console._inputEncoding != null)
					{
						inputEncoding = Console._inputEncoding;
					}
					else
					{
						uint consoleCP = Win32Native.GetConsoleCP ();
						Console._inputEncoding = Encoding.GetEncoding ((int) consoleCP);
						inputEncoding = Console._inputEncoding;
					}
				}
				return inputEncoding;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException ("value");
				}
				new UIPermission (UIPermissionWindow.SafeTopLevelWindows).Demand ();
				lock (Console.InternalSyncObject)
				{
					if (!Console.IsStandardConsoleUnicodeEncoding (value))
					{
						uint codePage = (uint) value.CodePage;
						if (!Win32Native.SetConsoleCP (codePage))
						{
							__Error.WinIOError ();
						}
					}
					Console._inputEncoding = (Encoding) value.Clone ();
					Console._in = null;
				}
			}
		}

		private static object InternalSyncObject
		{
			get
			{
				if (Console.s_InternalSyncObject == null)
				{
					object value = new object ();
					Interlocked.CompareExchange<object> (ref Console.s_InternalSyncObject, value, null);
				}
				return Console.s_InternalSyncObject;
			}
		}

		public static bool IsErrorRedirected
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._stdErrRedirectQueried)
				{
					return Console._isStdErrRedirected;
				}
				bool isStdErrRedirected;
				lock (Console.InternalSyncObject)
				{
					if (Console._stdErrRedirectQueried)
					{
						isStdErrRedirected = Console._isStdErrRedirected;
					}
					else
					{
						IntPtr stdHandle = Win32Native.GetStdHandle (-12);
						Console._isStdErrRedirected = Console.IsHandleRedirected (stdHandle);
						Console._stdErrRedirectQueried = true;
						isStdErrRedirected = Console._isStdErrRedirected;
					}
				}
				return isStdErrRedirected;
			}
		}

		public static bool IsInputRedirected
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._stdInRedirectQueried)
				{
					return Console._isStdInRedirected;
				}
				bool isStdInRedirected;
				lock (Console.InternalSyncObject)
				{
					if (Console._stdInRedirectQueried)
					{
						isStdInRedirected = Console._isStdInRedirected;
					}
					else
					{
						Console._isStdInRedirected = Console.IsHandleRedirected (Console.ConsoleInputHandle);
						Console._stdInRedirectQueried = true;
						isStdInRedirected = Console._isStdInRedirected;
					}
				}
				return isStdInRedirected;
			}
		}

		public static bool IsOutputRedirected
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._stdOutRedirectQueried)
				{
					return Console._isStdOutRedirected;
				}
				bool isStdOutRedirected;
				lock (Console.InternalSyncObject)
				{
					if (Console._stdOutRedirectQueried)
					{
						isStdOutRedirected = Console._isStdOutRedirected;
					}
					else
					{
						Console._isStdOutRedirected = Console.IsHandleRedirected (Console.ConsoleOutputHandle);
						Console._stdOutRedirectQueried = true;
						isStdOutRedirected = Console._isStdOutRedirected;
					}
				}
				return isStdOutRedirected;
			}
		}

		public static bool KeyAvailable
		{
			[SecuritySafeCritical]
			[HostProtection (SecurityAction.LinkDemand, UI = true)]
			get
			{
				if (Console._cachedInputRecord.eventType == 1)
				{
					return true;
				}
				Win32Native.InputRecord ir = default(Win32Native.InputRecord);
				int num = 0;
				while (true)
				{
					if (!Win32Native.PeekConsoleInput (Console.ConsoleInputHandle, out ir, 1, out num))
					{
						int lastWin32Error = Marshal.GetLastWin32Error ();
						if (lastWin32Error == 6)
						{
							break;
						}
						__Error.WinIOError (lastWin32Error, "stdin");
					}
					if (num == 0)
					{
						return false;
					}
					if (Console.IsKeyDownEvent (ir) && !Console.IsModKey (ir))
					{
						return true;
					}
					if (!Win32Native.ReadConsoleInput (Console.ConsoleInputHandle, out ir, 1, out num))
					{
						__Error.WinIOError ();
					}
				}
				throw new InvalidOperationException (Environment.GetResourceString ("InvalidOperation_ConsoleKeyAvailableOnFile"));
			}
		}*/

		public static int LargestWindowHeight
		{
			get
			{
				return Console.LargestWindowHeight;
			}
		}

		public static int LargestWindowWidth
		{
			get
			{
				return Console.LargestWindowWidth;
			}
		}

		public static bool NumberLock
		{
			get
			{
				return Console.NumberLock;
			}
		}

		/*public static TextWriter Out
		{
			[HostProtection (SecurityAction.LinkDemand, UI = true)]
			get
			{
				if (Console._out == null)
				{
					Console.InitializeStdOutError (true);
				}
				return Console._out;
			}
		}

		public static Encoding OutputEncoding
		{
			[SecuritySafeCritical]
			get
			{
				if (Console._outputEncoding != null)
				{
					return Console._outputEncoding;
				}
				Encoding outputEncoding;
				lock (Console.InternalSyncObject)
				{
					if (Console._outputEncoding != null)
					{
						outputEncoding = Console._outputEncoding;
					}
					else
					{
						uint consoleOutputCP = Win32Native.GetConsoleOutputCP ();
						Console._outputEncoding = Encoding.GetEncoding ((int) consoleOutputCP);
						outputEncoding = Console._outputEncoding;
					}
				}
				return outputEncoding;
			}
			[SecuritySafeCritical]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException ("value");
				}
				new UIPermission (UIPermissionWindow.SafeTopLevelWindows).Demand ();
				lock (Console.InternalSyncObject)
				{
					if (Console._out != null && !Console._isOutTextWriterRedirected)
					{
						Console._out.Flush ();
						Console._out = null;
					}
					if (Console._error != null && !Console._isErrorTextWriterRedirected)
					{
						Console._error.Flush ();
						Console._error = null;
					}
					if (!Console.IsStandardConsoleUnicodeEncoding (value))
					{
						uint codePage = (uint) value.CodePage;
						if (!Win32Native.SetConsoleOutputCP (codePage))
						{
							__Error.WinIOError ();
						}
					}
					Console._outputEncoding = (Encoding) value.Clone ();
				}
			}
		}

		private static object ReadKeySyncObject
		{
			get
			{
				if (Console.s_ReadKeySyncObject == null)
				{
					object value = new object ();
					Interlocked.CompareExchange<object> (ref Console.s_ReadKeySyncObject, value, null);
				}
				return Console.s_ReadKeySyncObject;
			}
		}*/

		public static string Title
		{
			get
			{
				return Console.Title;
			}
			set
			{
				Console.Title = value;
			}
		}

		/*public static bool TreatControlCAsInput
		{
			[SecuritySafeCritical]
			get
			{
				IntPtr consoleInputHandle = Console.ConsoleInputHandle;
				if (consoleInputHandle == Win32Native.INVALID_HANDLE_VALUE)
				{
					throw new IOException (Environment.GetResourceString ("IO.IO_NoConsole"));
				}
				int num = 0;
				if (!Win32Native.GetConsoleMode (consoleInputHandle, out num))
				{
					__Error.WinIOError ();
				}
				return (num & 1) == 0;
			}
			[SecuritySafeCritical]
			set
			{
				new UIPermission (UIPermissionWindow.SafeTopLevelWindows).Demand ();
				IntPtr consoleInputHandle = Console.ConsoleInputHandle;
				if (consoleInputHandle == Win32Native.INVALID_HANDLE_VALUE)
				{
					throw new IOException (Environment.GetResourceString ("IO.IO_NoConsole"));
				}
				int num = 0;
				bool consoleMode = Win32Native.GetConsoleMode (consoleInputHandle, out num);
				if (value)
				{
					num &= -2;
				}
				else
				{
					num |= 1;
				}
				if (!Win32Native.SetConsoleMode (consoleInputHandle, num))
				{
					__Error.WinIOError ();
				}
			}
		}*/

		/*public static int WindowHeight
		{
			get
			{
				return Console.WindowHeight;
			}
			set
			{
				Console.SetWindowSize (Console.WindowWidth, value);
			}
		}

		public static int WindowLeft
		{
			get
			{
				return Console.WindowLeft;
			}
			set
			{
				Console.SetWindowPosition (value, Console.WindowTop);
			}
		}

		public static int WindowTop
		{
			get
			{
				return Console.WindowTop;
			}
			set
			{
				Console.SetWindowPosition (Console.WindowLeft, value);
			}
		}

		public static int WindowWidth
		{
			get
			{
				return Console.WindowWidth;
			}
			set
			{
				Console.SetWindowSize (value, Console.WindowHeight);
			}
		}

		//
		// Static Methods
		//
		public static void Beep (int frequency, int duration)
		{
			Console.Beep (frequency, duration);
		}

		public static void Beep ()
		{
			Console.Beep (800, 200);
		}

		/*private static bool BreakEvent (int controlType)
		{
			if (controlType != 0 && controlType != 1)
			{
				return false;
			}
			ConsoleCancelEventHandler cancelCallbacks = Console._cancelCallbacks;
			if (cancelCallbacks == null)
			{
				return false;
			}
			ConsoleSpecialKey controlKey = (controlType == 0) ? ConsoleSpecialKey.ControlC : ConsoleSpecialKey.ControlBreak;
			Console.ControlCDelegateData controlCDelegateData = new Console.ControlCDelegateData (controlKey, cancelCallbacks);
			WaitCallback callBack = new WaitCallback (Console.ControlCDelegate);
			if (!ThreadPool.QueueUserWorkItem (callBack, controlCDelegateData))
			{
				return false;
			}
			TimeSpan timeout = new TimeSpan (0, 0, 30);
			controlCDelegateData.CompletionEvent.WaitOne (timeout, false);
			if (!controlCDelegateData.DelegateStarted)
			{
				return false;
			}
			controlCDelegateData.CompletionEvent.WaitOne ();
			controlCDelegateData.CompletionEvent.Close ();
			return controlCDelegateData.Cancel;
		}*/

		/*public static void Clear ()
		{
			bc = new BufferConsole ();
		}*/

/*		[SecurityCritical]
		private static ConsoleColor ColorAttributeToConsoleColor (Win32Native.Color c)
		{
			if ((short) (c & Win32Native.Color.BackgroundMask) != 0)
			{
				c >>= 4;
			}
			return (ConsoleColor) c;
		}

		[SecurityCritical]
		private static Win32Native.Color ConsoleColorToColorAttribute (ConsoleColor color, bool isBackground)
		{
			if ((color & (ConsoleColor) (-16)) != ConsoleColor.Black)
			{
				throw new ArgumentException (Environment.GetResourceString ("Arg_InvalidConsoleColor"));
			}
			Win32Native.Color color2 = (Win32Native.Color) color;
			if (isBackground)
			{
				color2 <<= 4;
			}
			return color2;
		}

		[SecuritySafeCritical]
		private unsafe static bool ConsoleHandleIsWritable (SafeFileHandle outErrHandle)
		{
			byte b = 65;
			int num2;
			int num = Win32Native.WriteFile (outErrHandle, &b, 0, out num2, IntPtr.Zero);
			return num != 0;
		}

		private static void ControlCDelegate (object data)
		{
			Console.ControlCDelegateData controlCDelegateData = (Console.ControlCDelegateData) data;
			try
			{
				controlCDelegateData.DelegateStarted = true;
				ConsoleCancelEventArgs consoleCancelEventArgs = new ConsoleCancelEventArgs (controlCDelegateData.ControlKey);
				controlCDelegateData.CancelCallbacks (null, consoleCancelEventArgs);
				controlCDelegateData.Cancel = consoleCancelEventArgs.Cancel;
			}
			finally
			{
				controlCDelegateData.CompletionEvent.Set ();
			}
		}

		[SecuritySafeCritical]
		private static Win32Native.CONSOLE_SCREEN_BUFFER_INFO GetBufferInfo (bool throwOnNoConsole, out bool succeeded)
		{
			succeeded = false;
			IntPtr consoleOutputHandle = Console.ConsoleOutputHandle;
			if (!(consoleOutputHandle == Win32Native.INVALID_HANDLE_VALUE))
			{
				Win32Native.CONSOLE_SCREEN_BUFFER_INFO result;
				if (!Win32Native.GetConsoleScreenBufferInfo (consoleOutputHandle, out result))
				{
					bool consoleScreenBufferInfo = Win32Native.GetConsoleScreenBufferInfo (Win32Native.GetStdHandle (-12), out result);
					if (!consoleScreenBufferInfo)
					{
						consoleScreenBufferInfo = Win32Native.GetConsoleScreenBufferInfo (Win32Native.GetStdHandle (-10), out result);
					}
					if (!consoleScreenBufferInfo)
					{
						int lastWin32Error = Marshal.GetLastWin32Error ();
						if (lastWin32Error == 6 && !throwOnNoConsole)
						{
							return default(Win32Native.CONSOLE_SCREEN_BUFFER_INFO);
						}
						__Error.WinIOError (lastWin32Error, null);
					}
				}
				if (!Console._haveReadDefaultColors)
				{
					Console._defaultColors = (byte) (result.wAttributes & 255);
					Console._haveReadDefaultColors = true;
				}
				succeeded = true;
				return result;
			}
			if (!throwOnNoConsole)
			{
				return default(Win32Native.CONSOLE_SCREEN_BUFFER_INFO);
			}
			throw new IOException (Environment.GetResourceString ("IO.IO_NoConsole"));
		}

		[SecurityCritical]
		private static Win32Native.CONSOLE_SCREEN_BUFFER_INFO GetBufferInfo ()
		{
			bool flag;
			return Console.GetBufferInfo (true, out flag);
		}

		[SecuritySafeCritical]
		private static Stream GetStandardFile (int stdHandleName, FileAccess access, int bufferSize)
		{
			IntPtr stdHandle = Win32Native.GetStdHandle (stdHandleName);
			SafeFileHandle safeFileHandle = new SafeFileHandle (stdHandle, false);
			if (safeFileHandle.IsInvalid)
			{
				safeFileHandle.SetHandleAsInvalid ();
				return Stream.Null;
			}
			if (stdHandleName != -10 && !Console.ConsoleHandleIsWritable (safeFileHandle))
			{
				return Stream.Null;
			}
			bool useFileAPIs = Console.GetUseFileAPIs (stdHandleName);
			return new __ConsoleStream (safeFileHandle, access, useFileAPIs);
		}

		[SecurityCritical, SuppressUnmanagedCodeSecurity]
		[DllImport ("QCall", CharSet = CharSet.Ansi)]
		private static extern int GetTitleNative (StringHandleOnStack outTitle, out int outTitleLength);

		private static bool GetUseFileAPIs (int handleType)
		{
			switch (handleType)
			{
				case -12:
					return !Console.IsStandardConsoleUnicodeEncoding (Console.OutputEncoding) || Console.IsErrorRedirected;
				case -11:
					return !Console.IsStandardConsoleUnicodeEncoding (Console.OutputEncoding) || Console.IsOutputRedirected;
				case -10:
					return !Console.IsStandardConsoleUnicodeEncoding (Console.InputEncoding) || Console.IsInputRedirected;
				default:
					return true;
			}
		}

		[SecuritySafeCritical]
		private static void InitializeStdOutError (bool stdout)
		{
			lock (Console.InternalSyncObject)
			{
				if (!stdout || Console._out == null)
				{
					if (stdout || Console._error == null)
					{
						Stream stream;
						if (stdout)
						{
							stream = Console.OpenStandardOutput (256);
						}
						else
						{
							stream = Console.OpenStandardError (256);
						}
						TextWriter textWriter;
						if (stream == Stream.Null)
						{
							textWriter = TextWriter.Synchronized (StreamWriter.Null);
						}
						else
						{
							Encoding outputEncoding = Console.OutputEncoding;
							textWriter = TextWriter.Synchronized (new StreamWriter (stream, outputEncoding, 256, true) {
								HaveWrittenPreamble = true,
								AutoFlush = true
							});
						}
						if (stdout)
						{
							Console._out = textWriter;
						}
						else
						{
							Console._error = textWriter;
						}
					}
				}
			}
		}

		[SecurityCritical]
		private static bool IsAltKeyDown (Win32Native.InputRecord ir)
		{
			return (ir.keyEvent.controlKeyState & 3) != 0;
		}

		[SecuritySafeCritical]
		private static bool IsHandleRedirected (IntPtr ioHandle)
		{
			SafeFileHandle handle = new SafeFileHandle (ioHandle, false);
			int fileType = Win32Native.GetFileType (handle);
			if ((fileType & 2) != 2)
			{
				return true;
			}
			int num;
			bool consoleMode = Win32Native.GetConsoleMode (ioHandle, out num);
			return !consoleMode;
		}

		[SecurityCritical]
		private static bool IsKeyDownEvent (Win32Native.InputRecord ir)
		{
			return ir.eventType == 1 && ir.keyEvent.keyDown;
		}

		[SecurityCritical]
		private static bool IsModKey (Win32Native.InputRecord ir)
		{
			short virtualKeyCode = ir.keyEvent.virtualKeyCode;
			return (virtualKeyCode >= 16 && virtualKeyCode <= 18) || virtualKeyCode == 20 || virtualKeyCode == 144 || virtualKeyCode == 145;
		}

		private static bool IsStandardConsoleUnicodeEncoding (Encoding encoding)
		{
			UnicodeEncoding unicodeEncoding = encoding as UnicodeEncoding;
			return unicodeEncoding != null && Console.StdConUnicodeEncoding.CodePage == unicodeEncoding.CodePage && Console.StdConUnicodeEncoding.bigEndian == unicodeEncoding.bigEndian;
		}

		[SecuritySafeCritical]
		public unsafe static void MoveBufferArea (int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			if (sourceForeColor < ConsoleColor.Black || sourceForeColor > ConsoleColor.White)
			{
				throw new ArgumentException (Environment.GetResourceString ("Arg_InvalidConsoleColor"), "sourceForeColor");
			}
			if (sourceBackColor < ConsoleColor.Black || sourceBackColor > ConsoleColor.White)
			{
				throw new ArgumentException (Environment.GetResourceString ("Arg_InvalidConsoleColor"), "sourceBackColor");
			}
			Win32Native.COORD dwSize = Console.GetBufferInfo ().dwSize;
			if (sourceLeft < 0 || sourceLeft > (int) dwSize.X)
			{
				throw new ArgumentOutOfRangeException ("sourceLeft", sourceLeft, Environment.GetResourceString ("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (sourceTop < 0 || sourceTop > (int) dwSize.Y)
			{
				throw new ArgumentOutOfRangeException ("sourceTop", sourceTop, Environment.GetResourceString ("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (sourceWidth < 0 || sourceWidth > (int) dwSize.X - sourceLeft)
			{
				throw new ArgumentOutOfRangeException ("sourceWidth", sourceWidth, Environment.GetResourceString ("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (sourceHeight < 0 || sourceTop > (int) dwSize.Y - sourceHeight)
			{
				throw new ArgumentOutOfRangeException ("sourceHeight", sourceHeight, Environment.GetResourceString ("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (targetLeft < 0 || targetLeft > (int) dwSize.X)
			{
				throw new ArgumentOutOfRangeException ("targetLeft", targetLeft, Environment.GetResourceString ("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (targetTop < 0 || targetTop > (int) dwSize.Y)
			{
				throw new ArgumentOutOfRangeException ("targetTop", targetTop, Environment.GetResourceString ("ArgumentOutOfRange_ConsoleBufferBoundaries"));
			}
			if (sourceWidth == 0 || sourceHeight == 0)
			{
				return;
			}
			new UIPermission (UIPermissionWindow.SafeTopLevelWindows).Demand ();
			Win32Native.CHAR_INFO[] array = new Win32Native.CHAR_INFO[sourceWidth * sourceHeight];
			dwSize.X = (short) sourceWidth;
			dwSize.Y = (short) sourceHeight;
			Win32Native.COORD bufferCoord = default(Win32Native.COORD);
			Win32Native.SMALL_RECT sMALL_RECT = default(Win32Native.SMALL_RECT);
			sMALL_RECT.Left = (short) sourceLeft;
			sMALL_RECT.Right = (short) (sourceLeft + sourceWidth - 1);
			sMALL_RECT.Top = (short) sourceTop;
			sMALL_RECT.Bottom = (short) (sourceTop + sourceHeight - 1);
			bool flag;
			fixed (Win32Native.CHAR_INFO* ptr = array)
			{
				flag = Win32Native.ReadConsoleOutput (Console.ConsoleOutputHandle, ptr, dwSize, bufferCoord, ref sMALL_RECT);
			}
			if (!flag)
			{
				__Error.WinIOError ();
			}
			Win32Native.COORD cOORD = default(Win32Native.COORD);
			cOORD.X = (short) sourceLeft;
			Win32Native.Color color = Console.ConsoleColorToColorAttribute (sourceBackColor, true);
			color |= Console.ConsoleColorToColorAttribute (sourceForeColor, false);
			short wColorAttribute = (short) color;
			for (int i = sourceTop; i < sourceTop + sourceHeight; i++)
			{
				cOORD.Y = (short) i;
				int num;
				if (!Win32Native.FillConsoleOutputCharacter (Console.ConsoleOutputHandle, sourceChar, sourceWidth, cOORD, out num))
				{
					__Error.WinIOError ();
				}
				if (!Win32Native.FillConsoleOutputAttribute (Console.ConsoleOutputHandle, wColorAttribute, sourceWidth, cOORD, out num))
				{
					__Error.WinIOError ();
				}
			}
			Win32Native.SMALL_RECT sMALL_RECT2 = default(Win32Native.SMALL_RECT);
			sMALL_RECT2.Left = (short) targetLeft;
			sMALL_RECT2.Right = (short) (targetLeft + sourceWidth);
			sMALL_RECT2.Top = (short) targetTop;
			sMALL_RECT2.Bottom = (short) (targetTop + sourceHeight);
			fixed (Win32Native.CHAR_INFO* ptr2 = array)
			{
				flag = Win32Native.WriteConsoleOutput (Console.ConsoleOutputHandle, ptr2, dwSize, bufferCoord, ref sMALL_RECT2);
			}
		}

		public static void MoveBufferArea (int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
		{
			Console.MoveBufferArea (sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, ' ', ConsoleColor.Black, Console.BackgroundColor);
		}

		[TargetedPatchingOptOut ("Performance critical to inline this type of method across NGen image boundaries")]
		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardError ()
		{
			return Console.OpenStandardError (256);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardError (int bufferSize)
		{
			if (bufferSize < 0)
			{
				throw new ArgumentOutOfRangeException ("bufferSize", Environment.GetResourceString ("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return Console.GetStandardFile (-12, FileAccess.Write, bufferSize);
		}

		[TargetedPatchingOptOut ("Performance critical to inline this type of method across NGen image boundaries")]
		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardInput ()
		{
			return Console.OpenStandardInput (256);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardInput (int bufferSize)
		{
			if (bufferSize < 0)
			{
				throw new ArgumentOutOfRangeException ("bufferSize", Environment.GetResourceString ("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return Console.GetStandardFile (-10, FileAccess.Read, bufferSize);
		}

		[TargetedPatchingOptOut ("Performance critical to inline this type of method across NGen image boundaries")]
		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardOutput ()
		{
			return Console.OpenStandardOutput (256);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static Stream OpenStandardOutput (int bufferSize)
		{
			if (bufferSize < 0)
			{
				throw new ArgumentOutOfRangeException ("bufferSize", Environment.GetResourceString ("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return Console.GetStandardFile (-11, FileAccess.Write, bufferSize);
		}*/

		public static int Read ()
		{
			return Console.Read ();
		}

		public static ConsoleKeyInfo ReadKey (bool intercept)
		{
			return Console.ReadKey (intercept);
		}

		public static ConsoleKeyInfo ReadKey ()
		{
			return Console.ReadKey (false);
		}
			
		public static string ReadLine ()
		{
			return Console.ReadLine ();
		}

		public static void ResetColor ()
		{
			Console.ResetColor ();
			bc.csc[0] = ConsoleColor.White;
			bc.csc[1] = ConsoleColor.Black;
		}

		public static void SetBufferSize (int width, int height)
		{
			Console.SetBufferSize (width, height);
		}
			
		public static void SetCursorPosition (int left, int top)
		{
			bc.Seek (left, top);
		}

		/*[SecuritySafeCritical]
		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void SetError (TextWriter newError)
		{
			if (newError == null)
			{
				throw new ArgumentNullException ("newError");
			}
			new SecurityPermission (SecurityPermissionFlag.UnmanagedCode).Demand ();
			Console._isErrorTextWriterRedirected = true;
			newError = TextWriter.Synchronized (newError);
			lock (Console.InternalSyncObject)
			{
				Console._error = newError;
			}
		}

		[SecuritySafeCritical]
		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void SetIn (TextReader newIn)
		{
			if (newIn == null)
			{
				throw new ArgumentNullException ("newIn");
			}
			new SecurityPermission (SecurityPermissionFlag.UnmanagedCode).Demand ();
			newIn = TextReader.Synchronized (newIn);
			lock (Console.InternalSyncObject)
			{
				Console._in = newIn;
			}
		}

		[SecuritySafeCritical]
		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void SetOut (TextWriter newOut)
		{
			if (newOut == null)
			{
				throw new ArgumentNullException ("newOut");
			}
			new SecurityPermission (SecurityPermissionFlag.UnmanagedCode).Demand ();
			Console._isOutTextWriterRedirected = true;
			newOut = TextWriter.Synchronized (newOut);
			lock (Console.InternalSyncObject)
			{
				Console._out = newOut;
			}
		}*/

		/*public unsafe static void SetWindowPosition (int left, int top)
		{
			Console.SetWindowPosition(left, top);
		}
			
		public unsafe static void SetWindowSize (int width, int height)
		{
			Console.SetWindowSize(width, height);
		}*/

		/*[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void Write (decimal value)
		{
			Console.Out.Write (value);
		}*/
			
		/*public static void Write (char[] buffer)
		{
			Console.Write (buffer);
		}
			
		public static void Write (char[] buffer, int index, int count)
		{
			Console.Write (buffer, index, count);
		}*/

		/*[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void Write (double value)
		{
			Console.Out.Write (value);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void Write (float value)
		{
			Console.Out.Write (value);
		}*/

		public static void Write (string value)
		{
			foreach (char c in value)
				bc.AppendMore(c, bg, fg);					/*TODO: Implement orderly*/
		}

		/*[CLSCompliant (false)]
		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void Write (uint value)
		{
			Console.Out.Write (value);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void Write (long value)
		{
			Console.Out.Write (value);
		}

		[CLSCompliant (false)]
		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void Write (ulong value)
		{
			Console.Out.Write (value);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void Write (object value)
		{
			Console.Out.Write (value);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void Write (int value)
		{
			Console.Out.Write (value);
		}*/

		public static void Write (char value)
		{
			bc.AppendMore(value, bg, fg);
		}

		/*[CLSCompliant (false)]
		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void Write (string format, object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator (__arglist);
			int num = argIterator.GetRemainingCount () + 4;
			object[] array = new object[num];
			array [0] = arg0;
			array [1] = arg1;
			array [2] = arg2;
			array [3] = arg3;
			for (int i = 4; i < num; i++)
			{
				array [i] = TypedReference.ToObject (argIterator.GetNextArg ());
			}
			Console.Out.Write (format, array);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void Write (string format, params object[] arg)
		{
			if (arg == null)
			{
				Console.Out.Write (format, null, null);
				return;
			}
			Console.Out.Write (format, arg);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void Write (string format, object arg0, object arg1, object arg2)
		{
			Console.Out.Write (format, arg0, arg1, arg2);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void Write (string format, object arg0, object arg1)
		{
			Console.Out.Write (format, arg0, arg1);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void Write (string format, object arg0)
		{
			Console.Out.Write (format, arg0);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void Write (bool value)
		{
			Console.Out.Write (value);
		}*/

		public static void WriteLine ()
		{
			bc.pos_x = 0;
			bc.pos_y++;
		}

		/*[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (bool value)
		{
			Console.Out.WriteLine (value);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (char value)
		{
			Console.Out.WriteLine (value);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (char[] buffer)
		{
			Console.Out.WriteLine (buffer);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (char[] buffer, int index, int count)
		{
			Console.Out.WriteLine (buffer, index, count);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (decimal value)
		{
			Console.Out.WriteLine (value);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (double value)
		{
			Console.Out.WriteLine (value);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (float value)
		{
			Console.Out.WriteLine (value);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (int value)
		{
			Console.Out.WriteLine (value);
		}

		[CLSCompliant (false)]
		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (uint value)
		{
			Console.Out.WriteLine (value);
		}

		[CLSCompliant (false)]
		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (ulong value)
		{
			Console.Out.WriteLine (value);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (object value)
		{
			Console.Out.WriteLine (value);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (string value)
		{
			Console.Out.WriteLine (value);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (string format, object arg0)
		{
			Console.Out.WriteLine (format, arg0);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (string format, object arg0, object arg1)
		{
			Console.Out.WriteLine (format, arg0, arg1);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (string format, object arg0, object arg1, object arg2)
		{
			Console.Out.WriteLine (format, arg0, arg1, arg2);
		}

		[CLSCompliant (false)]
		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (string format, object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator (__arglist);
			int num = argIterator.GetRemainingCount () + 4;
			object[] array = new object[num];
			array [0] = arg0;
			array [1] = arg1;
			array [2] = arg2;
			array [3] = arg3;
			for (int i = 4; i < num; i++)
			{
				array [i] = TypedReference.ToObject (argIterator.GetNextArg ());
			}
			Console.Out.WriteLine (format, array);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (string format, params object[] arg)
		{
			if (arg == null)
			{
				Console.Out.WriteLine (format, null, null);
				return;
			}
			Console.Out.WriteLine (format, arg);
		}

		[HostProtection (SecurityAction.LinkDemand, UI = true)]
		public static void WriteLine (long value)
		{
			Console.Out.WriteLine (value);
		}

		//
		// Static Events
		//
		public static event ConsoleCancelEventHandler CancelKeyPress
		{
			[SecuritySafeCritical]
			add
			{
				new UIPermission (UIPermissionWindow.SafeTopLevelWindows).Demand ();
				lock (Console.InternalSyncObject)
				{
					Console._cancelCallbacks = (ConsoleCancelEventHandler) Delegate.Combine (Console._cancelCallbacks, value);
					if (Console._hooker == null)
					{
						Console._hooker = new Console.ControlCHooker ();
						Console._hooker.Hook ();
					}
				}
			}
			[SecuritySafeCritical]
			remove
			{
				new UIPermission (UIPermissionWindow.SafeTopLevelWindows).Demand ();
				lock (Console.InternalSyncObject)
				{
					Console._cancelCallbacks = (ConsoleCancelEventHandler) Delegate.Remove (Console._cancelCallbacks, value);
					if (Console._hooker != null && Console._cancelCallbacks == null)
					{
						Console._hooker.Unhook ();
					}
				}
			}
		}

		//
		// Nested Types
		//
		private sealed class ControlCDelegateData
		{
			internal ConsoleSpecialKey ControlKey;

			internal bool Cancel;

			internal bool DelegateStarted;

			internal ManualResetEvent CompletionEvent;

			internal ConsoleCancelEventHandler CancelCallbacks;

			internal ControlCDelegateData (ConsoleSpecialKey controlKey, ConsoleCancelEventHandler cancelCallbacks)
			{
				this.ControlKey = controlKey;
				this.CancelCallbacks = cancelCallbacks;
				this.CompletionEvent = new ManualResetEvent (false);
			}
		}

		internal sealed class ControlCHooker : CriticalFinalizerObject
		{
			private bool _hooked;

			[SecurityCritical]
			private Win32Native.ConsoleCtrlHandlerRoutine _handler;

			[SecurityCritical]
			internal ControlCHooker ()
			{
				this._handler = new Win32Native.ConsoleCtrlHandlerRoutine (Console.BreakEvent);
			}

			~ControlCHooker ()
			{
				this.Unhook ();
			}

			[SecuritySafeCritical]
			internal void Hook ()
			{
				if (!this._hooked)
				{
					if (!Win32Native.SetConsoleCtrlHandler (this._handler, true))
					{
						__Error.WinIOError ();
					}
					this._hooked = true;
				}
			}

			[ReliabilityContract (Consistency.WillNotCorruptState, Cer.Success), SecuritySafeCritical]
			internal void Unhook ()
			{
				if (this._hooked)
				{
					if (!Win32Native.SetConsoleCtrlHandler (this._handler, false))
					{
						__Error.WinIOError ();
					}
					this._hooked = false;
				}
			}
		}

		[Flags]
		internal enum ControlKeyState
		{
			RightAltPressed = 1,
			LeftAltPressed = 2,
			RightCtrlPressed = 4,
			LeftCtrlPressed = 8,
			ShiftPressed = 16,
			NumLockOn = 32,
			ScrollLockOn = 64,
			CapsLockOn = 128,
			EnhancedKey = 256
		}*/
	}
}

using System.Runtime.InteropServices;

namespace KeyLogger
{
    class KeyState
    {
        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(int keyNumber);
    }
}

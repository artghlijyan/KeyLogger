using System.Runtime.InteropServices;

namespace KeyLogger
{
    static class KeyState
    {
        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(int keyNumber);
    }
}

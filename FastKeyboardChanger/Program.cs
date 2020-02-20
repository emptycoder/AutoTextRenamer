using FastKeyboardChanger.Utils;
using System;
using System.Windows.Forms;

namespace FastKeyboardChanger
{
    static class Program
    {
        private static bool ctrlTapped = false;
        static KeyHook keyHook = new KeyHook();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            keyHook.KeyDownHandler += KeyHook_KeyDownHandler;
            keyHook.KeyUpHandler += KeyHook_KeyUpHandler;
            Application.Run();
        }

        private static bool KeyHook_KeyUpHandler(Keys key)
        {
            if (key == Keys.LControlKey)
            {
                ctrlTapped = false;
            }
            else if (key == Keys.B && ctrlTapped)
            {
                return true;
            }

            return false;
        }

        private static bool KeyHook_KeyDownHandler(Keys key)
        {
            if (key == Keys.LControlKey)
            {
                ctrlTapped = true;
            }
            else if (key == Keys.B && ctrlTapped)
            {
                if (Clipboard.ContainsText(TextDataFormat.Text))
                {
                    string clipboardText = Clipboard.GetText(TextDataFormat.UnicodeText);
                    Clipboard.SetText(Mapping.EnToRu(clipboardText));
                    keyHook.SendWait("^{v}");
                }
                return true;
            }

            return false;
        }
    }
}

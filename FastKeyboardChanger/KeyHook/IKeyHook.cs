using System.Windows.Forms;

namespace FastKeyboardChanger
{
    public delegate bool KeyHandler(Keys key);
    public interface IKeyHook
    {
        event KeyHandler KeyUpHandler;
        event KeyHandler KeyDownHandler;
        // Emulate key press with pausing key hook
        void SendKeysWait(string keys);
    }
}

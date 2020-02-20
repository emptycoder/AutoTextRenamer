using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FastKeyboardChanger
{
    public partial class KeyHook : IKeyHook, IDisposable
    {
        public bool isKeyHookActive { get; private set; } = true;
        private const int WH_KEYBOARD_LL = 13;
        private IntPtr _hookID = IntPtr.Zero;
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private Type keysType = typeof(Keys);

        static readonly object mouseDownEventKey = new object();
        static readonly object mouseUpEventKey = new object();
        private EventHandlerList listEventDelegates = new EventHandlerList();
        private Dictionary<IntPtr, object> events = new Dictionary<IntPtr, object>();
        public event KeyHandler KeyUpHandler
        {
            add
            {
                listEventDelegates.AddHandler(mouseDownEventKey, value);
            }
            remove
            {
                listEventDelegates.RemoveHandler(mouseDownEventKey, value);
            }
        }
        public event KeyHandler KeyDownHandler
        {
            add
            {
                listEventDelegates.AddHandler(mouseUpEventKey, value);
            }
            remove
            {
                listEventDelegates.RemoveHandler(mouseUpEventKey, value);
            }
        }

        public KeyHook()
        {
            events.Add((IntPtr)257, mouseDownEventKey);
            events.Add((IntPtr)256, mouseUpEventKey);
            events.Add((IntPtr)260, mouseDownEventKey);
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    _hookID = SetWindowsHookEx(WH_KEYBOARD_LL, HookCallback,
                        GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        private IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if ((isKeyHookActive && nCode >= 0) && events.TryGetValue(wParam, out object typeEvent))
            {
                int key = Marshal.ReadInt32(lParam);
                KeyHandler keyHandler = listEventDelegates[typeEvent] as KeyHandler;
                if (Enum.IsDefined(keysType, key) && keyHandler != null && keyHandler.Invoke((Keys)key))
                {
                    return (IntPtr)1;
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public void SendKeysWait(string keys)
        {
            isKeyHookActive = false;
            SendKeys.SendWait(keys);
            isKeyHookActive = true;
        }

        public void Dispose()
        {
            UnhookWinEvent(_hookID);
        }
    }
}

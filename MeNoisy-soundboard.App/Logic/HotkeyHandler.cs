using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;

namespace MeNoisySoundboard.App.Logic
{
    // Based on https://gist.github.com/Stasonix/3181083
    public class HotkeyHandler
    {
        public static HashSet<Key> KeysDown = new HashSet<Key>();
        public static event Action<HashSet<Key>>? OnGlobalHotkey;

        #region Win32 API

        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        const int WH_KEYBOARD_LL = 13;  // Keyboard events
        const int WM_KEYDOWN = 0x100;   // Key down
        const int WM_KEYUP = 0x0101;    // Key up

        private static IntPtr _hhok = IntPtr.Zero;

        private static IntPtr hookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Key key = KeyInterop.KeyFromVirtualKey(vkCode);
                if (wParam == (IntPtr)WM_KEYDOWN && !KeysDown.Contains(key))
                {
                    KeysDown.Add(key);
                    OnGlobalHotkey?.Invoke(KeysDown);
                }
                else if (wParam == (IntPtr)WM_KEYUP && KeysDown.Contains(key))
                {
                    KeysDown.Remove(key);
                    OnGlobalHotkey?.Invoke(KeysDown);
                }
                
                // Not handled : don't block user inputs
                return (IntPtr)0;
            }
            else
                return CallNextHookEx(_hhok, code, (int)wParam, lParam);
        }

        #endregion

        public static void SetHook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            _hhok = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, hInstance, 0);
        }

        public static void UnHook()
        {
            UnhookWindowsHookEx(_hhok);
        }
    }
}

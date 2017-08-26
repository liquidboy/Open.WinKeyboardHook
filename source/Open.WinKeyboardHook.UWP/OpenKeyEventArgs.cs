using System;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Open.WinKeyboardHook
{
    public class OpenKeyEventArgs : ICoreWindowEventArgs
    {
        public bool Handled { get; set; }
        public VirtualKey VirtualKeys { get; set; }

        public OpenKeyEventArgs(VirtualKey keys)
        {
            VirtualKeys = keys;
        }
    }
}
//
// Authors:
//   Lucas Ontivero lucasontivero@gmail.com
//
// Copyright (C) 2014 Lucas Ontivero
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Open.WinKeyboardHook
{
    public sealed class KeyboardInterceptor : IKeyboardInterceptor
    {
        //public event EventHandler<OpenKeyEventArgs> KeyDown;
        public event EventHandler<OpenKeyEventArgs> KeyPress;
        //public event EventHandler<OpenKeyEventArgs> KeyUp;

        private bool _isInitialized = false;

        private bool isDownControl = false;
        private bool isDownShift = false;
        private bool isDownAlt = false;
        private bool isDownWin = false;

        public void StartCapturing()
        {
            HookKeyboard();
        }

        public void StopCapturing()
        {
            UnhookKeyboard();
        }

        private void HookKeyboard()
        {
            if (_isInitialized) return;
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
            _isInitialized = true;
        }
        private void UnhookKeyboard() {
            if (!_isInitialized) return;
            Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp -= CoreWindow_KeyUp;
            _isInitialized = false;
        }

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            var keys = BuildKeyData(args.VirtualKey, false);
            //KeyUp?.Invoke(null, new OpenKeyEventArgs(keys));
            KeyPress?.Invoke(null, new OpenKeyEventArgs(keys));
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            var keys = BuildKeyData(args.VirtualKey, true);
            //KeyDown?.Invoke(null, new OpenKeyEventArgs(keys));
            KeyPress?.Invoke(null, new OpenKeyEventArgs(keys));
        }

        private VirtualKey BuildKeyData(VirtualKey virtualKeyCode, bool isDown = true)
        {
            if (virtualKeyCode == VirtualKey.Control) isDownControl = isDown;
            if (virtualKeyCode == VirtualKey.Shift) isDownShift = isDown;
            if (virtualKeyCode == VirtualKey.Menu) isDownAlt = isDown;
            if (virtualKeyCode == VirtualKey.LeftWindows) isDownWin = isDown;

            return virtualKeyCode |
                   (isDownControl ? VirtualKey.Control : VirtualKey.None) |
                   (isDownShift ? VirtualKey.Shift : VirtualKey.None) |
                   (isDownAlt ? VirtualKey.Menu : VirtualKey.None) |
                   (isDownWin ? VirtualKey.LeftWindows : VirtualKey.None);
        }
    }

}
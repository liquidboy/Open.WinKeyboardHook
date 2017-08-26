using System;
using Windows.UI.Core;

namespace Open.WinKeyboardHook
{
    public interface IKeyboardInterceptor
    {
        void StartCapturing();
        void StopCapturing();
        //event EventHandler<OpenKeyEventArgs> KeyDown;
        //event EventHandler<OpenKeyEventArgs> KeyUp;
        event EventHandler<OpenKeyEventArgs> KeyPress;
    }
}
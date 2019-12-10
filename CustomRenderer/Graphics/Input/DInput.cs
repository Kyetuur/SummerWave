using SummerWave.Renderer.System;
using SharpDX;
using SharpDX.DirectInput;
using System;

namespace SummerWave.Renderer.Graphics.Input
{
    public class DInput
    {
        public KeyboardState _KeyboardState;
        public DirectInput _DirectInput { get; set; }
        public Keyboard Keyboard { get; set; }

        internal void Initialize(DSystemConfiguration configuration, IntPtr windowsHandle)
        {
            _DirectInput = new DirectInput();

            Keyboard = new Keyboard(_DirectInput);
            Keyboard.Properties.BufferSize = 256;
            Keyboard.SetCooperativeLevel(windowsHandle, CooperativeLevel.Foreground | CooperativeLevel.NonExclusive);

            try
            {
                Keyboard.Acquire();
            }
            catch (SharpDXException ex)
            {
                throw ex;
            }
        }
        public void Shutdown()
        {
            // Release the keyboard.
            Keyboard?.Unacquire();
            Keyboard?.Dispose();
            Keyboard = null;
            // Release the main interface to direct input.
            _DirectInput?.Dispose();
            _DirectInput = null;
        }

    }
}
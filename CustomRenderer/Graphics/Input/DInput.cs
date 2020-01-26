using SummerWave.Renderer.System;
using SharpDX;
using SharpDX.DirectInput;
using System;

namespace SummerWave.Renderer.Graphics.Input
{
    public class DInput
    {
        public KeyboardState KeyboardState;
        public DirectInput DirectInput { get; set; }
        public Keyboard Keyboard { get; set; }

        internal void Initialize(DSystemConfiguration configuration, IntPtr windowsHandle)
        {
            DirectInput = new DirectInput();
            Keyboard = new Keyboard(DirectInput);
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
    }
}
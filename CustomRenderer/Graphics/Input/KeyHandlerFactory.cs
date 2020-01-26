using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SummerWave.Renderer.Graphics.Input
{
    internal class KeyHandlerFactory
    {
        private readonly DInput _input;

        public KeyHandlerFactory(DInput input)
        {
            _input = input;
        }

        internal IEnumerable<KeyHandler> GetCurrentKeyHandlers()
        {
            var handlers = new HashSet<KeyHandler>();
            try
            {
                var currentState = _input.Keyboard.GetCurrentState();
                if (currentState == null)
                    throw new InvalidOperationException();

                //WSAD Handlers
                if (currentState.PressedKeys.Contains(Key.W))
                    handlers.Add(new WKeyHandler());
                if (currentState.PressedKeys.Contains(Key.S))
                    handlers.Add(new SKeyHandler());
                if (currentState.PressedKeys.Contains(Key.A))
                    handlers.Add(new AKeyHandler());
                if (currentState.PressedKeys.Contains(Key.D))
                    handlers.Add(new DKeyHandler());
                //Arrows Handlers
                if (currentState.PressedKeys.Contains(Key.Left))
                    handlers.Add(new LeftArrowKeyHandler());
                if (currentState.PressedKeys.Contains(Key.Right))
                    handlers.Add(new RightArrowKeyHandler());
                if (currentState.PressedKeys.Contains(Key.Up))
                    handlers.Add(new UpArrowKeyHandler());
                if (currentState.PressedKeys.Contains(Key.Down))
                    handlers.Add(new DownArrowKeyHandler());
                if (currentState.PressedKeys.Contains(Key.Escape))
                    handlers.Add(new EscapeKeyHandler());

                return handlers;
            }
            catch(SharpDX.SharpDXException ex) when (ex.Descriptor == ResultCode.InputLost || ex.Descriptor == ResultCode.NotAcquired) //Lost focusu
            {
                try{
                    _input.Keyboard.Acquire(); // Retry aquire keyboard
                }catch(SharpDX.SharpDXException)
                {//tbh we cannot do anything now
                }
                return Enumerable.Empty<KeyHandler>();
            }
            
        }
    }
}
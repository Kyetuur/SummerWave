using SummerWave.Renderer.Graphics.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerWave.Renderer.Graphics.Input
{
    public class DPositionChangeHandler
    {
        private DPosition _position;

        private DInput _input;

        private KeyHandlerFactory _keyHandlerFactory;
        public DPositionChangeHandler(DPosition position, DInput input)
        {
            _position = position ?? throw new ArgumentNullException(nameof(position));
            _input = input ?? throw new ArgumentNullException(nameof(input));
            _keyHandlerFactory = new KeyHandlerFactory(_input);
        }

        public void Handle(float deltaTime)
        {
            var keyHandlers = _keyHandlerFactory.GetCurrentKeyHandlers();
            foreach (var keyHandler in keyHandlers)
                keyHandler.Handle(_position,deltaTime);
        }

       
    }
}

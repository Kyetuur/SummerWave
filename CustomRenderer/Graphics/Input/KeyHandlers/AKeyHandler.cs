using System;

namespace SummerWave.Renderer.Graphics.Input
{
    internal class AKeyHandler : KeyHandler
    {
        public override void Handle(DPosition position,float deltaTime)
        {
            var m_ForwardSpeed = deltaTime * 0.1f;
            float radians = position.RotationY * 0.0174532925f;
            position.PositionX -= (float)Math.Cos(radians) * m_ForwardSpeed;
            position.PositionZ += (float)Math.Sin(radians) * m_ForwardSpeed;
        }
    }
}
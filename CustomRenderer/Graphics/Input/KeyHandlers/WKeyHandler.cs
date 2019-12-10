using System;

namespace SummerWave.Renderer.Graphics.Input
{
    internal class WKeyHandler : KeyHandler
    { 
        public override void Handle(DPosition position, float deltaTime)
        {
            var m_ForwardSpeed = deltaTime * 0.03f;
            float radiansY = position.RotationY.ToRadians();
            float radiansX = position.RotationX.ToRadians();
            position.PositionX += (float)Math.Sin(radiansY) * (float)Math.Cos(radiansX) * m_ForwardSpeed;
            position.PositionZ += (float)Math.Cos(radiansY) * (float)Math.Cos(radiansX) * m_ForwardSpeed;

            position.PositionY -= (float)Math.Sin(radiansX) * m_ForwardSpeed * 0.3f;
        }
    }
}
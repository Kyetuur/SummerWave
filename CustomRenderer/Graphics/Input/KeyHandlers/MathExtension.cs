using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerWave.Renderer.Graphics.Input
{
    public static class MathExtension
    {
        public static float ToRadians(this float degree)
        {
            return degree * 0.0174532925f;
        }
        public static void PreserveNormalPosition(this DPosition position)
        {
            if (position.RotationY > 360)
                position.RotationY -= 360;
            if (position.RotationX > 90)
                position.RotationX = 90;
            if (position.RotationX < -90)
                position.RotationX = -90;
            if (position.RotationY < -360)
                position.RotationY += 360;
        }
    }
}

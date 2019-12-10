using System;

namespace SummerWave.Renderer.Graphics.Input
{
    public class DPosition 
    {
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float RotationX { get; set; }
        public float RotationY { get; set; }
        public float RotationZ { get; set; }

        public void SetPosition(float x, float y, float z)
        {
            PositionX = x;
            PositionY = y;
            PositionZ = z;
        }
    }
}
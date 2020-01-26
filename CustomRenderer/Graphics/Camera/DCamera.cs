using SharpDX;
using SummerWave.Renderer.Graphics.Input;

namespace SummerWave.Renderer.Graphics.Camera
{
    public class DCamera
    {
        private float PositionX { get; set; }
        private float PositionY { get; set; }
        private float PositionZ { get; set; }
        private float RotationX { get; set; }
        private float RotationY { get; set; }
        private float RotationZ { get; set; }
        public Matrix ViewMatrix { get; private set; }

        public void SetPosition(float x, float y, float z)
        {
            PositionX = x;
            PositionY = y;
            PositionZ = z;
        }
        public void SetRotation(float x, float y, float z)
        {
            RotationX = x;
            RotationY = y;
            RotationZ = z;
        }
        public Vector3 GetPosition()
        {
            return new Vector3(PositionX, PositionY, PositionZ);
        }
        public void Render()
        {
            Vector3 position = new Vector3(PositionX, PositionY, PositionZ);
            Vector3 lookAt = new Vector3(0, 0, 1.0f);
            float pitch = RotationX.ToRadians();
            float yaw = RotationY.ToRadians();
            float roll = RotationZ.ToRadians();
            Matrix rotationMatrix = Matrix.RotationYawPitchRoll(yaw, pitch, roll);
            lookAt = Vector3.TransformCoordinate(lookAt, rotationMatrix);
            lookAt = position + lookAt;
            Vector3 up = Vector3.TransformCoordinate(Vector3.UnitY, rotationMatrix);
            ViewMatrix = Matrix.LookAtLH(position, lookAt, up);
        }
    }
}
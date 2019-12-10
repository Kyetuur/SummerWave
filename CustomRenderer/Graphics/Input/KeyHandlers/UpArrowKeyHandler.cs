namespace SummerWave.Renderer.Graphics.Input
{
    internal class UpArrowKeyHandler : KeyHandler
    {
        public override void Handle(DPosition position, float deltaTime)
        {
            position.RotationX -= deltaTime * 0.3f;
            position.PreserveNormalPosition();
        }
    }
}
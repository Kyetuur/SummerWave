namespace SummerWave.Renderer.Graphics.Input
{
    internal class RightArrowKeyHandler : KeyHandler
    {
        public override void Handle(DPosition position, float deltaTime)
        {
            position.RotationY += deltaTime * 0.3f;
            position.PreserveNormalPosition();
        }
    }
}
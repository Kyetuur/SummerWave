namespace SummerWave.Renderer.Graphics.Input
{
    internal class DownArrowKeyHandler : KeyHandler
    {
        public override void Handle(DPosition position, float deltaTime)
        {
            position.RotationX += deltaTime * 0.3f;
            position.PreserveNormalPosition();
        }
    }
}
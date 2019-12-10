using System;

namespace SummerWave.Renderer.Graphics.Input
{
    internal class EscapeKeyHandler : KeyHandler
    {
        public override void Handle(DPosition position, float deltaTime)
        {
            Environment.Exit(0);
        }
    }
}
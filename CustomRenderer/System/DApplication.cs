using SummerWave.Renderer.Graphics;
using SummerWave.Renderer.Graphics.Camera;
using SummerWave.Renderer.Graphics.Input;
using SummerWave.Renderer.Graphics.Models;
using SharpDX;
using System;
using System.Threading;

namespace SummerWave.Renderer.System
{
    public class DApplication
    {
        // Properties
        public DInput Input { get; private set; }
        private DDX11 D3D { get; set; }
        public DCamera Camera { get; set; }
        public DPosition Position { get; set; }
        public DPositionChangeHandler PositionChangeHandler { get; set; }
        public DSurface Terrain { get; set; }
        public DColorShader ColorShader { get; set; }

        public bool Initialize(DSystemConfiguration configuration, IntPtr windowHandle)
        {
            try
            {
                Input = new DInput();
                Input.Initialize(configuration, windowHandle);

                D3D = new DDX11();
                D3D.Initialize(configuration, windowHandle);

                Camera = new DCamera();
                Camera.SetPosition(0.0f, 0.0f, -1.0f);
                Camera.Render();
                Camera.SetPosition(50.0f, 2.0f, -7.0f);


                Terrain = new DSurface();
                Terrain.Initialize(D3D.Device);

                ColorShader = new DColorShader();
                ColorShader.Initialize(D3D.Device, windowHandle);

                Position = new DPosition();
                Position.SetPosition(Camera.GetPosition().X, Camera.GetPosition().Y, Camera.GetPosition().Z);

                PositionChangeHandler = new DPositionChangeHandler(Position, Input);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Shutdown()
        {
            // Release the position object.
            Position = null;
            // Release the camera object.
            Camera = null;   
  
            // Release the color shader object.
            ColorShader?.ShutDown();
            ColorShader = null;
            // Release the tree object.
            Terrain?.ShutDown();
            Terrain = null;
            // Release the input object.
            Input?.Shutdown();
            Input = null;
            // Release the Direct3D object.
            D3D?.ShutDown();
            D3D = null;
        }
        private void HandleInput(float deltaTime)
        {
            PositionChangeHandler.Handle(deltaTime);
            Camera.SetPosition(Position.PositionX, Position.PositionY, Position.PositionZ);
            Camera.SetRotation(Position.RotationX, Position.RotationY, Position.RotationZ);
        }
        public bool Frame(float frameTime)
        {
            // Do the frame input processing.
            HandleInput(frameTime);

            // Render the graphics.
            if (!RenderGraphics())
                return false;

            return true;
        }
        private bool RenderGraphics()
        {
            // Clear the scene.
            D3D.BeginScene(0.0f, 0.0f, 0.0f, 1.0f);

            // Generate the view matrix based on the camera's position.
            Camera.Render();

            // Get the world, view, projection, and ortho matrices from the camera and Direct3D objects.
            Matrix worldMatrix = D3D.WorldMatrix;
            Matrix cameraViewMatrix = Camera.ViewMatrix;
            Matrix projectionMatrix = D3D.ProjectionMatrix;
            Matrix orthoD3DMatrix = D3D.OrthoMatrix;

            // Render the terrain buffers.
            Terrain.Render(D3D.DeviceContext);

            // Render the model using the color shader.
            if (!ColorShader.Render(D3D.DeviceContext, Terrain.IndexCount, worldMatrix, cameraViewMatrix, projectionMatrix))
                return false;

            // Turn off the Z buffer to begin all 2D rendering.
            D3D.TurnZBufferOff();

            // Turn on the alpha blending before rendering the text.
            D3D.TurnOnAlphaBlending();

        

            // Turn off alpha blending after rendering the text.
            D3D.TurnOffAlphaBlending();

            // Turn the Z buffer back on now that all 2D rendering has completed.
            D3D.TurnZBufferOn();

            // Present the rendered scene to the screen.
            D3D.EndScene();

            return true;
        }
    }
}
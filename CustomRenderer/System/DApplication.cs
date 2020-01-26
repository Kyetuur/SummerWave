using SummerWave.Renderer.Graphics;
using SummerWave.Renderer.Graphics.Camera;
using SummerWave.Renderer.Graphics.Input;
using SummerWave.Renderer.Graphics.Models;
using SharpDX;
using System;
using System.Threading;
using SimulationEngine;

namespace SummerWave.Renderer.System
{
    public class DApplication
    {
        public DInput Input { get; private set; }
        private DDX11 D3D { get; set; }
        public DCamera Camera { get; set; }
        public DPosition Position { get; set; }
        public DPositionChangeHandler PositionChangeHandler { get; set; }
        public DSurface Terrain { get; set; }
        public DColorShader ColorShader { get; set; }

        public bool Initialize(DSystemConfiguration configuration, IntPtr windowHandle,Surface surface)
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
                Camera.SetPosition(50.0f, 2.0f, 10.0f);
                Terrain = new DSurface();
                Terrain.Initialize(D3D.Device, surface);
                ColorShader = new DColorShader();
                ColorShader.Initialize(D3D.Device, windowHandle);
                Position = new DPosition();
                Position.SetPosition(Camera.GetPosition().X, Camera.GetPosition().Y , Camera.GetPosition().Z); // Ustawienie Position == Camera
                Camera.SetRotation(0.32f, -0.9f, 0);
                Position.RotationX = 0.32f;
                Position.RotationY = -90f;
                PositionChangeHandler = new DPositionChangeHandler(Position, Input);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void HandleInput(float deltaTime)
        {
            PositionChangeHandler.Handle(deltaTime);
            Camera.SetPosition(Position.PositionX, Position.PositionY, Position.PositionZ);
            Camera.SetRotation(Position.RotationX, Position.RotationY, Position.RotationZ);
        }
        public void Frame(float frameTime)
        {
            HandleInput(frameTime);
            RenderGraphics(frameTime);
        }
        private void RenderGraphics(double frameTime)
        {
            D3D.BeginScene(0.0f, 0.0f, 0.0f, 1.0f);
            Camera.Render();
            Matrix worldMatrix = D3D.WorldMatrix;
            Matrix cameraViewMatrix = Camera.ViewMatrix;
            Matrix projectionMatrix = D3D.ProjectionMatrix;
            Terrain.Render(D3D.DeviceContext,frameTime);
            ColorShader.Render(D3D.DeviceContext, Terrain.IndexCount, worldMatrix, cameraViewMatrix, projectionMatrix);
            D3D.EndScene();
        }
    }
}
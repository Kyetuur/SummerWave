using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Runtime.InteropServices;

namespace SummerWave.Renderer.Graphics.Models
{
    public class DSurface
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct DVertexType
        {
            internal Vector3 position;
            internal Vector4 color;
        }

        private int Resolution;
        private SharpDX.Direct3D11.Buffer VertexBuffer { get; set; }
        private SharpDX.Direct3D11.Buffer IndexBuffer { get; set; }
        private int VertexCount { get; set; }
        public int IndexCount { get; private set; }

        private SimulationEngine.Surface _surface;
        private SharpDX.Direct3D11.Device _device;

        public DSurface() { }

        public void Initialize(SharpDX.Direct3D11.Device device, SimulationEngine.Surface surface)
        {
            Resolution = surface.Resolution;
            _surface = surface;
            InitializeBuffers(device);
        }
        private void InitializeBuffers(SharpDX.Direct3D11.Device device)
        {
            _device = device;
            try
            {
                DVertexType[] vertices = GetVertexBuffer();
                int[] indices = GetIndexBuffer();
                VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, vertices);
                IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, indices);
            }
            catch
            {
                throw new BufferNotInitializedException();
            }
        }
        private DVertexType[] GetVertexBuffer()
        {
            VertexCount = Resolution * Resolution * 8;
            IndexCount = VertexCount;
            int[] indices = new int[IndexCount];
            DVertexType[] vertices = new DVertexType[VertexCount];
            int index = 0;
            for (int j = 0; j < Resolution; j++)
            {
                for (int i = 0; i < Resolution; i++)
                {
                    float positionX = (float)i;
                    float positionZ = (float)j;
                    vertices[index].position = new Vector3(positionX, _surface.Grid[i][j].Height/100, positionZ);
                    vertices[index].color = new Vector4(_surface.Grid[i][j].Height / 100 + 0.5f, 0,0,1f);
                    index++;
                }
            }
            return vertices;
        }
        private int[] GetIndexBuffer()
        {
            var indices = new int[IndexCount];
            int index = 0;

            for (int x = 0; x < Resolution - 1; x++)
            {
                for (int z = 0; z < Resolution - 1; z++)
                {
                    int offset = x * Resolution + z;
                    indices[index] = (short)(offset + 0);
                    indices[index + 1] = (short)(offset + 1);
                    indices[index + 2] = (short)(offset + Resolution);
                    indices[index + 3] = (short)(offset + 1);
                    indices[index + 4] = (short)(offset + Resolution + 1);
                    indices[index + 5] = (short)(offset + Resolution);
                    index += 6;
                }
            }
            return indices;
        }
        public void ShutDown()
        {
            ShutdownBuffers();
        }
        private void ShutdownBuffers()
        {
            IndexBuffer?.Dispose();
            IndexBuffer = null;
            VertexBuffer?.Dispose();
            VertexBuffer = null;
        }
        public void Render(DeviceContext deviceContext, double frameTime)
        {
            var newPoints = _surface.Recalculate(frameTime);
            RecalculateVertexBuffors(newPoints);

            RenderBuffers(deviceContext);
        }
        private void RecalculateVertexBuffors(global::System.Collections.Generic.List<global::System.Collections.Generic.List<SimulationEngine.SurfacePoint>> newPoints)
        {
            try
            {
                VertexBuffer.Dispose();
                var newBuffer = GetVertexBuffer();
                VertexBuffer = SharpDX.Direct3D11.Buffer.Create(_device, BindFlags.VertexBuffer, newBuffer);
            }
            catch
            {
                throw new BufferNotInitializedException();
            }
        }
        private void RenderBuffers(DeviceContext deviceContext)
        {
            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, Utilities.SizeOf<DVertexType>(), 0));
            deviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineStripWithAdjacency;
        }
    }
}
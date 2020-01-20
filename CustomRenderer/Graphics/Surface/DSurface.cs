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

        private int m_TerrainWidth, m_TerrainHeight;

        private SharpDX.Direct3D11.Buffer VertexBuffer { get; set; }
        private SharpDX.Direct3D11.Buffer IndexBuffer { get; set; }
        private int VertexCount { get; set; }
        public int IndexCount { get; private set; }

        private SimulationEngine.Surface _surface;
        private SharpDX.Direct3D11.Device _device;

        public DSurface() { }

        public void Initialize(SharpDX.Direct3D11.Device device, SimulationEngine.Surface surface)
        {
            m_TerrainWidth =  surface.m_resolution; 
            m_TerrainHeight = surface.m_resolution;
            _surface = surface;
            InitializeBuffers(device);
        }
        private void InitializeBuffers(SharpDX.Direct3D11.Device device)
        {
            _device = device;
            try
            {
                
                (int[] indices, DVertexType[] vertices) = GetBuffers();

                VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, vertices);

                IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, indices);

                vertices = null;
                indices = null;

            }
            catch
            {
                throw new BufferNotInitializedException();
            }
        }

        private (int[], DVertexType[]) GetBuffers()
        {
            VertexCount = (m_TerrainWidth - 1) * (m_TerrainHeight - 1) * 8;
            IndexCount = VertexCount;
            int[] indices = new int[IndexCount];
            DVertexType[] vertices = new DVertexType[VertexCount];
            int index = 0;

            for (int j = 0; j < (m_TerrainHeight - 1); j++)
            {
                for (int i = 0; i < (m_TerrainWidth - 1); i++)
                {
                    float positionX = (float)i;
                    float positionZ = (float)j;
                    vertices[index].position = new Vector3(positionX, _surface.grid[i][j].Height /100, positionZ);
                    vertices[index].color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                    indices[index] = index;
                    index++;
                }
            }

            return (indices,vertices);
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
        public void Render(DeviceContext deviceContext,double frameTime)
        {
            var newPoints = _surface.Recalculate(frameTime);
            RecalculateVertexBuffors(newPoints);

            RenderBuffers(deviceContext);
        }

        private void RecalculateVertexBuffors(global::System.Collections.Generic.List<global::System.Collections.Generic.List<SimulationEngine.SurfacePoint>> newPoints)
        {
            try
            {
                VertexCount = (m_TerrainHeight - 1) * (m_TerrainWidth - 1) * 8;
                IndexCount = VertexCount;
                DVertexType[] vertices = new DVertexType[VertexCount];
                int[] indices = new int[IndexCount];
                int index = 0;

                for (int j = 0; j < (m_TerrainHeight - 1); j++)
                {
                    for (int i = 0; i < (m_TerrainWidth - 1); i++)
                    {
                        float positionX = (float)i;
                        float positionZ = (float)j;
                        vertices[index].position = new Vector3(positionX, newPoints[i][j].Height /100, positionZ);
                        vertices[index].color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                        indices[index] = index;
                        index++;
                    }
                }

                VertexBuffer = SharpDX.Direct3D11.Buffer.Create(_device, BindFlags.VertexBuffer, vertices);
                IndexBuffer = SharpDX.Direct3D11.Buffer.Create(_device, BindFlags.IndexBuffer, indices);

                vertices = null;
                indices = null;

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
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.LineList;
        }
    }
}
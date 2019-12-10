using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
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

        public DSurface() { }

        public void Initialize(SharpDX.Direct3D11.Device device)
        {
            m_TerrainWidth =  100; 
            m_TerrainHeight = 100;
            InitializeBuffers(device);
        }
        private void InitializeBuffers(SharpDX.Direct3D11.Device device)
        {
            try
            {
                VertexCount = (m_TerrainWidth - 1) * (m_TerrainHeight - 1) * 8;
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
                        vertices[index].position = new Vector3(positionX, 0.0f, positionZ);
                        vertices[index].color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
                        indices[index] = index;
                        index++;
                    }
                }

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
        public void Render(DeviceContext deviceContext)
        {
            RenderBuffers(deviceContext);
        }
        private void RenderBuffers(DeviceContext deviceContext)
        {
            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, Utilities.SizeOf<DVertexType>(), 0));
            deviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.PointList;
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace SummerWave.Renderer.Graphics.Models
{
    [Serializable]
    internal class DDeviceNotInitialized : ResourceNotInitializedException
    {
        public DDeviceNotInitialized() : this ("DirectX Device not initialized! ")
        {
        }

        public DDeviceNotInitialized(string message) : base(message)
        {
        }

        public DDeviceNotInitialized(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DDeviceNotInitialized(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
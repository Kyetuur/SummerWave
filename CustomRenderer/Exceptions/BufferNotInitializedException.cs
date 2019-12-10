using System;
using System.Runtime.Serialization;

namespace SummerWave.Renderer.Graphics.Models
{
    [Serializable]
    internal class BufferNotInitializedException : ResourceNotInitializedException
    {
        public BufferNotInitializedException() : this ("Buffer not initialized!")
        {
        }

        public BufferNotInitializedException(string message) : base(message)
        {
        }

        public BufferNotInitializedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BufferNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
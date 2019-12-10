using System;
using System.Runtime.Serialization;

namespace SummerWave.Renderer.Graphics.Models
{
    [Serializable]
    internal class ShaderNotInitializedException : ResourceNotInitializedException
    {
        public ShaderNotInitializedException() : this("Shader not initialized!")
        {
        }

        public ShaderNotInitializedException(string message) : base(message)
        {
        }

        public ShaderNotInitializedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShaderNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
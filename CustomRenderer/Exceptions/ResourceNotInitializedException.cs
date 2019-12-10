using System;
using System.Runtime.Serialization;

namespace SummerWave.Renderer.Graphics.Models
{
    internal abstract class ResourceNotInitializedException : Exception
    {
        public ResourceNotInitializedException()
        {
        }

        public ResourceNotInitializedException(string message) : base(message)
        {
        }

        public ResourceNotInitializedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResourceNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using SummerWave.Renderer.Graphics.Models;
using System;
using System.Runtime.Serialization;

namespace SummerWave.Renderer.System
{
    [Serializable]
    internal class InputNotInitializedException : ResourceNotInitializedException
    {
        public InputNotInitializedException() : this("Input not initialized!")
        {
        }

        public InputNotInitializedException(string message) : base(message)
        {
        }

        public InputNotInitializedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InputNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
namespace TinyOData.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class TinyODataApplyException : Exception
    {
        public TinyODataApplyException()
        {
        }

        public TinyODataApplyException(string message)
            : base(message)
        {
        }

        public TinyODataApplyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected TinyODataApplyException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
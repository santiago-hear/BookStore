using System.Runtime.Serialization;

namespace BookStore.Model.Exceptions
{
    public class MaxLimitReachedException : Exception
    {
        public MaxLimitReachedException(string message) : base(message) { }
        protected MaxLimitReachedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}

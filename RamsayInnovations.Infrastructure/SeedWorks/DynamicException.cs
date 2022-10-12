using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace RamsayInnovations.Infrastructure
{
    public static class DynamicException
    {
    
        public static string Formatted(DbUpdateException innerException)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");
            try
            {
                foreach (var result in innerException.Entries)
                {
                    builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }
            builder.Append(ObtenerExcepcion(innerException.InnerException));
            return builder.ToString();
        }
        private static string ObtenerExcepcion(Exception InnerException)
        {
            if (InnerException.InnerException != null)
            {
                return ObtenerExcepcion(InnerException.InnerException);
            }
            else
            {
                return InnerException.Message;
            }
        }
    }
    [Serializable]
    public class DynamicFormattedException : Exception
    {
        protected DynamicFormattedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public DynamicFormattedException() : base() { }
        public DynamicFormattedException(string message) : base(message) { }
        public DynamicFormattedException(string message, Exception innerException) : base(message, innerException) { }
    }
}

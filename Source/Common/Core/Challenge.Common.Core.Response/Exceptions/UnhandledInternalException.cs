namespace Challenge.Common.Core.Response.Exceptions
{
    public class UnhandledInternalException : Exception
    {
        public UnhandledInternalException()
            : base("Unhandled internal server error.")
        {
        }
    }
}

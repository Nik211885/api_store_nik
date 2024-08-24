namespace ApplicationCore.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string mss) : base(mss) { }
    }
}

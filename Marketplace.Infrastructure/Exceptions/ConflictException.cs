namespace Marketplace.Infrastructure.Exceptions
{
    public class ConflictException : BaseAppException
    {
        public ConflictException(string message)
            : base("A conflict error occured", new ErrorDataObject(message, "conflict"))
        {
            this.StatusCode = System.Net.HttpStatusCode.Conflict;
        }
    }
}

namespace Marketplace.Infrastructure.Exceptions
{
    public class NotFoundException : BaseAppException
    {
        public NotFoundException(string message)
            : base("The Entity wasn't found", new ErrorDataObject(message, "not_found"))
        {
            this.StatusCode = System.Net.HttpStatusCode.NotFound;
        }
    }
}

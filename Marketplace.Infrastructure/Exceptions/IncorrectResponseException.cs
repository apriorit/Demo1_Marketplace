namespace Marketplace.Infrastructure.Exceptions
{
    public class IncorrectResponseException : BaseAppException
    {
        public IncorrectResponseException(string message)
            : base("Incorrect Response", new ErrorDataObject(message, "Incorrect_response"))
        {
            this.StatusCode = System.Net.HttpStatusCode.ExpectationFailed;
        }
    }
}

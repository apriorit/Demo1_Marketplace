namespace Marketplace.Api.Helpers
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public ErrorResponse(string message, object errorDate = null)
        {
            Message = message;
            Data = errorDate;
        }
    }
}

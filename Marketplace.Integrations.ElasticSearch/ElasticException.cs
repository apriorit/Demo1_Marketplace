using Marketplace.Infrastructure.Exceptions;

namespace Marketplace.Integrations.ElasticSearch
{
    public class ElasticException : BaseAppException
    {
        public ElasticException(string message)
            : base(message, new ErrorDataObject(message, "elastic"))
        {
            this.StatusCode = System.Net.HttpStatusCode.NotFound;
        }
    }
}

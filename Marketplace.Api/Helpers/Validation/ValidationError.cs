using Newtonsoft.Json;

namespace Marketplace.Api.Helpers.Validation
{
    /// <summary>
    /// Class for validation error
    /// </summary>
    public class ValidationError
    {
        /// <summary>
        /// Name of field for whick validation failed
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        /// <summary>
        /// Description of validation error.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Initializes a new instance of the of the <see cref="ValidationError"/> class.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="message"></param>
        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}

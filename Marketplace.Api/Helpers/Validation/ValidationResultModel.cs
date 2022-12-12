using Microsoft.AspNetCore.Mvc.ModelBinding;
using Marketplace.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Api.Helpers.Validation
{
    /// <summary>
    /// Represents result of model validation
    /// </summary>
    public class ValidationResultModel : ErrorDataObject
    {
        /// <summary>
        /// List of errors occured while validation
        /// </summary>
        public List<ValidationError> Error { get; }

        public ValidationResultModel(ModelStateDictionary modelState)
            : base("One or more validation errors occured", "validation")
        {
            Error = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
    }
}

using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamsayInnovations.WebApi.Extensions
{
    public static class FluentValidacionExtensions
    {
        public static string ToText(this ValidationResult validationResult)
        {
            switch (validationResult.IsValid)
            {
                case true:
                    return "Ok";
                case false:
                    StringBuilder sb = new StringBuilder();
                    foreach (var error in validationResult.Errors)
                    {
                        sb.Append($"\n{error.ErrorMessage}\n");
                    }
                    string Error = sb.ToString();
                    return Error;
            }
            return "";
        }
    }
}

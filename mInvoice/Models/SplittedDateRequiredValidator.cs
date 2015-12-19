using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mInvoice.Models
{
    public sealed class SplittedDateRequiredValidator : DataAnnotationsModelValidator<DateRequiredAttribute>
    {
        private string _message;
        private string _dayField;
        private string _monthField;
        private string _yearField;

        public SplittedDateRequiredValidator(ModelMetadata metadata, ControllerContext context, DateRequiredAttribute attribute)
            : base(metadata, context, attribute)
        {
            _message = attribute.ErrorMessage;
            _dayField = metadata.PropertyName + "_Day";
            _monthField = metadata.PropertyName + "_Month";
            _yearField = metadata.PropertyName + "_Year";
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            ModelClientValidationRule rule = new ModelClientValidationRule
            {
                ErrorMessage = _message,
                ValidationType = "splittedDateRequiredValidator"
            };

            rule.ValidationParameters.Add("dayFieldId", _dayField);
            rule.ValidationParameters.Add("monthFieldId", _monthField);
            rule.ValidationParameters.Add("yearFieldId", _yearField);

            return new[] { rule };
        }
    }
}
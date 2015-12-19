using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace mInvoice.Models
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class DateRequiredAttribute : ValidationAttribute
    {
        public DateRequiredAttribute() : base() { }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentUICulture, ErrorMessageString, name);
        }
        public override bool IsValid(object value)
        {
            DateTime dateTime = (DateTime)value;
            return (dateTime != DateTime.MinValue && dateTime != DateTime.MaxValue);
        }
    }
}
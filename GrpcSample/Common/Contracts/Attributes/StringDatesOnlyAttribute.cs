using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Common.Contracts.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class StringDatesOnlyAttribute : ValidationAttribute
    {
        public StringDatesOnlyAttribute()
        {
            ErrorMessage = "This property supports string dates only! Use date format: dd.MM.yyyy, example: 12.02.2022";
        }


        // OVERRIDE ///////////////////////////////////////////////////////////////////////////////
        public override Boolean IsValid(Object value)
        {
            if (value == null)
                return true;

            if (!(value is String dateTimeStr))
                return false;

            if (String.IsNullOrEmpty(dateTimeStr))
                return true;

            if (!DateTime.TryParseExact(dateTimeStr, "dd.MM.yyyy", null, DateTimeStyles.AssumeUniversal, out DateTime dateTime))
                return false;

            return true;
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace Common.Contracts.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateTimeRangeAttribute : ValidationAttribute
    {
        public DateTimeRangeAttribute()
        {
            From = new DateTime(2020, 1, 1);
            To = DateTime.Today + TimeSpan.FromDays(1);
        }


        // PROPERTIES /////////////////////////////////////////////////////////////////////////////
        public DateTime From { get; set; }
        public DateTime To { get; set; }


        // OVERRIDE ///////////////////////////////////////////////////////////////////////////////
        public override Boolean IsValid(Object value)
        {
            ErrorMessage = $"This property is not type of \"{typeof(DateTime)}\"";

            if (!(value is DateTime dateTime))
                return false;

            ErrorMessage = $"This property must be in range from \"{From:d}\" to \"{To:d}\", but current value is \"{dateTime:d}\"";

            if (dateTime < From || dateTime > To)
                return false;

            return true;
        }
    }
}
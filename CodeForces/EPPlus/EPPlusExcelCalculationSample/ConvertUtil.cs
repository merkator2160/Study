using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EPPlusExcelFormDemo
{
    internal static class ConvertUtil
    {
        internal static Boolean IsNumeric(Object candidate)
        {
            if (candidate == null) return false;
            return (candidate.GetType().IsPrimitive || candidate is Double || candidate is Decimal || candidate is DateTime || candidate is TimeSpan || candidate is Int64);
        }

        internal static Boolean IsNumericString(Object candidate)
        {
            if (candidate != null)
            {
                return Regex.IsMatch(candidate.ToString(), @"^[\d]+(\,[\d])?");
            }
            return false;
        }

        /// <summary>
        /// Convert an object value to a double 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="ignoreBool"></param>
        /// <returns></returns>
        internal static Double GetValueDouble(Object v, Boolean ignoreBool = false)
        {
            Double d;
            try
            {
                if (ignoreBool && v is Boolean)
                {
                    return 0;
                }
                if (IsNumeric(v))
                {
                    if (v is DateTime)
                    {
                        d = ((DateTime)v).ToOADate();
                    }
                    else if (v is TimeSpan)
                    {
                        d = DateTime.FromOADate(0).Add((TimeSpan)v).ToOADate();
                    }
                    else
                    {
                        d = Convert.ToDouble(v, CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    d = 0;
                }
            }

            catch
            {
                d = 0;
            }
            return d;
        }
    }
}

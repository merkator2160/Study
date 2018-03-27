
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Collections;
using System.Configuration;

namespace Notificator.Core.Security
{
    public static class SecurityUtils
    {
        internal static bool GetBooleanValue(NameValueCollection config, string valueName, bool defaultValue)
        {
            string sValue = config[valueName];
            if (sValue == null)
            {
                return defaultValue;
            }

            bool result;
            if (bool.TryParse(sValue, out result))
            {
                return result;
            }
            else
            {
                throw new ProviderException("Value_must_be_boolean, " + valueName);
            }
        }

        internal static int GetIntValue(NameValueCollection config, string valueName, int defaultValue, bool zeroAllowed, int maxValueAllowed)
        {
            string sValue = config[valueName];

            if (sValue == null)
            {
                return defaultValue;
            }

            int iValue;
            if (!Int32.TryParse(sValue, out iValue))
            {
                if (zeroAllowed)
                {
                    throw new ProviderException("Value_must_be_non_negative_integer, " + valueName);
                }

                throw new ProviderException("Value_must_be_positive_integer, " + valueName);
            }

            if (zeroAllowed && iValue < 0)
            {
                throw new ProviderException("Value_must_be_non_negative_integer, " + valueName);
            }

            if (!zeroAllowed && iValue <= 0)
            {
                throw new ProviderException("Value_must_be_positive_integer, " + valueName);
            }

            if (maxValueAllowed > 0 && iValue > maxValueAllowed)
            {
                throw new ProviderException("Value_too_big, " + valueName + ", max: " + maxValueAllowed.ToString(CultureInfo.InvariantCulture));
            }

            return iValue;
        }

        internal static void CheckParameter(ref string param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize, string paramName)
        {
            if (param == null)
            {
                if (checkForNull)
                {
                    throw new ArgumentNullException(paramName);
                }
                return;
            }

            param = param.Trim();
            if (checkIfEmpty && param.Length < 1)
            {
                throw new ArgumentException("Parameter_can_not_be_empty, paramName", paramName);
            }

            if (maxSize > 0 && param.Length > maxSize)
            {
                throw new ArgumentException(paramName + " Parameter_too_long, max:" + maxSize.ToString(CultureInfo.InvariantCulture), paramName);
            }

            if (checkForCommas && param.Contains(","))
            {
                throw new ArgumentException("Parameter_can_not_contain_comma, " + paramName, paramName);
            }
        }

        internal static void CheckArrayParameter(ref string[] param, bool checkForNull, bool checkIfEmpty, bool checkForCommas, int maxSize, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (param.Length < 1)
            {
                throw new ArgumentException("Parameter_array_empty, paramName, " + paramName);
            }

            Hashtable values = new Hashtable(param.Length);
            for (int i = param.Length - 1; i >= 0; i--)
            {
                CheckParameter(ref param[i], checkForNull, checkIfEmpty, checkForCommas, maxSize,
                    paramName + "[ " + i.ToString(CultureInfo.InvariantCulture) + " ]");
                if (values.Contains(param[i]))
                {
                    throw new ArgumentException("Parameter_duplicate_array_element, " + paramName + ", " + paramName);
                }
                else
                {
                    values.Add(param[i], param[i]);
                }
            }
        }
    }
}


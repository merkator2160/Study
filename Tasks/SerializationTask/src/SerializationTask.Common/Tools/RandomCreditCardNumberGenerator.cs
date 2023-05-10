﻿namespace SerializationTask.Common.Tools
{
    public static class RandomCreditCardNumberGenerator
    {
        /*
         Copy Kev Hunter's changes August 16, 2009 from from https://kevhunter.wordpress.com/2009/08/16/creating-fake-credit-card-numbers/
         Include comments from Zoltan siaynoq(http://en.gravatar.com/siaynoq) April 20, 2011
         Included in GitHub by MNF 31 May 2016.
         MNF 31 May 2016 Added PrefixAndLength struct and methods to generate random numbers of different types in the same call to GetCreditCardNumbers

        This is a port of the port of of the Javascript credit card number generator now in C#
        * by Kev Hunter https://kevhunter.wordpress.com
        * See the license below. Obviously, this is not a Javascript credit card number
         generator. However, The following class is a port of a Javascript credit card
         number generator.
         @author robweber
         Javascript credit card number generator Copyright (C) 2006 Graham King
         graham@darkcoding.net

         This program is free software; you can redistribute it and/or modify it
         under the terms of the GNU General Public License as published by the
         Free Software Foundation; either version 2 of the License, or (at your
         option) any later version.
         This program is distributed in the hope that it will be useful, but
         WITHOUT ANY WARRANTY; without even the implied warranty of
         MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General
         Public License for more details.

         You should have received a copy of the GNU General Public License along
         with this program; if not, write to the Free Software Foundation, Inc.,
         51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
         www.darkcoding.net
        */
        /*
        Example of use:
            var random = new Random();
            var cardsList = random.GetCreditCardNumbers(RandomCreditCardNumberGenerator.BuildPrefixAndLengthArrayForVisaMasterCardAmex(), 10);
        */

        public static String[] AMEX_PREFIX_LIST = new[] { "34", "37" };


        public static String[] DINERS_PREFIX_LIST = new[]
                                                        {
                                                            "300",
                                                            "301", "302", "303", "36", "38"
                                                        };


        public static String[] DISCOVER_PREFIX_LIST = new[] { "6011" };


        public static String[] ENROUTE_PREFIX_LIST = new[]
                                                        {
                                                            "2014",
                                                            "2149"
                                                        };

        public static String[] JCB_PREFIX_LIST = new[]
                                                          {
                                                        "35"
                                                        };


        public static String[] MASTERCARD_PREFIX_LIST = new[]
                                                            {
                                                                "51",
                                                                "52", "53", "54", "55",
                                                                "2221",
                                                                "2222",
                                                                "2223",
                                                                "2224",
                                                                "2225",
                                                                "2226",
                                                                "2227",
                                                                "2228",
                                                                "2229",
                                                                "223",
                                                                "224",
                                                                "225",
                                                                "226",
                                                                "227",
                                                                "228",
                                                                "229",
                                                                "23",
                                                                "24",
                                                                "25",
                                                                "26",
                                                                "270",
                                                                "271",
                                                                "2720"
                                                            };


        public static String[] VISA_PREFIX_LIST = new[]
                                                    {
                                                        "4539",
                                                        "4556", "4916", "4532", "4929", "40240071", "4485", "4716", "4"
                                                    };


        public static String[] VOYAGER_PREFIX_LIST = new[] { "8699" };

        public struct PrefixAndLength
        {
            public PrefixAndLength(String prefix, Int32 length)
            {
                Prefix = prefix;
                Length = length;
            }
            public String Prefix { get; set; }
            public Int32 Length { get; set; }
        }

        public static IEnumerable<PrefixAndLength> BuildPrefixAndLengthList(String[] prefixList, Int32 length)
        {
            var list = from p in prefixList select new PrefixAndLength(p, length);
            return list;
        }
        /// <summary>
        /// This is an example how BuildPrefixAndLengthList can be used
        /// </summary>
        /// <returns></returns>
        public static PrefixAndLength[] BuildPrefixAndLengthArrayForVisaMasterCardAmex()
        {
            var list = BuildPrefixAndLengthList(VISA_PREFIX_LIST, 16)
                .Union(BuildPrefixAndLengthList(MASTERCARD_PREFIX_LIST, 16))
                .Union(BuildPrefixAndLengthList(AMEX_PREFIX_LIST, 15))
                 ;
            return list.ToArray();
        }
        /// <summary>
        /// Better to use extension overload with [this Random random] paramenter. See http://stackoverflow.com/questions/2706500/how-do-i-generate-a-random-int-number-in-c
        /// </summary>
        /// <param name="prefixAndLengthList"></param>
        /// <param name="howMany"></param>
        /// <returns></returns>
        public static IEnumerable<String> GetCreditCardNumbers(PrefixAndLength[] prefixAndLengthList, Int32 howMany)
        {
            var rndGen = new Random();
            return rndGen.GetCreditCardNumbers(prefixAndLengthList, howMany);
        }
        public static IEnumerable<String> GetCreditCardNumbers(this Random random, PrefixAndLength[] prefixAndLengthList, Int32 howMany)
        {
            var result = new Stack<String>();
            for (var i = 0; i < howMany; i++)
            {
                var randomPrefix = random.Next(0, prefixAndLengthList.Length - 1);

                var prefixAndLength = prefixAndLengthList[randomPrefix];

                result.Push(random.CreateFakeCreditCardNumber(prefixAndLength.Prefix, prefixAndLength.Length));
            }

            return result;
        }
        /*
      'prefix' is the start of the  CC number as a string, any number
        private of digits   'length' is the length of the CC number to generate.
     * Typically 13 or  16
        */
        private static String CreateFakeCreditCardNumber(this Random random, String prefix, Int32 length)
        {
            var ccnumber = prefix;
            while (ccnumber.Length < length - 1)
            {
                var rnd = random.NextDouble() * 1.0f - 0f;

                ccnumber += Math.Floor(rnd * 10);
            }


            // reverse number and convert to int
            var reversedCCnumberstring = ccnumber.ToCharArray().Reverse();

            var reversedCCnumberList = reversedCCnumberstring.Select(c => Convert.ToInt32(c.ToString()));

            // calculate sum //Luhn Algorithm http://en.wikipedia.org/wiki/Luhn_algorithm
            var sum = 0;
            var pos = 0;
            var reversedCCnumber = reversedCCnumberList.ToArray();

            while (pos < length - 1)
            {
                var odd = reversedCCnumber[pos] * 2;

                if (odd > 9)
                    odd -= 9;

                sum += odd;

                if (pos != length - 2)
                    sum += reversedCCnumber[pos + 1];

                pos += 2;
            }

            // calculate check digit
            var checkdigit =
                Convert.ToInt32((Math.Floor((Decimal)sum / 10) + 1) * 10 - sum) % 10;

            ccnumber += checkdigit;

            return ccnumber;
        }
        public static IEnumerable<String> GetCreditCardNumbers(String[] prefixList, Int32 length, Int32 howMany)
        {
            var result = new Stack<String>();
            var random = new Random();
            for (var i = 0; i < howMany; i++)
            {
                var randomPrefix = random.Next(0, prefixList.Length - 1);

                if (randomPrefix > 1)  //Why??, is it a bug ? it never will select last element
                {
                    randomPrefix--;
                }

                var ccnumber = prefixList[randomPrefix];

                result.Push(random.CreateFakeCreditCardNumber(ccnumber, length));
            }

            return result;
        }
        public static IEnumerable<String> GenerateMasterCardNumbers(Int32 howMany)
        {
            return GetCreditCardNumbers(MASTERCARD_PREFIX_LIST, 16, howMany);
        }
        public static String GenerateMasterCardNumber()
        {
            return GetCreditCardNumbers(MASTERCARD_PREFIX_LIST, 16, 1).First();
        }
        public static Boolean IsValidCreditCardNumber(String creditCardNumber)
        {
            try
            {
                var reversedNumber = creditCardNumber.ToCharArray().Reverse();

                var mod10Count = 0;
                for (var i = 0; i < reversedNumber.Count(); i++)
                {
                    var augend = Convert.ToInt32(reversedNumber.ElementAt(i).ToString());

                    if ((i + 1) % 2 == 0)
                    {
                        var productstring = (augend * 2).ToString();
                        augend = 0;
                        for (var j = 0; j < productstring.Length; j++)
                        {
                            augend += Convert.ToInt32(productstring.ElementAt(j).ToString());
                        }
                    }

                    mod10Count += augend;
                }

                if (mod10Count % 10 == 0)
                {
                    return true;
                }
            }
            catch { }

            return false;
        }
    }
}
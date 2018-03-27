using System;
using System.Collections.Generic;
using System.Text;

namespace CodeForces.Units
{
    public static class CodeForcesUnit
    {
        public static void Run()
        {
            TanyaAndToysExample();
        }
        private static void TanyaAndToysExample()
        {
            var firstLine = Console.ReadLine();
            var m = Convert.ToInt64(firstLine.Split(' ')[1]);

            var secondLine = Console.ReadLine();
            var strArray = secondLine.Split(' ');
            var array = Array.ConvertAll(strArray, s => Int64.Parse(s));

            var hs = new HashSet<Int64>(array);

            var j = 0;
            var previousToAdd = 0;
            var resultString = new StringBuilder(20000000);
            var resultCount = 0;

            while (m > 0)
            {
                if (!hs.Contains(++j))
                {
                    if (m >= previousToAdd + j)
                    {
                        if (previousToAdd == 0)
                        {
                            previousToAdd = j;
                            continue;
                        }

                        m -= previousToAdd;
                        resultCount++;
                        resultString.Append(previousToAdd);
                        resultString.Append(' ');
                        previousToAdd = j;
                    }
                    else
                    {
                        if (m > j)
                        {
                            previousToAdd = j;
                        }
                        else
                        {
                            m = 0;
                            if (previousToAdd != 0)
                            {
                                resultCount++;
                                resultString.Append(previousToAdd);
                                resultString.Append(' ');
                            }
                        }
                    }
                }
            }

            Console.WriteLine(resultCount);
            Console.WriteLine(resultString);
        }
    }
}
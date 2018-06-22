using System;
using System.Collections.Generic;
using System.Text;

namespace CodeForces.Units
{
	public static class CodeForcesUnit
    {
        public static void Run()
        {
	        XorShortLists();
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
		private static void XorBinaryLists()
	    {
			var a = new Boolean[9] { false, true, false, true, false, false, false, true, true };
		    var b = new Boolean[9] { true, false, false, true, true, true, true, false, false };
		    var c = new Boolean[9];


			for(int i = 0; i < a.Length; i++)
			{
				c[i] = a[i] ^ b[i];
			}

			foreach (var x in c)
		    {
			    Console.WriteLine(x);
		    }
	    }
	    private static void XorShortLists()
	    {
		    var a = new Int16[9] { 4, 25, 136, 54, 42, 223, 57, 74, 5 };
		    var b = new Int16[9] { 237, 0, 255, 5, 12, 128, 93, 32, 44 };
		    var c = new Int16[9];
			
		    for(int i = 0; i < a.Length; i++)
		    {
			    c[i] = (Int16)(a[i] ^ b[i]);
		    }

		    foreach(var x in c)
		    {
			    Console.WriteLine(x);
		    }
	    }
	}
}
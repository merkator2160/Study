using System;
using System.Collections.Generic;

namespace CodeForces.Units
{
    public static class SortedSetUnit
    {
        public static void Run()
        {
            var ss = new SortedSet<Char>();
            var ss1 = new SortedSet<Char>();

            ss.Add('A');
            ss.Add('B');
            ss.Add('C');
            ss.Add('Z');
            ShowColl(ss, "Первая коллекция: ");

            ss1.Add('X');
            ss1.Add('Y');
            ss1.Add('Z');
            ShowColl(ss1, "Вторая коллекция");

            ss.SymmetricExceptWith(ss1);
            ShowColl(ss, "Исключили разноименность (одинаковые элементы) двух множеств: ");

            ss.UnionWith(ss1);
            ShowColl(ss, "Объединение множеств: ");

            ss.ExceptWith(ss1);
            ShowColl(ss, "Вычитание множеств");

            Console.ReadKey();
        }

        private static void ShowColl(IEnumerable<Char> collection, String message)
        {
            Console.WriteLine(message);
            Console.WriteLine(String.Join(" ", collection));
            Console.WriteLine("\n");
        }
    }
}
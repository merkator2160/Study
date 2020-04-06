using CodeForces.Units.Interview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CodeForces.Units.Interview
{
	public static class InterviewUnit
	{
		public static void Run()
		{
			var currentClassTaskMethods = typeof(InterviewUnit).GetMethods().Where(x => x.ToString().Contains("Task")).ToArray();
			foreach(var x in currentClassTaskMethods)
				PrintTaskResult(x);
		}
		private static void PrintTaskResult(MethodInfo methodInfo)
		{
			Console.WriteLine(methodInfo.Name);
			Console.WriteLine();
			methodInfo.Invoke(null, null);
			Console.ReadKey();
			Console.Clear();
		}


		// TASKS //////////////////////////////////////////////////////////////////////////////////
		public static void Task1()
		{
			var a = new A();
			var b = new B();
			var c = new C();
			var d = new D();

			a.Method();
			b.Method();
			c.Method();
			d.Method();

			a = b;
			a.Method();

			a = c;
			a.Method();
			c.Method();

			a = d;
			a.Method();
		}   // Override, new
		public static void Task2()
		{
			var numbers = new List<Int32> { -1, 20634, 523, -634, 434, -2167, 5235 };
			var positiveNumbers = from x in numbers where x > 0 orderby x select x;
			Console.WriteLine(String.Join(", ", positiveNumbers));
		}   // LINQ execution
		public static void Task3()
		{
			Task3Class.TestMethod(new Task3Class.Task3B());
		}   // Static, override
		public static void Task4()
		{
			var entities = new List<Entity>
			{
				new Entity() { Id = 1, Name = "Entity_1"},
				new Entity() { Id = 2, Name = "Entity_2"},
				new Entity() { Id = 3, Name = "Entity_3"},
				new Entity() { Id = 4, Name = "Entity_4"},
				new Entity() { Id = 5, Name = "Entity_5"},
				new Entity() { Id = 6, Name = "Entity_6"},
				new Entity() { Id = 7, Name = "Entity_7"},
			};
			var filtered = from x in entities where x.Id > 3 orderby x.Id descending select x;
			foreach(var x in filtered)
			{
				Console.WriteLine(x.Id);
			}
		}   // LINQ filtration
		public static void Task5()
		{
			var input = new List<Int32> { -1, 2, 3, -4, 5 };

			var result1 = input.Where(x => x > 0).Select(x => x * 2);
			var result2 = input.Select(x => x * 2).Where(x => x > 0);   // low efficiency

			Console.WriteLine(String.Join(" ", result1));
			Console.WriteLine(String.Join(" ", result2));
		}   // LINQ effectiveness
		public static void Task6()
		{
			var input = new List<Int32> { -1, 2, 3, -4, 5 };
			var result = input.Where(x => x > 0);
			input.Remove(3);
			Console.WriteLine(String.Join(", ", result));
		}   // LINQ compilation
		public static void Task7()
		{
			Console.WriteLine(new Task7().StructField.A);
		}   // Internal, default struct constructor
		public static void Task8()
		{
			try
			{
				Console.WriteLine("Try");
				throw new Exception("Exception");
				return;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				Console.WriteLine("Finally");
			}
		}   // Try catch
		public static void Task9()
		{
			var input = "str";

			SupportMethod4(input);
			Console.WriteLine(input);

			Action stringManipulation = () =>
			{
				input += "_test";
			};
			stringManipulation.Invoke();
			Console.WriteLine(input);
		}   // String immutable
		public static void Task10()
		{
			var str1 = "test";
			var str2 = "test";
			var str3 = "test3";

			Console.WriteLine(ReferenceEquals(str1, str2));
			Console.WriteLine(ReferenceEquals(str1, str3));
			Console.WriteLine(ReferenceEquals(str2, str3));
		}   // String internment, string pool
		public static void Task11()
		{
			try
			{
				var e = 2.718;
				Object obj = e;

				Console.WriteLine(obj);
				Console.WriteLine((Int32)obj);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}   // Immutability, pack and unpack
		public static void Task12()
		{
			var x = 5;
			Decimal y = x / 12;
			Console.WriteLine(y);
		}   // Loss of fraction
		public static void Task13()
		{
			var d = 5.15;
			d = d / 0;
			Console.WriteLine(d);

			var f = 5.15f;
			f = f / 0;
			Console.WriteLine(f);

			try
			{
				var dc = 5.12m;
				dc = dc / 0;
				Console.WriteLine(dc);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}   // Division
		public static void Task14()
		{
			Console.WriteLine(1 + 2 + "A");
			Console.WriteLine(1 + "A" + 2);
			Console.WriteLine("A" + 1 + 2);
		}   // Auto cast to string
		public static void Task15()
		{
			Int32 someInt;

			SupportMethod2(out someInt);
			Console.WriteLine(someInt);

			SupportMethod1(ref someInt);
			Console.WriteLine(someInt);

			SupportMethod3(someInt);
			Console.WriteLine(someInt);
		}   // Ref out
		public static void Task16()
		{
			var test = new TryCatchTask();
			try
			{
				test.DoTestAsync();
			}
			catch(ArgumentOutOfRangeException ae)
			{
				Console.WriteLine("First catch");
				Console.WriteLine(ae.Message);
			}
			catch(Exception e)
			{
				Console.WriteLine("Second catch");
				Console.WriteLine(e.Message);
			}
			finally
			{
				Console.WriteLine("Done");
			}
		}   // Try catch async
		public static void Task17()
		{
			Action simpleAction = () => { };
			var array = new[] { 1, 2, 3, 4 };

			foreach(var x in array)
			{
				simpleAction += () => Console.WriteLine(x);
			}
			simpleAction();
			Console.WriteLine();

			array[3] = 1;
			simpleAction();
		}   // NOT Closures, no catching
		public static void Task18()
		{
			var counter = 0;
			SupportMethod5(() => counter++);
			Console.WriteLine(counter.ToString());
		}   // Closures heap box catching 1
		public static void Task19()
		{
			Action simpleAction = () => { };
			for(var i = 0; i < 4; i++)
			{
				simpleAction += () => Console.WriteLine(i);
			}
			simpleAction();
		}   // Closures heap box catching 2
		public static void Task20()
		{
			var delList = new List<Action>();
			for(var i = 0; i < 10; i++)
			{
				delList.Add(() => Console.WriteLine(i));
			}

			foreach(var x in delList)
			{
				x();
			}
		}   // Closures heap box catching 3
		public static void Task21()
		{
			if(SupportMethod6() && SupportMethod7())
			{
				Console.WriteLine("блок if выполнен");
			}
		}   // Methods execution insede IF
		public static void Task22()
		{
			Action a = () => Console.Write("A");
			Action b = () => Console.Write("B");
			Action c = () => Console.Write("C");

			var s = a + b + c + Console.WriteLine;

			s();                  //ABC
			(s - a)();            //BC
			(s - b)();            //AC
			(s - c)();            //AB
			(s - (a + b))();      //C
			(s - (b + c))();      //A
			(s - (a + c))();      //ABC

			s = a + b + a;
			(s - a)();            // AB
		}   // Delegate subtractions


		// SUPPORT FUNCTIONS //////////////////////////////////////////////////////////////////////
		private static void SupportMethod1(ref Int32 value)
		{
			value = 1;
		}
		private static void SupportMethod2(out Int32 value)
		{
			value = 2;
		}
		private static void SupportMethod3(Int32 value)
		{
			value = 0;
		}
		private static void SupportMethod4(String value)
		{
			value += "_test";
		}
		private static void SupportMethod5(Func<Int32> counter)
		{
			for(var i = 0; i < 10; ++i)
				Console.Write("{0}, ", counter());
		}
		private static Boolean SupportMethod6()
		{
			Console.WriteLine(nameof(SupportMethod6));
			return false;
		}
		private static Boolean SupportMethod7()
		{
			Console.WriteLine(nameof(SupportMethod7));
			return true;
		}


		// COMPILATION ERRORS /////////////////////////////////////////////////////////////////////

		//public void OptionalParamFunc(Int32 p1, Int32 p2 = 2, Int32 p3)
		//{

		//}


		//struct Sample
		//{
		//    private Int32 First { get; set; } = 10;
		//    protected Int32 Second;
		//    public Sample()
		//    {
		//        First++;
		//    }
		//}
	}
}
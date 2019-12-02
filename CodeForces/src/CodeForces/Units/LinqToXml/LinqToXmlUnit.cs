using LinqToXml;
using LinqToXml.Models;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace CodeForces.Units.LinqToXml
{
	public static class LinqToXmlUnit
	{
		public static void Run()
		{
			//Example1();
			//Example2();
			//Example3();
			//Example4();
			//Example5();
			//Example6();
			//Example7();
			//Example8();
			//Example9();
			//Example10();
			//Example11();
			//Example12();
			//Example13();
			//Example14();
			//Example15();
			//Example16();
			//Example17();
			//Example18();
			//Example19();
			//Example20();
			//Example21();
			//Example22();
			//Example23();
			//Example24();
			//Example25();
			//Example26();
			//Example27();
			//Example28();
			//Example29();
			//Example30();
			//Example31();
			//Example32();
			//Example33();
			//Example34();
			//Example35();
			//Example36();
			//Example37();
			//Example38();
			//Example39();
			//Example40();
			//Example41();
			//Example42();
			//Example43();
			//Example44();
			//Example45();
			//Example46();
			Example47();
		}


		// EXAMPLES ///////////////////////////////////////////////////////////////////////////////
		private static void Example1()
		{
			var xEmployees =
				new XElement("Employees",
					new XElement("Employee",
						new XAttribute("type", "Programmer"),
						new XElement("FirstName", "Alex"),
						new XElement("LastName", "Erohin")),
					new XElement("Employee",
						new XAttribute("type", "Editor"),
						new XElement("FirstName", "Elena"),
						new XElement("LastName", "Volkova")));

			Console.WriteLine(xEmployees);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example2()
		{
			var xDocument =
				new XDocument(
					new XElement("Employees",
						new XElement("Employee",
							new XAttribute("type", "Programmer"),
							new XElement("FirstName", "Alex"),
							new XElement("LastName", "Erohin")),
						 new XElement("Employee",
							new XAttribute("type", "Editor"),
							new XElement("FirstName", "Elena"),
							new XElement("LastName", "Volkova"))));

			Console.WriteLine(xDocument.ToString());
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example3()
		{
			XNamespace nameSpace = "http://www.professorweb.ru/LINQ";
			var xDocument =
							new XDocument(
								new XElement("Employees",
									// Добавляем пространство имен с префиксом
									new XAttribute(XNamespace.Xmlns + "linq", nameSpace),
									new XElement("Employee",
										new XAttribute("type", "Programmer"),
										new XElement("FirstName", "Alex"),
										new XElement("LastName", "Erohin")),
									new XElement("Employee",
										new XAttribute("type", "Editor"),
										new XElement("FirstName", "Elena"),
										new XElement("LastName", "Volkova"))));

			Console.WriteLine(xDocument.ToString());
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example4()
		{
			var name = new XElement("Name", "Alex");
			Console.WriteLine(name.ToString());
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example5()
		{
			var name = new XElement("Person",
				new XElement("FirstName", "Alex"),
				new XElement("LastName", "Erohin"));

			Console.WriteLine($"XML: \n\n{name}\n\nИзвлекаем значение: {(String)name}");
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example6()
		{
			try
			{
				var smoker = new XElement("Smoker", "Tue");
				//var smoker = new XElement("Smoker", "True");
				Console.WriteLine(smoker);
				Console.WriteLine((Boolean)smoker);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex);
			}

			Console.ReadKey();
			Console.Clear();
		}
		private static void Example7()
		{
			var xDocument =
				new XDocument(
					new XElement("Employees",
						new XElement("Employee",
							new XAttribute("type", "Programmer"),
							new XElement("FirstName", "Alex"),
							new XElement("LastName", "Erohin")),
						new XElement("Employee",
							new XAttribute("type", "Editor"),
							new XElement("FirstName", "Elena"),
							new XElement("LastName", "Volkova"))));

			var elements = xDocument.Element("Employees").Elements("Employee");

			foreach(var x in elements)
				Console.WriteLine($"Исходный элемент: {x.Name} : значение = {x.Value}");

			foreach(var x in elements)
			{
				Console.WriteLine($"Удаление: {x.Name} = {x.Value}");
				x.Remove();
			}

			Console.WriteLine("\n" + xDocument);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example8()
		{
			var xDocument =
				new XDocument(
					new XElement("Employees",
						new XElement("Employee",
							new XAttribute("type", "Programmer"),
							new XElement("FirstName", "Alex"),
							new XElement("LastName", "Erohin")),
						new XElement("Employee",
							new XAttribute("type", "Editor"),
							new XElement("FirstName", "Elena"),
							new XElement("LastName", "Volkova"))));

			var elements = xDocument.Element("Employees").Elements("Employee");

			foreach(var x in elements)
				Console.WriteLine($"Исходный элемент: {x.Name} : значение = {x.Value}");

			foreach(var x in elements.ToArray())
			{
				Console.WriteLine($"Удаление: {x.Name} = {x.Value}");
				x.Remove();
			}

			Console.WriteLine("\n" + xDocument);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example9()
		{
			var firstName = new XElement("FirstName", "Alex");
			Console.WriteLine((String)firstName);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example10()
		{
			var emps = new[]
			{
				new Employee { FirstName = "Alex", LastName="Erohin", EmployeType=EmployeTypes.Programmer},
				new Employee { FirstName="Elena", LastName="Volkova", EmployeType=EmployeTypes.Editor}
			};

			var xEmp = new XElement("Employees",
				emps.Select(p => new XElement("Employee",
									new XAttribute("type", p.EmployeType),
									new XElement("FirstName", p.FirstName),
									new XElement("LastName", p.LastName))));
			Console.WriteLine(xEmp);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example11()
		{
			var xEmployee = new XElement("Employee",
				new XAttribute("type", "Programmer"));

			Console.WriteLine(xEmployee);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example12()
		{
			var xEmployee = new XElement("Employee");
			var xAttr = new XAttribute("type", "Programmer");
			xEmployee.Add(xAttr);

			Console.WriteLine(xEmployee);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example13()
		{
			var xEmployee = new XElement("Employee",
								new XComment("Добавление нового сотрудника"));

			Console.WriteLine(xEmployee);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example14()
		{
			var xEmployee = new XElement("Employee");
			var xCom = new XComment("Добавление нового сотрудника");

			xEmployee.Add(xCom);
			Console.WriteLine(xEmployee);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example15()
		{
			var xDoc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
										new XElement("Employee"));

			Console.WriteLine(xDoc);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example16()
		{
			var xDoc = new XDocument(new XElement("Employee"));
			var xDeclaration = new XDeclaration("1.0", "UTF-8", "yes");

			xDoc.Declaration = xDeclaration;
			Console.WriteLine(xDoc);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example17()
		{
			// Add new elements only after document type or you will get:
			// Unhandled Exception: System.InvalidOperationException: 
			// This operation would create an incorrectly structured document.
			var xDoc = new XDocument(new XDocumentType("Employees", null, "Employees.dtd", null),
										new XElement("Employee"));
			Console.WriteLine(xDoc);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example18()
		{
			// Add new elements only after document type or you will get:
			// Unhandled Exception: System.InvalidOperationException: 
			// This operation would create an incorrectly structured document.
			var xDoc = new XDocument();
			var docType = new XDocumentType("Employees", null, "Employees.dtd", null);
			xDoc.Add(docType, new XElement("Employee"));

			Console.WriteLine(xDoc);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example19()
		{
			var xDoc = new XDocument(
							new XDeclaration("1.0", "UTF-8", "yes"),
							new XDocumentType("Employees", null, "Employees.dtd", null),
							new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
							new XElement("Employees"));

			Console.WriteLine(xDoc);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example20()
		{
			XNamespace ns = "http://www.professorweb.ru/LINQ";
			var xEmps = new XElement(ns + "Employee");

			Console.WriteLine(xEmps);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example21()
		{
			var xDoc = new XDocument(
							new XProcessingInstruction("EployeCataloger", "out-of-print"),
							new XElement("Employees",
								new XElement("Employee",
									new XProcessingInstruction("EmployeeDeleter", "delete"),
									new XElement("FirstName", "Alex"),
									new XElement("LastName", "Erohin"))));
			Console.WriteLine(xDoc);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example22()
		{
			String[] names = { "John", "Paul", "George", "Pete" };
			var xNames = new XElement("Beatles",
							from n in names
							select new XElement("Name", n));

			names[3] = "Ringo";

			Console.WriteLine(xNames);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example23()
		{
			String[] names = { "John", "Paul", "George", "Pete" };
			var xNames = new XStreamingElement("Beatles",
							from n in names
							select new XElement("Name", n));
			names[3] = "Ringo";

			Console.WriteLine(xNames);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example24()
		{
			var xDoc = new XDocument(
				new XElement("Employees",
					new XElement("Employee",
						new XAttribute("type", "Programmer"),
						new XAttribute("language", "Russian"),
						new XElement("FirstName", "Alex"),
						new XElement("LastName", "Erohin"))));
			xDoc.Save("employees1.xml");

			Console.WriteLine(xDoc);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example25()
		{
			var xDoc = new XDocument(
				new XElement("Employees",
					new XElement("Employee",
						new XAttribute("type", "Programmer"),
						new XAttribute("language", "Russian"),
						new XElement("FirstName", "Alex"),
						new XElement("LastName", "Erohin"))));
			xDoc.Save("employees2.xml", SaveOptions.DisableFormatting);

			Console.WriteLine(xDoc);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example26()
		{
			var xElement = new XElement("Employees",
					new XElement("Employee",
						new XAttribute("type", "Programmer"),
						new XAttribute("language", "Russian"),
						new XElement("FirstName", "Alex"),
						new XElement("LastName", "Erohin")));
			xElement.Save("employees3.xml", SaveOptions.None);

			Console.WriteLine(xElement);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example27()
		{
			var xDoc = XDocument.Load("employees1.xml", LoadOptions.SetBaseUri | LoadOptions.SetLineInfo);
			Console.WriteLine(xDoc);

			var firstName = xDoc.Descendants("FirstName").First();

			Console.WriteLine($"firstName Строка {((IXmlLineInfo)firstName).LineNumber} - Позиция {((IXmlLineInfo)firstName).LinePosition}");

			Console.WriteLine($"Базовый URI: {firstName.BaseUri}");
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example28()
		{
			var xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Employees><Employee type=\"Programmer\" language=\"Russian\"><FirstName>Alex</FirstName><LastName>Erohin</LastName></Employee></Employees>";
			var xElement = XElement.Parse(xml);

			Console.WriteLine(xElement);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example29()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
						  new XDeclaration("1.0", "UTF-8", "yes"),
						  new XDocumentType("Employees", null, "Employees.dtd", null),
						  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
						  // Обратите внимание, что в следующей строке сохраняется 
						  // ссылка на первый элемент
						  new XElement("Employees", firstEmployee =
							new XElement("Employee",
							  new XAttribute("type", "Programmer"),
							  new XElement("FirstName", "Alex"),
							  new XElement("LastName", "Erohin")),
							new XElement("Employee",
							  new XAttribute("type", "Editor"),
							  new XElement("FirstName", "Elena"),
							  new XElement("LastName", "Volkova"))));

			Console.WriteLine(firstEmployee.NextNode);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example30()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
						  new XDeclaration("1.0", "UTF-8", "yes"),
						  new XDocumentType("Employees", null, "Employees.dtd", null),
						  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
						  // Обратите внимание, что в следующей строке сохраняется 
						  // ссылка на первый элемент
						  new XElement("Employees", firstEmployee =
							new XElement("Employee",
							  new XAttribute("type", "Programmer"),
							  new XElement("FirstName", "Alex"),
							  new XElement("LastName", "Erohin")),
							new XElement("Employee",
							  new XAttribute("type", "Editor"),
							  new XElement("FirstName", "Elena"),
							  new XElement("LastName", "Volkova"))));

			Console.WriteLine(firstEmployee.NextNode);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example31()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
						  new XDeclaration("1.0", "UTF-8", "yes"),
						  new XDocumentType("Employees", null, "Employees.dtd", null),
						  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
						  // Обратите внимание, что в следующей строке сохраняется 
						  // ссылка на первый элемент
						  new XElement("Employees", firstEmployee =
							new XElement("Employee",
							  new XAttribute("type", "Programmer"),
							  new XElement("FirstName", "Alex"),
							  new XElement("LastName", "Erohin")),
							new XElement("Employee",
							  new XAttribute("type", "Editor"),
							  new XElement("FirstName", "Elena"),
							  new XElement("LastName", "Volkova"))));

			Console.WriteLine(firstEmployee.Parent);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example32()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
						  new XDeclaration("1.0", "UTF-8", "yes"),
						  new XDocumentType("Employees", null, "Employees.dtd", null),
						  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
						  // Обратите внимание, что в следующей строке сохраняется 
						  // ссылка на первый элемент
						  new XElement("Employees", firstEmployee =
							new XElement("Employee",
							  new XAttribute("type", "Programmer"),
							  new XElement("FirstName", "Alex"),
							  new XElement("LastName", "Erohin")),
							new XElement("Employee",
							  new XAttribute("type", "Editor"),
							  new XElement("FirstName", "Elena"),
							  new XElement("LastName", "Volkova"))));

			foreach(var node in firstEmployee.Nodes())
				Console.WriteLine(node);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example33()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees", firstEmployee =
				new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName", "Alex"),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova"))));

			foreach(var node in firstEmployee.Nodes())
				Console.WriteLine(node);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example34()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees", firstEmployee =
				new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName", "Alex"),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova"))));

			foreach(var node in firstEmployee.Nodes().OfType<XElement>())
				Console.WriteLine(node);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example35()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees", firstEmployee =
				new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName", "Alex"),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova"))));

			foreach(var node in firstEmployee.Nodes().OfType<XComment>())
				Console.WriteLine(node);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example36()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees", firstEmployee =
				new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName", "Alex"),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova"))));

			foreach(var attr in firstEmployee.Attributes())
				Console.WriteLine(attr);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example37()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees", firstEmployee =
				new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName", "Alex"),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova"))));

			foreach(var node in firstEmployee.Elements())
				Console.WriteLine(node);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example38()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees", firstEmployee =
				new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName", "Alex"),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova"))));

			foreach(var node in firstEmployee.Elements("FirstName"))
				Console.WriteLine(node);
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example39()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees", firstEmployee =
				new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName", "Alex"),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova"))));

			Console.WriteLine(firstEmployee.Element("LastName"));
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example40()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees", firstEmployee =
				new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName",
					  new XText("Alex"),
					  new XElement("NickName", "alexsave")),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova"))));

			foreach(var element in firstEmployee
				.Element("FirstName")
				.Element("NickName")
				.Ancestors())
			{
				Console.WriteLine(element.Name);
			}
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example41()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees", firstEmployee =
				new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName",
					  new XText("Alex"),
					  new XElement("NickName", "alexsave")),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova"))));

			foreach(var element in firstEmployee
				.Element("FirstName")
				.Element("NickName")
				.AncestorsAndSelf())
			{
				Console.WriteLine(element.Name);
			}
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example42()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees", firstEmployee =
				new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName",
					  new XText("Alex"),
					  new XElement("NickName", "alexsave")),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova"))));

			foreach(var element in firstEmployee.Descendants())
			{
				Console.WriteLine(element.Name);
			}
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example43()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees", firstEmployee =
				new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName",
					  new XText("Alex"),
					  new XElement("NickName", "alexsave")),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova"))));

			foreach(var element in firstEmployee.DescendantsAndSelf())
			{
				Console.WriteLine(element.Name);
			}
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example44()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees",
				new XComment("Начало списка"),
				firstEmployee = new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName",
					  new XText("Alex"),
					  new XElement("NickName", "alexsave")),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova")),
				new XComment("Конец списка")));

			foreach(var node in firstEmployee.NodesAfterSelf())
			{
				Console.WriteLine(node);
			}
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example45()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees",
				new XComment("Начало списка"),
				firstEmployee = new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName",
					  new XText("Alex"),
					  new XElement("NickName", "alexsave")),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova")),
				new XComment("Конец списка")));

			foreach(var node in firstEmployee.ElementsAfterSelf())
			{
				Console.WriteLine(node);
			}
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example46()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees",
				new XComment("Начало списка"),
				firstEmployee = new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName",
					  new XText("Alex"),
					  new XElement("NickName", "alexsave")),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova")),
				new XComment("Конец списка")));

			foreach(var node in firstEmployee.NextNode.NodesBeforeSelf())
			{
				Console.WriteLine(node);
			}
			Console.ReadKey();
			Console.Clear();
		}
		private static void Example47()
		{
			XElement firstEmployee;

			var xDocument = new XDocument(
			  new XDeclaration("1.0", "UTF-8", "yes"),
			  new XDocumentType("Employees", null, "Employees.dtd", null),
			  new XProcessingInstruction("EmployeeCataloger", "out-of-print"),
			  // Обратите внимание, что в следующей строке сохраняется 
			  // ссылка на первый элемент
			  new XElement("Employees",
				new XComment("Начало списка"),
				firstEmployee = new XElement("Employee",
				  new XComment("Это программист"),
				  new XProcessingInstruction("ProgrammerHandler", "new"),
				  new XAttribute("type", "Programmer"),
				  new XElement("FirstName",
					  new XText("Alex"),
					  new XElement("NickName", "alexsave")),
				  new XElement("LastName", "Erohin")),
				new XElement("Employee",
				  new XAttribute("type", "Editor"),
				  new XElement("FirstName", "Elena"),
				  new XElement("LastName", "Volkova")),
				new XComment("Конец списка")));

			foreach(var node in firstEmployee.NextNode.ElementsBeforeSelf())
			{
				Console.WriteLine(node);
			}
			Console.ReadKey();
			Console.Clear();
		}
	}
}
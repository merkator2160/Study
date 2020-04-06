using CodeForces.Units.CodeForces.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeForces.Units.CodeForces
{
	internal static class AkvelonUnit
	{
		public static void Run()
		{
			var input = "[\"B2,E5,F6\", \"A1,B2,C3,D4\", \"D4,G7,I9\", \"G7,H8\"]";
			var data = JsonConvert.DeserializeObject<String[]>(input);
			var tree = BuildTree(data);

			PrintTree(tree);

			Console.ReadKey();
		}
		private static BranchDto BuildTree(String[] data)
		{
			var branches = data.Select(CreateBranch).ToList();
			CombineBranches(branches);
			FindRoot(branches);

			return branches[0];
		}
		private static BranchDto CreateBranch(String branchData)
		{
			var tier = new BranchDto();
			var ids = branchData.Split(',');

			tier.Person = ids.First();
			tier.EmployeesList = ids.Skip(1).Select(p => new BranchDto()
			{
				Person = p,
				EmployeesList = new List<BranchDto>()
			}).ToList();

			return tier;
		}
		private static void CombineBranches(List<BranchDto> branches)
		{
			while(branches.Count > 1)
			{
				var initialBranchesLength = branches.Count;
				var current = 0;

				while(current < branches.Count)
				{
					foreach(var emp in branches[current].EmployeesList)
					{
						var brunch = branches.FirstOrDefault(p => emp.Person.Equals(p.Person));
						if(brunch == null)
							continue;

						emp.EmployeesList.AddRange(brunch.EmployeesList);
						branches.Remove(brunch);
					}

					current++;
				}

				if(initialBranchesLength == branches.Count) // We have stuck. Who is root?
					break;
			}
		}
		private static void FindRoot(List<BranchDto> branches)
		{
			while(branches.Count > 1)
			{
				AttachBranch(branches, branches[0], branches[1]);
			}
		}
		private static void AttachBranch(List<BranchDto> branches, BranchDto tree, BranchDto secondBranch)
		{
			var brunch = tree.EmployeesList.FirstOrDefault(p => secondBranch.Person.Equals(p.Person));
			if(brunch == null)
			{
				foreach(var emp in tree.EmployeesList)
				{
					AttachBranch(branches, emp, secondBranch);
				}

				return;
			}

			brunch.EmployeesList.AddRange(secondBranch.EmployeesList);
			branches.Remove(secondBranch);
		}
		private static void PrintTree(BranchDto tree, Int32 tier = 0)
		{
			for(var i = 0; i < tier; i++)
				Console.Write("   ");

			Console.WriteLine($"{tree.Person}");

			tier++;
			foreach(var x in tree.EmployeesList)
			{
				PrintTree(x, tier);
			}
		}
	}
}
using System;
using System.Collections.Generic;

namespace AkvelonTask.Models
{
	public class BranchDto
	{
		public String Person { get; set; }
		public List<BranchDto> EmployeesList { get; set; }

		public override String ToString()
		{
			return Person;
		}
	}
}
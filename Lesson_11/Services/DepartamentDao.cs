using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lesson_11.Models;


namespace Lesson_11.Services
{
	/// <summary>
	/// Интерейс к хранению данных департаментов.
	/// </summary>
	public static class DepartamentDao
	{
		public static List<Departament> LoadDepartament()
		{
			var result = new List<Departament>();
			using (var fs = new FileStream(Path.Combine(Environment.CurrentDirectory, @"Data\", "data.xml"), FileMode.OpenOrCreate))
			{
				System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(List<Departament>));
				result = (List<Departament>)s.Deserialize(fs);
			}
			SetSalaryToChief(result);

			return result;
		}

		static void SetSalaryToChief(List<Departament> deps)
		{
			foreach(var dep in deps)
			{
				if (dep.SubDepartaments.Any())
					SetSalaryToChief(dep.SubDepartaments);
				var workersSlary = dep.GetWorkersMonthSalary() + dep.SubDepartaments.Sum(s => s.GetSumAllSalaryFromDepartament());
				workersSlary = workersSlary * (decimal)0.15;
				dep.Chief.SetSalary(workersSlary > 1300 ? workersSlary : 1300);
			}
		}
	}
}

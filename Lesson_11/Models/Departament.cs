using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Lesson_11.Models
{
	/// <summary>
	/// Модель для осписания отдела.
	/// </summary>
	public class Departament
	{
		/// <summary>
		/// Конструктор для сериализации.
		/// </summary>
		public Departament()
		{
			Workers = new List<Worker>();
			SubDepartaments = new List<Departament>();
		}

		/// <summary>
		/// Контруирует объект отдела.
		/// </summary>
		/// <param name="name">Название отдела.</param>
		/// <param name="chief">Начальника отдела.</param>
		/// <param name="workers">Перечсление всех работников отдела.</param>
		/// <param name="subDepartaments">Перечисление всех поддепортаментов.</param>
		public Departament(string name, Chief chief, IEnumerable<Worker> workers, IEnumerable<Departament> subDepartaments)
		{
			Name = name;
			Chief = chief ?? new Chief();
			Workers = new List<Worker>(workers ?? Enumerable.Empty<Worker>());
			SubDepartaments = new List<Departament>(subDepartaments ?? Enumerable.Empty<Departament>());
		}

		/// <summary>
		/// Название отдела.
		/// </summary>
		[XmlAttribute("Name")]
		public string Name { get; set; }

		/// <summary>
		/// Начальник департамента.
		/// </summary>
		[XmlElement("Chief")]
		public Chief Chief { get; set; }

		/// <summary>
		/// Список работников.
		/// </summary>
		[XmlArray("Workers")]
		public List<Worker> Workers { get; set; }

		/// <summary>
		/// Подчиненные департаменты.
		/// </summary>
		[XmlArray("SubDepartaments")]
		public List<Departament> SubDepartaments { get; set; }

		/// <summary>
		/// Возвращает общую месячную зарплату всех работников.
		/// </summary>
		public decimal GetWorkersMonthSalary()
		{
			return Workers.Sum(w => w.MonthSalary);
		}

		/// <summary>
		/// Возвращает месячную зарплату начальника департамента.
		/// </summary>
		public decimal GetChiefMonthSalary()
		{
			return Chief.MonthSalary;
		}

		/// <summary>
		/// Возвращает сумму всех зарплат отдела и его подотделов.
		/// </summary>
		public decimal GetSumAllSalaryFromDepartament()
		{
			return GetChiefMonthSalary() + GetWorkersMonthSalary() + SubDepartaments.Sum(d=>d.GetSumAllSalaryFromDepartament());
		}
	}
}

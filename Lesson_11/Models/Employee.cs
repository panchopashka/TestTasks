using System;
using System.Xml.Serialization;

namespace Lesson_11.Models
{
	/// <summary>
	/// Абстрактный класс с общими для сотрудников фирмы свойствами.
	/// </summary>
	public abstract class Employee
	{
		/// <summary>
		/// Конутруктор для сериализации объекта.
		/// </summary>
		public Employee()
		{
		}

		/// <summary>
		/// Конутруирует объект сотрудника фирмы.
		/// </summary>
		/// <param name="firstName">Имя.</param>
		/// <param name="secondName">Фамилия.</param>
		/// <param name="patronymic">Отчество.</param>
		public Employee(string firstName, string secondName, string patronymic)
		{
			FirstName = firstName;
			SecondName = secondName;
			Patronymic = patronymic;
		}

		/// <summary>
		/// Имя.
		/// </summary>
		[XmlAttribute("FirstName")]
		public string FirstName { get; set; }

		/// <summary>
		/// Фамилия.
		/// </summary>
		[XmlAttribute("SecondName")]
		public string SecondName { get; set; }

		/// <summary>
		/// Отчество.
		/// </summary>
		[XmlAttribute("Patronymic")]
		public string Patronymic { get; set; }

		/// <summary>
		/// Зарплата в месяц для сотрудника.
		/// </summary>
		public abstract decimal MonthSalary { get; }

		/// <summary>
		/// Название должности.
		/// </summary>
		public abstract string PositionName { get; }
	}
}

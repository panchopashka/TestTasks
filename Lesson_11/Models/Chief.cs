using System;
using System.Xml.Serialization;

namespace Lesson_11.Models
{
	/// <summary>
	/// Модель, описывающая должность руководителя.
	/// </summary>
	public class Chief : Employee
	{
		/// <summary>
		/// Переменная для хранения зарплаты.
		/// </summary>
		private decimal _salary;

		/// <summary>
		/// Конутруктор для сериализации объекта.
		/// </summary>
		public Chief()
		{
		}

		/// <summary>
		/// Конутруирует объект руководителя в фирме.
		/// </summary>
		/// <param name="firstName">Имя.</param>
		/// <param name="secondName">Фамилия.</param>
		/// <param name="patronymic">Отчество.</param>
		/// <param name="type">Тип руководителя.</param>
		public Chief(string firstName, string secondName, string patronymic, ChiefType type):base(firstName, secondName, patronymic)
		{
			ChiefType = type;
		}

		/// <summary>
		/// Тип руководителя.
		/// </summary>
		[XmlAttribute("ChiefType")]
		public ChiefType ChiefType { get; set; }

		public override decimal MonthSalary
		{
			get { return _salary; } 
		}

		/// <summary>
		/// Задаёт значение зарплаты.
		/// </summary>
		public void SetSalary(decimal salary)
		{
			_salary = salary;
		}

		public override string PositionName
		{
			get
			{
				if (ChiefType == ChiefType.HEAD_DEPARTMENT)
					return "Начальник департамента";
				if (ChiefType == ChiefType.DEPUTY_DIRECTOR)
					return "Заместитель директора";
				if (ChiefType == ChiefType.DIRECTOR)
					return "Директор";
				throw new NotImplementedException("Get position name not implemented.");
			}
		}
	}
}

using System;
using System.Xml.Serialization;

namespace Lesson_11.Models
{
	/// <summary>
	/// Модель работника или интерна.
	/// </summary>
	public class Worker: Employee
	{
		/// <summary>
		/// Пустой конструктор для поддержки сериализации.
		/// </summary>
		public Worker()
		{
		}

		/// <summary>
		/// Конутруирует объект работника фирмы.
		/// </summary>
		/// <param name="firstName">Имя.</param>
		/// <param name="secondName">Фамилия.</param>
		/// <param name="patronymic">Отчество.</param>
		/// <param name="type">Тип работника.</param>
		/// <param name="monthWorkingHours">Количество часов работы в месяц.</param>
		/// <param name="salaryRate">Ставка зарплаты.</param>
		public Worker(string firstName, string secondName, string patronymic, WorkerType type, int monthWorkingHours, int salaryRate) : base(firstName, secondName, patronymic)
		{
			WorkerType = type;
			MonthWorkingHours = monthWorkingHours;
			SalaryRate = salaryRate;
		}

		/// <summary>
		/// Тип работника.
		/// </summary>
		[XmlAttribute("WorkerType")]
		public WorkerType WorkerType { get; set; }

		/// <summary>
		/// Количество часов работы в месяц. Может быть 0, для тех кто полчает месячную зарплату.
		/// </summary>
		[XmlAttribute("MonthWorkingHours")]
		public int MonthWorkingHours { get; set; }

		/// <summary>
		/// Ставка зарплаты. Почасовая или помесячная.
		/// </summary>
		[XmlAttribute("SalaryRate")]
		public int SalaryRate { get; set; }

		public override decimal MonthSalary
		{
			get
			{
				if (WorkerType == WorkerType.INTERN)
					return SalaryRate;
				if (WorkerType == WorkerType.WORKMAN)
					return SalaryRate * MonthWorkingHours;
				throw new NotImplementedException("Count salary not implemented.");
			}
		}

		public override string PositionName
		{
			get
			{
				if (WorkerType == WorkerType.INTERN)
					return "Рабочий";
				if (WorkerType == WorkerType.WORKMAN)
					return "Интерн";
				throw new NotImplementedException("Get position name not implemented.");
			}
		}
	}
}

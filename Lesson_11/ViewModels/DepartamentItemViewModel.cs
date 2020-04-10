using System.Collections.ObjectModel;
using Lesson_11.Models;
using System.Collections.Generic;

namespace Lesson_11.ViewModels
{
	/// <summary>
	/// View Model для содержимого отдела. (другие отделы или сотрудники).
	/// </summary>
	public class DepartamentItemViewModel: BaseViewModel
	{
		/// <summary>
		/// Закрытый контрутор.
		/// </summary>
		/// <param name="type">Тип элемента.</param>
		/// <param name="name">Название элемента.</param>
		protected DepartamentItemViewModel(DepartamentItemType type, string name)
		{
			Type = type;
			Name = name;
		}

		/// <summary>
		/// Контруирует view model для содержиомого отдела.
		/// </summary>
		/// <param name="type">Тип элемента.</param>
		/// <param name="name">Название элемента.</param>
		/// <param name="deps">Перечисление входящих внутрь департаментов.</param>
		public DepartamentItemViewModel(DepartamentItemType type, string name, IEnumerable<DepartamentItemViewModel> deps):this(type, name)
		{
			Children = new ObservableCollection<DepartamentItemViewModel>(deps);
		}

		/// <summary>
		/// Контруирует view model для содержиомого отдела.
		/// </summary>
		/// <param name="type">Тип элемента.</param>
		/// <param name="name">Название элемента.</param>
		/// <param name="emp">Модель работника.</param>
		public DepartamentItemViewModel(DepartamentItemType type, string name, Employee emp) : this(type, name)
		{
			Employee = emp;
		}

		/// <summary>
		/// Тип содержимого отдела.
		/// </summary>
		public DepartamentItemType Type { get; set; }

		/// <summary>
		/// Имя элемента, которое будет отображаться.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Список содержащийся внутри данного элемента.
		/// </summary>
		public ObservableCollection<DepartamentItemViewModel> Children { get; set; }

		/// <summary>
		/// Ссылка на модель работника. Мб Null, если текущий элемент работник.
		/// </summary>
		public Employee Employee { get; set; }
	}
}

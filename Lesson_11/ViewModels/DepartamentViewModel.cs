using Lesson_11.Models;
using Lesson_11.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Lesson_11.ViewModels
{
	/// <summary>
	/// ViewModel для отображения департаментов.
	/// </summary>
	public class DepartamentViewModel: BaseViewModel
	{
		/// <summary>
		/// Список всех департаментов.
		/// </summary>
		public ObservableCollection<DepartamentItemViewModel> Items { get; set; }

		/// <summary>
		/// Выделенный элемент.
		/// </summary>
		public DepartamentItemViewModel SelectedItem { get; set; }

		public DepartamentViewModel()
		{
			var deps = DepartamentDao.LoadDepartament();
			Items = new ObservableCollection<DepartamentItemViewModel>(deps.Select(GetVM));
		}

		DepartamentItemViewModel GetVM(Departament dep)
		{
			var result = new List<DepartamentItemViewModel>();
			result.Add(new DepartamentItemViewModel(DepartamentItemType.CHIEF, GetEmployeeName(dep.Chief), dep.Chief));
			result.AddRange(dep.Workers.Select(w => new DepartamentItemViewModel(DepartamentItemType.WORKER, GetEmployeeName(w), w)));
			result.AddRange(dep.SubDepartaments.Select(GetVM));
			return new DepartamentItemViewModel(DepartamentItemType.DEPARTAMENT, dep.Name, result);
		}

		string GetEmployeeName(Employee emp)
		{
			return $"{emp.SecondName} {emp.FirstName} {emp.Patronymic}";
		}
	}
}

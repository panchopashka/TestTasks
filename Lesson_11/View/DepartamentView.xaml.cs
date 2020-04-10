using Lesson_11.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Lesson_11.View
{
	/// <summary>
	/// Логика взаимодействия для DepartamentView.xaml
	/// </summary>
	public partial class DepartamentView : Window
	{
		public DepartamentView()
		{
			InitializeComponent();
			this.DataContext = new DepartamentViewModel();
		}

		private void FolderView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			(DataContext as DepartamentViewModel).SelectedItem = ((TreeView)sender).SelectedItem as DepartamentItemViewModel;
		}
	}
}

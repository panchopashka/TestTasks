using Microsoft.Win32;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Lessons_9_10.View
{
	/// <summary>
	/// Логика взаимодействия для MainView.xaml
	/// </summary>
	public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == true)
			{
				var bindingExpression = BindingOperations.GetBindingExpression(TextPathToFile, TextBlock.TextProperty);
				if(bindingExpression!=null)
				{
					var item = bindingExpression.DataItem;
					var itemType = item.GetType();
					var property = itemType.GetProperty(bindingExpression.ResolvedSourcePropertyName, BindingFlags.Instance | BindingFlags.Public);
					property.SetValue(item, openFileDialog.FileName);
					bindingExpression.UpdateTarget();
					bindingExpression.UpdateSource();
				}
				//TextPathToFile.Text = openFileDialog.FileName;
				//BindingOperations.GetBindingExpression(TextPathToFile, TextBlock.TextProperty).UpdateSource();
			}
		}
	}
}

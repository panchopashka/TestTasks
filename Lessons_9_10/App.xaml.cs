using Lessons_9_10.Services;
using Lessons_9_10.View;
using Lessons_9_10.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lessons_9_10
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			var service = TelegramService.GetTelegramService();
			var mainWindow = new MainView();
			MessagesDao.DeserilizeUsersAndMessages();
			//var dialogManager = new DialogManager(mainWindow);

			//dialogManager.Register<ChatViewModel, ChatView>();

			mainWindow.Closed += Close;
			
			mainWindow.DataContext = new MainViewModel(mainWindow.Dispatcher, service);
			mainWindow.Show();
		}

		private void Close(object sender, EventArgs e)
		{
			MessagesDao.SerializeUsersAndMessages();
			Process.GetCurrentProcess().Kill();
		}
	}
}

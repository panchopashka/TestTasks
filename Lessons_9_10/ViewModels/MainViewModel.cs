using Lessons_9_10.Models;
using Lessons_9_10.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Lessons_9_10.ViewModels
{
	/// <summary>
	/// View model для отображения чата пользователей.
	/// </summary>
	public class MainViewModel : BaseViewModel
	{
		/// <summary>
		/// Ссылка на диспетчер для управления основным потоком.
		/// </summary>
		private readonly Dispatcher _dispatcher;

		/// <summary>
		/// Сервис для работы с телеграмом.
		/// </summary>
		private readonly TelegramService _telegramService;

		/// <summary>
		/// Конструирует view model для отображения чата пользователей.
		/// </summary>
		/// <param name="dispatcher">Ссылка на диспетчер для управления основным потоком.</param>
		/// <param name="telegramService">Сервис для работы с телеграмом.</param>
		public MainViewModel(Dispatcher dispatcher, TelegramService telegramService)
		{
			Users = new ObservableCollection<TelegramUser>(MessagesDao.UsersMapper.Select(u=>u.Value));
			Messages = new ObservableCollection<UserMessage>();
			_dispatcher = dispatcher;
			_telegramService = telegramService;
			_telegramService.OnMessageCome += _telegramService_OnMessageCome;
			RunBot();
		}

		/// <summary>
		/// Запустить бота.
		/// </summary>
		private void RunBot()
		{
			_telegramService.Run();
		}

		/// <summary>
		/// Вызывается при получении нового сообщения.
		/// </summary>
		/// <param name="user">Пользователей.</param>
		/// <param name="msg">Сообщение.</param>
		private void _telegramService_OnMessageCome(TelegramUser user, UserMessage msg)
		{
			_dispatcher.Invoke(() =>
			{
				if (!Users.Contains(user))
					Users.Add(user);
				if (SelectedUser != null && SelectedUser.ChatId == user.ChatId)
					Messages.Add(msg);
			});
		}

		/// <summary>
		/// Список пользователей.
		/// </summary>
		public ObservableCollection<TelegramUser> Users { get; set; }

		/// <summary>
		/// Выделенный в главном окне пользователь.
		/// </summary>
		private TelegramUser _selectedUser;
		public TelegramUser SelectedUser { get => _selectedUser;
			set {
				_selectedUser = value;
				if (value != null)
				{
					Messages = new ObservableCollection<UserMessage>(value.Messages);
					IsSelectedUser = true;
				}
				else
				{
					Messages.Clear();
					IsSelectedUser = false;
				}
				} }

		// Команда отправки сообщения.
		private RelayCommand sendcommand;
		public RelayCommand SendCommand
		{
			get
			{
				return sendcommand ??
				  (sendcommand = new RelayCommand(obj =>
				  {
					  if (!string.IsNullOrEmpty(PathToFile))
					  {
						  Task.Run(() => _telegramService.SendDocumentAsync(SelectedUser.ChatId, PathToFile, TextToSend));
						  TextToSend = "";
						  PathToFile = "";
						  return;
					  }
					  if (string.IsNullOrEmpty(TextToSend))
						  return;
					  Task.Run(() => _telegramService.SendMessageAsync(SelectedUser.ChatId, TextToSend));
					  TextToSend = "";
				  }));
			}
		}

		/// <summary>
		/// Текст  для отправки пользователю.
		/// </summary>
		public string TextToSend { get; set; }

		/// <summary>
		/// Путь к прикрепляемому файлу.
		/// </summary>
		public string PathToFile { get; set; }

		/// <summary>
		/// Список сообщений.
		/// </summary>
		public ObservableCollection<UserMessage> Messages { get; set; }

		/// <summary>
		/// Флаг, указывающий выделен ли пользователь.
		/// </summary>
		public bool IsSelectedUser { get; set; }
	}
}

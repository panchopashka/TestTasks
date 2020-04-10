using Lessons_9_10.Models;
using MihaZupan;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;

namespace Lessons_9_10.Services
{
	/// <summary>
	/// Сервис для общения с телеграмом.
	/// </summary>
	public class TelegramService
	{
		/// <summary>
		/// Токен бота.
		/// </summary>
		const string token = "****";

		/// <summary>
		/// Экземпляр сервиса общения с телеграмом.
		/// </summary>
		protected static TelegramService Instance { get; set; }

		/// <summary>
		/// Конструирует экземпляр сервиса.
		/// </summary>
		protected TelegramService()
		{
			Initialize();
		}

		/// <summary>
		/// Событие получения сообщения.
		/// </summary>
		public event BotMessageGetHandler OnMessageCome;

		/// <summary>
		/// Клиент телеграма.
		/// </summary>
		public TelegramBotClient Client { get; set; }

		/// <summary>
		/// Синглтон.
		/// </summary>
		public static TelegramService GetTelegramService()
		{
			if (Instance == null)
				Instance = new TelegramService();
			return Instance;
		}

		/// <summary>
		/// Проинициализировать поля.
		/// </summary>
		private void Initialize()
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
			ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
			var proxy = new HttpToSocks5Proxy("208.102.51.6", 58208);
			Client = new TelegramBotClient(token, proxy);
			Client.OnMessage += Bot_OnMessage;
		}

		public void Run()
		{
			Client.StartReceiving();
		}

		async void Bot_OnMessage(object sender, MessageEventArgs e)
		{
			var chatId = e.Message.Chat.Id;
			var firstName = e.Message.Chat.FirstName;
			var nickName = e.Message.Chat.Username;
			var user = MessagesDao.GetUser(chatId, firstName, nickName);
			UserMessage userMessage;

			if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
			{
				int idx = e.Message.Photo.Length - 1;
				var path = $"_{e.Message.Photo[idx].FileId}.jpg";
				await DownLoad(e.Message.Photo[idx].FileId, path);
				userMessage = new UserMessage(DateTime.Now, UserMessageType.PHOTO, "фото");
				userMessage.Path = path;
			}
			else if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Audio)
			{
				string exc = e.Message.Audio.MimeType.Split('/')[1];
				var path = $"_{e.Message.Audio.Title}.{exc}";
				await DownLoad(e.Message.Audio.FileId, path);
				userMessage = new UserMessage(DateTime.Now, UserMessageType.AUDIO, "аудио");
				userMessage.Path = path;
			}
			else if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
			{
				userMessage = new UserMessage(DateTime.Now, UserMessageType.TEXT, e.Message.Text);
			}
			else if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Document)
			{
				var path = $"_{e.Message.Document.FileName}";
				await DownLoad(e.Message.Document.FileId, path);
				userMessage = new UserMessage(DateTime.Now, UserMessageType.DOCUMENT, "документ");
				userMessage.Path = path;
			}
			else
				return;
			user.Messages.Add(userMessage);
			OnMessageCome?.Invoke(user, userMessage);
		}

		/// <summary>
		/// Отправить сообщение пользователю.
		/// </summary>
		/// <param name="chatId">Идентификатор чата.</param>
		/// <param name="text">Текст сообщения.</param>
		public async Task SendMessageAsync(long chatId, string text)
		{
			await Client.SendTextMessageAsync(new Telegram.Bot.Types.ChatId(chatId), text);
		}

		/// <summary>
		/// Отправить сообщение пользователю.
		/// </summary>
		/// <param name="chatId">Идентификатор чата.</param>
		/// <param name="path">Путь к файлу.</param>
		public async Task SendDocumentAsync(long chatId, string path, string text)
		{
			using (var stream = File.OpenRead(path))
			{
				InputOnlineFile iof = new InputOnlineFile(stream);
				iof.FileName = Path.GetFileName(path);
				await Client.SendDocumentAsync(new Telegram.Bot.Types.ChatId(chatId), iof, text);
			}
		}

		/// <summary>
		/// Скачивает файл.
		/// </summary>
		/// <param name="fileId">Идентификатор файла.</param>
		/// <param name="path">Путь для сохранения файла.</param>
		async Task DownLoad(string fileId, string path)
		{
			var file = await Client.GetFileAsync(fileId);
			using (var fs = new FileStream(path, FileMode.Create))
			{
				await Client.DownloadFileAsync(file.FilePath, fs);
			}
		}
	}

	public delegate void BotMessageGetHandler(TelegramUser user, UserMessage msg);
}

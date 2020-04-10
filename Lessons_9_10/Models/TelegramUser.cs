using System.Collections.Generic;

namespace Lessons_9_10.Models
{
	/// <summary>
	/// Модель для пользователя телеграма.
	/// </summary>
	public class TelegramUser
	{
		/// <summary>
		/// Конутруктор для поддержки сериализации.
		/// </summary>
		public TelegramUser()
		{
			Messages = new List<UserMessage>();
		}

		/// <summary>
		/// Конструирует пользователя.
		/// </summary>
		/// <param name="chatId">Идентификатор чата пользователя.</param>
		/// <param name="firstName">Имя пользователя.</param>
		/// <param name="nickName">Никнейм.</param>
		public TelegramUser(long chatId, string firstName, string nickName)
		{
			Messages = new List<UserMessage>();
			ChatId = chatId;
			FirstName = firstName;
			NickName = nickName;
		}

		/// <summary>
		/// Идентификатор чата пользователя.
		/// </summary>
		public long ChatId { get; set; }

		/// <summary>
		/// Имя пользователя.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Никнейм пользвоателя.
		/// </summary>
		public string NickName { get; set; }

		/// <summary>
		/// Список сообщений.
		/// </summary>
		public List<UserMessage> Messages { get; private set; }

	}
}

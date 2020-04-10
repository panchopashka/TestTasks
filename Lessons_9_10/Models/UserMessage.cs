using System;

namespace Lessons_9_10.Models
{
	/// <summary>
	/// Модель сообщения пользователя.
	/// </summary>
	public class UserMessage
	{
		/// <summary>
		/// Пустой конструктор для поддержки сериализации.
		/// </summary>
		public UserMessage()
		{

		}
		/// <summary>
		/// Конструирует обхект сообщения.
		/// </summary>
		/// <param name="dateTime">Дата время сообщения.</param>
		/// <param name="type">Тип сообщения.</param>
		/// <param name="text"></param>
		public UserMessage(DateTime dateTime, UserMessageType type, string text)
		{
			DateTime = dateTime;
			Type = type;
			Text = text;
		}

		/// <summary>
		/// Дата и время получения сообщения.
		/// </summary>
		public DateTime DateTime { get; set; }

		/// <summary>
		/// Тип сообщения.
		/// </summary>
		public UserMessageType Type { get; set; }

		/// <summary>
		/// Текст сообщения.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Путь к сохраненному файлу.
		/// </summary>
		public string Path { get; set; }
	}
}

using Lessons_9_10.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lessons_9_10.Services
{
	/// <summary>
	/// DAO для хранения сообщений пользователей.
	/// </summary>
	public static class MessagesDao
	{
		private const string fileName = "users.json";

		/// <summary>
		/// Сопостовление идентификатора чата к пользваотелю.
		/// </summary>
		public static Dictionary<long, TelegramUser> UsersMapper = new Dictionary<long, TelegramUser>();

			/// <summary>
			/// Получить модель пользователя.
			/// </summary>
			/// <param name="chatId">Идентификтаор чата с пользователем.</param>
			/// <param name="firstName">Имя пользователя</param>
			/// <param name="nickName">Ник пользователя.</param>
			public static TelegramUser GetUser(long chatId, string firstName, string nickName)
			{
				TelegramUser user;
				if (UsersMapper.TryGetValue(chatId, out user))
					return user;
				user = new TelegramUser(chatId, firstName, nickName);
				UsersMapper.Add(chatId, user);
				return user;
			}

		public static void SerializeUsersAndMessages()
		{
			var result = JsonConvert.SerializeObject(UsersMapper.Select(u => u.Value).ToList());
			File.WriteAllText(fileName, result);
		}

		public static void DeserilizeUsersAndMessages()
		{
			if (!File.Exists(fileName))
				return;
			var loaded = File.ReadAllText(fileName);
			var users = JsonConvert.DeserializeObject<List<TelegramUser>>(loaded);
			foreach (var user in users)
				UsersMapper.Add(user.ChatId, user);
		}
	}
}

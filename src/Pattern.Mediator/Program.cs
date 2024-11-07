
using Pattern.Mediator;
using Pattern.Mediator.Components;

Console.WriteLine("Mediator Pattern Demo Chat Application");

IMediator chatApp = new ChatMediator();

IUser johnDoe = User.Create(chatApp, "John Doe");
IUser jackSmith = User.Create(chatApp, "Jack Smith");
IUser bobMartin = User.Create(chatApp, "Bob Martin");

chatApp.RegisterChatUser(johnDoe);
chatApp.RegisterChatUser(jackSmith);
chatApp.RegisterChatUser(bobMartin);

johnDoe.SendMessage("Hello, everyone!");
jackSmith.SendMessage("Any one got extra phone charger?");
bobMartin.SendMessage("Got one Extra Phone Charger");

Console.WriteLine("Press any key to exit...");
Console.ReadKey();


namespace Pattern.Mediator.Components
{
	/// <summary>
	/// Интерфейс описывает контракт пользователя
	/// </summary>
	public interface IUser
	{
		/// <summary>
		/// Идентификатор пользователя в чате
		/// </summary>
		int ChatId { get; }

		/// <summary>
		/// Отправить сообщение
		/// </summary>
		/// <param name="message"> Сообщение </param>
		void SendMessage(string message);

		/// <summary>
		/// Получить сообщение
		/// </summary>
		/// <param name="message"> Сообщение </param>
		void ReceiveMessage(string message);
	}

	/// <summary>
	/// Класс описывает пользователя чата
	/// </summary>
	public class User : IUser
	{
		private static int _counter = 1000;
		private readonly IMediator _mediator;
		private readonly string _userName;

		/// <summary>
		/// Статический фабричный метод для создания нового пользователя
		/// </summary>
		/// <param name="mediator"> Посредник </param>
		/// <param name="userName"> Имя пользователя </param>
		/// <returns></returns>
		public static User Create(IMediator mediator, string userName) => new User(mediator, userName);

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="mediator"> Посредник </param>
		/// <param name="userName"> Имя пользователя </param>
		private User(IMediator mediator, string userName)
		{
			_mediator = mediator;
			_userName = userName;
			ChatId = ++_counter;
		}

		/// <summary>
		/// Идентификатор пользователя в чате
		/// </summary>
		public int ChatId { get; }

		/// <summary>
		/// Получить сообщение
		/// </summary>
		/// <param name="message"> Сообщение </param>
		public void ReceiveMessage(string message)
		{
			Console.WriteLine($"{_userName} received message: {message}");
		}

		/// <summary>
		/// Отправить сообщение
		/// </summary>
		/// <param name="message"> Сообщение </param>
		public void SendMessage(string message)
		{
			Console.WriteLine($"{_userName} sending message: {message}");
			_mediator.SendMessage(message, ChatId);
		}
	}

}

namespace Pattern.Mediator
{
	/// <summary>
	/// Интерфейс описывает контракт посредника
	/// </summary>
	public interface IMediator
	{
		/// <summary>
		/// Отправить сообщение
		/// </summary>
		/// <param name="message"> Сообщение </param>
		/// <param name="chatId"> Идентификатор пользователя в чате </param>
		public void SendMessage(string message, int chatId);

		/// <summary>
		/// Зарегистрировать пользователя чата
		/// </summary>
		/// <param name="user"> Пользователь </param>
		public void RegisterChatUser(IUser user);
	}

	/// <summary>
	/// Класс описывает посредника чата
	/// </summary>
	public class ChatMediator : IMediator
	{
		/// <summary>
		/// Словарь пользователей
		/// </summary>
		private readonly Dictionary<int, IUser> _users;

		/// <summary>
		/// Конструктор
		/// </summary>
		public ChatMediator()
		{
			// Инициализация словаря пользователей
			_users = new Dictionary<int, IUser>();
		}

		/// <summary>
		/// Зарегистрировать пользователя чата
		/// </summary>
		/// <param name="user"></param>
		public void RegisterChatUser(IUser user)
		{
			// Проверить, содержится ли пользователь в словаре,
			// если нет, то добавить
			_users.TryAdd(user.ChatId, user);
		}

		/// <summary>
		/// Отправить сообщение
		/// </summary>
		/// <param name="message"> Сообщение </param>
		/// <param name="chatId"> Идентификатор пользователя в чате </param>
		public void SendMessage(string message, int chatId)
		{
			// Отправить сообщение всем пользователям, кроме отправителя
			foreach (var user in _users.Where(x => x.Key != chatId))
			{
				// Посредник (Mediator) отправляет сообщение пользователю
				user.Value.ReceiveMessage(message);
			}
		}
	}
}
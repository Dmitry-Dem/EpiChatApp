namespace EpiChatApp.Models
{
	public class User
	{
		private static uint _id;
		public uint Id { get; private init; } //{ get { return _id; } private set { _id = value; } }
		public string Name { get; init; }
		public uint Age { get; init; }
		public string Login { get; init; }
		public string Password { get; init; }
		public User()
		{
			Id = ++_id;
		}
		public User(string name, uint age, string login, string password)
		{
			Id = ++_id;
			Name = name;
			Age = age;
			Login = login;
			Password = password;
		}
		public override string ToString()
		{
			return $"Id:{_id}, Name:{Name}, Age:{Age}, Login:{Login}, Password:{Password}";
		}
	}
}

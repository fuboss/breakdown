namespace UnityCore.Common
{
	public abstract class Singleton<T> where T : Singleton<T>, new()
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				if (_instance == null)
					lock (typeof(T))
					{
						if (_instance == null)
							_instance = new T();
					}

				return _instance;
			}
		}

		public static bool HasInstance => _instance != null;
	}
}
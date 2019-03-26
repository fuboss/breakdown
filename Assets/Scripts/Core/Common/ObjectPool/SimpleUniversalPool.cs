namespace UnityCore.Common.ObjectPool
{
	public class SimpleUniversalPool : IUniversalPool
	{
		public T GetItem<T>() where T : class, IPoolable, new()
		{
			return new T();
		}
	}
}
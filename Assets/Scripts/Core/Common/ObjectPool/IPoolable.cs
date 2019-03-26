using System;

namespace UnityCore.Common.ObjectPool
{
	public interface IPoolable
	{
		object Target { get; set; }
		event Action<IPoolable> OnDispose;
		void Use();
		void Dispose();
	}
}
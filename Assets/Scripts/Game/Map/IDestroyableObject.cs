using UnityEngine;

namespace Game.Map
{
	public interface IDestroyableObject
	{
		bool IsDestroyed { get; }
		GameObject gameObject { get; }
	}
}
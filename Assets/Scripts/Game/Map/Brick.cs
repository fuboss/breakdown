using UnityEngine;

namespace Game.Map
{
	[RequireComponent(typeof(BoxCollider))]
	public class Brick : MonoBehaviour, IDestroyableObject
	{
		public int Score;
		public bool IsDestroyed { get; private set; } = false;
		private void OnCollisionEnter(Collision other)
		{
			IsDestroyed = true;
		}
	}
}
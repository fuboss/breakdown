using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Map
{
	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(Rigidbody))]
	public class Crusher : MonoBehaviour, IDestroyableObject
	{
		public bool CanMove;
		public float MoveSpeed;
		public bool IsDestroyed { get; private set; } = false;
		private Transform _transform;

		private bool _moveRight;
		private bool _moveUp;
		
		[ShowInInspector]
		private Vector3 _direction;

		private void Start()
		{
			_transform = transform;
			_direction =  Vector2.one.normalized; // initial direction
		}
		
		private void OnCollisionEnter(Collision other)
		{
			//ignore other collisions
			if (other.gameObject.CompareTag("DeadZone"))
			{
				//life --
				IsDestroyed = true;
				return;
			}
			_direction = Vector2.Reflect(_direction, other.contacts[0].normal);
		}

		private void FixedUpdate()
		{
			if(!CanMove)
				return;

			Vector2 nextPos = Vector3.Lerp(_transform.position, _transform.position + _direction, Time.fixedDeltaTime * MoveSpeed);
			_transform.position = nextPos;
		}
	}
}
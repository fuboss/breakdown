using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Breakdown.Player
{
	public class PlayerController : MonoBehaviour
	{
		[ShowInInspector]
		public bool CanMove { get; set; }

		[SerializeField, Range(0.1f, 10)]
		private float _moveSpeed = 10;

		private const float MoveThreshold = 0.001f;
		private const float ViewportOffset = 0.08f;	//todo: make it dynamic, depending on playerPlatform size

		private Transform _transform;
		private Camera _camera;

		private void Awake()
		{
			_transform = transform;
			_camera = Camera.main;
		}

		public void FixedUpdate()
		{
			if (!CanMove)
				return;

			var input = Input.GetAxis("Horizontal");
			if(Math.Abs(input) < MoveThreshold ) 
				return;

			var viewPort = _camera.WorldToViewportPoint(_transform.position);
			var desireX = Mathf.Lerp(viewPort.x, viewPort.x + input, _moveSpeed * Time.fixedDeltaTime);
			Vector2 newPosition = _camera.ViewportToWorldPoint(new Vector3(Mathf.Clamp(desireX, ViewportOffset, 1 - ViewportOffset), viewPort.y));
			_transform.position = newPosition;
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using Breakdown;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Map
{
	public class Map : MonoBehaviour
	{
		[Required]
		public LevelCatalog Catalog;
		
		public Crusher Crusher { get; private set; }

		[SerializeField, FoldoutGroup("Crusher")]
		private Vector2 _crusherSpawnPoint;

		[SerializeField, FoldoutGroup("Crusher"), Required]
		private Crusher _crusherPrefab;

		[Space, SerializeField, FoldoutGroup("Bricks")]
		private Brick[] _brickPrefabs;
		[SerializeField, FoldoutGroup("Bricks")]
		private Transform _brickStartPoint;
		[SerializeField, FoldoutGroup("Bricks")]
		private float _brickInterval;
		[SerializeField, FoldoutGroup("Bricks")]
		private float _brickVerticalInterval;

		private readonly List<IDestroyableObject> _destroyables = new List<IDestroyableObject>();
		private readonly List<IDestroyableObject> _toKill = new List<IDestroyableObject>();

		private Coroutine _handleDestroy;

		[Button]
		public void BuildMap(int level)
		{
			//cleanup
			if (_handleDestroy != null)
			{
				StopCoroutine(_handleDestroy);
				_handleDestroy = null;
			}
			
			foreach (var obj in _destroyables)
			{
				Destroy(obj.gameObject);
			}
			_destroyables.Clear();
			
			//build map
			var layout = Catalog.levels[level];
			var spawnPosition = Vector3.zero;
			for (int rowIndex = 0; rowIndex < layout.brickRows.Count; rowIndex++)
			{
				var row = layout.brickRows[rowIndex];
				for (int columnIndex = 0; columnIndex < row.bricks.Length; columnIndex++)
				{
					var brickIndex = row.bricks[columnIndex];
					//empty
					if (brickIndex == -1)
					{
						spawnPosition.x += _brickInterval;
						continue;
					}

					
					var brickPrefab = _brickPrefabs[brickIndex];
					var brick = Instantiate(brickPrefab, _brickStartPoint);
					brick.transform.localPosition = spawnPosition;
					spawnPosition.x += _brickInterval;
					
					_destroyables.Add(brick);
				}

				spawnPosition.x = 0;
				spawnPosition.y -= _brickVerticalInterval;
			}
			
			SpawnCrusher();
			_handleDestroy = StartCoroutine(HandleDestroyables());
		}

		public void SpawnCrusher()
		{
			if (GameManager.Instance.Data.Lives.Value > 0)
			{
				Crusher = Instantiate(_crusherPrefab, _crusherSpawnPoint, Quaternion.identity);
				_destroyables.Add(Crusher);
			}
		}

		private IEnumerator HandleDestroyables()
		{
			while (true)
			{
				yield return null;

				foreach (var obj in _destroyables.ToArray())
				{
					if (!obj.IsDestroyed)
						continue;

					switch (obj)
					{
						case Brick brick:
							_toKill.Add(brick);
							DestroyBrick(brick);
							continue;
						case Crusher crusher:
							_toKill.Add(crusher);
							DestroyCrusher(crusher);
							break;
					}
				}

				//kill destroyed objects
				foreach (var toKill in _toKill)
				{
					_destroyables.Remove(toKill);
				}

				_toKill.Clear();
			}
		}

		private static void DestroyCrusher(Crusher crusher)
		{
			GameManager.Instance.Data.ChangeLives(-1);
			Destroy(crusher.gameObject);
			//play sound
			//play fx
		}

		private static void DestroyBrick(Brick brick)
		{
			GameManager.Instance.Data.ChangeScore(brick.Score);
			Destroy(brick.gameObject);
			//play sound
			//play fx
		}
	}
}
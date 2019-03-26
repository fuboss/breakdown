using System;
using System.Collections.Generic;

namespace UnityCore.Common.ObjectPool
{
	public class PoolManager 
	{
		public const string NAME = "PoolManager";
		public const string OBJECTS_POOL = "-ObjectsPool-";
		public const string UNITY_POOL = "-UnityObjectsPool-";

		private readonly Dictionary<string, IPool> _pools;

		public PoolManager()
		{
			_pools = new Dictionary<string, IPool>();
		}

		public TPool CreatePool<TContent, TPool>(string id, PoolSettings settings)
			where TContent : class 
			where TPool : AbstractPool<TContent>, new()
		{
			if (_pools.ContainsKey(id))
				throw new Exception($"Pool with id {id} already registered");

			TPool pool = null;
			try
			{
				pool = new TPool();
				pool.Init(settings);
				_pools.Add(id, pool);
				return pool;
			}
			catch (Exception exception)
			{
				throw;
			}
		}

		public AbstractPool<T> GetPool<T>(string id) where T : class
		{
			if (_pools.ContainsKey(id))
			{
				if (_pools[id] is AbstractPool<T>)
					return _pools[id] as AbstractPool<T>;
				throw new Exception(
					$"Pool cast exception. Pool with id {id} isn't pool of type {typeof(AbstractPool<T>).Name} and it's real type is {_pools[id].GetType()}");
			}

			throw new Exception($"Missing pool with id: {id} in PoolManager");
		}

		public T GetItemFromPool<T>(string id, ref IPoolable poolable) where T : class
		{
			return GetPool<T>(id).GetItem(ref poolable);
		}

		public T GetItemFromPool<T>(string id) where T : class
		{
			IPoolable poolable = null;
			return GetItemFromPool<T>(id, ref poolable);
		}

		public bool ExistPool(string id)
		{
			return _pools.ContainsKey(id);
		}
	}
}
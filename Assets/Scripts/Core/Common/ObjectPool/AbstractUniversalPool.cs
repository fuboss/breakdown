using System;
using System.Collections.Generic;

namespace UnityCore.Common.ObjectPool
{
    public abstract class AbstractUniversalPool< TPoolSettings > : IUniversalPool where TPoolSettings : PoolSettings
    {

        private static int _poolsCount;
        private TPoolSettings _settings;
        private PoolManager _manager;
        private Dictionary< Type, IPool > _componentsPools = new Dictionary< Type, IPool >();

        protected AbstractUniversalPool( PoolManager manager, TPoolSettings settings )
        {
            _manager = manager;
            _settings = settings;
        }

        public T GetItem< T >( ref IPoolable poolable ) where T : class, IPoolable, new()
        {
            var componentType = typeof( T );

            if( !_componentsPools.ContainsKey( componentType ) )
            {
                var poolName = string.Format( "{0}_{1}", GetHashCode(), _poolsCount++ );
                var newPool = GetNewPool< T >( _manager, _settings, poolName );
                _componentsPools.Add( componentType, newPool );
            }

            var pool = _componentsPools[ componentType ] as AbstractPool< T >;
            return pool.GetItem( ref poolable );
        }

        public T GetItem< T >() where T : class, IPoolable, new()
        {
            IPoolable poolable = null;
            return GetItem< T >( ref poolable );
        }
        
        protected abstract IPool GetNewPool< T >( PoolManager manager, TPoolSettings settings, string newPoolID )
            where T : class, IPoolable, new();
    }
}

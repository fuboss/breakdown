using System;
using System.Collections.Generic;

namespace UnityCore.Common.ObjectPool
{
    public abstract class AbstractPool< T > : IPool where T : class
    {
		protected LinkedList< IPoolable > _freeItems;

#if UNITY_EDITOR
        public LinkedList< IPoolable > FreeItems
        {
            get { return _freeItems; }
        }
#endif
        public PoolSettings Settings { get; protected set; }
	    public int FreeItemsCount { get { return _freeItems.Count; } }
	    public int TotalCreatedItemsCount { get; private set; }

		protected AbstractPool()
        {
            _freeItems = new LinkedList< IPoolable >();
        }

        public virtual void DisposeItem( object item )
        {
            if( item is IPoolable )
            {
                ( item as IPoolable ).Dispose();
            }
            else
            {
                throw new Exception($"Object dispose exception. Item {item} of type {item.GetType().Name} isn't IPoolable, but should be, cos Pool {GetType().Name} should have possibility to Dispose it" );
            }
        }

        public T GetItem( ref IPoolable poolable )
        {
            T result = null;
            poolable = null;

            if( _freeItems.Count > 0 )
            {
                poolable = _freeItems.Last.Value;
                result = poolable.Target as T;
                _freeItems.RemoveLast();
            }
            else
            {
                result = CreateItem( ref poolable );
            }
			poolable.Use();
            poolable.OnDispose += HandlerDispose;

            return result;
        }

        public T GetItem()
        {
            IPoolable poolable = null;
            return GetItem( ref poolable );
        }

        public virtual void Init( PoolSettings settings )
        {
            Settings = settings ?? new PoolSettings();

            for( var i = 0; i < Settings.PreloadCount; i++ )
            {
                Extend();
            }
        }

        public virtual void Uninit()
        {
        }

	    public void Extend()
	    {
		    IPoolable poolable = null;
		    CreateItem( ref poolable );
			FistInitLogic( poolable );
		    _freeItems.AddLast( poolable );
	    }
        protected virtual void DisposeLogic( object item )
        {
        }

	    protected virtual void FistInitLogic( object item )
	    {
	    }

	    protected TPoolSettings GetSettings< TPoolSettings >() where TPoolSettings : PoolSettings
        {
            return Settings as TPoolSettings;
        }

	    protected T CreateItem( ref IPoolable poolable )
	    {
		    T item = CustomCreateItem( ref poolable );
		    TotalCreatedItemsCount++;
		    return item;
	    }

	    protected abstract T CustomCreateItem( ref IPoolable poolable );

        private void HandlerDispose( IPoolable poolable )
        {
            if( Settings.Pooling )
            {
                _freeItems.AddLast( poolable );
            }
            poolable.OnDispose -= HandlerDispose;
            DisposeLogic( poolable );
        }
    }
}

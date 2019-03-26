namespace UnityCore.Common.ObjectPool
{
    public interface IPool
    {
        PoolSettings Settings { get; }
	    int FreeItemsCount { get; }
	    int TotalCreatedItemsCount { get; }

        void DisposeItem( object item );
        void Init( PoolSettings settings );
        void Uninit();
	    void Extend();
    }
}

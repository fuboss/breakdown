namespace UnityCore.Common.ObjectPool
{
    public interface IUniversalPool
    {
        T GetItem< T >() where T : class, IPoolable, new();
    }
}

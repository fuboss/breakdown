using System;
using System.Linq;
using Sirenix.OdinInspector;

namespace UnityCore.Common
{
    public abstract class OdinMonoSingleton<T> : SerializedMonoBehaviour where T : OdinMonoSingleton<T>
    {
        public static T Instance
        {
            get
            {
                // Instance requiered for the first time, we look for it
                if (_instance == null)
                {
                    var instances = FindObjectsOfType<T>();

                    //Special case - call from OnDestroy
                    if (instances.Length == 0 && !ReferenceEquals(_instance, null))
                        return _instance;

                    if (instances.Length == 1)
                    {
                        _instance = instances[0];
                        if (!_isInitialized)
                        {
                            _isInitialized = true;
                            _instance.Init();
                        }
                    }
                    else
                    {
                        if (instances.Length > 1)
                        {
                            var instancesNames = string.Join(", ", instances.Select(i => i.name).ToArray());
                            throw new InvalidOperationException($"There is several instances of {typeof(T).Name} on {instancesNames}");
                        }
                        else
                            throw new InvalidOperationException("There is no instance of " + typeof(T).Name);
                    }
                }

                return _instance;
            }
        }

        private static T _instance;
        private static bool _isInitialized;

        public static bool HasInstance
        {
            get { return _instance != null; }
        }


        // If no other monobehaviour request the instance in an awake function
        // executing before this one, no need to search the object.
        protected virtual void Awake()
        {
            if (!_isInitialized)
            {
                _isInitialized = true;
                Instance.Init();
            }
        }

        // This function is called when the instance is used the first time
        // Put all the initializations you need here, as you would do in Awake
        protected virtual void Init()
        {

        }

        protected virtual void OnDestroy()
        {
            _isInitialized = false;
            _instance = null;
        }
    }
}
using System;

namespace UnityCore.ReactiveProperties
{
    public interface IReactiveProperty<out T>
    {
        T Value { get; }

        /// <summary>
        /// Fires when value changed
        /// </summary>
        event Action<T> OnValueChanged;

        /// <summary>
        /// Fires when value changed. Previous and current values
        /// </summary>
        event Action<T, T> ValueChanged;
    }
}
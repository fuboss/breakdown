using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityCore.ReactiveProperties
{
	[Serializable]
	public class ReactiveProperty<T> : ReactiveProperty, IReactiveProperty<T>
	{
		public override object NonCastedValue => _value;
		
		protected T _prevValue;

		//[HideLabel]
		[SerializeField]//[InlineProperty]
		[OnValueChanged("OnValueChangedByInspector")]
		protected T _value;

        [HideInInspector]
	    private InvocationPolicy _policy;

        public ReactiveProperty(T value, InvocationPolicy policy = InvocationPolicy.ON_VALUE_CHANGED)
        {
            _policy = policy;
            _value = value;
		}

		public ReactiveProperty(InvocationPolicy policy = InvocationPolicy.ON_VALUE_CHANGED)
		{
		    _policy = policy;
            _value = default(T);
		}

		/// <inheritdoc />
		public event Action<T> OnValueChanged;

		/// <inheritdoc />
		public event Action<T, T> ValueChanged;

		
		public virtual T Value
		{
			get { return _value; }
			set
			{
				//TODO: добавить lock, чтобы запретить изменять Value, до конца рассылки всех евентов ValueChanged
				//TODO: add invocation policy (условия замены значения)
				if (_value == null || _policy == InvocationPolicy.EVERY_TIME || !_value.Equals(value))
				{
					SetValue(value);
				}
			}
		}

		private void SetValue(T value)
		{
			SilentSet(value);
			NotifyChanged();

			ValueChanged?.Invoke(_prevValue, value);
			OnValueChanged?.Invoke(_value);
			PostValueChanged();
		}

		protected void OnValueChangedUnsubscribe()
		{
			var delegates = OnValueChanged?.GetInvocationList();
			if (delegates != null)
			{
				foreach (var del in delegates)
				{
					OnValueChanged -= (Action<T>) del;
				}
				
			}
		}

		protected virtual void PostValueChanged()
		{
			
		}

		protected void SilentSet(T newValue)
		{
			_prevValue = _value;
			_value = newValue;
		}

		public static implicit operator T(ReactiveProperty<T> value)
		{
			return value.Value;
		}

		public override string ToString()
		{
			return _value != null ? _value.ToString() : "Null value";
		}

		private void OnValueChangedByInspector()
		{
			SetValue(_value);
		}
	}

	public interface IReactiveProperty
	{
		event Action OnChanged;
		object NonCastedValue { get; }
	}
	
	public class ReactiveProperty : IReactiveProperty
	{
		public event Action OnChanged;
		public virtual object NonCastedValue { get; protected set; }

		protected void NotifyChanged()
		{
			OnChanged?.Invoke();
		}
	}

    public enum InvocationPolicy
    {
        ON_VALUE_CHANGED = 0,
        EVERY_TIME
    }
}
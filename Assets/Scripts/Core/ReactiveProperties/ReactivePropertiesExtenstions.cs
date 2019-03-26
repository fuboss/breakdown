using System;

namespace UnityCore.ReactiveProperties
{
	public static class ReactivePropertiesExtenstions
	{
		public static void Subscribe<T>(this IReactiveProperty<T> target, Action<T> onValueChanged, bool setInstant = false)
		{
			target.OnValueChanged += onValueChanged;
			if (setInstant)
			{
				onValueChanged(target.Value);
			}
		}
		
		public static void Subscribe<T>(this IReactiveProperty<T> target, Action<T,T> onValueChanged, bool setInstant = false)
		{
			target.ValueChanged += onValueChanged;
			if (setInstant)
			{
				onValueChanged(target.Value, target.Value);
			}
		}
		
		public static void Link<T>(this ReactiveProperty<T> lhs, ReactiveProperty<T> rhs, bool setInstant = false)
		{
			Action<T> handler = val => rhs.Value = val;
			lhs.OnValueChanged += handler;
			if (setInstant)
			{
				handler(lhs.Value);
			}
		}
	}
}
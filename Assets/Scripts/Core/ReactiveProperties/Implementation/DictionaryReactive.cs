
using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityCore.ReactiveProperties
{
	public class DictionaryReactive<TKey, TValue> : ReactiveProperty, IEnumerable<TKey>
	{
		private readonly Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

		public event Action<KeyValuePair<TKey, TValue>> OnValueChanged;

		public TValue this[TKey key]
		{
			get
			{
				return _dictionary[key];
			}
			set
			{
				_dictionary[key] = value;
				NotifyValueChanged(key, value);
			}
		}

		public int Count => _dictionary.Count;

		public void Add(TKey key, TValue obj)
		{
			_dictionary.Add(key, obj);
			NotifyValueChanged(key, obj);
		}

		public void Remove(TKey key)
		{
			var value = _dictionary[key];
			_dictionary.Remove(key);
			NotifyValueChanged(key, value);
		}

		public void Clear()
		{
			_dictionary.Clear();
			NotifyChanged();
		}

		private void NotifyValueChanged(TKey key, TValue value)
		{
			NotifyChanged();
			OnValueChanged?.Invoke(new KeyValuePair<TKey, TValue>(key, value));
		}

		IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
		{
			return _dictionary.Keys.GetEnumerator();
		}

		private IEnumerator<TKey> GetEnumerator()
		{
			return _dictionary.Keys.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
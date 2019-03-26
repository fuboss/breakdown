using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityCore.ReactiveProperties
{
    public interface IListReactive<out T> : IReactiveProperty, IEnumerable<T>
    {
        event Action<T> OnValueAdded;
        event Action<T> OnValueRemoved;
        event Action OnRefresh;
        int Count { get; }
        int IndexOf(object item);
        bool Contains(object item);
    }

    [Serializable]
    public class ListReactive<T> : IListReactive<T>
    {
        [SerializeField] 
        private List<T> _list = new List<T>();

        public T this[int index]
        {
            get { return _list[index]; }
            set
            {
                _list[index] = value;
                NotifyListChanged();
            }
        }

        public int Count => _list.Count;
        public List<T> InnerList => _list;
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event Action OnChanged;
        public object NonCastedValue => _list;

        public event Action<T> OnValueAdded;
        public event Action<T> OnValueRemoved;
        public event Action OnRefresh;

        public void Add(T obj)
        {
            _list.Add(obj);
            OnValueAdded?.Invoke(obj);
            NotifyListChanged();
        }

        public void RemoveAt(int index)
        {
            var obj = _list[index];
            _list.RemoveAt(index);
            OnValueRemoved?.Invoke(obj);
            NotifyListChanged();
        }

        public void Remove(T obj)
        {
            _list.Remove(obj);
            OnValueRemoved?.Invoke(obj);
            NotifyListChanged();
        }

        public void Sort()
        {
            _list.Sort();
            NotifyListChanged();
        }

        public void Clear()
        {
//            foreach (var item in _list)
//                OnValueRemoved.SafeRaise(item);
            
            _list.Clear();
            NotifyListChanged();
        }

        public bool Contains(object item)
        {
            return _list.Contains((T)item);
        }

        public void AddRange(IEnumerable<T> add)
        {
            _list.AddRange(add);
            foreach (var element in add) 
                OnValueAdded?.Invoke(element);
            NotifyListChanged();
        }

        public int IndexOf(object item)
        {
            return _list.IndexOf((T)item);
        }

        public void Insert(int index, T obj)
        {
            _list.Insert(index, obj);
            NotifyListChanged();
        }

        public void Refresh()
        {
            NotifyListChanged();
            OnRefresh?.Invoke();
        }
        
        private void NotifyListChanged()
        {
            NotifyChanged();
        }

        private void NotifyChanged()
        {
            OnChanged?.Invoke();
        }

        private IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}
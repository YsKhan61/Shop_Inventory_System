using System;


namespace SIS.Utilities
{
    public class Event<T>
    {
        public event Action<T> baseEvent = delegate { };
        public void InvokeEvent(T type) => baseEvent?.Invoke(type);
        public void AddListener(Action<T> listener) => baseEvent += listener;
        public void RemoveListener(Action<T> listener) => baseEvent -= listener;
    }

    public class Event
    {
        public event Action baseEvent = delegate { };
        public void InvokeEvent() => baseEvent.Invoke();
        public void AddListener(Action listener) => baseEvent += listener;
        public void RemoveListener(Action listener) => baseEvent -= listener;

    }

    public class DoubleEvent<T, U>
    {
        public event Action<T, U> baseEvent = delegate { };
        public void InvokeEvent(T type, U type2) => baseEvent?.Invoke(type, type2);
        public void AddListener(Action<T, U> listener) => baseEvent += listener;
        public void RemoveListener(Action<T, U> listener) => baseEvent -= listener;
    }

    public class  DoubleFuncEvent<T, U, V>
    {
        public event Func<T, U, V> baseEvent;
        public V InvokeEvent(T type, U type2) => baseEvent(type, type2);
        public void AddListener(Func<T, U, V> listener) => baseEvent += listener;
        public void RemoveListener(Func<T, U, V> listener) => baseEvent -= listener;
    }
}



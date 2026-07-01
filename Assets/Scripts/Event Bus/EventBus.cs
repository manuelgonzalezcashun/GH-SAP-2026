using System;

public static class EventBus
{
    static class GameEvent<T>
    {
        public static Action<T> onEvent;
    }
    public static void Subscribe<T>(Action<T> listener)
    {
        GameEvent<T>.onEvent += listener;
    }
    public static void UnSubscribe<T>(Action<T> listener)
    {
        GameEvent<T>.onEvent -= listener;
    }
    public static void Raise<T>(T eventData)
    {
        GameEvent<T>.onEvent?.Invoke(eventData);
    }
}
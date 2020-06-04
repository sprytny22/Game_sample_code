using System;
using System.Collections.Generic;

public static class Observer
{
    private static Dictionary<Type, List<Action<object>>> listeners = new Dictionary<Type, List<Action<object>>>();
    public static void RegisterListener<T>(Action<object> listener) where T : class
    {
        if (listeners.ContainsKey(typeof(T)) == false)
            listeners.Add(typeof(T), new List<Action<object>>());

        listeners[typeof(T)].Add(listener);
    }

    public static void Publish<T>(T publishedEvent) where T : class
    {
        if (listeners.ContainsKey(typeof(T)) == false)
            return;

        foreach(var action in listeners[typeof(T)])
        {
            action.Invoke(publishedEvent);
        }
    }
}

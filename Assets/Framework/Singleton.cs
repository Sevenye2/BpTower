using System;

namespace Framework
{
    public class Singleton<T> where T : Singleton<T>, new()
    {
        // ReSharper disable once InconsistentNaming
        private static readonly Lazy<T> m_instance = new(() => { var t = new T(); return t; });
        public static T Instance => m_instance.Value;

    }
}

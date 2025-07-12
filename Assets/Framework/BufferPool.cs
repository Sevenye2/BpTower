using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Framework
{
    public class BufferPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Stack<T> _pools = new();


        protected BufferPool()
        {
        }

        public BufferPool(T prefab)
        {
            _prefab = prefab;
            BufferGCHandler.Register(this as BufferPool<Component>);
        }

        ~BufferPool()
        {
            BufferGCHandler.UnRegister(this as BufferPool<Component>);
        }

        /// <summary>
        /// activated from the bool. create a new by prefab if the pool is empty
        /// need set pos and rot and parent after created.
        /// </summary>
        /// <returns></returns>
        public T Create()
        {
            var behaviour = _pools.Count == 0 ? Object.Instantiate(_prefab) : _pools.Pop();
            BufferGCHandler.Push(behaviour);
            return behaviour;
        }

        public async UniTask<T> CreateAsync()
        {
            T behaviour;
            if (_pools.Count == 0)
            {
                var require = Object.InstantiateAsync(_prefab);
                await require;
                behaviour = require.Result[0];
            }
            else
                behaviour = _pools.Pop();

            BufferGCHandler.Push(behaviour);
            return behaviour;
        }

        public async UniTask CreateAsync(int count, Action<T, int> onCreated = null, Action onCompleted = null)
        {
            if (count == 0)
                return;

            var c = count;
            var index = 0;
            while (_pools.Count > 0)
            {
                var behaviour = _pools.Pop();
                onCreated?.Invoke(behaviour, index);

                BufferGCHandler.Push(behaviour);
                index++;
                c--;

                if (c == 0)
                    return;
            }


            var require = Object.InstantiateAsync(_prefab, c);

            await require;

            foreach (var behaviour in require.Result)
            {
                onCreated?.Invoke(behaviour, index);
                BufferGCHandler.Push(behaviour);
                index++;
            }

            onCompleted?.Invoke();
        }

        public void Destroy(T obj)
        {
            _pools.Push(obj);
            BufferGCHandler.Remove(obj);
        }

        public void Clear()
        {
            foreach (var obj in _pools)
            {
                Object.Destroy(obj.gameObject);
            }
            _pools.Clear();
        }
    }
}


public static class BufferGCHandler
{
    private static readonly List<Component> RunningObj = new();
    private static readonly List<BufferPool<Component>> Pools = new();

    public static void Register(BufferPool<Component> pool)
    {
        Pools.Add(pool);
    }

    public static void UnRegister(BufferPool<Component> pool)
    {
        Pools.Remove(pool);
    }

    public static void Push(Component component)
    {
        RunningObj.Add(component);
    }

    public static void Remove(Component component)
    {
        RunningObj.Remove(component);
    }


    public static void Clear()
    {
        RunningObj.ForEach(obj => Object.Destroy(obj.gameObject));
        RunningObj.Clear();

        Pools.ForEach(pool => pool.Clear());
    }
}
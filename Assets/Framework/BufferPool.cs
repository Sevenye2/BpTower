using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
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
        }

        /// <summary>
        /// activated from the bool. create a new by prefab if the pool is empty
        /// need set pos and rot and parent after created.
        /// </summary>
        /// <returns></returns>
        public T Create()
        {
            if (_pools.Count == 0)
                return Object.Instantiate(_prefab);

            var behaviour = _pools.Pop();
            return behaviour;
        }
        
        public async UniTask<T> CreateAsync()
        {
            if (_pools.Count == 0)
            {
                var require = Object.InstantiateAsync(_prefab);
                await require; 
                return require.Result[0];
            }

            var behaviour = _pools.Pop();
            return behaviour;
        }

        public async UniTask CreateAsync(int count, Action<T> onCreated = null, Action onCompleted = null)
        {
            if (count == 0)
                return;

            var c = _pools.Count;

            while (_pools.Count > 0)
            {
                var behaviour = _pools.Pop();
                onCreated?.Invoke(behaviour);
                if (c == 0)
                    return;
            }

            var require = Object.InstantiateAsync(_prefab);
            
            await require;

            foreach (var behaviour in require.Result)
            {
                onCreated?.Invoke(behaviour);
            }

            onCompleted?.Invoke();
        }

        public void Destroy(T obj)
        {
            _pools.Push(obj);
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
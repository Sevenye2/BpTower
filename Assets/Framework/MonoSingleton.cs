using UnityEngine;

namespace Framework
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        protected virtual T LoadAndGetComponent() => null;
        public static T Instance
        {
            get
            {

                if (_instance != null && _instance.gameObject == null)
                    _instance = null;

                if (_instance) return _instance;
            
                var obj = new GameObject();
                var singleton = obj.AddComponent<T>();
                var behavior = singleton.LoadAndGetComponent();
                if (behavior ==null)
                {
                    _instance = singleton;
                    obj.name = Instance.GetType().Name;
                }
                else
                {
                    _instance = behavior;
                    Destroy(obj);
                }

                return _instance;
            }
        }

        private static T _instance = null;

        public virtual void Awake()
        {
            if ((_instance != null && _instance.gameObject == null) || _instance == null)
                _instance = this as T;
        }


        public virtual void Discard()
        {
            Destroy(gameObject);
            _instance = null;
        }


    }
}


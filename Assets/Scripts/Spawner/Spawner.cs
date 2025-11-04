using System;
using Spawnable;
using UnityEngine;
using UnityEngine.Pool;

namespace Spawner
{
    public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _poolCapacity = 5;
        [SerializeField] private int _poolMaxSize = 5;
        
        [SerializeField] protected float RepeatRate = 1f;

        private ObjectPool<T> _pool;
        
        public event Action<T> Released;

        private void Awake()
        {
            _pool = new ObjectPool<T>(
                createFunc: () => Instantiate(_prefab),
                actionOnGet: ActionOnGet,
                actionOnRelease: ActionOnRelease,
                actionOnDestroy: Destroy,
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );
        }

        protected T GetObject() => 
            _pool.Get();

        private void ActionOnGet(T spawnable)
        {
            spawnable.Reset();
        
            spawnable.Disappeared += OnDisappeared;
        }
    
        private void ActionOnRelease(T spawnable)
        {
            spawnable.Release();
        
            spawnable.Disappeared -= OnDisappeared;
        }
    
        private void OnDisappeared(T spawnable)
        {
            Released?.Invoke(spawnable);
            
            _pool.Release(spawnable);
        }
    }
}
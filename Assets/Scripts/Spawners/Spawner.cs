using System;
using Spawnable;
using UnityEngine;
using UnityEngine.Pool;

namespace Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
    {
        [SerializeField] protected float RepeatRate = 1f;
        
        [SerializeField] private T _prefab;
        [SerializeField] private int _poolCapacity = 5;
        [SerializeField] private int _poolMaxSize = 5;

        private ObjectPool<T> _pool;
        private EntityCounter _entityCounter;

        public event Action<T> Released;
        public event Action<EntityCounter> Updated;

        private void Awake()
        {
            _pool = new ObjectPool<T>(
                createFunc: Create,
                actionOnGet: ActionOnGet,
                actionOnRelease: ActionOnRelease,
                actionOnDestroy: Destroy,
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize
            );
            
            _entityCounter = new EntityCounter();
        }

        protected T GetObject()
        {
            UpdateEntityCounter();

            return _pool.Get();
        }

        private T Create()
        {
            return Instantiate(_prefab);
        }

        private void UpdateEntityCounter()
        {
            _entityCounter.Update(_pool.CountAll, _pool.CountActive);
            
            Updated?.Invoke(_entityCounter);
        }

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
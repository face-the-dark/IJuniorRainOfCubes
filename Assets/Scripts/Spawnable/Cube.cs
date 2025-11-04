using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawnable
{
    [RequireComponent(typeof(ColorChanger))]
    [RequireComponent(typeof(LifeTimer))]
    [RequireComponent(typeof(CollisionDetector))]
    public class Cube : MonoBehaviour, ISpawnable<Cube>
    {
        [SerializeField] private float _minSpawnLength = -10f;
        [SerializeField] private float _maxSpawnLength = 10f;
        [SerializeField] private float _spawnHeight = 20f;

        private CollisionDetector _collisionDetector;
        private ColorChanger _colorChanger;
        private LifeTimer _lifeTimer;

        public event Action<Cube> Disappeared;

        private void Awake()
        {
            _collisionDetector = GetComponent<CollisionDetector>();
            _colorChanger = GetComponent<ColorChanger>();
            _lifeTimer = GetComponent<LifeTimer>();
        }

        private void OnEnable()
        {
            _collisionDetector.PlatformCollisionDetected += OnPlatformCollisionDetected;
            _lifeTimer.TimerExpired += OnTimerExpired;
        }

        private void OnDisable()
        {
            _collisionDetector.PlatformCollisionDetected -= OnPlatformCollisionDetected;
            _lifeTimer.TimerExpired -= OnTimerExpired;
        }

        public void Reset()
        {
            _collisionDetector.Reset();
            _colorChanger.Reset();

            transform.position = GenerateRandomPosition();
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.SetActive(true);
        }

        public void Release() => 
            gameObject.SetActive(false);

        private Vector3 GenerateRandomPosition()
        {
            float positionX = Random.Range(_minSpawnLength, _maxSpawnLength);
            float positionZ = Random.Range(_minSpawnLength, _maxSpawnLength);

            return new Vector3(positionX, _spawnHeight, positionZ);
        }

        private void OnPlatformCollisionDetected()
        {
            _colorChanger.SetRandomColor();
            _lifeTimer.StartTimer();
        }

        private void OnTimerExpired() => 
            Disappeared?.Invoke(this);
    }
}
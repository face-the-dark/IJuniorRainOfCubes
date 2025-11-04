using System;
using UnityEngine;

namespace Spawnable
{
    [RequireComponent(typeof(TransparencyReducer))]
    public class Bomb : MonoBehaviour, ISpawnable<Bomb>
    {
        private const int OverlapSphereArraySize = 100;
        
        [SerializeField] private float _explosionRadius = 5;
        [SerializeField] private float _explosionForce = 20;

        private TransparencyReducer _reducer;

        private Collider[] _targets;

        public event Action<Bomb> Disappeared;

        private void Awake()
        {
            _reducer = GetComponent<TransparencyReducer>();
            _targets = new Collider[OverlapSphereArraySize];
        }

        private void OnEnable() => 
            _reducer.HasDecreased += Explode;

        private void OnDisable() => 
            _reducer.HasDecreased -= Explode;

        public void Reset()
        {
            _reducer.Reset();
        
            gameObject.SetActive(true);
        }

        public void Release() => 
            gameObject.SetActive(false);

        public void SetPosition(Vector3 position) => 
            transform.position = position;

        private void Explode()
        {
            Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, _targets);

            foreach (Collider target in _targets)
                if (target is not null && target.TryGetComponent(out Rigidbody targetRigidbody))
                    targetRigidbody.AddExplosionForce(_explosionForce, targetRigidbody.transform.position, _explosionRadius,
                        0f, ForceMode.Impulse);
        
            Disappeared?.Invoke(this);
        }
    }
}
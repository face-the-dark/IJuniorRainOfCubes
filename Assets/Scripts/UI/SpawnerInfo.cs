using Spawnable;
using Spawners;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SpawnerInfo<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
    {
        [SerializeField] private Spawner<T> _spawner;
        [SerializeField] private TextMeshProUGUI _spawnedObjectsCount;
        [SerializeField] private TextMeshProUGUI _instantiatedObjectsCount;
        [SerializeField] private TextMeshProUGUI _activeObjectsCount;

        private void OnEnable() => 
            _spawner.Updated += OnUpdated;

        private void OnDisable() => 
            _spawner.Updated -= OnUpdated;

        private void OnUpdated(EntityCounter entityCounter)
        {
            _spawnedObjectsCount.text = entityCounter.SpawnedObjectsCount.ToString();
            _instantiatedObjectsCount.text = entityCounter.InstantiatedObjectsCount.ToString();
            _activeObjectsCount.text = entityCounter.ActiveObjectsCount.ToString();
        }
    }
}
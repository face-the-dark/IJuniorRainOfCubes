using System.Collections;
using Spawnable;
using UnityEngine;

namespace Spawners
{
    public class CubeSpawner : Spawner<Cube>
    {
        private Coroutine _spawnCoroutine;

        private void Start()
        {
            StopSpawnCoroutine();
            StartCoroutine(Spawn());
        }

        private void StopSpawnCoroutine()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

        private IEnumerator Spawn()
        {
            WaitForSeconds wait = new WaitForSeconds(RepeatRate);

            while (enabled)
            {
                GetObject();

                yield return wait;
            }
        }
    }
}
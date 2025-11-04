using Spawnable;
using UnityEngine;

namespace Spawner
{
    public class BombSpawner : Spawner<Bomb>
    {
        [SerializeField] Spawner<Cube> _cubeSpawner;

        private void OnEnable() => 
            _cubeSpawner.Released += OnReleased;

        private void OnDisable() => 
            _cubeSpawner.Released -= OnReleased;

        private void OnReleased(Cube cube) => 
            Spawn(cube);

        private void Spawn(Cube cube)
        {
            Bomb bomb = GetObject();

            bomb.SetPosition(cube.transform.position);
        }
    }
}
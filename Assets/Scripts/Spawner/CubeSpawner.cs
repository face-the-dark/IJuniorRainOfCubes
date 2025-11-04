using Spawnable;

namespace Spawner
{
    public class CubeSpawner : Spawner<Cube>
    {
        private void Start() => 
            Spawn();

        private void Spawn() => 
            InvokeRepeating(nameof(GetObject), 0.0f, RepeatRate);
    }
}
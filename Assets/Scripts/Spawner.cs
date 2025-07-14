using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => ActionOnRelease(cube),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetObject), 0.0f, _repeatRate);
    }

    private Cube GetObject()
    {
        return _pool.Get();
    }

    private void ActionOnGet(Cube cube)
    {
        cube.Reset();
        
        cube.CubeDisappeared += OnCubeDisappeared;
    }
    
    private void ActionOnRelease(Cube cube)
    {
        cube.gameObject.SetActive(false);
        
        cube.CubeDisappeared -= OnCubeDisappeared;
    }
    
    private void OnCubeDisappeared(Cube cube)
    {
        _pool.Release(cube);
    }
}
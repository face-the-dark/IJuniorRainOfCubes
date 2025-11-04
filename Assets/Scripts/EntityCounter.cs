public class EntityCounter
{
    public int SpawnedObjectsCount { get; private set; }
    public int InstantiatedObjectsCount { get; private set; }
    public int ActiveObjectsCount { get; private set; }

    public void Update(int poolCountAll, int poolCountActive)
    {
        SpawnedObjectsCount++;
        InstantiatedObjectsCount = poolCountAll;
        ActiveObjectsCount = poolCountActive;
    }
}
using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private bool _isFirstCollision = false;
    
    public event Action PlatformCollisionDetected;

    public void Reset()
    {
        _isFirstCollision = false;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        bool hasPlatformComponent = other.gameObject.TryGetComponent(out Platform platform);
        
        if (hasPlatformComponent && _isFirstCollision == false)
        {
            _isFirstCollision = true;

            PlatformCollisionDetected?.Invoke();
        }
    }
}

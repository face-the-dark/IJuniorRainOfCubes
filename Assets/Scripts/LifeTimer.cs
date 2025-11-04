using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LifeTimer : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    public event Action TimerExpired;
    public event Action<float> DurationGenerated;
    
    public void StartTimer() => 
        StartCoroutine(StartCountdown());

    private IEnumerator StartCountdown()
    {
        float seconds = Random.Range(_minLifeTime, _maxLifeTime);
        
        DurationGenerated?.Invoke(seconds);
        
        yield return new WaitForSeconds(seconds);
        
        TimerExpired?.Invoke();
    }
}
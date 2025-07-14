using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LifeTimer : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    public event Action TimerExpired;

    public void StartTimer()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(Random.Range(_minLifeTime, _maxLifeTime));
        
        TimerExpired?.Invoke();
    }
}
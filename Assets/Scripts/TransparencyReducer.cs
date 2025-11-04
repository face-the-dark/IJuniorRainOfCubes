using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LifeTimer))]
[RequireComponent(typeof(Renderer))]
public class TransparencyReducer : MonoBehaviour
{
    [SerializeField] private float _startTransparency = 1;
    [SerializeField] private float _endTransparency = 0;

    private LifeTimer _lifeTimer;
    private Renderer _renderer;

    private Color _originalColor;

    public event Action HasDecreased;

    private void Awake()
    {
        _lifeTimer = GetComponent<LifeTimer>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _lifeTimer.DurationGenerated += OnDurationGenerated;

        _lifeTimer.StartTimer();
    }

    private void OnDisable()
    {
        _lifeTimer.DurationGenerated -= OnDurationGenerated;
    }

    public void Reset()
    {
        _renderer.material.color = _originalColor;
    }

    private void OnDurationGenerated(float duration)
    {
        StartCoroutine(Reduce(duration));
    }

    private IEnumerator Reduce(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime <= duration)
        {
            elapsedTime += Time.deltaTime;

            _originalColor = _renderer.material.color;
            float alpha = Mathf.Lerp(_startTransparency, _endTransparency, elapsedTime / duration);
            _renderer.material.color = new Color(_originalColor.r, _originalColor.g, _originalColor.b, alpha);

            yield return null;
        }

        HasDecreased?.Invoke();
    }
}
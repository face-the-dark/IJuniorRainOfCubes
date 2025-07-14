using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;

    private Color _baseColor;
    
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        
        _baseColor = _renderer.material.color;
    }

    public void Reset()
    {
        _renderer.material.color = _baseColor;
    }

    public void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }
}

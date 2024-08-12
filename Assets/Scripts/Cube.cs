using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _minLiveTime;
    [SerializeField] private int _maxLiveTime;

    private WaitForSeconds _lifeTime;
    private Color _defaultColor;
    private Renderer _renderer;

    public event Action<Cube> TimeIsUp; 

    private void Awake()
    {
        _defaultColor = Color.white;
        _renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_renderer.material.color == _defaultColor)
        {
            _renderer.material.color = Random.ColorHSV();
        }

        StartCoroutine(Living());
    }

    private IEnumerator Living()
    {
        _lifeTime = new WaitForSeconds(Random.Range(_minLiveTime, _maxLiveTime));
        yield return _lifeTime;
        SetDefaultColor();
        TimeIsUp?.Invoke(this);
    }
    
    private void SetDefaultColor()
    {
        _renderer.material.color = _defaultColor;
    }
}

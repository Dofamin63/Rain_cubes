using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;
    [SerializeField] private Color _defaultColor;
    private CubeSpawner _spawner;
    private bool _isFirstCollision = true;

    public void Init(CubeSpawner spawner)
    {
        _spawner = spawner;
        _isFirstCollision = true;
        GetComponent<Renderer>().material.color = _defaultColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isFirstCollision)
        {
            _isFirstCollision = false;
            GetComponent<Renderer>().material.color = Random.ColorHSV();
            StartCoroutine(Living());
        }
    }

    private IEnumerator Living()
    {
        yield return new WaitForSeconds(Random.Range(_minTime, _maxTime));
        _spawner.OnRelease(this);
    }
}
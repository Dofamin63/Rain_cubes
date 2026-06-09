using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _minTransparency;
    [SerializeField] private float _maxTransparency;
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;
    [SerializeField] private Color _color;
    
    private BombSpawner _spawner;
    private Material _material;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _material = GetComponent<Renderer>().material;
    }

    public void Init(BombSpawner spawner)
    {
        _material.color = new Color(_color.r, _color.g, _color.b, _maxTransparency);
        _spawner = spawner;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        StartCoroutine(Living());
    }

    private IEnumerator Living()
    {
        float duration = Random.Range(_minTime, _maxTime);
        float zeroTime = 0f;

        while (zeroTime < duration)
        {
            zeroTime += Time.deltaTime;
            float alpha = Mathf.Lerp(_maxTransparency, _minTransparency, zeroTime / duration);
            _material.color = new Color(_color.r, _color.g, _color.b, alpha); 
            yield return null;
        }

        Explode();
        _spawner.ReleaseBomb(this);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject != gameObject && hit.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(_force, transform.position, _radius);
            }
        }
    }
}
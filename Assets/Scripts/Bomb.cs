using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColorChanger), typeof(Exploder))]
public class Bomb : SpawnableObject<Bomb>
{
    [SerializeField] private float _minTransparency;
    [SerializeField] private float _maxTransparency;
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    [SerializeField] private Color _color;

    private ColorChanger _colorChanger;
    private Exploder _exploder;

    protected override void Awake()
    {
        base.Awake();
        _colorChanger = GetComponent<ColorChanger>();
        _exploder = GetComponent<Exploder>();
    }

    public override void Init()
    {
        base.Init();
        _colorChanger.SetAlpha(_color, _maxTransparency);
        StartCoroutine(Living());
    }

    private IEnumerator Living()
    {
        float duration = Random.Range(MinTime, MaxTime);
        float zeroTime = 0f;

        while (zeroTime < duration)
        {
            zeroTime += Time.deltaTime;
            float alpha = Mathf.Lerp(_maxTransparency, _minTransparency, zeroTime / duration);
            _colorChanger.SetAlpha(_color, alpha);
            yield return null;
        }

        _exploder.Explode(transform.position, _radius, _force, gameObject);
        NotifyLifeEnded();
    }
}
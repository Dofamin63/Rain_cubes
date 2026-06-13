using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class Cube : SpawnableObject<Cube>
{
    [SerializeField] private Color _defaultColor;
    
    private ColorChanger _colorChanger;
    private bool _isFirstCollision;

    protected override void Awake()
    {
        base.Awake();
        _colorChanger = GetComponent<ColorChanger>();
    }

    public override void Init()
    {
        base.Init();
        _isFirstCollision = true;
        _colorChanger.SetColor(_defaultColor);
    }
    
    protected override Cube GetSpawnableObject() => this;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isFirstCollision)
        {
            _isFirstCollision = false;
            _colorChanger.SetRandomColor();
            StartCoroutine(Living());
        }
    }

    private IEnumerator Living()
    {
        yield return new WaitForSeconds(Random.Range(MinTime, MaxTime));
        NotifyLifeEnded();
    }
}
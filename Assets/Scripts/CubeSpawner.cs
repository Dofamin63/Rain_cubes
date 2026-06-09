using System.Collections;
using UnityEngine;

public class CubeSpawner: Spawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private float _delay = 1f;
    [SerializeField] private int _minPosition = -23;
    [SerializeField] private int _maxPosition = 23;
    [SerializeField] private int _height = 15;

    private void Start() => StartCoroutine(Spawning());

    private IEnumerator Spawning()
    {
        while (enabled)
        {
            Cube cube = Pool.Get();
            cube.transform.position = new Vector3(Random.Range(_minPosition, _maxPosition), _height, Random.Range(_minPosition, _maxPosition));
            cube.Init(this);
            yield return new WaitForSeconds(_delay);
        }
    }

    public void OnRelease(Cube cube)
    {
        _bombSpawner.Spawning(cube.transform.position);
        Pool.Release(cube);
    }
}
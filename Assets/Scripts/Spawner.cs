using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _delay;

    private ObjectPool<Cube> _pool;
    private WaitForSeconds _spawnDelay;
    private int _minPosition = -23;
    private int _maxPosition = 23;
    private int _height = 15;
    
    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => OnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj));

        _spawnDelay = new WaitForSeconds(_delay);
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (enabled)
        {
            _pool.Get();
            yield return _spawnDelay;
        }
    }
    
    private void OnGet(Cube cube)
    {
        cube.LifeOver += OnRelease;
        cube.transform.position = new Vector3(Random.Range(_minPosition, _maxPosition), _height, Random.Range(_minPosition, _maxPosition));
        cube.gameObject.SetActive(true);
    }

    private void OnRelease(Cube cube)
    {
        cube.LifeOver -= OnRelease;
        _pool.Release(cube);
    }
}
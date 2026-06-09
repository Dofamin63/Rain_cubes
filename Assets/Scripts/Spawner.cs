using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : Component
{
    [SerializeField] private T _prefab;
    protected ObjectPool<T> Pool;
    
    public int TotalSpawned { get; private set; }
    public int TotalCreated { get; private set; }
    public int ActiveCount => Pool.CountActive;

    protected virtual void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: () => { TotalCreated++; return Instantiate(_prefab); },
            actionOnGet: (obj) => { obj.gameObject.SetActive(true); TotalSpawned++; },
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject)
        );
    }
}
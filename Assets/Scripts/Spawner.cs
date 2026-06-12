using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : SpawnableObject<T>
{
    [SerializeField] private T _prefab;
    
    protected ObjectPool<T> Pool;

    public int TotalSpawned { get; private set; }
    public int TotalCreated { get; private set; }
    public int ActiveCount => Pool.CountActive;

    public event Action<Spawner<T>> CountChanged;

    protected virtual void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: () => 
            { 
                TotalCreated++; 
                T obj = Instantiate(_prefab); 
                obj.LifeEnded += OnObjectLifeEnded;
                return obj; 
            },
            actionOnGet: (obj) => 
            { 
                obj.gameObject.SetActive(true); 
                TotalSpawned++;
                CountChanged?.Invoke(this);
            },
            actionOnRelease: (obj) => 
            { 
                obj.gameObject.SetActive(false);
                CountChanged?.Invoke(this); 
            },
            actionOnDestroy: (obj) => 
            {
                obj.LifeEnded -= OnObjectLifeEnded;
                Destroy(obj.gameObject);
            }
        );
    }

    protected virtual void OnObjectLifeEnded(T obj) => Pool.Release(obj);
}
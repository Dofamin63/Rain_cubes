using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class SpawnableObject<T> : MonoBehaviour where T : SpawnableObject<T>
{
    [SerializeField] protected float MinTime;
    [SerializeField] protected float MaxTime;

    protected Rigidbody ObjectRigidbody;

    public event Action<T> LifeEnded;

    protected virtual void Awake()
    {
        ObjectRigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Init()
    {
        ObjectRigidbody.velocity = Vector3.zero;
        ObjectRigidbody.angularVelocity = Vector3.zero;
    }

    protected void NotifyLifeEnded()
    {
        LifeEnded?.Invoke((T)this);
    }
}
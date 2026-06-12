using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    public void Spawning(Vector3 position)
    {
        Bomb bomb = Pool.Get();
        bomb.transform.position = position;
        bomb.Init();
    }
}
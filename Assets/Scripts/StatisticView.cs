using TMPro;
using UnityEngine;

public class StatisticView : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;
    private TextMeshProUGUI _text;

    public void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.text = $"Spawned cubes: {_cubeSpawner.TotalSpawned} | Created: {_cubeSpawner.TotalCreated} | Active: {_cubeSpawner.ActiveCount}\n" +
                     $"Spawned bomb: {_bombSpawner.TotalSpawned} | Created: {_bombSpawner.TotalCreated} | Active: {_bombSpawner.ActiveCount}";
    }
}
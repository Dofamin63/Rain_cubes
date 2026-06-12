using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public abstract class StatisticView<T> : MonoBehaviour where T : SpawnableObject<T>
{
    [SerializeField] private Spawner<T> _spawner;
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        UpdateDisplay(_spawner);
    }

    private void OnEnable()
    {
        _spawner.CountChanged += UpdateDisplay;
    }

    private void OnDisable()
    {
        _spawner.CountChanged -= UpdateDisplay;
    }

    private void UpdateDisplay(Spawner<T> spawner)
    {
        _text.text = $"Spawned {typeof(T).Name}: {spawner.TotalSpawned} | " +
                     $"Created: {spawner.TotalCreated} | " +
                     $"Active: {spawner.ActiveCount}";
    }
}
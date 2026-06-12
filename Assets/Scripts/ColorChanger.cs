using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void SetAlpha(Color baseColor, float alpha)
    {
        _renderer.material.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
    }

    public void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }
}
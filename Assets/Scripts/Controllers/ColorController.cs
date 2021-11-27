using UnityEngine;

public class ColorController : MonoBehaviour
{
    private Color defaultColor;
    private Renderer renderer;

    private void Awake()
    {
        InitData();
    }

    private void InitData()
    {
        renderer = GetComponent<Renderer>();
        defaultColor = renderer.material.color;
    }

    public void SetColor(Color color) => SetSpriteColor(color);
    public void SetDefaultColor() => SetSpriteColor(defaultColor);

    private void SetSpriteColor(Color color) => renderer.material.color = color;
}
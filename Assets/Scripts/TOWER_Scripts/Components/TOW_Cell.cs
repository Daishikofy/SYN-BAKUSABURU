using UnityEngine;

public class Cell : MonoBehaviour
{
    public int value;
    public Color empty, full;
    public Gradient selected;

    public SpriteRenderer spriteRenderer;
    public void Init(int cellValue)
    {
        this.value = cellValue;
        float alpha = 0.5f + 0.5f * (cellValue/100f);
        spriteRenderer.color = new Color(empty.r, empty.g, empty.b, alpha);
    }
    public void Init()
    {
        float alpha = 0.5f + 0.5f * (value/100f);
        spriteRenderer.color = new Color(empty.r, empty.g, empty.b, alpha);
    }
    
}
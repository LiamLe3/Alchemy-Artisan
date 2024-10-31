using UnityEngine;

public class VisualsManager : MonoBehaviour
{
    [SerializeField] Sprite[] baseGameSprites;
    [SerializeField] Color[] colors;
    [SerializeField] Sprite[] potionSprites;

    public Color GetColor(int index)
    {
        return colors[index];
    }

    public Sprite GetSprite(int index)
    {
        return baseGameSprites[index];
    }

    public Sprite GetPotionSprite(int index)
    {
        return potionSprites[index];
    }
}
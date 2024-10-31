using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] Sprite[] gemSprites;
    [SerializeField] Sprite[] potionSprites;
    [SerializeField] Sprite[] ingredientSprites;
    [SerializeField] Sprite noItemSprite;
    [SerializeField] Color[] colors;

    public Item MakeGem(int itemId, float remainingTime = 0)
    {
        return new Gem(itemId, gemSprites[itemId], remainingTime, colors[itemId]);
    }

    public Item MakePotion(int itemId)
    {
        return new Potion(itemId, potionSprites[itemId]);
    }

    public Item MakeIngredient(int itemId)
    {
        return new Ingredient(itemId, ingredientSprites[itemId]);
    }

    public Item MakeNullItem()
    {
        return new NullItem(noItemSprite);
    }
}

using UnityEngine;

public class IngredientPile : Interactable
{
    [SerializeField] int ingredientType;

    public override Item Interact(Item itemHeld)
    {
        if(itemHeld is NullItem) return itemFactory.MakeIngredient(ingredientType);
        if(itemHeld is Ingredient && itemHeld.GetId() == ingredientType) return itemFactory.MakeNullItem();
        
        return itemHeld;
    }
}
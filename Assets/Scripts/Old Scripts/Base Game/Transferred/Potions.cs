using UnityEngine;

public class Potions : Interactables
{
    [SerializeField] int potionType;
    public override Itemss Interact(Itemss itemHeld)
    {
        int itemID = itemHeld.id;
        if(itemID != NO_GEM && itemID != potionType) return itemHeld;

        if(itemID == NO_GEM)
        {
            return new Itemss(potionType, 0, false, false);
        }
        return GemItem.NO_GEM;
    }
}
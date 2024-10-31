using UnityEngine;

public class EmeraldChest : Interactables
{
    Animator myAnimator;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public override Itemss Interact(Itemss itemHeld)
    {
        int itemID = itemHeld.id;
        if(itemID != EMERALD && itemID != NO_GEM) return itemHeld;
        if(!itemHeld.isGem) return itemHeld;

        myAnimator.SetBool("isOpen", true);

        if(itemID == EMERALD)
        {
            return GemItem.NO_GEM;
        }
        else
        {
            return GemItem.EMERALD;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            myAnimator.SetBool("isOpen", false);
        }
    }
}
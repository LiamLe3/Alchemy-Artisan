using UnityEngine;

public class Incinerator : Interactable
{
    AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    public override Item Interact(Item heldItem)
    {
        if(heldItem is NullItem) return heldItem;

        audioSource.Play();
        return itemFactory.MakeNullItem();
    }
}

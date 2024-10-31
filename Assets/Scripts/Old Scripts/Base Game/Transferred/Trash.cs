using UnityEngine;

public class Trash : Interactables
{
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override Itemss Interact(Itemss itemHeld)
    {
        int itemID = itemHeld.id;
        if(itemID == NO_GEM) return GemItem.NO_GEM;

        audioSource.Play();
        return GemItem.NO_GEM;
    }
}

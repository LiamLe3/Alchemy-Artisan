using UnityEngine;

public class Interactables : MonoBehaviour
{
    protected const int COAL = 0;
    protected const int EMERALD = 5;
    protected const int NO_GEM = 9;

    public virtual Itemss Interact(Itemss itemHeld)
    {
        Debug.Log("Player has interacted with an object.");
        return GemItem.NO_GEM;
    }
}
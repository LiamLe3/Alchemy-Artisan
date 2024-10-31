using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected ItemFactory itemFactory;
    protected GameManager gameManager;

    protected virtual void Awake()
    {
        itemFactory = FindObjectOfType<ItemFactory>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public virtual Item Interact(Item itemHeld)
    {
        Debug.Log("Player has interacted with an object.");
        return null;
    }
}
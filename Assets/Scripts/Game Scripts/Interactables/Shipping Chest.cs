using UnityEngine;

public class ShippingChest : Interactable
{
    Animator myAnimator;
    OrderManager orderManager;
    AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        myAnimator = GetComponent<Animator>();
        orderManager = FindObjectOfType<OrderManager>();
        audioSource = GetComponent<AudioSource>();
    }

    public override Item Interact(Item itemHeld)
    {
        return orderManager.SendShipment(itemHeld);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {   
            myAnimator.SetBool("isOpen", true);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            myAnimator.SetBool("isOpen", false);
            audioSource.Play();
        }
    }
}
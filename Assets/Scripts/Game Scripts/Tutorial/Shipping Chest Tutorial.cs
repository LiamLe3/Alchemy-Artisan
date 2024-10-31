using UnityEngine;

public class ShippingChestTutorial : Interactable
{
    Animator myAnimator;
    OrderManagerTutorial orderManagerTutorial;
    AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();
        myAnimator = GetComponent<Animator>();
        orderManagerTutorial = FindObjectOfType<OrderManagerTutorial>();
        audioSource = GetComponent<AudioSource>();
    }

    public override Item Interact(Item itemHeld)
    {
        return orderManagerTutorial.SendShipment(itemHeld);
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
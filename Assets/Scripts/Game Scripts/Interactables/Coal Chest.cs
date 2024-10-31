using UnityEngine;

public class CoalChest : Interactable
{
    const int COAL = 0;
    Animator myAnimator;
    AudioSource audioSource;
    protected override void Awake()
    {
        base.Awake();
        myAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public override Item Interact(Item itemHeld)
    {
        if(itemHeld is Gem && itemHeld.GetId() == COAL) return itemFactory.MakeNullItem();
        if(itemHeld is NullItem) return itemFactory.MakeGem(COAL, 5);

        return itemHeld;
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
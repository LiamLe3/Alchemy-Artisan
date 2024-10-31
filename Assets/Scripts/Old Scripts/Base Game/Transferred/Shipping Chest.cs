using UnityEngine;

public class ShippingChests : Interactables
{
    const int NO_ORDER = -1;
    const float MAX_TIMER = 100f;

    Animator myAnimator;
    OrderManagers orderManager;
    GameManagers gameManager;
    PotionGenerator potionGenerator;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        orderManager = FindObjectOfType<OrderManagers>();
        gameManager = FindObjectOfType<GameManagers>();
        potionGenerator = FindObjectOfType<PotionGenerator>();
    }

    public override Itemss Interact(Itemss itemHeld)
    {
        
        if(!itemHeld.isGem)
        {
            HandlePotion(itemHeld);
            return GemItem.NO_GEM;
        }

        int itemID = itemHeld.id;
        if(itemID == NO_GEM) return itemHeld;
        
        if(itemID == EMERALD || itemID == COAL)
        {
            gameManager.UpdateScore(itemHeld);
            myAnimator.SetBool("isOpen", true);
            return GemItem.NO_GEM;
        }

        int lowestTimerIndex = NO_ORDER;
        float lowestTimer = MAX_TIMER;
        for(int index = 0; index < 3; index++)
        {
            if(orderManager.GetOrder(index) == itemID)
            {
                if(orderManager.GetOrderTimer(index) < lowestTimer)
                {
                    lowestTimerIndex = index;
                    lowestTimer = orderManager.GetOrderTimer(index);
                }
            }
        }

        if(lowestTimerIndex == NO_ORDER)
            return itemHeld;

        myAnimator.SetBool("isOpen", true);
        orderManager.StartNewOrder(lowestTimerIndex);

        if(!itemHeld.isPolished)
            gameManager.UpdateScore(itemHeld);
        else
            gameManager.UpdateScore(itemHeld);
        

        return GemItem.NO_GEM;
    }

    void HandlePotion(Itemss itemHeld)
    {
        if(itemHeld.id == orderManager.GetPotionOrder())
        {
            gameManager.UpdateScore(itemHeld);
            orderManager.potionOrder = potionGenerator.GetRandomPotion();
            orderManager.UpdatePotionOrderUI(orderManager.potionOrder);
        }
        else
        {
            gameManager.UpdateScore(GemItem.COAL);
        }
        return;

    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            myAnimator.SetBool("isOpen", false);
        }
    }
}
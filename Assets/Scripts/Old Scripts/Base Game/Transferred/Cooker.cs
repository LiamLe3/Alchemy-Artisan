using UnityEngine;

public class Cooker : Interactables
{
    Animator myAnimator;
    FloatingSlider gemTimerDisplay;
    GameManagers gameManager;

    const int NOT_COOKING = -1;
    [SerializeField] static float gemCookingDuration = 5;    
    int cookingGem = NOT_COOKING;
    float timer = 0;
    bool isOvercooked;
    Itemss gemToCook;
    
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        gemTimerDisplay = GetComponentInChildren<FloatingSlider>();
        gameManager = FindObjectOfType<GameManagers>();
    }

    void Start()
    {
        gemTimerDisplay.SetSliderDisplay(false);
        gemTimerDisplay.SetMaxValue(gemCookingDuration);
    }

    void Update()
    {
        if (gameManager.IsGameEnded()) return;
        CountDownCooker();
    }

    public override Itemss Interact(Itemss itemHeld)
    {
        int itemID = itemHeld.id;
        if(itemID == NO_GEM && cookingGem == NOT_COOKING) return itemHeld;

        if(itemID != NO_GEM && cookingGem != NOT_COOKING) return itemHeld;
        if(!itemHeld.isGem) return itemHeld;
        
        Itemss gem = GemItem.NO_GEM;
        
        if(itemID != NO_GEM && cookingGem == NOT_COOKING)
        {
            if(itemID == COAL)
                return itemHeld;
            timer = itemHeld.timeRemaining;
            gemToCook = itemHeld;
            cookingGem = (int)Mathf.Ceil(timer/gemCookingDuration);
            gemTimerDisplay.SetNewUI(cookingGem);
        }
        else if(itemID == NO_GEM && cookingGem != NOT_COOKING)
        {
            gem = ProduceGem(timer);
            cookingGem = NOT_COOKING;
            myAnimator.SetBool("isOvercooked", false);
        }

        bool isCooking = cookingGem > NOT_COOKING;
        gemTimerDisplay.SetSliderDisplay(isCooking);
        myAnimator.SetBool("isCooking", isCooking);

        return gem;
    }

    Itemss ProduceGem(float timer)
    {
        Itemss gem = timer switch
        {
            < 0 => GemItem.COAL,
            < 5 => GemItem.DIAMOND,
            < 10 => GemItem.RUBY,
            < 15 => GemItem.AMETHYST,
            < 20 => GemItem.TOPAZ,
            < 25 => GemItem.EMERALD,
            _ => GemItem.EMERALD
        };

        bool isPolished = gemToCook == gem && gemToCook.isPolished;
        
        gemToCook = null;
        return new Itemss(gem.id, timer, isPolished);
    }

    void CountDownCooker()
    {
        if(cookingGem == NOT_COOKING || isOvercooked)
            return;
        
        if(timer <= 0)
        {
            myAnimator.SetBool("isOvercooked", true);
            gemTimerDisplay.UpdateSlider(gemCookingDuration);

            isOvercooked = true;
            return;
        }

        timer -= Time.deltaTime;

        bool isNextGem = cookingGem > Mathf.Ceil(timer/gemCookingDuration);
        if(isNextGem)
        {
            cookingGem--;
            gemTimerDisplay.SetNewUI(cookingGem);
        }

        gemTimerDisplay.UpdateSlider(timer%gemCookingDuration);

    }
}
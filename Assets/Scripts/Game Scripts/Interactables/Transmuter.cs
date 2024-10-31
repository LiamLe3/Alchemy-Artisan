using UnityEngine;

public class Transmuter : Interactable
{   
    const int DIAMOND = 4;

    ProgressBar transmuteProgressBar;
    Animator myAnimator;
    
    Item transmuterGem;
    [SerializeField] float transmuteDuration = 5f;
    bool isOverCooked;

    protected override void Awake()
    {
        base.Awake();
        transmuteProgressBar = GetComponentInChildren<ProgressBar>();
        myAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        transmuterGem = itemFactory.MakeNullItem();

        transmuteProgressBar.SetMaxValue(transmuteDuration);
        transmuteProgressBar.SetVisible(false);
    }

    void Update()
    {
        if(gameManager.CheckGameStopped()) return;
        
        TransmuteGem();
    }

    public override Item Interact(Item heldItem)
    {
        if(transmuterGem is Gem)
            if(heldItem is NullItem)
                return GiveTransmuterGem();
            else
                return heldItem;

        if(heldItem is not Gem gem) return heldItem;
        transmuterGem = gem;

        SetUI(((Gem)transmuterGem).GetTimer());
        ToggleVisibilityAndAnimation(true);
        return itemFactory.MakeNullItem();
    }

    Item GiveTransmuterGem()
    {
        Item gemToGive = transmuterGem;
        transmuterGem = itemFactory.MakeNullItem();
        isOverCooked = false;
        ToggleVisibilityAndAnimation(false);
        
        return gemToGive;
    }
    
    void ToggleVisibilityAndAnimation(bool isTransmuting)
    {
        transmuteProgressBar.SetVisible(isTransmuting);
        myAnimator.SetBool("isTransmuting", isTransmuting);
    }

    void TransmuteGem()
    {
        if(transmuterGem is not Gem gem || isOverCooked) return;

        if(gem.GetTimer() <= 0)
        {
            if(transmuterGem.GetId() == DIAMOND)
            {
                isOverCooked = true;
                transmuterGem = itemFactory.MakeGem(0, transmuteDuration);
                SetUI(transmuteDuration);
                return;
            }

            transmuterGem = itemFactory.MakeGem(gem.GetId() + 1, transmuteDuration);
            SetUI(transmuteDuration);
        }
        else
        {
            gem.CountDownTimer();
            transmuteProgressBar.UpdateProgressBar(gem.GetTimer());
        }
    }

    void SetUI(float time)
    {
        transmuteProgressBar.SetIcon(transmuterGem.GetSprite());
        transmuteProgressBar.SetColor(((Gem)transmuterGem).GetColor());
        transmuteProgressBar.UpdateProgressBar(time);
    }
}
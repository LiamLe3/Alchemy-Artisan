using UnityEngine;

public class Cauldron : Interactable
{
    ProgressBar brewingProgressBar;
    Animator myAnimator;

    [SerializeField] SpriteRenderer[] ingredientRenderers = {null, null};
    [SerializeField] SpriteRenderer potionRenderer = null;
    Item[] cauldronIngredients = {null, null};
    Item potionCreated;

    bool isPotionReady;
    bool isBrewing;

    [SerializeField] float brewingDuration = 10f;
    float brewingProgress = 0f;
    

    protected override void Awake()
    {
        base.Awake();
        myAnimator = GetComponent<Animator>();
        brewingProgressBar = GetComponentInChildren<ProgressBar>();
    }

    void Start()
    {
        SetIngredientRenderer(0, itemFactory.MakeNullItem());
        SetIngredientRenderer(1, itemFactory.MakeNullItem());
        potionCreated = itemFactory.MakeNullItem();
        potionRenderer.sprite = potionCreated.GetSprite();

        brewingProgressBar.SetMaxValue(brewingDuration);
        brewingProgressBar.SetVisible(false);
    }

    void Update()
    {
        if(gameManager.CheckGameStopped()) return;
        BrewPotion();
    }

    public override Item Interact(Item itemHeld)
    {
        if(isBrewing) return itemHeld;

        if(itemHeld is NullItem)
        {
            if(isPotionReady)
                return GivePotion();
            else if(cauldronIngredients[0] is not NullItem)
                return GiveBackIngredient();
        }
        else if(itemHeld is Ingredient)
        {
            if(!isPotionReady)
            {
                PutIngredientIntoCauldron(itemHeld);
                return itemFactory.MakeNullItem();
            }
        }

        return itemHeld;
    }

    Item GivePotion()
    {
        Item potionToGive = potionCreated;
        potionCreated = itemFactory.MakeNullItem();
        isPotionReady = false;
        potionRenderer.sprite = potionCreated.GetSprite();

        return potionToGive;
    }

    Item GiveBackIngredient()
    {
        Item ingredientToGive = cauldronIngredients[0];
        SetIngredientRenderer(0, itemFactory.MakeNullItem());

        return ingredientToGive;
    }

    void PutIngredientIntoCauldron(Item item)
    {
        if(cauldronIngredients[0] is NullItem)
        {
            SetIngredientRenderer(0, item);
        }
        else
        {
            SetIngredientRenderer(1, item);

            potionCreated = CreatePotion(cauldronIngredients[0], cauldronIngredients[1]);
            ToggleBrewing(true);
        }
    }

    Item CreatePotion(Item ingredient1, Item ingredient2)
    {
        if(ingredient1 == ingredient2)
            return itemFactory.MakePotion(cauldronIngredients[0].GetId());
        else
        {
            int potionId = ingredient1.GetId() + ingredient2.GetId();
            return itemFactory.MakePotion(2 + potionId);
        }
    }

    void BrewPotion()
    {
        if(!isBrewing) return;

        brewingProgress += Time.deltaTime;
        brewingProgressBar.UpdateProgressBar(brewingProgress);
        if(brewingProgress >= brewingDuration)
        {
            SetIngredientRenderer(0, itemFactory.MakeNullItem());
            SetIngredientRenderer(1, itemFactory.MakeNullItem());
            
            isPotionReady = true;
            brewingProgress = 0f;
            potionRenderer.sprite = potionCreated.GetSprite();
            ToggleBrewing(false);
        }
    }

    void SetIngredientRenderer(int index, Item item)
    {
        cauldronIngredients[index] = item;
        ingredientRenderers[index].sprite = item.GetSprite();
    }

    void ToggleBrewing(bool toggle)
    {
        isBrewing = toggle;
        brewingProgressBar.SetVisible(toggle);
        myAnimator.SetBool("isBrewing", toggle);
    }
}
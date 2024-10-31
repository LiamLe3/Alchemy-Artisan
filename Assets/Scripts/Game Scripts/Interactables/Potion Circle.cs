using System.Linq;
using UnityEngine;

public class PotionCircle : Interactable
{
    const int RedPotionId = 0;
    const int BluePotionId = 1;
    const int YellowPotionId = 2;
    const int PurplePotionId = 3;
    const int OrangePotionId = 4;
    const int GreenPotionId = 5;
    const int WhitePotionId = 6;
    const int BlackPotionId = 7;
    const int BrownPotionId = 8;
    
    ProgressBar combiningProgressBar;

    [SerializeField] SpriteRenderer[] potionSlotRenderers = {null, null, null};
    [SerializeField] SpriteRenderer createdPotionRenderer = null;
    Item[] potionSlots = {null, null, null};
    Item potionCreated;

    bool isPotionReady;
    bool isCombining;

    [SerializeField] float combiningDuration = 10f;
    float combiningProgress = 0f;
    
    protected override void Awake()
    {
        base.Awake();
        combiningProgressBar = GetComponentInChildren<ProgressBar>();
    }

    void Start()
    {
        for(int index = 0; index < 3; index++)
            SetPotionSlotRenderer(index, itemFactory.MakeNullItem());

        potionCreated = itemFactory.MakeNullItem();
        createdPotionRenderer.sprite = potionCreated.GetSprite();

        combiningProgressBar.SetMaxValue(combiningDuration);
        combiningProgressBar.SetVisible(false);
    }

    void Update()
    {
        BrewPotion();
    }

    public override Item Interact(Item itemHeld)
    {
        if(isCombining) return itemHeld;

        if(itemHeld is NullItem)
        {
            if(isPotionReady)
                return GivePotion();
            else if(potionSlots[2] is NullItem)
                return GiveBackIngredient();
        }
        else if(itemHeld is Potion)
        {
            int potionId = itemHeld.GetId();
            if(potionId > GreenPotionId) return itemHeld;

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
        isPotionReady = false;
        potionCreated = itemFactory.MakeNullItem();
        createdPotionRenderer.sprite = potionCreated.GetSprite();

        return potionToGive;
    }

    Item GiveBackIngredient()
    {
        int potionSlot;
        if(potionSlots[1] is not NullItem)
            potionSlot = 1;
        else
            potionSlot = 0;
        Item potionToGive = potionSlots[potionSlot];
        SetPotionSlotRenderer(potionSlot, itemFactory.MakeNullItem());
        return potionToGive;
    }

    void PutIngredientIntoCauldron(Item item)
    {
        if(potionSlots[0] is NullItem)
            SetPotionSlotRenderer(0, item);
        else if(potionSlots[1] is NullItem)
            SetPotionSlotRenderer(1, item);
        else
        {
            SetPotionSlotRenderer(2, item);

            potionCreated = CreatePotion();
            ToggleCombining(true);
        }
    }

    Item CreatePotion()
    {
        if(HasRBYPotion()) return itemFactory.MakePotion(WhitePotionId);
        if(HasPOGPotion()) return itemFactory.MakePotion(BlackPotionId);
        
        return itemFactory.MakePotion(BrownPotionId);
    }

    bool HasRBYPotion()
    {
        bool hasRedPotion = potionSlots.Any(potion => potion.GetId() == RedPotionId);
        bool hasBluePotion = potionSlots.Any(potion => potion.GetId() == BluePotionId);
        bool hasYellowPotion = potionSlots.Any(potion => potion.GetId() == YellowPotionId);

        return hasRedPotion && hasBluePotion && hasYellowPotion;
    }

    bool HasPOGPotion()
    {
        bool hasPurplePotion = potionSlots.Any(potion => potion.GetId() == PurplePotionId);
        bool hasOrangePotion = potionSlots.Any(potion => potion.GetId() == OrangePotionId);
        bool hasGreenPotion = potionSlots.Any(potion => potion.GetId() == GreenPotionId);

        return hasPurplePotion && hasOrangePotion && hasGreenPotion;
    }

    void BrewPotion()
    {
        if(!isCombining) return;

        combiningProgress += Time.deltaTime;
        combiningProgressBar.UpdateProgressBar(combiningProgress);
        if(combiningProgress >= combiningDuration)
        {
            for(int index = 0; index < 3; index++)
                SetPotionSlotRenderer(index, itemFactory.MakeNullItem());
            
            isPotionReady = true;
            combiningProgress = 0f;
            ToggleCombining(false);
            createdPotionRenderer.sprite = potionCreated.GetSprite();
        }
    }

    void ToggleCombining(bool toggle)
    {
        isCombining = toggle;
        combiningProgressBar.SetVisible(toggle);
    }

    void SetPotionSlotRenderer(int index, Item item)
    {
        potionSlots[index] = item;
        potionSlotRenderers[index].sprite = item.GetSprite();
    }
}
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    const float MaxTime = 100f;
    const int NoOrder = -1;
    const int Cooldown = 0;
    const int Order = 1;

    ItemFactory itemFactory;
    GameManager gameManager;

    [SerializeField] Image[] orderImages;
    [SerializeField] Sprite[] orderSprites;
    [SerializeField] Color[] orderColors;

    Item[] orderItems = {null, null, null, null};
    [SerializeField] float[] orderTimers = {0f, 15f, 30f, 45f};
    ProgressBar[] orderProgressBars;
    bool potionOrderAvailable = false;

    [SerializeField] float potionOrderDuration = 25f;
    [SerializeField] float specialPotionOrderDuration = 70f;
    [SerializeField] float gemOrderDuration = 5f;
    [SerializeField] float gemOrderDurationModifier = 25f;
    [SerializeField] float cooldownDuration = 5f;

    void Awake()
    {
        itemFactory = FindObjectOfType<ItemFactory>();
        gameManager = GetComponent<GameManager>();
        orderProgressBars = GetComponentsInChildren<ProgressBar>();
    }

    void Start()
    {
        for(int index = 0; index < 4; index++)
        {
            orderItems[index] = itemFactory.MakeNullItem();
            orderProgressBars[index].SetMaxValue(orderTimers[index]);
        }
    }
    
    void Update()
    {
        if(gameManager.CheckGameStopped()) return;
        CountdownOrder();
    }

    Item CreatePotionOrder(int index)
    {
        potionOrderAvailable = false;
        int id = Random.Range(0, 8);
        float duration;
        if(id < 6)
            duration = potionOrderDuration;
        else
            duration = specialPotionOrderDuration;
        
        UpdateMaxDuration(index, duration);

        return itemFactory.MakePotion(id);
    }
    
    Item CreateGemOrder(int index)
    {
        int id = Random.Range(1, 5);
        
        UpdateMaxDuration(index, id * gemOrderDuration + gemOrderDurationModifier);
        
        return itemFactory.MakeGem(id);
    }

    void CountdownOrder()
    {
        for(int orderIndex = 0; orderIndex < 4; orderIndex++)
        {
            orderTimers[orderIndex] -= Time.deltaTime;
            orderProgressBars[orderIndex].UpdateProgressBar(orderTimers[orderIndex]);

            if(orderTimers[orderIndex] < 0)
            {
                if(orderItems[orderIndex] is NullItem)
                {
                    if(potionOrderAvailable)
                        orderItems[orderIndex] = CreatePotionOrder(orderIndex);
                    else
                        orderItems[orderIndex] = CreateGemOrder(orderIndex);

                    UpdateOrderUI(orderIndex, Order);
                }
                else
                    gameManager.GameOver();
            }
        }
    }

    public Item SendShipment(Item item)
    {
        if(!(item is Gem || item is Potion)) return item;

        int lowestIndex = NoOrder;
        float lowestTime = MaxTime;
        for(int index = 0; index < 4; index++)
            if(item == orderItems[index])
                if(orderTimers[index] < lowestTime)
                {
                    lowestIndex = index;
                    lowestTime = orderTimers[index];
                }

        if(lowestIndex == NoOrder) return item;

        if(item is Potion && !potionOrderAvailable)
            potionOrderAvailable = true;
        
        orderItems[lowestIndex] = itemFactory.MakeNullItem();
        UpdateOrderUI(lowestIndex, Cooldown);
        UpdateMaxDuration(lowestIndex, cooldownDuration);
        gameManager.CalculateShipmentScore(item);

        return itemFactory.MakeNullItem();
    }

    void UpdateOrderUI(int index, int iconIndex)
    {
        orderImages[index].sprite = orderItems[index].GetSprite();
        orderProgressBars[index].SetIcon(orderSprites[iconIndex]);
        orderProgressBars[index].SetColor(orderColors[iconIndex]);
    }

    void UpdateMaxDuration(int index, float duration)
    {
        orderTimers[index] = duration;
        orderProgressBars[index].SetMaxValue(duration);
    }

    public void ActivatePotionOrders()
    {
        potionOrderAvailable = true;
    }

    public void ReduceOrderCooldown()
    {
        cooldownDuration--;
    }

    public void ReduceOrderDuration()
    {
        potionOrderDuration--;
        specialPotionOrderDuration --;
        gemOrderDurationModifier -= 2;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class OrderManagers : MonoBehaviour
{
    [SerializeField] Image heldItemImage;
    [SerializeField] Image[] orderImagesList;
    [SerializeField] Image potionOrderImage;
    
    VisualsManager visualsManager;
    DifficultyManagers difficultyManager;
    GameManagers gameManager;
    PotionGenerator potionGenerator;

    const int TOPAZ = 4;
    const int COOLDOWN = 6;
    const int ORDER = 7;
    const int INACTIVE = 8;
    
    const int NO_GEM = 9;

    int[] ordersGem = {NO_GEM, NO_GEM, NO_GEM};
    int[] ordersActivity = {INACTIVE, INACTIVE, INACTIVE};
    float[] orderTimers;
    FloatingSlider[] orderSliders;

    public int potionOrder = -1;

    void Awake()
    {
        visualsManager = FindObjectOfType<VisualsManager>();
        difficultyManager = FindObjectOfType<DifficultyManagers>();
        gameManager = FindObjectOfType<GameManagers>();
        potionGenerator = FindObjectOfType<PotionGenerator>();

        orderSliders = GetComponentsInChildren<FloatingSlider>();
    }

    void Start()
    {      
        float orderCooldown = difficultyManager.GetOrderCooldown();
        orderTimers = new float[] {orderCooldown, orderCooldown, orderCooldown};
    }

    void Update()
    {
        if(gameManager.IsGameEnded()) return;

        for(int index = 0; index < 3; index++)
        {
            if(ordersActivity[index] == INACTIVE) continue;

            CountDownOrder(index);

            if(orderTimers[index] <= 0)
            {
                if(ordersActivity[index] == ORDER)
                {
                    gameManager.CheckGameLost(index);
                    if (gameManager.IsGameEnded())return;
                }
                else
                {
                    ordersGem[index] = MakeOrder();
                    UpdateOrderUI(index, ordersGem[index]);
                }
            }

            orderSliders[index].UpdateSlider(orderTimers[index]);
        }
    }

    void CountDownOrder(int index)
    {
        orderTimers[index] -= Time.deltaTime;
    }

    public void UpdateHeldItemUI(Itemss itemHeld)
    {
        Sprite sprite;
        if(itemHeld.isGem)
            sprite = visualsManager.GetSprite(itemHeld.id);
        else
            sprite = visualsManager.GetPotionSprite(itemHeld.id);
        heldItemImage.sprite = sprite;
    }

    public void UpdatePotionOrderUI(int itemID)
    {
        Sprite sprite = visualsManager.GetPotionSprite(itemID);
        potionOrderImage.sprite = sprite;
    }

    int MakeOrder()
    {
        int gem = Random.Range(difficultyManager.GetGemDifficulty(), TOPAZ+1);

        return gem;
    }

    void UpdateOrderUI(int index, int orderItem)
    {
        float maxPatience = difficultyManager.GetMaxPatience();
        UpdateUI(index, ORDER, maxPatience, orderItem);
    }

    public void UpdateCooldownUI(int index)
    {
        float cooldown = difficultyManager.GetOrderCooldown();
        UpdateUI(index, COOLDOWN, cooldown);
    }

    void UpdateUI(int index, int activity, float maxValue, int orderItem = NO_GEM)
    {
        Sprite  sprite =  visualsManager.GetSprite(orderItem);
        orderImagesList[index].sprite = sprite;

        ordersActivity[index] = activity;

        orderSliders[index].SetNewUI(activity);
        orderSliders[index].SetMaxValue(maxValue);
        orderTimers[index] = maxValue;
    }

    public int GetOrder(int index)
    {
        return ordersGem[index];
    }

    public void StartNewOrder(int index)
    {
        ordersGem[index] = NO_GEM;
        UpdateCooldownUI(index);
    }

    public int GetOrderActivity(int index)
    {
        return ordersActivity[index];
    }

    public float GetOrderTimer(int index)
    {
        return orderTimers[index];
    }

    public void InitiateOrder(int index, float orderCooldown)
    {
        orderSliders[index].SetMaxValue(orderCooldown);
        ordersActivity[index] = COOLDOWN;
    }
    public void SetPotionOrder()
    {
        potionOrder = potionGenerator.GetRandomPotion();
        UpdatePotionOrderUI(potionOrder);
    }
    public int GetPotionOrder()
    {
        return potionOrder;
    }
}

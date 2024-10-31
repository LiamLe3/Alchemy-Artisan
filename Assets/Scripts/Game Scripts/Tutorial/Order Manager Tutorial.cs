using UnityEngine;
using UnityEngine.UI;

public class OrderManagerTutorial : MonoBehaviour
{
    const int Cooldown = 0;
    const int Order = 1;
    const int Diamond = 4;

    ItemFactory itemFactory;

    [SerializeField] Image orderImage;
    [SerializeField] Sprite[] orderSprites;
    [SerializeField] Color[] orderColors;

    Item orderItem = null;
    [SerializeField] float orderTimer = 0f;
    ProgressBar orderProgressBar;

    [SerializeField] float orderDuration = 60f;
    [SerializeField] float cooldownDuration = 5f;

    void Awake()
    {
        itemFactory = FindObjectOfType<ItemFactory>();
        orderProgressBar = GetComponentInChildren<ProgressBar>();
    }

    void Start()
    {
        orderItem = itemFactory.MakeNullItem();
        orderProgressBar.SetMaxValue(orderTimer);
    }
    
    void Update()
    {
        CountdownOrder();
    }

    void CountdownOrder()
    {
        orderTimer -= Time.deltaTime;
        orderProgressBar.UpdateProgressBar(orderTimer);

        if(orderTimer < 0)
        {
            if(orderItem is NullItem)
            {
                orderItem = itemFactory.MakeGem(Diamond);
                UpdateOrderUI(Order);
                UpdateMaxDuration(orderDuration);
            }
        }
    }

    public Item SendShipment(Item item)
    {
        if(item != orderItem) return item;
        
        orderItem = itemFactory.MakeNullItem();
        UpdateOrderUI(Cooldown);
        UpdateMaxDuration(cooldownDuration);

        return itemFactory.MakeNullItem();
    }

    void UpdateOrderUI(int iconIndex)
    {
        orderImage.sprite = orderItem.GetSprite();
        orderProgressBar.SetIcon(orderSprites[iconIndex]);
        orderProgressBar.SetColor(orderColors[iconIndex]);
    }

    void UpdateMaxDuration(float duration)
    {
        orderTimer = duration;
        orderProgressBar.SetMaxValue(duration);
    }
}
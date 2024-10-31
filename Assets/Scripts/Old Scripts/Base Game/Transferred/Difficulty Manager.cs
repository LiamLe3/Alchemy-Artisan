using UnityEngine;
using TMPro;

public class DifficultyManagers : MonoBehaviour
{
    OrderManagers orderManager;
    
    const int DIAMOND = 1;
    const int RUBY = 2;
    const int AMETHYST = 3;
    const int TOPAZ = 4;

    int gemDifficulty = TOPAZ;

    [SerializeField] float orderCooldown = 5.0f;
    [SerializeField] float maxPatience =  45.0f;
    [SerializeField] TextMeshProUGUI levelText;

    int ordersCompleted = 0;

    void Awake()
    {
        orderManager = FindObjectOfType<OrderManagers>();
    }

    void Start()
    {
        ApplyDifficulty();
    }

    public int GetGemDifficulty()
    {
        return gemDifficulty;
    }

    public float GetOrderCooldown()
    {
        return orderCooldown;
    }

    public float GetMaxPatience()
    {
        return maxPatience;
    }
    
    public void CheckDifficulty()
    {
        ordersCompleted++;
        ApplyDifficulty();
    }
    
    void ApplyDifficulty()
    {
        switch(ordersCompleted)
        {
            case 0:
                orderManager.InitiateOrder(0, orderCooldown);
                levelText.text = "Level: 1";
                break;
            case 1:
                orderManager.InitiateOrder(1, orderCooldown);
                levelText.text = "Level: 2";
                break;
            case 2:
                orderManager.InitiateOrder(2, orderCooldown);
                levelText.text = "Level: 3";
                break;
            case 3:
                gemDifficulty = AMETHYST;
                levelText.text = "Level: 4";
                break;
            case 4:
                gemDifficulty = RUBY;
                levelText.text = "Level: 5";
                break;
            case 5:
                gemDifficulty = DIAMOND;
                levelText.text = "Level: 6";
                break;

            case 7:
                orderManager.SetPotionOrder();
                levelText.text = "Level: 7";
                break;

            case 10:
                orderCooldown--;
                levelText.text = "Level: 8";
                break;
            case 15:
                orderCooldown--;
                levelText.text = "Level: 9";
                break;
            case 20:
                orderCooldown--;
                levelText.text = "Level: 10";
                break;
            case 25:
                orderCooldown--;
                levelText.text = "Level: 11";
                break;
            case 30:
                maxPatience--;;
                levelText.text = "Level: 12";
                break;
            case 35:
                maxPatience--;
                levelText.text = "Level: 13";
                break;
            case 40:
                maxPatience--;
                levelText.text = "Level: 14";
                break;
            case 45:
                maxPatience--;
                levelText.text = "Level: 15";
                break;
            case 50:
                maxPatience--;
                levelText.text = "Level: 16";
                break;
            case 60:
                maxPatience--;
                levelText.text = "Level: MAX";
                break;
            default:
                Debug.Log("No difficulty milestone met...");
                break;
        }
    }
}
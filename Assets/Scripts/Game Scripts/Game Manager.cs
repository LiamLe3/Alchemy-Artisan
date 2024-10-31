using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    OrderManager orderManager;

    AudioSource audioSource;
    [SerializeField] AudioClip gameOverSFX;
    [SerializeField] AudioClip successSFX;

    int gameScore;
    bool isGameStopped = false;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] float delay = 0f;

    int ordersCompleted = 0;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        orderManager = GetComponent<OrderManager>();
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(2);
    }
    
    public void GameOver()
    {
        isGameStopped = true;
        StartCoroutine(ChangeSceneAfterDelay());
        audioSource.clip = gameOverSFX;
        audioSource.Play();
    }

    public void ToggleGameStop()
    {
        isGameStopped = !isGameStopped;
    }

    public bool CheckGameStopped()
    {
        return isGameStopped;
    }

    public void CalculateShipmentScore(Item item)
    {
        if(item is Potion)
            if(item.GetId() > 5)
                gameScore += 300;
            else
                gameScore += 100;
        else if(item is Gem gem)
        {
            gameScore += 50 * item.GetId();
            if(gem.IsPolished())
                gameScore += 50;
        }
        
        ordersCompleted++;
        Debug.Log(ordersCompleted);
        DifficultyModifier();
        scoreText.text = "Score\n" + gameScore.ToString();
        audioSource.clip = successSFX;
        audioSource.Play();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    void DifficultyModifier()
    {
        switch(ordersCompleted)
        {
            case 5:
                orderManager.ActivatePotionOrders();
                break;
            case 10:
                orderManager.ReduceOrderCooldown();
                break;
            case 15:
                orderManager.ReduceOrderCooldown();
                break;
            case 20:
                orderManager.ReduceOrderCooldown();
                break;
            case 25:
                orderManager.ReduceOrderCooldown();
                break;
            case 30:
                orderManager.ReduceOrderCooldown();
                break;
            case 35:
                orderManager.ReduceOrderDuration();
                break;
            case 40:
                orderManager.ReduceOrderDuration();
                break;
            case 45:
                orderManager.ReduceOrderDuration();
                break;
            case 50:
                orderManager.ReduceOrderDuration();
                break;
            case 55:
                orderManager.ReduceOrderDuration();
                break;
            case 60:
                orderManager.ReduceOrderDuration();
                break;
            case 65:
                orderManager.ReduceOrderDuration();
                break;
            case 70:
                orderManager.ReduceOrderDuration();
                break;
            case 75:
                orderManager.ReduceOrderDuration();
                break;
            case 80:
                orderManager.ReduceOrderDuration();
                break;
        }
    }
}
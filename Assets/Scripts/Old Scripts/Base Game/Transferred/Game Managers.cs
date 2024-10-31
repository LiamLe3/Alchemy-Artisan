using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagers : MonoBehaviour
{
    OrderManagers orderManager; 
    DifficultyManagers difficultyManager;

    AudioSource audioSource;
    [SerializeField] AudioClip badShipmentSFX;
    [SerializeField] AudioClip failSFX;
    [SerializeField] AudioClip successSFX;

    const int COAL = 0;
    const int DIAMOND = 1;
    const int RUBY = 2;
    const int AMETHYST = 3;
    const int TOPAZ = 4;
    const int EMERALD = 5;
    const int ORDER = 7;

    int gameScore = 0;
    bool isGameEnded = false;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] float  delay = 4f;

    void Awake()
    {
        orderManager = FindObjectOfType<OrderManagers>();
        difficultyManager = FindObjectOfType<DifficultyManagers>();
        audioSource = GetComponent<AudioSource>();
    }

    public void UpdateScore(Itemss shippedItem)
    {
        int shippedScore = GetShippedScore(shippedItem.id);

        if(shippedItem.isPolished)
            shippedScore *= 2;
        else if(!shippedItem.isGem)
            shippedScore = 100;

        gameScore += shippedScore;
        scoreText.text = "Score: " + gameScore.ToString();

        if(shippedScore > 0)
        {
            audioSource.clip = successSFX;
            audioSource.Play();
            difficultyManager.CheckDifficulty();
        }
        else
        {
            audioSource.clip = badShipmentSFX;
            audioSource.Play();
        }
    }

    int GetShippedScore(int itemID)
    {
        switch (itemID)
        {
            case COAL:
                return 0;
            case DIAMOND:
                return 200;
            case RUBY:
                return 150;
            case AMETHYST:
                return 100;
            case TOPAZ:
                return 50;
            case EMERALD:
                return -50;
            default:
                return 0;
        }
    }

    public void CheckGameLost(int index)
    {
        if(orderManager.GetOrderActivity(index) == ORDER && orderManager.GetOrderTimer(index) <= 0)
        {
            
            isGameEnded = true;
            audioSource.clip = failSFX;
            audioSource.Play();
            StartCoroutine(ChangeSceneAfterDelay());
            return;
        }
        return;
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(2);
    }
    
    public bool IsGameEnded()
    {
        return isGameEnded;
    }
}
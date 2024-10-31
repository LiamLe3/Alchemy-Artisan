using TMPro;
using UnityEngine;

public class PotionGuideButton : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject potionGuidePanel;
    [SerializeField] TextMeshProUGUI buttonText;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        potionGuidePanel.SetActive(!potionGuidePanel.activeSelf);
    }
    
    public void ToggleGuide()
    {
        potionGuidePanel.SetActive(!potionGuidePanel.activeSelf);
        if(potionGuidePanel.activeSelf)
            buttonText.text = "Close Guide";
        else
            buttonText.text = "Potion Guide";
        
        gameManager.ToggleGameStop();
    }
}

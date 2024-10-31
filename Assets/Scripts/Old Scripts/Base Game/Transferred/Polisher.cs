using UnityEngine;

public class Polisher : Interactables
{
    const int TRANSPARENT = 10;

    GameManagers gameManager;
    FloatingSlider polisherSlider;
    VisualsManager visualsManager;
    AudioSource audioSource;
    [SerializeField] float polishDuration = 2.0f;
    [SerializeField] SpriteRenderer tableGemSpriteRenderer;
    public float timer = 0;

    Itemss gemToPolish;
    bool isGemOnTable;
    bool isPlayerNear;
    bool isGemPolished;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManagers>();
        visualsManager = FindObjectOfType<VisualsManager>();
        polisherSlider = GetComponentInChildren<FloatingSlider>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        polisherSlider.SetSliderDisplay(false);
        polisherSlider.SetMaxValue(polishDuration);
        tableGemSpriteRenderer.sprite = visualsManager.GetSprite(TRANSPARENT);
    }

    void Update()
    {
        if(gameManager.IsGameEnded()) return;
        if(isGemOnTable)
        {
            PolisherCounter(isPlayerNear);
        }
    }

    public override Itemss Interact(Itemss itemHeld)
    {
        int itemID = itemHeld.id;
        if(isGemOnTable && itemID != NO_GEM) return itemHeld;
        if(!isGemOnTable && itemID == NO_GEM) return itemHeld;
        if(itemID == COAL || itemID == EMERALD) return itemHeld;
        if(itemHeld.isPolished) return itemHeld;
        if(!itemHeld.isGem) return itemHeld;
        
        if(!itemHeld.isPolished && !isGemOnTable)
        {
            isGemOnTable = true;
            gemToPolish = itemHeld;
            isGemPolished = false;
            polisherSlider.SetSliderDisplay(true);
            tableGemSpriteRenderer.sprite = visualsManager.GetSprite(itemID);
        }
        else if(itemID == NO_GEM && isGemOnTable)
        {
            Itemss gem = gemToPolish;
            isGemOnTable = false;
            polisherSlider.SetSliderDisplay(false);
            timer = 0;
            tableGemSpriteRenderer.sprite = visualsManager.GetSprite(TRANSPARENT);
            if(isGemPolished)
            {
                gem = new Itemss(gemToPolish.id, gemToPolish.timeRemaining, true);
            }

            gemToPolish = null;
            return gem;
        }
        
        return GemItem.NO_GEM;
    }
    
    void PolisherCounter(bool isPlayerNear)
    {
        if(isGemPolished) return;

        timer += (isPlayerNear ? 1 : -1) * Time.deltaTime;

        if(isPlayerNear && timer >= polishDuration)
        {
            timer = polishDuration;
            isGemPolished = true;
            audioSource.Play();
        }
        else if(!isPlayerNear && timer <= 0)
        {
            timer = 0;
        }

        polisherSlider.UpdateSlider(timer);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }


}
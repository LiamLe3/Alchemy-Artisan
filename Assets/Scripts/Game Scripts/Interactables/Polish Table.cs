using UnityEngine;

public class PolishTable : Interactable
{
    const int COAL = 0;
    
    Item tableGem;
    [SerializeField] SpriteRenderer tableGemRenderer;
    
    float polishProgress = 0f;
    [SerializeField] float polishDuration = 4f;
    ProgressBar polishProgressBar;
    
    protected override void Awake()
    {
        base.Awake();
        polishProgressBar = GetComponentInChildren<ProgressBar>();
    }

    void Start()
    {
        tableGem = itemFactory.MakeNullItem();
        tableGemRenderer.sprite = tableGem.GetSprite();

        polishProgressBar.SetMaxValue(polishDuration); //Might need to be individual clicks instead
        polishProgressBar.UpdateProgressBar(0);
        polishProgressBar.SetVisible(false);
    }

    public override Item Interact(Item heldItem)
    {
        if(tableGem is Gem gem)
            if(heldItem is not NullItem)
                return heldItem;
            else
                if(gem.IsPolished())
                    return GivePolishedGem();   
                else
                {   
                    UpdatePolishProgress();
                    return heldItem;
                }

        if(heldItem is not Gem heldGem) return heldItem;

        if(heldGem.IsPolished() || heldGem.GetId() == COAL) return heldItem;
    
        tableGem = heldItem;
        polishProgressBar.SetVisible(true);
        tableGemRenderer.sprite = tableGem.GetSprite();

        return itemFactory.MakeNullItem();
    }

    Item GivePolishedGem()
    {
        Item gemToGive = tableGem;
        tableGem = itemFactory.MakeNullItem();
        tableGemRenderer.sprite = tableGem.GetSprite();

        polishProgress = 0f;
        polishProgressBar.UpdateProgressBar(polishProgress);
        polishProgressBar.SetVisible(false);

        return gemToGive;
    }

    void UpdatePolishProgress()
    {
        polishProgress += 1f;
        polishProgressBar.UpdateProgressBar(polishProgress);

        if(polishProgress == polishDuration)
        {
            ((Gem) tableGem).TogglePolished();
        }
    }

    public void SetPolishDuration(float newDuration)
    {
        polishDuration = newDuration;
    }
}
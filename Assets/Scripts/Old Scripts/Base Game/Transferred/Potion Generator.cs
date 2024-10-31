using System.Collections.Generic;
using UnityEngine;

public class PotionGenerator : Interactables
{
    const int NO_POTION = -1;
    const int TRANSPARENT = 10;
    private static readonly Dictionary<string, int> potionIndexMap = new Dictionary<string, int>
    {
        {"B", 0},
        {"P", 1},
        {"R", 2},
        {"Y", 3},
        {"BP", 4},
        {"BR", 5},
        {"BY", 6},
        {"PR", 7},
        {"PY", 8},
        {"RY", 9},
        {"BPR", 10},
        {"BPY", 11},
        {"BRY", 12},
        {"PRY", 13}
    };

    private static readonly Dictionary<int, string> basicPotionMap = new Dictionary<int, string>
    {
        {0, "B"},
        {1, "P"},
        {2, "R"},
        {3, "Y"}
    };

    List<int> potionsStored = new List<int>();

    Animator myAnimator;
    VisualsManager visualsManager;
    [SerializeField] List<SpriteRenderer> spriteRendererList;
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
        visualsManager = FindObjectOfType<VisualsManager>();
    }

    void Start()
    {
        for(int i  = 0; i < 3; i++)
        {
            spriteRendererList[i].sprite = visualsManager.GetSprite(TRANSPARENT);
        }
    }
    public override Itemss Interact(Itemss itemHeld)
    {
        if(itemHeld.isGem && itemHeld.id != NO_GEM) return itemHeld;
        if(potionsStored.Count > 2 && !itemHeld.isGem) return itemHeld;
        if (HasPotion(itemHeld)) return itemHeld;
        if(potionsStored.Count == 0 && itemHeld.isGem && itemHeld.id == NO_GEM) return itemHeld;
        if(itemHeld.id > 3 && !itemHeld.isGem) return itemHeld;
        Itemss newPotion = GemItem.NO_GEM;
        myAnimator.SetBool("isOpen", true);
        if(itemHeld.isGem && itemHeld.id == NO_GEM)
        {
            newPotion = new Itemss(GetPotionIndex(), 0, false, false);
            potionsStored = new List<int>();
            for(int i  = 0; i < 3; i++)
            {
                spriteRendererList[i].sprite = visualsManager.GetSprite(TRANSPARENT);
            }
        }
        else if(potionsStored.Count < 3)
        {
            potionsStored.Add(itemHeld.id);
            spriteRendererList[potionsStored.Count-1].sprite = visualsManager.GetPotionSprite(itemHeld.id);
            return GemItem.NO_GEM;
        }

        return newPotion;
    }
    
    bool HasPotion(Itemss itemHeld)
    {
        int n = potionsStored.Count;
        for(int i = 0; i < n; i++)
            if(potionsStored[i] == itemHeld.id)
                return true;
        
        return false;
    }

    int GetPotionIndex()
    {
        SortPotions();
        return potionIndexMap[GetPotion()];
    }

    void SortPotions()
    {
        int n = potionsStored.Count;
        bool swapped;

        for(int i = 0; i < n - 1; i++)
        {
            swapped = false;
            for(int j = 0; j < n - i - 1; j++)
            {
                if(potionsStored[j] > potionsStored[j + 1])
                {
                    int temp = potionsStored[j];
                    potionsStored[j] = potionsStored[j + 1];
                    potionsStored[j + 1] = temp;

                    swapped = true;
                }
            }
            if (!swapped)
            break;
        }
    }

    string GetPotion()
    {
        int n = potionsStored.Count;
        string potionKey = "";

        for(int i = 0; i < n; i++)
        {
            if(potionsStored[i] != -1)
                potionKey += basicPotionMap[potionsStored[i]];
        }

        return potionKey;
    }

    public int GetRandomPotion()
    {
        return Random.Range(0, 14);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            myAnimator.SetBool("isOpen", false);
        }
    }

}
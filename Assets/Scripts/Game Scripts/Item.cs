using UnityEngine;

public class Item
{
    protected int id;
    protected Sprite itemSprite;

    public static bool operator == (Item item1, Item item2)
    {
        if(ReferenceEquals(item1, null) || ReferenceEquals(item2, null))
        {
            return ReferenceEquals(item1, item2);
        }

        return item1.GetType() == item2.GetType() && item1.id == item2.id;
    }

    public static bool operator != (Item item1, Item item2)
    {
        return !(item1 == item2);
    }

    public override bool Equals(object obj)
    {
        return this == (Item) obj;
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }

    public int GetId()
    {
        return id;
    }

    public Sprite GetSprite()
    {
        return itemSprite;
    }
}

public class Gem : Item
{
    float timer;
    Color color;
    bool isPolished;
    

    public Gem(int id, Sprite itemSprite, float timer, Color color)
    {
        this.id = id;
        this.itemSprite = itemSprite;
        this.timer = timer;
        this.color = color;
    }

    public float GetTimer()
    {
        return timer;
    }
    
    public void SetMaxTimer(float timer)
    {
        this.timer = timer;
    }

    public void CountDownTimer()
    {
        this.timer -= Time.deltaTime;
    }

    public Color GetColor()
    {
        return color;
    }
    public bool IsPolished()
    {
        return isPolished;
    }

    public void TogglePolished()
    {
        isPolished = !isPolished;
    }
}

public class Potion : Item
{
    public Potion(int id, Sprite itemSprite)
    {
        this.id = id;
        this.itemSprite = itemSprite;
    }
}

public class Ingredient : Item
{
    public Ingredient(int id, Sprite itemSprite)
    {
        this.id = id;
        this.itemSprite = itemSprite;
    }
}

public class NullItem : Item
{
    public NullItem(Sprite itemSprite)
    {
        id = -1;
        this.itemSprite = itemSprite;
    }
}
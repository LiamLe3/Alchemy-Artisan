public class Itemss
{
    public int id { get;}
    public float timeRemaining { get;}
    public bool isPolished { get;}
    public bool isGem { get;}

    public Itemss(int id, float timeRemaining = 0, bool isPolished = false, bool isGem = true)
    {
        this.id = id;
        this.timeRemaining = timeRemaining;
        this.isPolished = isPolished;
        this.isGem = isGem;
    }

    public static bool operator == (Itemss item1, Itemss item2)
    {
        if(ReferenceEquals(item1, null))
        {
            return ReferenceEquals(item2, null);
        }

        return item1.id == item2.id;
    }

    public static bool operator != (Itemss item1, Itemss item2)
    {
        return !(item1.id == item2.id);
    }

    public override bool Equals(object obj)
    {
        if(obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Itemss other = (Itemss)obj;
        return id == other.id;
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }
}

public static class GemItem
{
    public static readonly Itemss COAL = new Itemss(0);
    public static readonly Itemss DIAMOND = new Itemss(1);
    public static readonly Itemss RUBY = new Itemss(2);
    public static readonly Itemss AMETHYST = new Itemss(3);
    public static readonly Itemss TOPAZ = new Itemss(4);
    public static readonly Itemss EMERALD = new Itemss(5, 25);
    public static readonly Itemss NO_GEM = new Itemss(9);
}
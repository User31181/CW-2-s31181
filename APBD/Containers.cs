namespace APBD;

public abstract class Container
{
    public ContainerShip? Ship { get; set; }
    public float LoadWeight { get; set; }
    public float Height { get; }
    public float Weight { get; }
    public float Depth { get; }
    public string SerialNumber { get; set; }
    public float MaxLoadWeight { get; }
    public static int Id=1;

    public Container(float height, float weight, float depth, float maxLoadWeight)
    {
        Ship = null;
        LoadWeight = 0;
        Height = height;
        Weight = weight;
        Depth = depth;
        MaxLoadWeight = maxLoadWeight;
    }

    protected string BasicInfo()
    {
        return SerialNumber + " - Load Weight: "+LoadWeight+", Height: " + Height + ", Weight: " + Weight + ", Depth: " + Depth + ", Max Load: " +
               MaxLoadWeight;
    }
    
    public virtual void Unload()
    {
        LoadWeight = 0;
    }
    
    public abstract string Info();

    public virtual void FillLoad(float loadWeight)
    {
        if (loadWeight + LoadWeight > MaxLoadWeight)
            throw new Exception("OverfillException");

        LoadWeight += loadWeight;
    }
    
}

public class CoolingContainer : Container
{
    public static Dictionary<string, float> items = new Dictionary<string, float>
    {
        { "Bananas", 13.3f },
        { "Chocolate", 18 },
        { "Fish", 2 },
        { "Meat", -15 },
        { "Ice Cream", -18 },
        { "Frozen Pizza", -30 },
        { "Cheese", 7.2f },
        { "Sausages", 5 },
        { "Butter", 20.5f },
        { "Eggs", 19 }
    };
    private String ProductInside { get; }

    private float Temperature { get; }
    
    public CoolingContainer(float heigth, float weight, float depth, float maxLoad, float temperature, string product) : base(heigth, weight, depth, maxLoad)
    {
        if(temperature > items[product])
            throw new Exception("Wrong temperature");
                
        Temperature = temperature;
        ProductInside = product;
        SerialNumber = "KON-C-" + Id++;
    }

    public override string Info()
    {
        return BasicInfo()+", Temperature: "+Temperature+", Product: "+ProductInside;
    }
    
}

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; }



    public LiquidContainer(float heigth, float weight, float depth, float maxLoad, int isHazardous) : base(heigth, weight, depth, maxLoad)
    {
        IsHazardous = isHazardous == 1;
        SerialNumber = "KON-L-" + Id++;
    }

    public void HazardNotify()
    {
        Console.WriteLine("Hazard Notify - "+SerialNumber);
    }

    public override string Info()
    {
        return BasicInfo()+", IsHazardous: "+IsHazardous;
    }

    public override void FillLoad(float loadWeight)
    {
        if ((IsHazardous && loadWeight + LoadWeight > MaxLoadWeight *0.5) || (!IsHazardous && loadWeight + LoadWeight > MaxLoadWeight*0.9))
        {
            Console.WriteLine("Dangerous Action");
            return;
        }
        base.FillLoad(loadWeight);
    }
}

public class GasContainer : Container, IHazardNotifier
{
    public float Pressure { get; }
    public GasContainer(float heigth, float weight, float depth, float maxLoad, float pressure) : base(heigth, weight, depth, maxLoad)
    {
        Pressure = pressure;
        SerialNumber = "KON-G-" + Id++;
    }
    
    public void HazardNotify()
    {
        Console.WriteLine("Hazard Notify - "+SerialNumber);
    }

    public override void Unload()
    {
        LoadWeight *= 0.05f;
    }

    public override string Info()
    {
        return BasicInfo()+", Pressure: "+Pressure;
    }
    
}
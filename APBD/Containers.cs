namespace APBD;

public abstract class Container
{
    public ContainerShip? ship { get; set; }
    public float LoadWeight { get; set; }
    public float Height { get; }
    public float Weight { get; }
    public float Depth { get; }
    public string SerialNumber { get; set; }
    public float MaxLoadWeight { get; }
    public static int _id=1;

    public Container(float height, float weight, float depth, float maxLoadWeight)
    {
        ship = null;
        LoadWeight = 0;
        Height = height;
        Weight = weight;
        Depth = depth;
        MaxLoadWeight = maxLoadWeight;
    }

    public string basicInfo()
    {
        return SerialNumber + " - Load Weight: "+LoadWeight+", Height: " + Height + ", Weight: " + Weight + ", Depth: " + Depth + ", Max Load: " +
               MaxLoadWeight;
    }
    
    public virtual void Unload()
    {
        LoadWeight = 0;
    }
    
    abstract public string getInfo();

    public virtual void FillLoad(float loadWeight)
    {
        if (loadWeight + LoadWeight > MaxLoadWeight)
            throw new Exception("OverfillException");

        LoadWeight += loadWeight;
    }
    
}

public class CoolingContainer : Container
{
    public static Dictionary<string, float> items;
    public String ProductInside { get; }
    
    public float Temperature { get; }
    
    public CoolingContainer(float heigth, float weight, float depth, float maxLoad, float temperature, string product) : base(heigth, weight, depth, maxLoad)
    {
        if(temperature > items[product])
            throw new Exception("Wrong temperature");
                
        Temperature = temperature;
        ProductInside = product;
        SerialNumber = "KON-C-" + _id++;
    }

    public override string getInfo()
    {
        return basicInfo()+", Temperature: "+Temperature+", Product: "+ProductInside;
    }

    public static void CreateDictionary()
    { 
        items = new Dictionary<string, float>();
    items["Bananas"] = 13.3f;
    items["Chocholate"] = 18;
    items["Fish"] = 2;
    items["Meat"] = -15;
    items["Ice Cream"] = -18;
    items["Frozen Pizza"] = -30;
    items["Cheese"] = 7.2f;
    items["Sausages"] = 5;
    items["Butter"] = 20.5f;
    items["Eggs"] = 19;
    }
}

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; }



    public LiquidContainer(float heigth, float weight, float depth, float maxLoad, float isHazardous) : base(heigth, weight, depth, maxLoad)
    {
        IsHazardous = isHazardous == 1;
        SerialNumber = "KON-L-" + _id++;
    }

    public void HazardNotify()
    {
        Console.WriteLine("Hazard Notify - "+SerialNumber);
    }

    public override string getInfo()
    {
        return basicInfo()+", IsHazardous: "+IsHazardous;
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
        SerialNumber = "KON-G-" + _id++;
    }
    
    public void HazardNotify()
    {
        Console.WriteLine("Hazard Notify - "+SerialNumber);
    }

    public override void Unload()
    {
        LoadWeight *= 0.05f;
    }

    public override string getInfo()
    {
        return basicInfo()+", Pressure: "+Pressure;
    }
    
}
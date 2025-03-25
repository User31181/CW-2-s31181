namespace APBD;

public class ContainerShip
{
    private static int _id;
    private int myId;
    public List<Container> Containers { get; set; }
    public float MaxSpeed { get; set; }
    public int MaxCapacity { get; set; }
    public float MaxContainersWeight { get; set; }
    

    public ContainerShip(float maxSpeed, int maxCapacity, float maxContainersWeight)
    {
        MaxSpeed = maxSpeed;
        MaxCapacity = maxCapacity;
        MaxContainersWeight = maxContainersWeight;
        
        Containers = new List<Container>();
        myId = _id++;
    }

    public string getInfo()
    {
        return "Ship "+myId+" - "+"Max speed: "+MaxSpeed+", Max Capacity: "+MaxCapacity+", Max Containers Weight: "+MaxContainersWeight+", Containers on ship: "+Containers.Count;
    }
    
    public bool loadContainter(Container container)
    {
        float sumWeight = container.Weight;
        for (int i = 0; i < Containers.Count; i++)
        {
            sumWeight += Containers[i].Weight;
        }

        if (sumWeight > MaxContainersWeight)
        {
            Console.WriteLine("Too much weight");
            return false;
        }
        
        if (Containers.Count == MaxCapacity)
        {
            Console.WriteLine("Ship "+myId+" is full");
            return false;
        }
        if(container.ship!=null)
        {
            Console.WriteLine("Container already on ship "+container.ship.myId);
            return false;
        }
        container.ship = this;
        Containers.Add(container);
        return true;
    }

    public void removeContainer(Container container)
    {
        Containers.Remove(container);
        container.ship = null;
    }
    
    
    
}
namespace APBD;

public class ContainerShip
{
    private static int _id;
    private int myId;
    public List<Container> Containers { get; }
    public float MaxSpeed { get; }
    public int MaxCapacity { get; }
    public float MaxContainersWeight { get; }
    

    public ContainerShip(float maxSpeed, int maxCapacity, float maxContainersWeight)
    {
        MaxSpeed = maxSpeed;
        MaxCapacity = maxCapacity;
        MaxContainersWeight = maxContainersWeight;
        
        Containers = new List<Container>();
        myId = _id++;
    }

    public string Info()
    {
        return "Ship "+myId+" - "+"Max speed: "+MaxSpeed+", Max Capacity: "+MaxCapacity+", Max Containers Weight: "+MaxContainersWeight+", Containers on ship: "+Containers.Count;
    }
    
    public bool LoadContainter(Container container)
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
        if(container.Ship!=null)
        {
            Console.WriteLine("Container already on ship "+container.Ship.myId);
            return false;
        }
        container.Ship = this;
        Containers.Add(container);
        return true;
    }

    public void RemoveContainer(Container container)
    {
        Containers.Remove(container);
        container.Ship = null;
    }
    
    
    
}
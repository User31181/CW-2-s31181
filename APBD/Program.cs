using APBD;

public class Program
{
    public static List<Container> Containers = new List<Container>();
    public static List<ContainerShip> ContainerShips = new List<ContainerShip>();
    public static void Main(string[] args)
    {
        bool run = true;
        while (run)
        {
            try
            {
                Console.WriteLine("Choose option");
                Console.WriteLine("1. Add container");
                Console.WriteLine("2. Fill container");
                Console.WriteLine("3. Unload container");
                Console.WriteLine("4. Add ship");
                Console.WriteLine("5. Fill ship");
                Console.WriteLine("6. Remove container from ship");
                Console.WriteLine("7. Swap containers between ships");
                Console.WriteLine("8. Show ship containers");
                Console.WriteLine("9. Show containers");
                Console.WriteLine("10. Show ships");
                Console.WriteLine("11. Exit");
                Console.Write("Response: ");
                int.TryParse(Console.ReadLine(), out int response);
                Console.Clear();
                switch (response)
                {
                    case 1:
                        CreateContainer();
                        break;
                    case 2:
                        FillContainer();
                        break;
                    case 3:
                        UnloadContainer();
                        break;
                    case 4:
                        CreateShip();
                        break;
                    case 5:
                        AddToShip();
                        break;
                    case 6:
                        RemoveContainerFromShip();
                        break;
                    case 7:
                        SwapContainersBetweenShips();
                        break;
                    case 8:
                        ShowShipContainers();
                        break;
                    case 9:
                        ShowContainers();
                        break;
                    case 10:
                        ShowShips();
                        break;
                    case 11:
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Invalid response");
                        break;
                }

                if(run)Console.ReadLine();
                Console.Clear();
            }
            catch (Exception e)
            {
                Console.ReadLine();
                Console.WriteLine("Something went wrong");
                Console.ReadLine();
            }
        }
    }

    private static void CreateContainer()
    {
        Console.WriteLine("What type of container do you want to create?");
        Console.WriteLine("1. Liquid container");
        Console.WriteLine("2. Gas container");
        Console.WriteLine("3. Cooling container");
        Console.Write("Response: ");
        int.TryParse(Console.ReadLine(), out int response);
        Console.Clear();
        Console.Write("Height: ");
        float.TryParse(Console.ReadLine(), out float heigth);
        Console.Write("Weight: ");
        float.TryParse(Console.ReadLine(), out float weight);
        Console.Write("Depth: ");
        float.TryParse(Console.ReadLine(), out float depth);
        Console.Write("Max load: ");
        float.TryParse(Console.ReadLine(), out float maxLoad);
        switch (response)
        {
            case 1:
                Console.Write("Is hazardous 1/0: ");
                int.TryParse(Console.ReadLine(), out int ishazardous);
                Containers.Add(new LiquidContainer(heigth,weight,depth,maxLoad, ishazardous));
                break;
            case 2:
                Console.Write("pressure: ");
                float.TryParse(Console.ReadLine(), out float pressure);
                Containers.Add(new GasContainer(heigth,weight,depth,maxLoad, pressure));
                break;
            case 3:
                Console.Write("Temperature: ");
                float.TryParse(Console.ReadLine(), out float temperature);
                foreach (string product in CoolingContainer.items.Keys)
                {
                    Console.WriteLine(product+"\t"+CoolingContainer.items[product]);
                }
                Console.Write("Write product name: ");
                string p = Console.ReadLine();
                try
                {
                    Containers.Add(new CoolingContainer(heigth,weight,depth,maxLoad, temperature, p));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                break;
        }
    }

    public static void FillContainer()
    {
        Container con = ChooseContainer();
        Console.Write("Weight: ");
        float.TryParse(Console.ReadLine(), out float weight);
        try{
        con.FillLoad(weight);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static void AddToShip()
    {
        
        for(int i=0;i<Containers.Count;i++)
            if(Containers[i].Ship==null) Console.WriteLine(i+1+". "+Containers[i].Info());
        Console.Write("Choose container: ");
        int.TryParse(Console.ReadLine(), out int con);
        Console.Clear();
        ContainerShip ship = ChooseShip();
        ship.LoadContainter(Containers[con-1]);
    }

    public static Container ChooseContainer()
    {
        for(int i=0;i<Containers.Count;i++)
            Console.WriteLine(i+1+". "+Containers[i].Info());
        Console.WriteLine("Choose container: ");
        int.TryParse(Console.ReadLine(), out int con);
        Console.Clear();
        return Containers[con-1];
    }
    
    public static ContainerShip ChooseShip()
    {
        for(int i=0;i<ContainerShips.Count;i++)
            Console.WriteLine(i+1+". "+ContainerShips[i].Info());
        Console.Write("Choose Ship: ");
        int.TryParse(Console.ReadLine(), out int ship);
        Console.Clear();
        return ContainerShips[ship-1];
    }

    private static void CreateShip()
    {
        Console.Write("Max speed: ");
        float.TryParse(Console.ReadLine(), out float maxSpeed);
        Console.Write("Max Capacity: ");
        int.TryParse(Console.ReadLine(), out int maxCapacity);
        Console.Write("Max Containers Weight: ");
        float.TryParse(Console.ReadLine(), out float maxContainersWeight);
        ContainerShips.Add(new ContainerShip(maxSpeed,maxCapacity,maxContainersWeight));
    }

    private static void ShowShipContainers()
    {
        ContainerShip ship = ChooseShip();
        Console.WriteLine("Contains: ");
        foreach (Container con in ship.Containers)
        {
            Console.WriteLine(con.Info());
        }
    }

    private static void UnloadContainer()
    {
        Container con = ChooseContainer();
        con.Unload();
    }

    private static void ShowContainers()
    {
        Console.WriteLine("Containers list:");
        for(int i=0;i<Containers.Count;i++)
            Console.WriteLine(i+1+". "+Containers[i].Info());
    }

    private static void ShowShips()
    {
        Console.WriteLine("Ships list:");
        for(int i=0;i<ContainerShips.Count;i++)
            Console.WriteLine(ContainerShips[i].Info());
    }

    private static void RemoveContainerFromShip()
    {
        ContainerShip ship = ChooseShip();
        for(int i=0;i<ship.Containers.Count;i++)
            Console.WriteLine(i+1+". "+ship.Containers[i].Info());
        Console.Write("Remove: ");
        int.TryParse(Console.ReadLine(), out int num);
        Container con = ship.Containers[num - 1];
        ship.RemoveContainer(con);
        con.Ship=null;
    }

    private static void SwapContainersBetweenShips()
    {
        ContainerShip ship = ChooseShip();
        for(int i=0;i<ship.Containers.Count;i++)
            Console.WriteLine(i+1+". "+ship.Containers[i].Info());
        Console.Write("Remove: ");
        int.TryParse(Console.ReadLine(), out int num);
        Container con = ship.Containers[num - 1];
        ship.RemoveContainer(con);
        ContainerShip shipToMoveTo = ChooseShip();
        if (!shipToMoveTo.LoadContainter(con))
        {
            ship.LoadContainter(con);
            Console.WriteLine("Impossible Action");
        }
    }
}
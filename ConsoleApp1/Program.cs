using System.ComponentModel;
using System.IO.Pipes;
using System.Xml.Serialization;

public abstract class Marka
{
    protected Marka(string? markaName)
    {
        MarkaName = markaName;
    }

    abstract public string? MarkaName { get; set; }

}
abstract public class Model : Marka
{
    abstract public string? ModelName { get; set; }
    abstract public string? ModelSeries { get; set; }
    abstract public override string? MarkaName { get; set; }

    public Model(string? markaName, string? modelName, string? modelSeries, string? color, string? engine) : base(markaName)
    {
        ModelName = modelName;
        ModelSeries = modelSeries;
    }
}
public class Car : Model
{
    public Car(string? markaName, string? modelName, string? modelSeries, DateTime year, string? color, string? engine) : base(markaName, modelName, modelSeries, color, engine)
    {
        Color = color;
        Engine = engine;
        Year = year;
    }
    public override string? MarkaName { get; set; }
    public override string? ModelName { get; set; }
    public override string? ModelSeries { get; set; }
    public string? Color { get; set; }
    public string? Engine { get; set; }
    public DateTime Year { get; set; }

    public override string ToString()
    {
        return $"Marka:{MarkaName}\nModel:{ModelName} {ModelSeries}\nYear{Year}\nEngine{Engine}\nColor:{Color}";
    }

}
public class Client
{
    public Client(string? name, string? surname)
    {
        Name = name;
        Surname = surname;
    }
    public Guid ID { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public Car? RentedCar { get; set; }
    public String? RentedDate { get; set; }
    public string? RentExpireDate { get; set; }
    public void RentingCar(Car Newcar, int days)
    {
        RentedCar = Newcar;
        DateTime TempDate = DateTime.Now;
        RentExpireDate = $"{TempDate.Year} {TempDate.Month} {TempDate.Day + days}";
    }

    public override string ToString()
    {
        return $"Name{Name}\nSurname:{Surname}\nRented car{RentedCar}\nRent expire date{RentExpireDate}\n";
    }
}

public class RentaCar
{
    public RentaCar(string? rentCarName, List<Car> cars)
    {
        Cars = new List<Car> { };
        Clients = new List<Client> { };
        RentCarName = rentCarName;
        Cars = cars;
    }
    public string? RentCarName { get; set; }
    public List<Car> Cars { get; set; }
    public List<Client> Clients { get; set; }
    public void ShowCars()
    {
        for (int i = 0; i < Cars.Count; i++)
        {
            Console.WriteLine($"{i + 1}:");
            Console.WriteLine(Cars[i]);
        }
    }

    public void ShowRentedCLients()
    {
        foreach (var item in Clients)
        {
            Console.WriteLine(item);
        }
    }
    public void RemoveCarByIndex(int index)
    {
        Cars.Remove(Cars[index]);
    }
    public Client CHoiceCar(Client client)
    {
        ShowCars();
        Console.WriteLine("Choice one of the cars by numbers");
        int choiceCar = int.Parse(Console.ReadLine());
        Console.WriteLine("How many days?");
        int choiceDays = int.Parse(Console.ReadLine());
        client.RentingCar(Cars[choiceCar - 1], choiceDays);
        RemoveCarByIndex(choiceCar - 1);
        return client;
    }

    public void start()
    {
        try
        {

            Console.WriteLine("1:Chocie car\n2:SHow rented clients\n3:Show Cars\n34:Exit");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Name:");
                    string? name = Console.ReadLine(); ;
                    Console.WriteLine("Surname:");
                    string? surname = Console.ReadLine(); ;
                    Clients.Add(CHoiceCar(new Client(name, surname))); break;
                case 2: ShowRentedCLients(); break;
                case 3: ShowCars(); break;
                case 4: return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        start();
    }

}



internal class Program
{
    static void Main(string[] args)
    {
        Car c1 = new Car("Bmw", "750", "li", DateTime.Now, "Black", "4.5");
        Car c2 = new Car("Bmw", "350", "i", DateTime.Now, "Black", "1.5");
        Car c3 = new Car("Bmw", "550", "i", DateTime.Now, "Black", "3.5");
        List<Car> cars = new List<Car> { c1, c2, c3 };
        RentaCar rentaCar = new RentaCar("StepRent", cars);
        Client client = new Client("Emil", "Tagiyev");
        rentaCar.start();
    }
}



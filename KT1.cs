using System;

class Car
{
    private string brand = "";
    private string model = "";
    private int year;
    private int speed;

    public string Brand
    {
        get { return brand; }
        set { brand = value; }
    }

    public string Model
    {
        get { return model; }
        set { model = value; }
    }

    public int Year
    {
        get { return year; }
        set { year = value; }
    }

    public int Speed
    {
        get { return speed; }
        set
        {
            if (value < 0)
                speed = 0;
            else
                speed = value;
        }
    }

    public Car(string brand, string model, int year, int speed)
    {
        this.brand = brand;
        this.model = model;
        this.year = year;
        Speed = speed;
    }

    public void Accelerate(int value)
    {
        if (value > 0)
            Speed += value;
    }

    public void PrintInfo()
    {
        Console.WriteLine("Марка: " + Brand);
        Console.WriteLine("Модель: " + Model);
        Console.WriteLine("Год: " + Year);
        Console.WriteLine("Скорость: " + Speed + " км/ч");
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Car car1 = new Car("BMW", "M5", 2022, 120);
        Car car2 = new Car("Toyota", "Camry", 2020, 80);

        Console.WriteLine("Первый автомобиль:");
        car1.PrintInfo();

        Console.WriteLine("Второй автомобиль:");
        car2.PrintInfo();

        car1.Speed = 150;
        Console.WriteLine("После изменения скорости:");
        car1.PrintInfo();

        car1.Accelerate(20);
        Console.WriteLine("После ускорения:");
        car1.PrintInfo();
    }
}

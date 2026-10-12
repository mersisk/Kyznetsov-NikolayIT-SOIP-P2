using System;

namespace kt2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Delivery delivery1 = new StandardDelivery("Обычная доставка");
            Delivery delivery2 = new ExpressDelivery("Быстрая доставка");
           
            Delivery delivery3 = new InternationalDelivery("Международная");

            decimal basePrice = 20m;

            delivery1.CalculatePrice(basePrice);
           
            delivery2.CalculatePrice(basePrice);
            delivery3.CalculatePrice(basePrice);

            Console.WriteLine(delivery1.ToString());
            Console.WriteLine(delivery2.ToString());
            Console.WriteLine(delivery3.ToString());
        }
    }

    abstract class Delivery
    {
        protected string name;
        protected decimal price;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length > 20)
                {
                    Console.WriteLine("Название должно быть от 1 до 20 символов");
                }
                else
                {
                    name = value;
                }
            }
        }

        public decimal Price
        {
            get
            {
                return price;
            }
        }

        public Delivery(string name)
        {
            Name = name;
        }
        public abstract void CalculatePrice(decimal basePrice);

      
        public override string ToString()
        {
            return "Доставка: " + Name + ", стоимость: " + Price + " рублей";
        }
    }

    
    
    class StandardDelivery : Delivery
    {
        public StandardDelivery(string name) : base(name){}
         public override void CalculatePrice(decimal basePrice)
        {
            price = basePrice * 1.0m;
        }
    }

    class ExpressDelivery : Delivery
    {
        public ExpressDelivery(string name) : base(name)
        {
        }

        public override void CalculatePrice(decimal basePrice)
        {
            price = basePrice * 2.5m;
        }
    }

    class InternationalDelivery : Delivery
    {
        public InternationalDelivery(string name) : base(name)
        {
        }

        public override void CalculatePrice(decimal basePrice)
        {
            price = basePrice * 4.0m;
        }
    }
}

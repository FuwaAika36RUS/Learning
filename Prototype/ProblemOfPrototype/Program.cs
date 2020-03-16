using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemOfPrototype
{
    public class Parking
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
        }
    }
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public override string ToString()
        {
            return $"{nameof(City)}: {City}, {nameof(Street)}: {Street}";
        }
    }
    public class Car
    {
        public string GovernmentNumber { get; set; }
        public string Brand { get; set; }
        public int Place { get; set; }
        public Parking Parking { get; set; }
        public override string ToString()
        {
            return $"{nameof(GovernmentNumber)}: {GovernmentNumber}, {nameof(Brand)}: {Brand}, {nameof(Place)}: {Place}, {nameof(Parking)}: {Parking}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var template = new Parking { Address = new Address { City = "Voronezh", Street = "Zhuckova 4" }, Name = "Zhuckova Parking" };
            var firstCar = new Car
            {
                Brand = "KIA",
                GovernmentNumber = "XXXXXX36RUS",
                Place = 120,
                Parking = template
            };
            firstCar.Parking.Address.Street = "Zhuckova 4a";
            var secondCar = new Car
            {
                Brand = "Volkswagen",
                GovernmentNumber = "XXXXXX48RUS",
                Place = 120,
                Parking = template
            };
            secondCar.Parking.Address.Street = "Zhuckova 4b";

            Console.WriteLine(firstCar);
            Console.WriteLine(secondCar);

            Console.ReadLine();
        }
    }
}

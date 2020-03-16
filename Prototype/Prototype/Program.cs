using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    public static class Extension
    {
        public static T DeepCopy<T>(this T self)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("Object type must be serializable");
            }

            if (self.Equals(null))
            {
                return default(T);
            }

            var binaryFormatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                binaryFormatter.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);
                return (T) binaryFormatter.Deserialize(stream);
            }
        }
    }
    [Serializable]
    public class Parking
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
        }
    }
    [Serializable]
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public override string ToString()
        {
            return $"{nameof(City)}: {City}, {nameof(Street)}: {Street}";
        }
    }
    [Serializable]
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

    public class CarFactory
    {
        private static Car firstParking = new Car
        {
            Parking = new Parking
                {Address = new Address {City = "Voronezh", Street = "Zhuckova 4"}, Name = "Zhuckova Parking"}
        };

        private static Car secondParking = new Car
        {
            Parking = new Parking
                {Address = new Address {City = "Lipetsk", Street = "Tereshkovoy 8"}, Name = "Central Parking"}
        };

        public static Car NewParkingCar(int place, string brand, string govNumber, Car prototype)
        {
            var template = prototype.DeepCopy();
            template.Place = place;
            template.Brand = brand;
            template.GovernmentNumber = govNumber;
            return template;
        }
        public static Car NewFirstParkingCar(int place, string brand, string govNumber)
        {
            return NewParkingCar(place, brand, govNumber, firstParking);
        }
        public static Car NewSecondParkingCar(int place, string brand, string govNumber)
        {
            return NewParkingCar(place, brand, govNumber, secondParking);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var kia = CarFactory.NewFirstParkingCar(113, "KIA", "XXXXXX36RUS");
            var volkswagen = CarFactory.NewSecondParkingCar(12, "Volkswagen", "XXXXXX48RUS");

            Console.WriteLine(kia);
            Console.WriteLine(volkswagen);

            Console.ReadLine();
        }
    }
}

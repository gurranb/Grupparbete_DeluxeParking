using Grupparbete_DeluxeParking.Models;

namespace Grupparbete_DeluxeParking
{
    internal class Helpers
    {
        public static void ListCities()
        {
            Console.WriteLine();
            Console.WriteLine("Id\tCity Name");
            Console.WriteLine("--------------------");

            List<City> cities = Database.GetAllCities();

            foreach (var city in cities)
            {
                Console.WriteLine(city.Id + "\t" + city.Cityname);
            }
            Console.ReadLine();
        }

        public static void ShowCitiesForMaking()
        {
            Console.WriteLine("Id\tCity Name");

            List<City> cities = Database.GetAllCities();

            foreach (var city in cities)
            {
                Console.WriteLine(city.Id + "\t" + city.Cityname);
            }
        }

        public static void InsertCity()
        {
            Console.WriteLine();
            Console.WriteLine("Input name of city");
            City newCity = new City
            {
                Cityname = Console.ReadLine()
            };

            Database.InsertCity(newCity);
            Console.WriteLine("Successfully added city " + newCity.Cityname + " to the list");
            Console.ReadLine();
        }

        public static void InsertParkingHouse()
        {
            Console.WriteLine();
            Console.WriteLine("Input name of Parkinghouse");
            ParkingHouse newParkingHouse = new ParkingHouse();
            newParkingHouse.HouseName = Console.ReadLine();
            ShowCitiesForMaking();
            Console.WriteLine("Input CityId");
            newParkingHouse.CityId = int.Parse(Console.ReadLine());
            Database.InsertParkingHouse(newParkingHouse);
            Console.WriteLine(newParkingHouse.HouseName + " was successfully created");
            Console.ReadLine();
        }

        public static void ListParkingHouses()
        {
            Console.WriteLine();
            Console.WriteLine("Id\tHouse Name\tTotal Slots");
            Console.WriteLine("------------------------------------");

            List<ParkingHouse> parkingHouses = Database.GetAllParkingHouses();

            foreach (var parkingHouse in parkingHouses)
            {
                Console.WriteLine($"{parkingHouse.Id,-5}\t{parkingHouse.HouseName,-15}\t{parkingHouse.TotalParkingSlots}");
            }
            Console.ReadLine();
        }
        public static void ListParkingSlots()
        {
            List<ParkingSlot> parkingSlots = Database.ListAvailableParkingSlots();
            Console.WriteLine();
            Console.WriteLine("City name\tHouse name\tSlotId\t\t\t\tOccupied Slots");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            foreach (var parkingSlot in parkingSlots)
            {
                Console.WriteLine($"{parkingSlot.CityName,-10}\t{parkingSlot.HouseName,-10}\t{parkingSlot.AllSlots,-30}\t{parkingSlot.OccupiedSlots}");
            }
            Console.ReadLine();
        }
        public static void ShowParkingSlots()
        {
            List<ParkingSlot> parkingSlots = Database.ListAvailableParkingSlots();
            Console.WriteLine();
            Console.WriteLine("City name\tHouse name\tSlotId\t\t\t\tOccupied Slots");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            foreach (var parkingSlot in parkingSlots)
            {
                Console.WriteLine($"{parkingSlot.CityName,-10}\t{parkingSlot.HouseName,-10}\t{parkingSlot.AllSlots,-30}\t{parkingSlot.OccupiedSlots}");
            }
        }
        public static void ShowParkingHousesForMaking()
        {
            Console.WriteLine();
            Console.WriteLine("Id\tHouse Name\tTotal Slots");

            List<ParkingHouse> parkingHouses = Database.GetAllParkingHouses();

            foreach (var parkingHouse in parkingHouses)
            {
                Console.WriteLine($"{parkingHouse.Id,-5}\t{parkingHouse.HouseName,-15}\t{parkingHouse.TotalParkingSlots}");
            }
        }

        public static void InsertParkingSpot()
        {
            Console.WriteLine();
            ShowParkingHousesForMaking();
            Console.Write("Input Parkinghouse ID: ");
            int parkingHouseId = int.Parse(Console.ReadLine());

            Console.Write("Input SlotNumber: ");
            string slotNumber = Console.ReadLine();

            Console.Write("Electric Outlet? Type 'true' or 'false': ");
            bool electricOutlet;
            while (!bool.TryParse(Console.ReadLine(), out electricOutlet))
            {
                Console.WriteLine("Invalid input. Please enter 'true' or 'false' for Electric Outlet.");
                Console.Write("Electric Outlet? Type 'true' or 'false': ");
            }

            ParkingSlot newParkingSlot = new ParkingSlot
            {
                ParkingHouseId = parkingHouseId,
                SlotNumber = slotNumber,
                ElectricOutlet = electricOutlet
            };

            int rowsAffected = Database.InsertParkingSlot(newParkingSlot);
            Console.WriteLine("A new parkingSlot in ParkingHouseId " + newParkingSlot.ParkingHouseId + " was successfully created");
            Console.ReadLine();
        }

        public static void MakeNewCar()
        {
            Console.WriteLine();
            Console.Write("Input make of the car: ");
            string carMake = Console.ReadLine();

            Console.Write("Input color of car: ");
            string color = Console.ReadLine();

            Random rnd = new Random();
            Car newCar = new Car
            {
                Make = carMake,
                Plate = "XPQ" + rnd.Next(100, 999),
                Color = color
            };
            Database.InsertCar(newCar);
            Console.WriteLine("Successfully created " + newCar.Make + " " + newCar.Plate);
            Console.ReadLine();
        }
        public static void ListCars()
        {
            Console.WriteLine();
            Console.WriteLine("Id\tPlate\t\tMake\t\tColor");
            Console.WriteLine("--------------------------------------------------");
            List<Car> parkedCars = Database.GetAllCars();
            foreach (Car car in parkedCars)
            {
                Console.WriteLine($"{car.Id,-3}\t{car.Plate,-10}\t{car.Make,-8}\t{car.Color,-8}");
            }
            Console.ReadLine();
        }
        public static void ShowCars()
        {
            Console.WriteLine();
            Console.WriteLine("Id\tPlate\t\tMake\t\tColor");
            Console.WriteLine("--------------------------------------------------");
            List<Car> parkedCars = Database.GetAllCars();
            foreach (Car car in parkedCars)
            {
                Console.WriteLine($"{car.Id,-3}\t{car.Plate,-10}\t{car.Make,-8}\t{car.Color,-8}");
            }
        }
        public static void ParkACar()
        {
            Console.WriteLine();
            ShowCars();
            Console.Write("Input carID: ");
            int carId = int.Parse(Console.ReadLine());
            ShowParkingSlots();
            Console.Write("Input SlotId: ");
            int spotId = int.Parse(Console.ReadLine());
            Database.ParkCar(carId, spotId);
            Console.WriteLine("Successfully parked car with ID " + carId + " to slotNumber " +  spotId);
            Console.ReadLine();
        }

    }
}

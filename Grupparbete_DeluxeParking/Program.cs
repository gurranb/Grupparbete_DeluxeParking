namespace Grupparbete_DeluxeParking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Deluxe Parking");
                Console.WriteLine("[1] List Cities\n[2] Add a City\n[3] List Parkinghouses\n" +
                    "[4] Add a ParkingHouse\n[5] Add new ParkingSlot\n[6] Create a car\n[7] List of cars" +
                    "\n[8] Park a car\n[9] Display all carSlots");
                var key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case '1':
                        Helpers.ListCities();
                        break;
                    case '2':
                        Helpers.InsertCity();
                        break;
                    case '3':
                        Helpers.ListParkingHouses();
                        break;
                    case '4':
                        Helpers.InsertParkingHouse();
                        break;
                    case '5':
                        Helpers.InsertParkingSpot();
                        break;
                    case '6':
                        Helpers.MakeNewCar();
                        break;
                    case '7':
                        Helpers.ListCars();
                        break;
                    case '8':
                        Helpers.ParkACar();
                        break;
                    case '9':
                        Helpers.ListParkingSlots();
                        break;

                }
            }
        }
    }
}
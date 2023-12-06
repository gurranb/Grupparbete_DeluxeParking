using Dapper;
using Grupparbete_DeluxeParking.Models;
using System.Data.SqlClient;
using System.Reflection;

namespace Grupparbete_DeluxeParking
{
    internal class Database
    {
        static string connString = "data source = .\\SQLEXPRESS; initial catalog = Parking10; persist security info = True; integrated security = True;";

        public static List<City> GetAllCities()
        {
            string sql = "SELECT * FROM Cities";
            List<City> cities = new List<City>();

            using (var connection = new SqlConnection(connString))
            {
                cities = connection.Query<City>(sql).ToList();
            }
            return cities;
        }

        public static int InsertCity(City city)
        {
            int affectedRows = 0;
            string sql = $"INSERT INTO Cities(CityName) VALUES ('{city.Cityname}')";

            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
            return affectedRows;
        }

        public static int InsertParkingHouse(ParkingHouse parkinghouse)
        {
            int affectedRows = 0;
            string sql = $"INSERT INTO ParkingHouses(HouseName, CityId) VALUES ('{parkinghouse.HouseName}', '{parkinghouse.CityId}')";

            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
            return affectedRows;
        }

        public static List<ParkingHouse> GetAllParkingHouses()
        {
            string sql = @"
                SELECT
	                ph.Id AS Id,
                    ph.HouseName,
                    COUNT(ps.[Id]) AS TotalParkingSlots
                FROM
                    Parkinghouses ph
                LEFT JOIN
                    ParkingSlots ps ON ph.Id = ps.ParkingHouseId
                GROUP BY
                ph.Id, ph.HouseName";
            List<ParkingHouse> parkingHouses = new List<ParkingHouse>();

            using (var connection = new SqlConnection(connString))
            {
                parkingHouses = connection.Query<ParkingHouse>(sql).ToList();

            }
            return parkingHouses;
        }

        public static int InsertParkingSlot(ParkingSlot parkingSlot)
        {
            int affectedRows = 0;
            string sql = $"INSERT INTO ParkingSlots(ParkingHouseId, SlotNumber, ElectricOutlet) VALUES ('{parkingSlot.ParkingHouseId}', '{parkingSlot.SlotNumber}', '{parkingSlot.ElectricOutlet}')";

            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
            return affectedRows;
        }

        public static int InsertCar(Car car)
        {
            int affectedRows = 0;
            string sql = $"INSERT INTO Cars(Plate, Make, Color) VALUES ('{car.Plate}', '{car.Make}', '{car.Color}')";

            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
            return affectedRows;
        }
        public static List<Car> GetAllCars()
        {
            string sql = "SELECT * FROM Cars";
            List<Car> parkedCars = new List<Car>();

            using (var connection = new SqlConnection(connString))
            {
                parkedCars = connection.Query<Car>(sql).ToList();
            }
            return parkedCars;
        }

        public static int ParkCar(int carId, int? spotId)
        {
            int affectedRows = 0;

            string sql = $"UPDATE Cars SET ParkingSlotsId=" + (spotId == null ? "NULL" : spotId) + $" WHERE Id = {carId}";
            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
            return affectedRows;
        }
        public static List<ParkingSlot> ListAvailableParkingSlots()
        {
            string sql = @"
                SELECT
                    c.CityName,
                    ph.HouseName,
                    STRING_AGG(ps.Id, ', ') AS AllSlots,
                    STRING_AGG(CASE WHEN car.ParkingSlotsId IS NULL THEN 'Free' ELSE car.Plate END, ', ') AS OccupiedSlots
                FROM 
                    Cities c
                JOIN
                    ParkingHouses ph ON ph.CityId = c.Id
                JOIN
                    ParkingSlots ps ON ph.Id = ps.ParkingHouseId
                LEFT JOIN
                    Cars car ON ps.Id = car.ParkingSlotsId
                GROUP BY 
                    ph.HouseName, c.CityName";

            using (var connection = new SqlConnection(connString))
            {
                return  connection.Query<ParkingSlot>(sql).ToList();
            }
        }

    }
}

using Dapper;
using Grupparbete_DeluxeParking.Models;
using System.Data.SqlClient;

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
        public static List<ParkingSlot>ShowAllParkingSlots()
        {
            string sql = @"
                SELECT
                    ph.HouseName,
                    STRING_AGG(CONVERT(VARCHAR(10), ps.SlotNumber) + CASE WHEN ps.ElectricOutlet = 1 THEN ' (E)' ELSE '(N)' END, ', ') AS ParkingSlots
                FROM
                    ParkingHouses ph
                LEFT JOIN
                    ParkingSlots ps ON ph.[Id] = ps.ParkingHouseId
                GROUP BY
                    ph.HouseName";

            List<ParkingSlot> allSlots = new List<ParkingSlot>();

            using (var connection = new SqlConnection(connString))
            {
                allSlots = connection.Query<ParkingSlot>(sql).ToList();

            }
            return allSlots;
        }
        public static List<ParkingSlot> ShowElectricOutletsCities()
        {
            string sql = @"
                SELECT
                    c.CityName AS CityName,
                    COUNT(ps.[Id]) AS TotalElectricSpots
                FROM
                    Cities c
                JOIN
                    ParkingHouses ph ON c.[Id] = ph.CityId
                JOIN
                    ParkingSlots ps ON ph.[Id] = ps.ParkingHouseId
                WHERE
                    ps.ElectricOutlet = 1
                GROUP BY
                    c.CityName";
            List<ParkingSlot> electricOutlets = new List<ParkingSlot>();

            using (var connection = new SqlConnection(connString))
            {
                electricOutlets = connection.Query<ParkingSlot>(sql).ToList();

            }
            return electricOutlets;
        }
        public static List<ParkingSlot> ShowElectricOutletsParkingHouses()
        {
            string sql = @"
                SELECT
                    ph.HouseName AS HouseName,
                    COUNT(ps.[Id]) AS TotalElectricSpots
                FROM
                    ParkingHouses ph
                JOIN
                    ParkingSlots ps ON ph.[Id] = ps.ParkingHouseId
                WHERE
                    ps.ElectricOutlet = 1
                GROUP BY
                    ph.HouseName";
            List<ParkingSlot> electricOutlets2 = new List<ParkingSlot>();

            using (var connection = new SqlConnection(connString))
            {
                electricOutlets2 = connection.Query<ParkingSlot>(sql).ToList();

            }
            return electricOutlets2;
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
        public static bool CarExists(int carId)
        {
            using (var connection = new SqlConnection(connString))
            {
                string sql = "SELECT COUNT(*) FROM Cars WHERE Id = @carId";
                int count = connection.QuerySingle<int>(sql, new { carId });

                return count > 0;
            }
        }
        public static bool SlotExists(int spotId)
        {
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM ParkingSlots WHERE Id = @spotId";
                int count = connection.QuerySingle<int>(sql, new { spotId });

                return count > 0;
            }
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
                return connection.Query<ParkingSlot>(sql).ToList();
            }
        }
    }
}

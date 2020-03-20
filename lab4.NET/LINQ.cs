using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace lab4.NET
{
    class LINQ
    {
        private static List<River> Rivers = River.Rivers;
        private static List<Sea> Seas = Sea.Seas;
        private static List<ShippingCompany> ShippingCompanies = ShippingCompany.ShippingCompanies;
        private static List<Ship> Ships = Ship.Ships;

        public static void CreateObjects()
        {
            River r1 = new River("Днепр", 5600, 750, 14);
            River r2 = new River("Рось", 3200, 430, 7);
            River r3 = new River("Волга", 14400, 920, 22);

            Sea sea1 = new Sea("Черное море", 422000);
            Sea sea2 = new Sea("Азовское море", 39000);

            ShippingCompany sc1 = new ShippingCompany("WaterLeader", r1);
            ShippingCompany sc2 = new ShippingCompany("Flying Ships", sea1);
            ShippingCompany sc3 = new ShippingCompany("Best SAAAS", r2);
            ShippingCompany sc4 = new ShippingCompany("Dnipro Stats", r1);
            ShippingCompany sc5 = new ShippingCompany("Super Sea", sea2);
            ShippingCompany sc6 = new ShippingCompany("Fast Rivers", r3);

            Ship s1 = new Ship("Наяда", 20, 2001, sc1, r1);
            Ship s2 = new Ship("Алмаз", 30, 2006, sc1, r1);
            Ship s3 = new Ship("Сапфир", 18, 2006, sc2, sea1);
            Ship s4 = new Ship("Аляска", 8, 2008, sc4, r1);
            Ship s5 = new Ship("Дельфин", 12, 1998, sc5, sea2);
            Ship s6 = new Ship("Победа", 18, 2018, sc6, r3);
            Ship s7 = new Ship("Аврора", 18, 2002, sc3, r2);
            Ship s8 = new Ship("Анталья", 30, 2015, sc2, sea1);
        }

        public static void OutputAllData()
        {
            OutputRivers();
            Console.WriteLine();
            OutputSeas();
            Console.WriteLine();
            OutputShippingCompanies();
            Console.WriteLine();
            OutputShips();
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void OutputRivers()
        {
            foreach (var e in River.Rivers)
            {
                Console.WriteLine(e);
            }
        }

        public static void OutputShips()
        {
            foreach (var e in Ship.Ships)
            {
                Console.WriteLine(e);
            }
        }

        public static void OutputSeas()
        {
            foreach (var e in Sea.Seas)
            {
                Console.WriteLine(e);
            }
        }

        public static void OutputShippingCompanies()
        {
            foreach (var e in ShippingCompany.ShippingCompanies)
            {
                Console.WriteLine(e);
            }
        }

        public static void Output(IEnumerable res)
        {
            foreach (var e in res)
            {
                Console.WriteLine(e);
            }
        }

        public static void GetLongestRiver()
        {
            var res = Rivers.Where(r => r.Length == Rivers.Max(r => r.Length));
            foreach (var e in res)
            {
                Console.WriteLine(e);
            }
        }

        public static void GetRiverWithShortestName()
        {
            var res = Rivers.OrderBy(r => r.Name.Length).FirstOrDefault();
            Console.WriteLine(res);
        }

        public static void GetShipsGroupByTransportation()
        {
            var res = Ships.Where(r => r.PassengerCapacity < 30).GroupBy(r => r.Type);
            foreach (var e in res)
            {
                Console.WriteLine("Место плавания:" + e.Key.Name);
                foreach (var t in e)
                {
                    Console.WriteLine("Корабль:" + t.Name);
                }

                Console.WriteLine();
            }
        }

        public static void GetShipsNameStartsWithA()
        {
            var res = from p in Ships where p.Name[0] == 'А' select p;
            //var res1 = Ships.Where(p => p.Name.StartsWith('A')).Select(p=>p.Name);
            foreach (var e in res)
            {
                Console.WriteLine(e);
            }
        }

        public static void GetShipsByConstructionYearTakeConcatSkip()
        {
            var res = Ships.Where(s => s.ConstructionYear > 2002).Take(2).Concat(Ships.Skip(4));
            Output(res);
        }

        public static void GetRiversByInflowCount()
        {
            var res = Rivers.OrderByDescending(r => r.InflowCount);
            Output(res);
        }

        public static void GetShipsJoinShippingCompanies()
        {
            var res = from s in Ships
                join p in ShippingCompanies on s.ShippingCompany.Name equals p.Name
                select new {Name = s.Name, ShippingCompany = p.Name, ConstructionYear = s.ConstructionYear};

            foreach (var e in res)
            {
                Console.WriteLine(
                    $"Корабль: {e.Name};  \tКомпания: {e.ShippingCompany};\t\tГод сборки: {e.ConstructionYear}");
            }
        }

        public static void GetShipsByConstructionYearDistinct()
        {
            var res = Ships.GroupBy(r => r.ConstructionYear).Select(g => g.First());
            Output(res);
        }

        public static void GetShipsIntersect()
        {
            var res = Ships.Take(5).Intersect(Ships.Skip(4));
            Output(res);
        }

        public static void GetShipsByContains()
        {
            var res = Ships.Where(p =>
                    p.Name.Contains('н', StringComparison.InvariantCultureIgnoreCase) && p.ConstructionYear > 2000)
                .Select(p => p.Name);
            Output(res);
        }

        public static void GetShipsByTransporationPlace()
        {
            var res = Ships.GroupBy(r => r.Type.GetType().Name);
            foreach (var e in res)
            {
                Console.WriteLine("Место плавания:" + e.Key);
                foreach (var t in e)
                {
                    Console.WriteLine("Корабль:" + t.Name);
                }

                Console.WriteLine();
            }

        }

        public static void GetShipsByBiggestArea() // 12
        {
            var res =
                Ships.Where(r => r.Type.GetType().Name == "Sea")
                    .Join(Seas, p => p.Type.Name, s => s.Name,
                        (p, s) => new {Ship = p.Name, Sea = s.Name, Area = s.Area})
                    .OrderBy(p => p.Area).ThenBy(r=>r.Ship);
            foreach (var e in res)
            {
                Console.WriteLine($"Ship: {e.Ship};\t Sea: {e.Sea};\t Area: {e.Area}");
            }
        }

        public static void GetShippingCompanyByLengthAndRiver()
        {
            var res = ShippingCompanies
                .Where(r => r.Location.GetType().Name == "River")
                .OrderByDescending(r => r.Name.Length)
                .FirstOrDefault();
            Console.WriteLine(res);
        }

        public static void GetShipsTakeWhilePassengerCapacity()
        {
            var res = Ships.TakeWhile(r => r.PassengerCapacity > 10);
            Output(res);
        }

        public static void GetShipsOrderByPasCapThenByDescConstYear()
        {
            var res = Ships
                .OrderBy(r => r.PassengerCapacity)
                .ThenByDescending(r => r.ConstructionYear);
            Output(res);
        }

        public static void Menu()
        {
            MethodInfo[] methodInfos = typeof(LINQ)
                .GetMethods(BindingFlags.Public | BindingFlags.Static);
            CreateObjects();
            while (true)
            {
                OutputAllData();

                int i = 1;
                foreach (MethodInfo methodInfo in methodInfos)
                {
                    if (methodInfo.Name.StartsWith("G"))
                    {
                        Console.WriteLine($"{i++}) {methodInfo.Name}");
                    }
                }

                Console.Write("Enter menu item: ");
                int key = Convert.ToInt32(Console.ReadLine());
                switch (key)
                {
                    case 1:
                        GetLongestRiver();
                        break;
                    case 2:
                        GetRiverWithShortestName(); 
                        break;
                    case 3:
                        GetShipsGroupByTransportation();
                        break;
                    case 4:
                        GetShipsNameStartsWithA();
                        break;
                    case 5:
                        GetShipsByConstructionYearTakeConcatSkip();
                        break;
                    case 6:
                        GetRiversByInflowCount();
                        break;
                    case 7:
                        GetShipsJoinShippingCompanies();
                        break;
                    case 8:
                        GetShipsByConstructionYearDistinct();
                        break;
                    case 9:
                        GetShipsIntersect();
                        break;
                    case 10:
                        GetShipsByContains();
                        break;
                    case 11:
                        GetShipsByTransporationPlace();
                        break;
                    case 12:
                        GetShipsByBiggestArea();
                        break;
                    case 13:
                        GetShippingCompanyByLengthAndRiver();
                        break;
                    case 14:
                        GetShipsTakeWhilePassengerCapacity();
                        break;
                    case 15:
                        GetShipsOrderByPasCapThenByDescConstYear();
                        break;
                    default:
                        Console.WriteLine("There are not such item in a menu!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}

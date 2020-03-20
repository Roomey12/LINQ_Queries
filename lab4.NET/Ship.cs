using System;
using System.Collections.Generic;
using System.Text;

namespace lab4.NET
{
    class Ship
    {
        public string Name { get; set; }
        public int PassengerCapacity { get; set; }
        public int ConstructionYear { get; set; }
        public ShippingCompany ShippingCompany { get; set; }
        public TransporationPlace Type { get; set; }

        public static List<Ship> Ships = new List<Ship>();
        public Ship(string name, int pasCap, int conYear, ShippingCompany shipComp, TransporationPlace type)
        {
            Name = name;
            PassengerCapacity = pasCap;
            ConstructionYear = conYear;
            ShippingCompany = shipComp;
            Type = type;
            Ships.Add(this);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name},\t {nameof(PassengerCapacity)}: {PassengerCapacity},\t {nameof(ConstructionYear)}: {ConstructionYear},\t {nameof(ShippingCompany)}: {ShippingCompany.Name},\t {nameof(Type)}: {Type.Name}";
        }
    }
}

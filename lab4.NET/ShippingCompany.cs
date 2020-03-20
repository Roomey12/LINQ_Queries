using System;
using System.Collections.Generic;
using System.Text;

namespace lab4.NET
{
    class ShippingCompany
    {
        public string Name { get; set; }
        public TransporationPlace Location { get; set; }

        public static List<ShippingCompany> ShippingCompanies = new List<ShippingCompany>();
        public ShippingCompany(string name, TransporationPlace location)
        {
            Name = name;
            Location = location;
            ShippingCompanies.Add(this);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name},\t {nameof(Location)}: {Location.Name}";
        }
    }
}

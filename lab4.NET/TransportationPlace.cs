using System;
using System.Collections.Generic;
using System.Text;

namespace lab4.NET
{
    interface TransporationPlace
    {
        string Name { get; set; }
        
    }


    class River : TransporationPlace
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public int MaxWidth { get; set; }
        public int InflowCount { get; set; }

        public static List<River> Rivers = new List<River>();
        public River(string name, int length, int maxWidth, int inflowCount)
        {
            Name = name;
            Length = length;
            MaxWidth = maxWidth;
            InflowCount = inflowCount;
            Rivers.Add(this);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name},\t {nameof(Length)}: {Length},\t {nameof(MaxWidth)}: {MaxWidth},\t {nameof(InflowCount)}: {InflowCount}";
        }
    }

    class Sea : TransporationPlace
    {
        public string Name { get; set; }
        public float Area { get; set; }
        public static List<Sea> Seas = new List<Sea>();
        public Sea(string name, float area)
        {
            Name = name;
            Area = area;
            Seas.Add(this);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name},\t {nameof(Area)}: {Area}";
        }
    }
}

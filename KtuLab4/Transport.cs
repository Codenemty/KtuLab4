using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KtuLab4
{
    public abstract class Transport : IComparable<Transport>, IEquatable<Transport>
    {
        public string CarPlate { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int YearMade { get; set; }
        public int MonthMade { get; set; }
        public DateTime LastTechDate { get; set; }
        public string Fuel { get; set; }
        public float FuelPer100 { get; set; }

        public Transport(string carPlate, string manufacturer, string model, int yearMade, int monthMade, DateTime lastTechDate, string fuel, float fuelPer100)
        {
            CarPlate = carPlate;
            Manufacturer = manufacturer;
            Model = model;
            YearMade = yearMade;
            MonthMade = monthMade;
            LastTechDate = lastTechDate;
            Fuel = fuel;
            FuelPer100 = fuelPer100;
        }

        public abstract int CompareTo(Transport other);
        public abstract TableRow ConvertToRow();
        public abstract bool Equals(Transport other);

        public abstract bool BetterThan(Transport other);
        public abstract bool NeedsTech();
    }
}
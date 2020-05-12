using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KtuLab4
{
    public class Truck : Transport
    {
        public float CarryingCapacity { get; set; }
        public Truck(string carPlate, string manufacturer, string model, int yearMade, int monthMade, DateTime lastTechDate, string fuel, float fuelPer100, float carryingCapacity) : base(carPlate, manufacturer, model, yearMade, monthMade, lastTechDate, fuel, fuelPer100)
        {
            CarryingCapacity = carryingCapacity;
        }

        public override int CompareTo(Transport other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(Transport other)
        {
            throw new NotImplementedException();
        }

        public override TableRow ConvertToRow()
        {
            TableRow row = new TableRow();
            row.Cells.Add(new TableCell() { Text = CarPlate });
            row.Cells.Add(new TableCell() { Text = Manufacturer });
            row.Cells.Add(new TableCell() { Text = Model });
            row.Cells.Add(new TableCell() { Text = YearMade.ToString() });
            row.Cells.Add(new TableCell() { Text = MonthMade.ToString() });
            row.Cells.Add(new TableCell() { Text = LastTechDate.ToString().Split(' ')[0] });
            row.Cells.Add(new TableCell() { Text = Fuel });
            row.Cells.Add(new TableCell() { Text = FuelPer100.ToString() });
            row.Cells.Add(new TableCell() { Text = CarryingCapacity.ToString() });
            return row;
        }

        public override bool BetterThan(Transport other)
        {
            if (other is Truck) return this.CarryingCapacity > ((Truck)other).CarryingCapacity;
            return false;
        }

        public override bool NeedsTech()
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace KtuLab4
{
    public class Bus : Transport
    {
        public int SeatCount { get; set; }
        public Bus(string carPlate, string manufacturer, string model, int yearMade, int monthMade, DateTime lastTechDate, string fuel, float fuelPer100, int seatCount) : base(carPlate, manufacturer, model, yearMade, monthMade, lastTechDate, fuel, fuelPer100)
        {
            SeatCount = seatCount;
        }

        public override int CompareTo(Transport other)
        {
            Bus b = (other as Bus);

            if (this.Manufacturer == b.Manufacturer) return this.YearMade.CompareTo(b.YearMade);
            return this.Manufacturer.CompareTo(other.Manufacturer);
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
            row.Cells.Add(new TableCell() { Text = SeatCount.ToString() });

            return row;
        }

        public override bool BetterThan(Transport other)
        {
            if (other is Bus) return this.SeatCount > ((Bus)other).SeatCount;
            return false;
        }

        public override bool NeedsTech()
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KtuLab4
{
    public static class IO
    {
        public static List<string> GetAllBranches()
        {
            List<string> branches = new List<string>();

            foreach(string branch in Directory.GetDirectories(HttpContext.Current.Server.MapPath("./App_Data/CompanyData")).ToList())
            {
                string name = branch.Split('\\').Last();
                branches.Add(name);
            }
            return branches;
        }

        public static List<Branch> GetBranchesFromFolder(string path)
        {
            List<Branch> branches = new List<Branch>();
            string[] files = Directory.GetFiles(HttpContext.Current.Server.MapPath($@"./App_Data/CompanyData/{path}/"));

            foreach (string file in files)
            {
                List<Transport> list = new List<Transport>();
                StreamReader sr = new StreamReader(file);

                string city = sr.ReadLine();
                string address = sr.ReadLine();
                string phone = sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] args = line.Split(';');
                    string type = args[0];
                    string carPlate = args[1];
                    string manufacturer = args[2];
                    string model = args[3];
                    int yearMade = int.Parse(args[4]);
                    int monthMade = int.Parse(args[5]);
                    int[] dates = args[6].Split('-').Select(x => int.Parse(x)).ToArray();
                    DateTime lastTechDate = new DateTime(dates[0], dates[1], dates[2]);
                    string fuel = args[7];
                    float fuelPer100 = float.Parse(args[8]);

                    switch (type.ToLower())
                    {
                        case "car":
                            int odometer = int.Parse(args[9]);
                            Car car = new Car(carPlate, manufacturer, model, yearMade, monthMade, lastTechDate, fuel, fuelPer100, odometer);
                            list.Add(car);
                            break;
                        case "truck":
                            float carryingCapacity = float.Parse(args[9]);
                            Truck truck = new Truck(carPlate, manufacturer, model, yearMade, monthMade, lastTechDate, fuel, fuelPer100, carryingCapacity);
                            list.Add(truck);
                            break;
                        case "bus":
                            int seatCount = int.Parse(args[9]);
                            Bus bus = new Bus(carPlate, manufacturer, model, yearMade, monthMade, lastTechDate, fuel, fuelPer100, seatCount);
                            list.Add(bus);
                            break;
                    }
                }

                sr.Close();
                Branch branch = new Branch(list, city, address, phone);
                branches.Add(branch);
            }
            return branches;
        }
        public static void PrintBestVehicles(Panel resultPanel, List<Transport> list)
        {
            Table t = new Table();
            Label tableLabel = new Label() { Text = "Best vehicles of it's type" };

            Car car = CompanyProgram.GetBest<Car>(list);
            Bus bus = CompanyProgram.GetBest<Bus>(list);
            Truck truck = CompanyProgram.GetBest<Truck>(list);

            TableHeaderRow thr = new TableHeaderRow();
            thr.Controls.Add(new TableHeaderCell() { Text = "Type" });
            thr.Controls.Add(new TableHeaderCell() { Text = "Manufacturer" });
            thr.Controls.Add(new TableHeaderCell() { Text = "Model" });
            thr.Controls.Add(new TableHeaderCell() { Text = "Car Plate" });
            thr.Controls.Add(new TableHeaderCell() { Text = "Age" });
            t.Rows.Add(thr);

            if (car != null)
            {
                DateTime carDate = new DateTime(DateTime.Now.Ticks - new DateTime(car.YearMade, car.MonthMade, 1).Ticks);

                TableRow carTr = new TableRow();
                carTr.Cells.Add(new TableCell() { Text = "Car" });
                carTr.Cells.Add(new TableCell() { Text = car.Manufacturer });
                carTr.Cells.Add(new TableCell() { Text = car.Model });
                carTr.Cells.Add(new TableCell() { Text = car.CarPlate });
                carTr.Cells.Add(new TableCell() { Text = $@"{carDate.Year}-{carDate.Month}" });
                t.Rows.Add(carTr);
            }
            if (bus != null)
            {
                DateTime busDate = new DateTime(DateTime.Now.Ticks - new DateTime(bus.YearMade, bus.MonthMade, 1).Ticks);

                TableRow busTr = new TableRow();
                busTr.Cells.Add(new TableCell() { Text = "Bus" });
                busTr.Cells.Add(new TableCell() { Text = bus.Manufacturer });
                busTr.Cells.Add(new TableCell() { Text = bus.Model });
                busTr.Cells.Add(new TableCell() { Text = bus.CarPlate });
                busTr.Cells.Add(new TableCell() { Text = $@"{busDate.Year}-{busDate.Month}" });
                t.Rows.Add(busTr);
            }
            if (truck != null)
            {
                DateTime truckDate = new DateTime(DateTime.Now.Ticks - new DateTime(truck.YearMade, truck.MonthMade, 1).Ticks);

                TableRow truckTr = new TableRow();
                truckTr.Cells.Add(new TableCell() { Text = "Truck" });
                truckTr.Cells.Add(new TableCell() { Text = truck.Manufacturer });
                truckTr.Cells.Add(new TableCell() { Text = truck.Model });
                truckTr.Cells.Add(new TableCell() { Text = truck.CarPlate });
                truckTr.Cells.Add(new TableCell() { Text = $@"{truckDate.Year}-{truckDate.Month}" });
                t.Rows.Add(truckTr);
            }
            if (t.Rows.Count < 2) throw new Exception();
            resultPanel.Controls.Add(tableLabel);
            resultPanel.Controls.Add(t);
            resultPanel.Controls.Add(new LiteralControl("<br/>"));
        }
        public static void PrintGasolineCars(Panel resultPanel, List<Car> list)
        {
            Table t = new Table();
            Label tableLabel = new Label() { Text = "Gasoline cars" };

            TableHeaderRow thr = new TableHeaderRow();
            thr.Controls.Add(new TableHeaderCell() { Text = "Car Plate" });
            thr.Controls.Add(new TableHeaderCell() { Text = "Manufacturer" });
            thr.Controls.Add(new TableHeaderCell() { Text = "Model" });
            thr.Controls.Add(new TableHeaderCell() { Text = "Year Made" });
            t.Rows.Add(thr);

            foreach (Car car in list)
            {
                TableRow tr = new TableRow();
                tr.Cells.Add(new TableCell() { Text = car.CarPlate });
                tr.Cells.Add(new TableCell() { Text = car.Manufacturer });
                tr.Cells.Add(new TableCell() { Text = car.Model });
                tr.Cells.Add(new TableCell() { Text = car.YearMade.ToString() });

                t.Rows.Add(tr);
            }
            if (t.Rows.Count < 2) throw new Exception();

            resultPanel.Controls.Add(tableLabel);
            resultPanel.Controls.Add(t);
            resultPanel.Controls.Add(new LiteralControl("<br/>"));
        }
        public static void PrintBusesByBranches(Panel resultPanel, List<Branch> branches)
        {

            foreach (Branch branch in branches)
            {
                Label label = new Label() { Text = $@"{branch.City}, {branch.Address} buses:" };
                Table t = new Table();
                TableHeaderRow thr = new TableHeaderRow();
                thr.Cells.Add(new TableHeaderCell() { Text = "Car plate" });
                thr.Cells.Add(new TableHeaderCell() { Text = "Manufacturer" });
                thr.Cells.Add(new TableHeaderCell() { Text = "Model" });
                thr.Cells.Add(new TableHeaderCell() { Text = "Year made" });
                thr.Cells.Add(new TableHeaderCell() { Text = "Month made" });
                thr.Cells.Add(new TableHeaderCell() { Text = "Last Tech Date" });
                thr.Cells.Add(new TableHeaderCell() { Text = "Fuel type" });
                thr.Cells.Add(new TableHeaderCell() { Text = "Fuel/100km" });
                thr.Cells.Add(new TableHeaderCell() { Text = "Seat Count" });
                t.Rows.Add(thr);

                List<Bus> buses = CompanyProgram.FilterBuses(branch.Vehicles);
                if (buses.Count == 0) continue;
                buses.Sort();
                foreach (Bus bus in buses)
                {
                    t.Rows.Add(bus.ConvertToRow());
                }
                resultPanel.Controls.Add(label);
                resultPanel.Controls.Add(t);
                resultPanel.Controls.Add(new LiteralControl("<br/>"));
            }
        }
    }
}
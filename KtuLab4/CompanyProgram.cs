using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KtuLab4
{
    public static class CompanyProgram
    {
        public static void Run(Panel dataPanel, Panel resultPanel, string selectedBranchName)
        {
            List<Branch> branches = IO.GetBranchesFromFolder(selectedBranchName);
            List<Transport> allVehicles = GetAllVehicles(branches);
            try
            {
                IO.PrintBestVehicles(resultPanel, allVehicles);
            }
            catch(Exception ex)
            {
                resultPanel.Controls.Add(new Label() { Text = "There were no best vehicles." });
                resultPanel.Controls.Add(new LiteralControl("<br/>"));
            }

            try
            {
                List<Car> gasolineCars = FilterGasolineCars(allVehicles);
                IO.PrintGasolineCars(resultPanel, gasolineCars);
            }
            catch(Exception ex)
            {
                resultPanel.Controls.Add(new Label() { Text = "There were no gasoline cars." });
                resultPanel.Controls.Add(new LiteralControl("<br/>"));
            }

            IO.PrintBusesByBranches(resultPanel, branches);


        }
        
        public static T GetBest<T>(List<Transport> list) where T : Transport
        {
            Transport best = null;
            foreach (Transport one in list)
            {
                if (one is T && best == null) best = one;
                if (one.BetterThan(best)) best = one;
            }
            if (best is T) return best as T;
            return null;
        }
        public static List<Bus> FilterBuses(List<Transport> list)
        {
            List<Bus> buses = new List<Bus>();
            foreach (Transport one in list)
            {
                if (one is Bus) buses.Add(one as Bus);
            }
            return buses;
        }
        public static List<Car> FilterGasolineCars(List<Transport> vehicles)
        {
            List<Car> list = new List<Car>();

            foreach (Transport one in vehicles)
            {
                if (one is Car && one.Fuel.ToLower() == "Gasoline".ToLower()) list.Add(one as Car);
            }

            return list;
        }
        public static List<Transport> GetAllVehicles(List<Branch> branches)
        {
            List<Transport> list = new List<Transport>();
            foreach (Branch branch in branches)
            {
                foreach (Transport vehicle in branch.Vehicles)
                {
                    list.Add(vehicle);
                }
            }
            return list;
        }
    }
}
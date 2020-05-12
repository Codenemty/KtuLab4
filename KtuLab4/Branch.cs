using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KtuLab4
{
    public class Branch
    {
        public List<Transport> Vehicles { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public Branch(List<Transport> vehicles, string city, string address, string phone)
        {
            Vehicles = vehicles;
            City = city;
            Address = address;
            Phone = phone;
        }
       
    }
}
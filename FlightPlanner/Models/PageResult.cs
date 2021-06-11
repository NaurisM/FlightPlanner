using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightPlanner.Models
{
    public class PageResult
    {
        public int Number { get; set; }
        public int TotalItems { get; set; }
        public Array Items { get; set; }
    }
}
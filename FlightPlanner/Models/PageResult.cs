using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightPlanner.Models
{
    public class PageResult<T>
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public T[] Items { get; set; }
    }
}
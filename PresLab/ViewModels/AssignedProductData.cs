using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresLab.ViewModels
{
    public class AssignedProductData
    {
        public int ProductID { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public bool Assigned { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PresLab.Models;

namespace PresLab.ViewModels
{
    public class BudgetData
    {
        public Order Order { get; set; }

        public Client Client { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
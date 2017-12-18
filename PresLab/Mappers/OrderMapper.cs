using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PresLab.DAL;
using PresLab.Models;
using PresLab.ViewModels;


namespace PresLab.Mappers
{
    public class OrderMapper
    {
        public void ConvertOrderModelToViewModel (Order order)
        {
            products = PresLabContext.Products.Include(x => x.Tests)
                foreach (Product p in products)
                {
                    foreach (Test t in p.Tests)
                    {
                    testsToBudgetList.Add(p.Type + p.Brand + t.Name + t.Price);
                    }
                }
        }

    }
}
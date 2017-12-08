using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PresLab.Models;

namespace PresLab.DAL
{
    public class PresLabInitializer : System.Data.Entity.DropCreateDatabaseAlways<PresLabContext> // DropCreateDatabaseIfModelChanges
    {
        protected override void Seed(PresLabContext context)
        {
            var products = new List<Product>
            {
            new Product{Type="Leche Descremada",Brand="Vaquita",Description="Larga vida, Tetrapack x1L" },
            new Product{Type="Papas Fritas",Brand="Lays",Description="Sabor original, bolsa x200gr"},
            new Product{Type="Agua Mineral",Brand="Fresh Water",Description="Bidón x5L"},
            new Product{Type="Hamburguesas",Brand="Super Mercado",Description="Caja x12u."},
            new Product{Type="Mermelada Light",Brand="BC",Description="Sabor Naranja, frasco x300gr."},
            new Product{Type="Salchichas de viena",Brand="EmbutidosA",Description="Blister x6u."},
            new Product{Type="Harina de Trigo",Brand="Favorita",Description="000, Paquete x1Kg"},
            new Product{Type="Leche Entera",Brand="Lactosa",Description="Larga vida, Tetrapack x1L"},

            };

            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();

            var tests = new List<Test>
            {
            new Test{Name="Relación Humedad-Proteínas",Description="lshkhsas sakbsdakjsa shbskdsa",Price=3000,},
            new Test{Name="Microbiológico",Description="Detección de microorganismos lksds sdjbs y sksa",Price=2580,},
            new Test{Name="Cenizas",Description="Detección de humedad",Price=1200,},
            new Test{Name="Fisico-químico",Description="Presencia de minerales",Price=5000,},
            new Test{Name="Bacteriológico",Description="Detección de bacterias a b y c",Price=3560,},
            new Test{Name="Zaraza",Description="sdbadkjsabdjsa shsbd",Price=3240,},
            new Test{Name="Simanzana",Description="sdbdsa sjbas shbsda",Price=1580,},

            };

            tests.ForEach(s => context.Tests.Add(s));

            // var product1 = products.FirstOrDefault();

            //tests.ForEach(test => product1.Tests.Add(test));

            context.SaveChanges();





        }
    }
}


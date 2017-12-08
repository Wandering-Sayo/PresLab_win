namespace PresLab.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using PresLab.Models;
    using PresLab.ViewModels;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<PresLab.DAL.PresLabContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PresLab.DAL.PresLabContext context)
        {
            var product = new List<Product>
            {
                new Product {Type="Leche Descremada",Brand="Vaquita",Description="Larga vida, Tetrapack x1L",},
                new Product{Type="Papas Fritas",Brand="Lays",Description="Sabor original, bolsa x200gr"},
                new Product{Type="Agua Mineral",Brand="Fresh Water",Description="Bidón x5L"},
                new Product{Type="Hamburguesas",Brand="Super Mercado",Description="Caja x12u."},
                new Product{Type="Mermelada Light",Brand="BC",Description="Sabor Naranja, frasco x300gr."},
                new Product{Type="Salchichas de viena",Brand="EmbutidosA",Description="Blister x6u."},
                new Product{Type="Harina de Trigo",Brand="Favorita",Description="000, Paquete x1Kg"},
                new Product{Type="Leche Entera",Brand="Lactosa",Description="Larga vida, Tetrapack x1L"},
                
            };
            product.ForEach(s => context.Products.AddOrUpdate(p => p.Type, s));
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
            tests.ForEach(s => context.Tests.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

        }
    }
}

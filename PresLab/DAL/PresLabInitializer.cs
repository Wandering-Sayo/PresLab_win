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
            new Product{Type="Leche Descremada",Brand="Vaquita",Description="Larga vida, Tetrapack x1L"},
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
            new Test{Name="Proteínas",Description="Recuento de Proteínas",Price=3000,},
            new Test{Name="Grasas",Description="Presencia de Grasas trans, saturadas e insaturadas",Price=2580,},
            new Test{Name="Hidratos de Carbono",Description="Recuento de azúcares",Price=1200,},
            new Test{Name="Cloruros",Description="Concentración de Cloruros",Price=5000,},
            new Test{Name="Cenizas",Description="Determinación de humedad",Price=3560,},
            new Test{Name="Nitratos",Description="Concentración de Nitratos",Price=3240,},
            new Test{Name="Nitritos",Description="Concentración de Nitritos",Price=1580,},
            new Test{Name="Gliadinas",Description="Determinación de Gluten",Price=2340,},
            new Test{Name="Dioxido de Azufre",Description="Presencia de S02",Price=1600,},
            new Test{Name="Índice de Peróxidos",Description="Determinación de oxidación",Price=1450,},
            new Test{Name="Acidez Acuosa",Description="Descripción",Price=3000,},
            new Test{Name="pH",Description="Determinación de basicidad o acidez",Price=1300,},
            new Test{Name="Índice de Refracción",Description="Descripción",Price=1340,},
            new Test{Name="Sorbato de Potasio",Description="Presencia de conservantes",Price=4060,},
            new Test{Name="Índice de Iodo",Description="Descripción",Price=1270,},
            new Test{Name="Salmonella",Description="Presencia de Salmonella",Price=3600,},
            new Test{Name="Listeria",Description="Presencia de Salmonella",Price=3670,},
            new Test{Name="O157",Description="Microbiológico",Price=5480,},
            new Test{Name="Coliformes Totales",Description="Presencia de Bacterias",Price=3500,},
            new Test{Name="E.Coli",Description="Presencia de Esteriqia Coli",Price=3450,},
            new Test{Name="Hongos y Levaduras",Description="Determinación de nivel de Contaminación",Price=3670,},
            new Test{Name="Recuento total de Aerobios Mesófilos",Description="Presencia de Microorganismos",Price=3670,},

            };

            tests.ForEach(s => context.Tests.Add(s));
            context.SaveChanges();

            var clients = new List<Client>
            {
            new Client{Name="Frigorífico El Viejo",Email="administracion@elviejo.com.ar", Description="Elaboración de fiambres y chacinados",},
            new Client{Name="Cliente de Prueba",Email="clienteprueba@hotmail.com", Description="Ejemplo muestreo",},
            new Client{Name="Supermercado Marko",Email="calidad@marko.com.ar", Description="Supermercado Mayorista",},
            new Client{Name="Castillo",Email="maria.lopez@castillo.com", Description="Elaboración aceitunas y derivados de oliva",},
            new Client{Name="Montefiore S.R.L.",Email="jose.calidad2@montefioresrl.com.ar", Description="Elaboración de jugos",},
            new Client{Name="NewCringe",Email="contacto@newcringe.com.ar", Description="Elaboración de helados indutriales",},


            };

            clients.ForEach(s => context.Clients.Add(s));
            context.SaveChanges();





        }
    }
}


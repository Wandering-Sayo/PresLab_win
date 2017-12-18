using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PresLab.Models
{

    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Denominación")]
        public string Type { get; set; }

        [Display(Name = "Marca")]
        public string Brand { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "Proveedor")]
        public string Supplier { get; set; }

        public int CategoryID { get; set; }
        [Display(Name = "Categoría")]
        public Category Category { get; set; }

        [Display(Name = "Análisis")]
        public virtual ICollection<Test> Tests { get; set; }

        public virtual ICollection<Order> Samplings { get; set; }
    }
}
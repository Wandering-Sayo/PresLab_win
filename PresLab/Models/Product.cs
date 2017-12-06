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
        public long ID { get; set; }

        public string Type { get; set; }

        public string Brand { get; set; }

        public string Description { get; set; }

        public string Supplier { get; set; }

        public virtual ICollection<Test> Tests { get; set; }
    }
}
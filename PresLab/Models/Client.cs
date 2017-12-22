using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PresLab.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientID { get; set; }

        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public string Email { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set;  }


    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PresLab.Models
{
    public class LabPresContext: DbContext
    {
        public LabPresContext():
            base("LabPresContext")
            {

            }

        public DbSet<Laboratory> Laboratories { get; set; }
    }
}
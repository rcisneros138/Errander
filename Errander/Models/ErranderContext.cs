using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Errander.Models
{
    public class ErranderContext:DbContext
    {
        public ErranderContext() : base("DefaultConnection") { }
        
        public DbSet<Errand> errands { get; set; }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Errander.Models
{
    public class TableViewModel
    {
        public IEnumerable<Errand> completedErrands { get; set; }
        public List<Errand> UnclaimedErrandsInUserCity { get; set; }
        public List<Errand> ErrandsUserHasClaimed { get; set; }


    }
}
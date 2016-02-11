using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Errander.Models
{
    public class Errand
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string CurrentLocation { get; set; }
        public string ErrandDestination { get; set; }
        public string City { get; set; }
        public bool IsCompleted { get; set; }
        public bool InProgress { get; set; }
        public double AmountOffered { get; set; }
        public DateTime NeededBy { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string AcceptingUserID { get; set; }
        
        public double Rating { get; set; }
        public DateTime? CompletionTime { get; set; }
    }
}
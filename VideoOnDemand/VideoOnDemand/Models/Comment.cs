using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoOnDemand.Models
{
    [Table("Commentaire")]
    public class Comment
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public User Author { get; set; }
        public Film Film { get; set; }
        public string Message { get; set; }
        public int Marks { get; set; }
        public DateTime Date { get; set; }
        public bool Validated {get; set;}
    }
}
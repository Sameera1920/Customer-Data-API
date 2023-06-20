using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp1.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int index { get; set; }
        public int age { get; set; }
        public string? eyeColor { get; set; }
        public string? name { get; set; }
        public string? gender { get; set; }
        public string? company { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
    }
}

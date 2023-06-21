﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp1.Models
{

    public class Address
    {
        [Key]
        public int Id { get; set; }
        public int? Number { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? Zipcode { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
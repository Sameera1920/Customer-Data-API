﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CustomerDataAPI.Models;

namespace CustomerDataAPI.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Index { get; set; }
        public int Age { get; set; }
        public string? EyeColor { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Company { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Address? Address { get; set; }
        public string? About { get; set; }
        public string? Registered { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public ICollection<string>? Tags{ get; set; }
    }
}
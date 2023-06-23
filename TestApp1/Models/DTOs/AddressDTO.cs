using System;
using System.ComponentModel.DataAnnotations;

namespace TestApp1.Models.DTOs
{
    public class AddressDTO
    {
        public int? Number { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? Zipcode { get; set; }
    }
}


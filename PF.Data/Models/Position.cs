using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace PF.Data.Models
{
    public class Position
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool? YearsOfService { get; set; }
    }
}

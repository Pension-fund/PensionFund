using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PF.Data.Models
{
    public class PensionModifier
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string ModifierName { get; set; }

        public bool Standalone { get; set; }

        public double? Coefficient { get; set; }

        public double? FixedPayment { get; set; }

        [JsonIgnore]
        public virtual ICollection<Person> People { get; set; }

    }
}

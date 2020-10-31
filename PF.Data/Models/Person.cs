using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PF.Data.Models
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Sex { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public Guid? ModifierId { get; set; }

        public virtual PensionModifier Modifier { get; set; }

        [JsonIgnore]
        public virtual ICollection<Experience> Experiences { get; set; }
    }
}

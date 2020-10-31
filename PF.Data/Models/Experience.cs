using System;
using System.ComponentModel.DataAnnotations;

namespace PF.Data.Models
{
    public class Experience
    {
        [Required]
        public Guid PositionId { get; set; }

        public virtual Position Position { get; set; }
        [Required]
        public Guid PersonId { get; set; }

        public virtual Person Person { get; set; }


        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public double Salary { get; set; }
    }
}

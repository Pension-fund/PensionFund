using System.ComponentModel.DataAnnotations;

namespace PF.Data.Models
{
    public class Settings
    {
        [Key]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}

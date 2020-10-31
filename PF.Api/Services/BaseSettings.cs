using PF.Data;
using System.Linq;

namespace PF.Api.Services
{
    public class BaseSettings : IBaseSettings
    {
        private readonly ApplicationDbContext _context;

        public BaseSettings(ApplicationDbContext context)
        {
            _context = context;
        }

        public double BaseSalary
        {
            get
            {
                var setting = _context.Settings.First(s => s.Key == nameof(BaseSalary));
                return double.Parse(setting.Value);
            }
        }

        public int MinAgeMale
        {
            get
            {
                var setting = _context.Settings.First(s => s.Key == nameof(MinAgeMale));
                return int.Parse(setting.Value);
            }
        }

        public int MinAgeFemale
        {
            get
            {
                var setting = _context.Settings.First(s => s.Key == nameof(MinAgeFemale));
                return int.Parse(setting.Value);
            }
        }

        public int MinExpMale
        {
            get
            {
                var setting = _context.Settings.First(s => s.Key == nameof(MinExpMale));
                return int.Parse(setting.Value);
            }
        }

        public int MinExpFemale
        {
            get
            {
                var setting = _context.Settings.First(s => s.Key == nameof(MinExpFemale));
                return int.Parse(setting.Value);
            }
        }

        public double AvgSalary
        {
            get
            {
                var setting = _context.Settings.First(s => s.Key == nameof(AvgSalary));
                return int.Parse(setting.Value);
            }
        }

    }
}

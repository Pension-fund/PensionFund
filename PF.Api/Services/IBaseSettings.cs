using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PF.Api.Services
{
    public interface IBaseSettings
    {
        double BaseSalary { get; }

        int MinAgeMale { get; }

        int MinAgeFemale { get; }

        int MinExpMale { get; }

        int MinExpFemale { get; }

        double AvgSalary { get; }
    }
}

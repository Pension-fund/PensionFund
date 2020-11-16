using PF.Data.Models;

namespace PF.Api.Services
{
    public interface IPensionCalculator
    {
        double Calculate(Person person);
    }
}

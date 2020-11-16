using System;
using System.Linq;
using PF.Data.Models;

namespace PF.Api.Services
{
    public class PensionCalculator : IPensionCalculator
    {
        private readonly IBaseSettings _settings;

        private readonly double _baseSalaryX10;

        private const string MALE = "M";

        private const string FEMALE = "F";

        public PensionCalculator(IBaseSettings settings)
        {
            _settings = settings;
            _baseSalaryX10 = _settings.BaseSalary * 10;
        }

        public double Calculate(Person person)
        {
            var pension = 0.0;
            if (CanUseStandardPension(person))
            {
                pension = CalculateBasePension(person);
            }

            pension = ApplyModifiers(person, pension);
            return pension;
        }

        //ЧИ ЛЮДИНА ОТРИМАЄ ЗВИЧАЙНУ ПЕНСІЮ
        public bool CanUseStandardPension(Person person)
        {
            var age = GetDifferenceInMonths(DateTime.Today, person.DateOfBirth) / 12;

            if (MALE.Equals(person.Sex, StringComparison.OrdinalIgnoreCase) && age < _settings.MinAgeMale || age < _settings.MinAgeFemale)
                    return false;

            var exp = person.Experiences?.Select(e => GetDifferenceInMonths(e.EndDate, e.StartDate)).Sum() / 12;

            if (exp == null)
                return false;

            if (MALE.Equals(person.Sex, StringComparison.OrdinalIgnoreCase) && exp < _settings.MinExpMale || exp < _settings.MinExpFemale)
                return false;

            return true;
        }

        //ОБРАХОВУЄ НАРАХУВАННЯ ПЕНСІЇ В ЗАЛЕЖНОСТІ ВІД КОЕФІЦІЄНТІВ
        public double CalculateBasePension(Person person)
        {
            var expInMonths = person.Experiences.Select(e => GetDifferenceInMonths(e.EndDate, e.StartDate)).Sum();

            var koef = expInMonths * 0.01 / 12;
            if (koef > 0.75)
            {
                koef = 0.75;
            }

            // see https://www.pfu.gov.ua/33659-zarobitna-plata-dlya-obchyslennya-pensiyi-za-vikom/ for formula
            var sumOfMonthKoefs = person.Experiences.Select(e => {
                var months = GetDifferenceInMonths(e.EndDate, e.StartDate);
                var k = e.Salary / _settings.AvgSalary;
                return k * months;
            }).Sum();
            var exp = expInMonths * 30;

            var zp = _settings.AvgSalary * sumOfMonthKoefs / exp;

            // see https://www.pfu.gov.ua/33650-obchyslennya-rozmiru-pensiyi-za-vikom/ for formula
            var pension = zp * koef;

            if (pension < _settings.BaseSalary)
            {
                return _settings.BaseSalary;
            }
            else if (pension > _baseSalaryX10)
            {
                return _baseSalaryX10;
            }
            else
            {
                return pension;
            }
        }

        // ЯКУ САМЕ ПЕНСІЮ ЛЮДИНА БУДЕ ОТРИМУВАТИ, ЯКЩО ВОНА МАЄ ДОДАТКОВІ ПРИВІЛЕГІЇ (ІНВАЛІДНІСТЬ, УЧАСНИК ВІЙНИ І ТД)
        public double ApplyModifiers(Person person, double pension)
        {
            if (person.Modifier == null)
            {
                return pension;
            }

            var modifier = person.Modifier;

            var pensionApplied = pension > 0.0001;

            if (!pensionApplied)
            {
                if (!modifier.Standalone)
                {
                    return pension;
                }

                if (modifier.Coefficient.HasValue)
                {
                    pension += _settings.BaseSalary * modifier.Coefficient.Value;
                }
            }
            else
            {
                if (modifier.Coefficient.HasValue)
                {
                    pension *= modifier.Coefficient.Value;
                }
            }

            if (modifier.FixedPayment.HasValue)
            {
                pension += modifier.FixedPayment.Value;
            }

            return pension;
        }

        // ОБРАХОВУЄ РІЗНИЦЮ МІЖ ДАТАМИ В МІСЯЦЯХ
        private int GetDifferenceInMonths(DateTime date1, DateTime date2)
        {
            if (date2 > date1)
            {
                var tmp = date1;
                date1 = date2;
                date2 = tmp;
            }

            var diff = (date1 - date2).Days / 30;

            return diff;
        }
    }
}

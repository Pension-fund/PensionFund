using Microsoft.Extensions.DependencyInjection;
using PF.Api.Services;
using PF.Data;
using PF.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PF.Api
{
    public class Setter
    {
        public static async Task EnsureDataSeeded(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var seeder = new Setter(context);
                await seeder.Seed();
            }
        }

        public Setter(ApplicationDbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected ApplicationDbContext Context { get; set; }

        public async Task Seed()
        {
            await AddDefaultModifiers();
            await AddDefaultVariables();
            await AddSamplePositions();
            await AddSamplePeople();
        }

        //TODO: add sample job and experiences

        public async Task AddDefaultModifiers()
        {
            Context.DisabilityGroups.Add(new Data.Models.PensionModifier
            {
                FixedPayment = 2702.70,
                Standalone = true,
                ModifierName = "A participant in hostilities",
                Type = "War"
            });

            Context.DisabilityGroups.Add(new Data.Models.PensionModifier
            {
                FixedPayment = 10646.00,
                Standalone = true,
                ModifierName = "1 disability group",
                Type = "War"
            });

            Context.DisabilityGroups.Add(new Data.Models.PensionModifier
            {
                FixedPayment = 4668.40,
                Standalone = true,
                ModifierName = "1 disability group",
                Type = "ChAS"
            });

            Context.DisabilityGroups.Add(new Data.Models.PensionModifier
            {
                FixedPayment = 5896.20,
                Standalone = true,
                ModifierName = "3 disability group",
                Type = "War"
            });

            await Context.SaveChangesAsync();
        }

        public async Task AddDefaultVariables()
        {
            Context.Settings.Add(new Data.Models.Settings
            {
                Key = nameof(BaseSettings.BaseSalary),
                Value = 5000.ToString()
            });

            Context.Settings.Add(new Data.Models.Settings
            {
                Key = nameof(BaseSettings.MinAgeFemale),
                Value = 60.ToString()
            });

            Context.Settings.Add(new Data.Models.Settings
            {
                Key = nameof(BaseSettings.MinAgeMale),
                Value = 60.ToString()
            });

            Context.Settings.Add(new Data.Models.Settings
            {
                Key = nameof(BaseSettings.MinExpFemale),
                Value = 27.ToString()
            });

            Context.Settings.Add(new Data.Models.Settings
            {
                Key = nameof(BaseSettings.MinExpMale),
                Value = 27.ToString()
            });

            Context.Settings.Add(new Data.Models.Settings
            {
                Key = nameof(BaseSettings.AvgSalary),
                Value = 7000.ToString()
            });

            await Context.SaveChangesAsync();
        }

        public async Task AddSamplePositions()
        {
            Context.Positions.Add(new Position
            {
                Name = "Teacher",
                YearsOfService = false
            });
            Context.Positions.Add(new Data.Models.Position
            {
                Name = "Doctor",
                YearsOfService = false
            });
            Context.Positions.Add(new Data.Models.Position
            {
                Name = "Policeman",
                YearsOfService = true
            });

            await Context.SaveChangesAsync();
        }

        public async Task AddSamplePeople()
        {
            var modifier = Context.DisabilityGroups.FirstOrDefault(g => g.ModifierName == "1 disability group" && g.Type == "War");
            var teacher = Context.Positions.FirstOrDefault(p => p.Name == "Teacher");
            var doctor = Context.Positions.FirstOrDefault(p => p.Name == "Doctor");
            var policeman = Context.Positions.FirstOrDefault(p => p.Name == "Policeman");

            var person1 = new Person
            {
                Name = "Ivan Ivanov",
                DateOfBirth = new DateTime(1950, 2, 12),
                Sex = "Male"
            };
            person1.Experiences = new List<Experience>
            {
                new Experience
                {
                    StartDate = new DateTime(1970, 1, 1),
                    EndDate = new DateTime(1990, 1, 1),
                    Salary = 100,
                    Position = teacher,
                    Person = person1
                },
                new Experience
                {
                    StartDate = new DateTime(1990, 1, 2),
                    EndDate = new DateTime(2000, 1, 1),
                    Salary = 1000,
                    Position = doctor,
                    Person = person1
                },
                new Experience
                {
                    StartDate = new DateTime(2000, 1, 2),
                    EndDate = new DateTime(2015, 1, 1),
                    Salary = 9000,
                    Position = policeman,
                    Person = person1
                }
            };
            Context.People.Add(person1);

            var person2 = new Person
            {
                Name = "Petro Petrov",
                DateOfBirth = new DateTime(1955, 4, 11),
                Sex = "Male"
            };
            person2.Experiences = new List<Experience>
            {
                new Experience
                {
                    StartDate = new DateTime(1970, 1, 1),
                    EndDate = new DateTime(1980, 1, 1),
                    Salary = 100,
                    Position = doctor,
                    Person = person2
                },
                new Experience
                {
                    StartDate = new DateTime(1980, 1, 2),
                    EndDate = new DateTime(2000, 1, 1),
                    Salary = 150,
                    Position = teacher,
                    Person = person2
                },
                new Experience
                {
                    StartDate = new DateTime(2000, 1, 2),
                    EndDate = new DateTime(2015, 1, 1),
                    Salary = 9000,
                    Position = policeman,
                    Person = person2
                }
            };
            Context.People.Add(person2);

            var person3 = new Person
            {
                Name = "Vita Vitova",
                DateOfBirth = new DateTime(2000, 6, 6),
                Sex = "Female",
                Modifier = modifier
            };
            person3.Experiences = new List<Experience>
            {
                new Experience
                {
                    StartDate = new DateTime(1970, 1, 1),
                    EndDate = new DateTime(1980, 1, 1),
                    Salary = 1000,
                    Position = policeman,
                    Person = person3
                },
                new Experience
                {
                    StartDate = new DateTime(1980, 1, 2),
                    EndDate = new DateTime(2000, 1, 1),
                    Salary = 1500,
                    Position = doctor,
                    Person = person3
                },
                new Experience
                {
                    StartDate = new DateTime(2000, 1, 2),
                    EndDate = new DateTime(2015, 1, 1),
                    Salary = 9000,
                    Position = teacher,
                    Person = person3
                }
            };
            Context.People.Add(person3);

            var person4 = new Person
            {
                Name = "Nina Ninova",
                DateOfBirth = new DateTime(1996, 6, 6),
                Sex = "Female"
            };
            Context.People.Add(person4);

            await Context.SaveChangesAsync();
        }

    }
}

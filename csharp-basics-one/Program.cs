using System;
using System.Linq;
using System.Collections.Generic;

namespace csharp_basics_one
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Company { get; set; }
    }

    public class EmployeeHandler
    {
        public EmployeeHandler() { }

        public static Dictionary<string, int> AverageAgeForEachCompany(List<Employee> employees)
        {
            var AverageAgePerCompany = employees.GroupBy(x => x.Company)
                    .Select(g => new { Company = g.Key, Avg = g.Average(d => d.Age) })
                    .OrderBy(o => o.Company.ToString())
                    .ToDictionary(v => v.Company, v => (int)Math.Ceiling(v.Avg));

            return AverageAgePerCompany;
        }

        public static Dictionary<string, int> CountOfEmployeesForEachCompany(List<Employee> employees)
        {
            var countOfEmployeesPerCompany = employees.GroupBy(x => x.Company)
                    .Select(g => new { Company = g.Key, Count = g.Count() })
                    .OrderBy(o => o.Company.ToString())
                    .ToDictionary(v => v.Company, v => v.Count);

            return countOfEmployeesPerCompany;
        }

        public static Dictionary<string, Employee> OldestAgeForEachCompany(List<Employee> employees)
        {
            var oldestAgePerCompany = employees
                    .GroupBy(x => x.Company)
                    .OrderBy(x => x.Key)
                    .Select(g => new
                    {
                        Company = g.Key,
                        Emp = employees.Where(e => e.Company == g.Key && e.Age == (int)g.Max(a => a.Age)).FirstOrDefault()
                    })
                    .ToDictionary(item => item.Company.ToString(), item => item.Emp);
            return oldestAgePerCompany;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var employees = new List<Employee>();
            employees.Add(new Employee { FirstName = "Ainslee", LastName = "Ginsie", Company = "Galaxy", Age = 28 });
            employees.Add(new Employee { FirstName = "Libbey", LastName = "Apdell", Company = "Starbucks", Age = 44 });
            employees.Add(new Employee { FirstName = "Illa", LastName = "Stebbings", Company = "Berkshire", Age = 49 });
            employees.Add(new Employee { FirstName = "Laina", LastName = "Sycamore", Company = "Berkshire", Age = 20 });
            employees.Add(new Employee { FirstName = "Abbe", LastName = "Parnell", Company = "Amazon", Age = 20 });
            employees.Add(new Employee { FirstName = "Ludovika", LastName = "Reveley", Company = "Berkshire", Age = 30 });

            employees.Add(new Employee { FirstName = "Rene", LastName = "Antos", Company = "Galaxy", Age = 44 });
            employees.Add(new Employee { FirstName = "Vinson", LastName = "Beckenham", Company = "Berkshire", Age = 45 });
            employees.Add(new Employee { FirstName = "Reed", LastName = "Lynock", Company = "Amazon", Age = 41 });
            employees.Add(new Employee { FirstName = "Wyndham", LastName = "Bamfield", Company = "Berkshire", Age = 34 });
            employees.Add(new Employee { FirstName = "Loraine", LastName = "Sappson", Company = "Amazon", Age = 49 });
            employees.Add(new Employee { FirstName = "Abbe", LastName = "Antonutti", Company = "Starbucks", Age = 47 });

            EmployeeHandler employeeHander = new EmployeeHandler();
            foreach (var emp in EmployeeHandler.AverageAgeForEachCompany(employees))
            {
                Console.WriteLine($"The average age for company {emp.Key} is {emp.Value}");
            }

            foreach (var emp in EmployeeHandler.CountOfEmployeesForEachCompany(employees))
            {
                Console.WriteLine($"The count of employees for company {emp.Key} is {emp.Value}");
            }
                        
            foreach (var emp in EmployeeHandler.OldestAgeForEachCompany(employees))
            {
                 Console.WriteLine($"The oldest employee of company {emp.Key} is {emp.Value.FirstName} {emp.Value.LastName} having age {emp.Value.Age}");
            }
            
            Console.ReadLine();
        }
    }
}

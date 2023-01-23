using Microsoft.Extensions.Caching.Memory;
using LabcorpCodingTest.Domain;
using LabcorpCodingTest.Domain.Entities;
using LabcorpCodingTest.Domain.Enums;

namespace LabcorpCodingTest.Infrastructure;

public class EmployeeDataFactory
{
    private readonly IMemoryCache _memoryCache;
    
    public EmployeeDataFactory(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void CreateEmployees()
    {
        ICollection<Employee> employees = new List<Employee>();

        for (int newId = 1; newId < 31; newId++)
        {
            Employee employee = newId switch
            {
                < 11 => new Employee
                {
                    Id = newId,
                    EmployeeWageType = EmployeeWageType.Hourly,
                    VacationDaysAccumulatedPerYear = DomainConstants.HourlyEmployeeMaxVacationDaysYearly
                },
                < 21 => new Employee
                {
                    Id = newId,
                    EmployeeWageType = EmployeeWageType.Salary,
                    VacationDaysAccumulatedPerYear = DomainConstants.SalaryEmployeeMaxVacationDaysYearly
                },
                _ => new Employee
                {
                    Id = newId,
                    EmployeeWageType = EmployeeWageType.Salary,
                    VacationDaysAccumulatedPerYear = DomainConstants.ManagerMaxVacationDaysYearly,
                    IsManager = true
                }
            };
                        
            employees.Add(employee);
        }

        _memoryCache.Set(CacheKeys.AllEmployees, employees);
    }
}

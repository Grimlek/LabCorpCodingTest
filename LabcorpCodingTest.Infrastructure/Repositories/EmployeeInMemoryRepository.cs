using Microsoft.Extensions.Caching.Memory;

using LabcorpCodingTest.Domain.Entities;
using LabcorpCodingTest.Domain.Repositories;

namespace LabcorpCodingTest.Infrastructure.Repositories;

public class EmployeeInMemoryRepository : IEmployeeRepository
{
    private readonly IMemoryCache _memoryCache;
    
    public EmployeeInMemoryRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public IReadOnlyCollection<Employee> GetAll()
    {
        return _memoryCache.Get<IReadOnlyCollection<Employee>>(CacheKeys.AllEmployees) ?? Array.Empty<Employee>();
    }

    public Employee? Get(int id)
    {
        ICollection<Employee>? employees = _memoryCache.Get<ICollection<Employee>>(CacheKeys.AllEmployees);
        return employees?.SingleOrDefault(e => e.Id == id);
    }
    
    public void Update(Employee employee)
    {
        ICollection<Employee>? employees = _memoryCache.Get<ICollection<Employee>>(CacheKeys.AllEmployees);

        Employee? savedEmployee = employees?.SingleOrDefault(e => e.Id == employee.Id);

        if (savedEmployee == null)
        {
            return;
        }
        
        savedEmployee.DaysWorked = employee.DaysWorked;
        savedEmployee.VacationDays = employee.VacationDays;
        savedEmployee.VacationDaysUsed = employee.VacationDaysUsed;
        
        _memoryCache.Set(CacheKeys.AllEmployees, employees);
    }
}

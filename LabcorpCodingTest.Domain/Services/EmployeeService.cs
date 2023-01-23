using LabcorpCodingTest.Domain.Entities;
using LabcorpCodingTest.Domain.Repositories;

namespace LabcorpCodingTest.Domain.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    
    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IReadOnlyCollection<Employee> GetEmployees()
    {
        return _employeeRepository.GetAll();
    }

    public void UpdateDaysWorked(int employeeId, int daysWorked)
    {
        Employee? employee = _employeeRepository.Get(employeeId);

        if (employee == null)
        {
            return;
        }
        
        employee.Work(daysWorked);
        
        _employeeRepository.Update(employee);
    }

    public void TakeVacation(int employeeId, double vacationDaysToTake)
    {
        Employee? employee = _employeeRepository.Get(employeeId);

        if (employee == null)
        {
            return;
        }
        
        employee.TakeVacation(vacationDaysToTake);
        
        _employeeRepository.Update(employee);
    }
}

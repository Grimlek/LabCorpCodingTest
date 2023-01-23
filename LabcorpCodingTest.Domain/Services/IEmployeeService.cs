using LabcorpCodingTest.Domain.Entities;

namespace LabcorpCodingTest.Domain.Services;

public interface IEmployeeService
{
    IReadOnlyCollection<Employee> GetEmployees();
    void UpdateDaysWorked(int employeeId, int daysWorked);
    void TakeVacation(int employeeId, double vacationDaysToTake);
}

using LabcorpCodingTest.Domain.Entities;

namespace LabcorpCodingTest.Domain.Repositories;

public interface IEmployeeRepository
{
    IReadOnlyCollection<Employee> GetAll();
    Employee? Get(int id);
    void Update(Employee employee);
}

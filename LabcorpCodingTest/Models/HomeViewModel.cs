using LabcorpCodingTest.Domain.Entities;

namespace LabcorpCodingTest.Models;

public class HomeViewModel
{
    public IReadOnlyCollection<Employee> Employees { get; set; } = Array.Empty<Employee>();

    public VacationDaysViewModel VacationDaysViewModel { get; set; } = new ();

    public WorkDaysViewModel WorkDaysViewModel { get; set; } = new ();
}

using System.ComponentModel.DataAnnotations;
using LabcorpCodingTest.Domain;

namespace LabcorpCodingTest.Models;

public class VacationDaysViewModel
{
    [Required]
    [Range(1, int.MaxValue)]
    public int? EmployeeId { get; init; }
    
    [Required(ErrorMessage = "Vacation days is required.")]
    [Range(0, DomainConstants.WorkDaysInYear)]
    public double? VacationDaysToTake { get; init; }
}

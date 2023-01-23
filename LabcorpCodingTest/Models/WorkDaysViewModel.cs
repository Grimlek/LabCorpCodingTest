using System.ComponentModel.DataAnnotations;
using LabcorpCodingTest.Domain;

namespace LabcorpCodingTest.Models;

public class WorkDaysViewModel
{
    [Required]
    [Range(1, int.MaxValue)]
    public int? EmployeeId { get; init; }
    
    [Required(ErrorMessage = "Work days is required.")]
    [Range(0, DomainConstants.WorkDaysInYear)]
    public int? DaysWorkedThisYear { get; init; }
}

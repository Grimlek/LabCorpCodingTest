using LabcorpCodingTest.Domain.Enums;
using LabcorpCodingTest.Domain.Exceptions;

namespace LabcorpCodingTest.Domain.Entities;

public class Employee
{
    public int Id { get; init; }
    public double VacationDays { get; set; }
    public double VacationDaysUsed { get; set; }
    public int DaysWorked { get; set; }
    public double VacationDaysAccumulatedPerYear { get; set; }
    public EmployeeWageType EmployeeWageType { get; set; }
    public bool IsManager { get; set; }

    public void Work(int daysWorked)
    {
        if (daysWorked < 0 || daysWorked > DomainConstants.WorkDaysInYear)
        {
            throw new WorkDaysOutOfRangeException(
                $"The days worked for an employee must be between 0 and {DomainConstants.WorkDaysInYear}.");
        }
        
        DaysWorked = daysWorked;
        
        double vacationAccrualRate = DomainConstants.WorkDaysInYear / VacationDaysAccumulatedPerYear;
        double vacationDays = daysWorked / vacationAccrualRate;
        
        VacationDays = Math.Round(vacationDays, 1, MidpointRounding.AwayFromZero);
        
        if (VacationDaysUsed > VacationDays)
        {
            // don't allow employee to reuse vacation
            VacationDays = 0;
        }
    }
    
    public void TakeVacation(double vacationDaysToTake)
    {
        if (vacationDaysToTake < 0)
        {
            throw new VacationDaysOutOfRangeException("The vacation days to take must be greater than 0.");
        }
        
        if (vacationDaysToTake > VacationDays)
        {
            throw new VacationDaysOutOfRangeException(
                "The vacation days to take cannot exceed the amount of vacation days available.");
        }
        
        VacationDaysUsed += vacationDaysToTake;
        VacationDays -= vacationDaysToTake;
    }
}

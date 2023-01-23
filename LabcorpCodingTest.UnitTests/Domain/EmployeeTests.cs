using LabcorpCodingTest.Domain;
using LabcorpCodingTest.Domain.Entities;
using LabcorpCodingTest.Domain.Exceptions;

namespace LabcorpCodingTest.UnitTests.Domain;

public class EmployeeTests
{
    [Theory]
    [InlineData(5, 50, 1)]
    [InlineData(10, 100, 3.8)]
    [InlineData(20, 150, 11.5)]
    [InlineData(30, 150, 17.3)]
    [InlineData(10, 260, 10)]
    [InlineData(15, 260, 15)]
    [InlineData(30, 260, 30)]
    public void Work_WhenValidInput_ShouldCalculateCorrectVacationDays(
        int vacationDaysAccumulatedPerYear, int daysWorked, double expectedVacationDays)
    {
        Employee employee = new Employee
        {
            VacationDaysAccumulatedPerYear = vacationDaysAccumulatedPerYear
        };
        
        employee.Work(daysWorked);
        
        Assert.Equal(expectedVacationDays, employee.VacationDays);
    }
    
    [Fact]
    public void Work_WhenCalledMultipleTimes_ShouldNotAllowReuseOfVacationDays()
    {
        Employee employee = new Employee
        {
            VacationDaysAccumulatedPerYear = DomainConstants.HourlyEmployeeMaxVacationDaysYearly,
            VacationDays = 0,
            VacationDaysUsed = 10,
            DaysWorked = DomainConstants.WorkDaysInYear
        };
        
        employee.Work(50);
        
        Assert.Equal(0, employee.VacationDays);
    }

    [Theory]
    [InlineData(-100)]
    [InlineData(DomainConstants.WorkDaysInYear + 1)]
    [InlineData(int.MaxValue)]
    public void Work_WhenInvalidInput_ShouldThrowException(int daysWorked)
    {
        Employee employee = new Employee();
        
        Action workAction = () => employee.Work(daysWorked);
        
        Assert.Throws<WorkDaysOutOfRangeException>(workAction);
    }

    [Theory]
    [InlineData(-100)]
    [InlineData(100)]
    public void TakeVacation_WhenInvalidInput_ShouldThrowException(int vacationDaysToTake)
    {
        Employee employee = new Employee
        {
            VacationDays = 1
        };
        
        Action takeVacationAction = () => employee.TakeVacation(vacationDaysToTake);
        
        Assert.Throws<VacationDaysOutOfRangeException>(takeVacationAction);
    }

    [Theory]
    [InlineData(1, 10, 9)]
    [InlineData(2, 10, 8)]
    [InlineData(1.5, 10, 8.5)]
    [InlineData(2, 10, 8, 4)]
    public void TakeVacation_WhenValid_ShouldCalculateCorrectVacationDays(
        int vacationDaysToTake, int vacationDaysAvailable, int expectedVacationDays, int vacationDaysUsed = 0)
    {
        Employee employee = new Employee
        {
            VacationDays = vacationDaysAvailable,
            VacationDaysUsed = vacationDaysUsed
        };
        
        employee.TakeVacation(vacationDaysToTake);
        
        Assert.Equal(expectedVacationDays, employee.VacationDays);
        Assert.Equal(vacationDaysToTake + vacationDaysUsed, employee.VacationDaysUsed);
    }
}

using System.Diagnostics;
using LabCorpCodingTest;
using Microsoft.AspNetCore.Mvc;
using LabcorpCodingTest.Domain.Exceptions;
using LabcorpCodingTest.Domain.Services;
using LabcorpCodingTest.Models;

namespace LabcorpCodingTest.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeService _employeeService;
    
    public HomeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public IActionResult Index()
    {
        HomeViewModel viewModel = new HomeViewModel
        {
            Employees = _employeeService.GetEmployees()
        };
        
        return View(viewModel);
    }

    [HttpPost("work")]
    public IActionResult Work(WorkDaysViewModel workDaysViewModel)
    {
        if (!ModelState.IsValid)
        {
            return _CreateErrorActionResult(workDaysViewModel: workDaysViewModel);
        }
        
        try
        {
            _employeeService.UpdateDaysWorked(
                workDaysViewModel.EmployeeId!.Value, 
                workDaysViewModel.DaysWorkedThisYear!.Value);
        }
        catch (WorkDaysOutOfRangeException wex)
        {
            ModelState.AddModelError(
                $"{nameof(workDaysViewModel)}.{nameof(workDaysViewModel.DaysWorkedThisYear)}", wex.Message);
            return _CreateErrorActionResult(workDaysViewModel: workDaysViewModel);
        }
        
        return LocalRedirect("~/");
    }
    
    [HttpPost("vacation")]
    public IActionResult TakeVacation(VacationDaysViewModel vacationDaysViewModel)
    {
        if (!ModelState.IsValid)
        {
            return _CreateErrorActionResult(vacationDaysViewModel);
        }
        
        try
        {
            _employeeService.TakeVacation(
                vacationDaysViewModel.EmployeeId!.Value, 
                vacationDaysViewModel.VacationDaysToTake!.Value);
        }
        catch (VacationDaysOutOfRangeException vex)
        {
            ModelState.AddModelError(
                $"{nameof(vacationDaysViewModel)}.{nameof(vacationDaysViewModel.VacationDaysToTake)}", vex.Message);
            return _CreateErrorActionResult(vacationDaysViewModel);
        }
        
        return LocalRedirect("~/");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private IActionResult _CreateErrorActionResult(
        VacationDaysViewModel? vacationDaysViewModel = null,
        WorkDaysViewModel? workDaysViewModel = null)
    {
        HomeViewModel viewModel = new HomeViewModel
        {
            Employees = _employeeService.GetEmployees(),
            WorkDaysViewModel = workDaysViewModel ?? new WorkDaysViewModel(),
            VacationDaysViewModel = vacationDaysViewModel ?? new VacationDaysViewModel()
        };
        
        if (vacationDaysViewModel != null)
        {
            TempData[Constants.ViewData.ShowVacationModal] = true;
        }
        
        if (workDaysViewModel != null)
        {
            TempData[Constants.ViewData.ShowWorkModal] = true;
        }
        
        return View(nameof(Index), viewModel);
    }
}

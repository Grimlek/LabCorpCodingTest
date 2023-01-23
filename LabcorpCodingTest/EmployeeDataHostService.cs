using LabcorpCodingTest.Infrastructure;

namespace LabCorpCodingTest;

public class EmployeeDataHostService : IHostedService
{
    private readonly EmployeeDataFactory _employeeDataFactory;
    
    public EmployeeDataHostService(EmployeeDataFactory employeeDataFactory)
    {
        _employeeDataFactory = employeeDataFactory;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _employeeDataFactory.CreateEmployees();
        await Task.CompletedTask;
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

using LabCorpCodingTest;
using LabcorpCodingTest.Domain.Repositories;
using LabcorpCodingTest.Domain.Services;
using LabcorpCodingTest.Infrastructure;
using LabcorpCodingTest.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;

services.AddControllersWithViews();

services.AddMemoryCache();

services.AddTransient<IEmployeeService, EmployeeService>();
services.AddTransient<IEmployeeRepository, EmployeeInMemoryRepository>();

services.AddTransient<EmployeeDataFactory>();
services.AddHostedService<EmployeeDataHostService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
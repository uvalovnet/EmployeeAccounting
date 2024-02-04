using BLL.Interfaces;
using BLL.Services;
using DB.Interfaces;
using DB.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

string constr = builder.Configuration.GetConnectionString("DBConnection");
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(x => new EmployeeRepository(constr));
builder.Services.AddScoped<ITimesheetRepository, TimesheetRepository>(x => new TimesheetRepository(constr));
builder.Services.AddScoped<IReasonRepository, ReasonRepository>(x => new ReasonRepository(constr));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITimesheetService, TimesheetService>();
builder.Services.AddScoped<IReasonService, ReasonService>();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

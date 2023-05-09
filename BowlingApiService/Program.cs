using BowlingApiService.BusinessLogicLayer;
using BowlingData.DatabaseLayer;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ICustomerData, CustomerdataControl>();
builder.Services.AddSingleton<ICustomerAccess, CustomerDatabaseAccess>();
builder.Services.AddSingleton<ILaneData, LanedataControl>();
builder.Services.AddSingleton<ILaneAccess, LaneDatabaseAccess>();
builder.Services.AddSingleton<IPriceData, PricedataControl>();
builder.Services.AddSingleton<IPriceAccess, PriceDatabaseAccess>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

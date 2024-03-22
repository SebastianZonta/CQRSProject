using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using VerticalSliceArchitecture.Database;

var builder = WebApplication.CreateBuilder(args);
var programAssembly = typeof(Program).Assembly;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFastEndpoints();
builder.Services.AddDbContext<ApplicationDbContext>(context =>
{
    context.UseSqlServer(builder.Configuration.GetConnectionString("Database"), options =>
    {
        options.CommandTimeout(2);
    });
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis:6379";
    options.InstanceName = "SampleInstance";
});

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(programAssembly));

builder.Services.AddValidatorsFromAssembly(programAssembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    //using var scope = app.Services.CreateScope();
    //var services = scope.ServiceProvider;

    //var context = services.GetRequiredService<ApplicationDbContext>();
    //if (context.Database.GetPendingMigrations().Any())
    //    await context.Database.MigrateAsync();
}

app.UseFastEndpoints();
app.UseCors(MyAllowSpecificOrigins);
app.Run();

using MyNotification.Models;
using MyNotification.Services;
using MyNotification.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});
//Dependency Concepts:
builder.Services.AddScoped<IEmailNotificationService, EmailNotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/sendEmailNotification", async (EmailNotificationRequestDto emailNotification, IEmailNotificationService emailNotificationService) =>
{
    var response = await emailNotificationService.EmailNotificationAlertAsync(emailNotification);
    return Results.Created("Notification", response);
}).WithName("SendNotification")
.WithOpenApi();

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseStaticFiles();

app.Run();

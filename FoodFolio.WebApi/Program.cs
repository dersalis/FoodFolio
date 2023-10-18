using FoodFolio.WebApi.Configurations;
using FoodFolio.WebApi.Entities;
using FoodFolio.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddAuthenticationServices();

// Add services to the container.
builder.AddServices();

builder.AddMiddlewareServices();

builder.AddDbContextServices();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.AddAutoMapperServices();

builder.AddSwaggerService();

builder.AddCorsServices();


var app = builder.Build();

app.UseStaticFiles(); // Static files

app.Seed();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/V1/swagger.json", "Restauranteo");
    });
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication(); // Autentykacja

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.Run();

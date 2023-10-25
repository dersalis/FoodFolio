using FoodFolio.WebApi.Configurations;
using FoodFolio.WebApi.Entities;
using FoodFolio.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.AddAuthenticationServices();
builder.AddFluentValidatorService();
builder.AddServices();
builder.AddMiddlewareServices();
builder.AddDbContextServices();
builder.Services.AddHttpContextAccessor();
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

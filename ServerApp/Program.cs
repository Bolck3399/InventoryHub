var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddOpenApi();   // Disponible en .NET 8/9

// Registrar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", p =>
        p.WithOrigins("http://localhost:5166")
        //p.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod());
});

var app = builder.Build();

// Usar la política registrada
app.UseCors("AllowFrontend");

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();   // Genera el documento OpenAPI automáticamente
}

app.UseHttpsRedirection();

app.MapGet("api/productlist", () =>
{
    return new[]
    {
        new
        {
            Id = 1,
            Name = "Laptop",
            Price = 1200.50,
            Stock = 25,
            Category = new { Id = 101, Name = "Electronics" }
        },
        new
        {
            Id = 2,
            Name = "Headphones",
            Price = 50.00,
            Stock = 100,
            Category = new { Id = 102, Name = "Accessories" }
        }
    };
});

app.Run();
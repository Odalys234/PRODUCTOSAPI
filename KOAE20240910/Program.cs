var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Crear una lista para almacenar objetos de tipo Producto (productos)
var products = new List<Product>();
// Configurar una ruta GET para obtener todos los productos
app.MapGet("/products", () =>
{
    return products; // Devuelve la lista de productos
});
// Configurar una ruta GET para obtener un producto específico por su ID
app.MapGet("/products/{id}", (int id) =>
{
    // Busca un producto en la lista que tenga el ID especificado
    var product = products.FirstOrDefault(p => p.Id == id);
    return product; // Devuelve el producto encontrado(o null si no se encuentra)
});

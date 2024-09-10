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
// Configurar una ruta POST para agregar un nuevo producto a la lista 
app.MapPost("/products", (Product product) =>
{
    products.Add(product); // Agrega el nuevo producto a la lista 
    return Results.Ok(); // Devuelve una respuesta HTTP 200 OK
});

// Configurar una ruta PUT para actualizar un producto existente por ID
app.MapPut("/products/{id}", (int id, Product product) =>
{
    // Busca un producto en la lista que tenga el ID especificado 
    var existingProduct = products.FirstOrDefault(p => p.Id == id);
    if (existingProduct != null)
    {
        // Actualiza los datos del producto existente con los datos proporcionados
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        return Results.Ok(); // Devuelve una respuesta HTTP 200 OK
    }
    else
    {
        return Results.NotFound(); // Devuelve una respuesta HTTP 404 Not Found si el producto no existe 
    }
});
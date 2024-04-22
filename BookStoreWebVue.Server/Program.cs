using TestOperations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<BookDataAccess>(_ => new BookDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<AuthorDataAccess>(_ => new AuthorDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<LanguageDataAccess>(_ => new LanguageDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<GenreDataAccess>(_ => new GenreDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<AddressDataAccess>(_ => new AddressDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<BookAvailabilityDataAccess>(_ => new BookAvailabilityDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<CountryDataAccess>(_ => new CountryDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<CustomerAddressDataAccess>(_ => new CustomerAddressDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<CustomerDataAccess>(_ => new CustomerDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<CustomerOrderDataAccess>(_ => new CustomerOrderDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<OrderHistoryDataAccess>(_ => new OrderHistoryDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<OrderLineDataAccess>(_ => new OrderLineDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<OrderStatusDataAccess>(_ => new OrderStatusDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<ShippingMethodDataAccess>(_ => new ShippingMethodDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<SupplierDataAccess>(_ => new SupplierDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<SupplyDataAccess>(_ => new SupplyDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<SupplyLineDataAccess>(_ => new SupplyLineDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<WarehouseDataAccess>(_ => new WarehouseDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddScoped<PublisherDataAccess>(_ => new PublisherDataAccess("Server=localhost;Port=5432;Username = postgres; Password=12345678;Database=bookstore"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

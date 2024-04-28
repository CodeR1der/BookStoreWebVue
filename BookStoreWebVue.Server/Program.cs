using BookStoreWebVue.Server.DataAccess;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddScoped<BookDataAccess>(_ => new BookDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<AuthorDataAccess>(_ => new AuthorDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<LanguageDataAccess>(_ => new LanguageDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<GenreDataAccess>(_ => new GenreDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<AddressDataAccess>(_ => new AddressDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<BookAvailabilityDataAccess>(_ => new BookAvailabilityDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<CountryDataAccess>(_ => new CountryDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<CustomerOrderDataAccess>(_ => new CustomerOrderDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<OrderHistoryDataAccess>(_ => new OrderHistoryDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<OrderLineDataAccess>(_ => new OrderLineDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<OrderStatusDataAccess>(_ => new OrderStatusDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<ShippingMethodDataAccess>(_ => new ShippingMethodDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<SupplierDataAccess>(_ => new SupplierDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<SupplyDataAccess>(_ => new SupplyDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<SupplyLineDataAccess>(_ => new SupplyLineDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<WarehouseDataAccess>(_ => new WarehouseDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<PublisherDataAccess>(_ => new PublisherDataAccess(configuration.GetConnectionString("MyConnectionString")));
builder.Services.AddScoped<UserDataAccess>(_ => new UserDataAccess(configuration.GetConnectionString("MyConnectionString")));

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

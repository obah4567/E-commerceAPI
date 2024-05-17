using E_commerceAPI.src.Domain.GeneratorData;
using E_commerceAPI.src.Domain.Services;
using E_commerceAPI.src.Infrastructure.DbContexts;
using E_commerceAPI.src.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<IWishListRepository, WishListRepository>();

//builder.Services.AddLogging(builder => builder.AddConsole());

builder.Services.AddLogging();

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});




var app = builder.Build();

//Insertion des fausses données generer grâce à Bogus 
/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    GeneratorDataByBogus.Seed(services);
}
*/

// Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

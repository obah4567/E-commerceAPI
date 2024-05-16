using E_commerceAPI.DbContexts;
using E_commerceAPI.Repository;
using E_commerceAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
});


var app = builder.Build();

/*using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<Context>();
    
    if (!dbContext.Database.CanConnect())
    {
        //throw new NotImplementedException("Can't connect to DB");

        Console.WriteLine("La base de données n'est pas accessible. Tentative de création...");
        dbContext.Database.EnsureCreated();
        Console.WriteLine("Base de données créée.");
    }
    //dbContext.Database.Migrate(); // Appliquer les migrations
}*/

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

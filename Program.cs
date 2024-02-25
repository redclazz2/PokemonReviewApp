//Welcome to the .NET API Program.cs
//In this file you can add modules to the API with the builder.

using Microsoft.EntityFrameworkCore;
using PokemonReviewApp;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if(args.Length == 1 && args[0].ToLower() == "seeddata")
{
	SeedData(app);
}

void SeedData(IHost app)
{
	var scopedFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

	using(var scope = scopedFactory.CreateScope())
	{
		var service =  scope.ServiceProvider.GetService<Seed>();
		service.SeedDataContext();
	}
}

//This the pipeline a HTTP request goes through:
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//Other stuff in the project are:
//1.AppSettings.json - configurations for when the app is running
//2.Properties/launchSettings.json - configurations for when the app is building

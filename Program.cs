//Welcome to the .NET API Program.cs
//In this file you can add modules to the API with the builder.

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

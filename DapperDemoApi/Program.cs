using DapperDemoData.Data;
using DapperDemoData.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

/*
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});
*/



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
        .WithOrigins("https://localhost:7129") // Add your front-end URL
        .AllowAnyHeader()
        .AllowAnyMethod());
});




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddTransient<IDataAccess, DataAccess>();
builder.Services.AddTransient<IPersonRepository, PersonRepository>();

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

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

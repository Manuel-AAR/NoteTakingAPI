using backend.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new ErrorHandlingFilter());
});

var app = builder.Build();

app.UseCors(policy =>
{
    policy.AllowAnyHeader()
        .WithMethods("GET", "POST", "PATCH", "DELETE")
        .WithOrigins("localhost:80")
        .AllowCredentials();
});

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


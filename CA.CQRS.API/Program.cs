using CA.CQRS.APPLICATION.DependencyInjection;
using CA.CQRS.INFRASTRUCTURE.DependencyInjection;
using CA.CQRS.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ── Layer Registrations ──
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ── Global Exception Middleware ──
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
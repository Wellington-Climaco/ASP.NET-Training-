using TreinandoApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbContexto>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();


app.Run();

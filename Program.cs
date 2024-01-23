using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TreinandoApi.Data;
using TreinandoApi.Repository;
using TreinandoApi.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
CreateServices(builder);

builder.Services.AddControllers().AddJsonOptions(x => {
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
});

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

void CreateServices(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<DbContexto>(options => options.UseSqlServer(connectionString));
    builder.Services.AddTransient<ITarefaRepository, TarefaRepository>();
    builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
}

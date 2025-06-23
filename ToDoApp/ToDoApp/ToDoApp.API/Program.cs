using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ToDoApp.API.Mapping;
using ToDoApp.Core.Interfaces;
using ToDoApp.Core.Settings;
using ToDoApp.Core.UnitOfWorks;
using ToDoApp.Repository.Data;
using ToDoApp.Repository.Repositories;
using ToDoApp.Repository.UnitOfWorks;
using ToDoApp.Service.Services;



var builder = WebApplication.CreateBuilder(args);

// Autofac Container kullanmak için
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// PostgreSQL + EF Core baðlantýsý
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile));


// Controller desteði
builder.Services.AddControllers();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ToDoApp API",
        Version = "v1",
        Description = "Görev Takip Uygulamasý API dokümantasyonu"
    });
});
// Mail
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<EmailSender>();



// Autofac ile Repository ve Service kayýtlarý
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterGeneric(typeof(GenericRepository<>))
        .As(typeof(IGenericRepository<>))
        .InstancePerLifetimeScope();

    containerBuilder.RegisterType<TodoTaskService>()
        .As<ITodoTaskService>()
        .InstancePerLifetimeScope();
});

var app = builder.Build();

//  Swagger aktif
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoApp API v1");
        c.RoutePrefix = string.Empty;  // Swagger'ý anasayfaya yerleþtirir
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

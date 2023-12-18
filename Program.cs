using Agenda_Back.Data;
using Agenda_Back.Data;
using Agenda_Back.Models.Profiles;
using Agenda_Back.Data.Repository.Implementation;
using Agenda_Back.Data.Repository.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Serilog;
using Serilog.Events;
using Agenda_Back.Services.Implementations;
using Agenda_Back.Services.Interfaces;
using Agenda_Back.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

//Cors

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        });
});

// Add services to the container.
builder.Services.AddControllers();


//Configuracion del logger de errores

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        "logs/myapp-.txt",
        rollingInterval: RollingInterval.Day,
        fileSizeLimitBytes: 500000, // Tamaño límite del archivo
        retainedFileCountLimit: 2 // Número máximo de archivos a mantener antes de eliminar los más antiguos
    )
    .CreateLogger();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("AgendaApiBearerAuth", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Acá pegar el token generado al loguearse."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "AgendaApiBearerAuth"
                } //Tiene que coincidir con el id seteado arriba en la definición
                
            }, new List<string>()
        }
    });
});

//ADD Context *****************************

builder.Services.AddDbContext<AplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
});

builder.Services.AddAuthentication("Bearer") //"Bearer" es el tipo de auntenticación que tenemos que elegir después en PostMan para pasarle el token
    .AddJwtBearer(options => //Acá definimos la configuración de la autenticación. le decimos qué cosas queremos comprobar. La fecha de expiración se valida por defecto.
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    }
);


//AUTOMAPPER **********************

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new ContactBookProfile());
    cfg.AddProfile(new ContactProfile());
    cfg.AddProfile(new SharedContactBookProfile());
    cfg.AddProfile(new UserProfile());
});

//Add Services ******************

//automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
 
//repository
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IContactBookRepository, ContactBookRepository>();
//service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactBookService, ContactBookService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin"); //cors

app.UseAuthentication();//********************

app.UseAuthorization();

app.MapControllers();

Log.CloseAndFlush();

app.Run();
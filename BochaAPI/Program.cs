using BochaAPI.Data;
using BochaAPI.Mappings;
using BochaAPI.Repositorios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();//para las imagenes
builder.Services.AddHttpClient();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();//INYECCION DE SOURCES

//Para que funcione la validacion en sawagger y no hacerlo en POSTMAN
builder.Services.AddSwaggerGen(options => 
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Bocha Store API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type= ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                },
                Scheme = "Oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In= ParameterLocation.Header
            },
            new List<string> ()
        }
    });
});

builder.Services.AddDbContext<BochaDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BochaConnectionStrings")));

builder.Services.AddDbContext<BochaAuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BochaAuthConnectionStrings")));


//Esta linea inyecta la IRegionRepositorio con la implementacion de SQLRegionRepositorio
//Si mi controlador quier usar el repositorio ya no es necesario inyectar mas la base de datos


builder.Services.AddScoped<ICategoriaRepositorio,SQLCategoriaRepositorio>();
builder.Services.AddScoped<IProductoRepositorio,SQLProductoRepositorio>();
builder.Services.AddScoped<IMarcaRepositorio,SQLMarcaRepositorio>();
builder.Services.AddScoped<ITokenRepositorio, TokenRepositorio>();
builder.Services.AddScoped<IMagenRepositorio, LocalImagenRepositorio>();

//builder.Services.AddScoped<IRegionRepositorio,InMemoryRegionRepositorio>();

//INYECTAR AUTOMAPER CUANDO LA APLICACION COMINECE
builder.Services.AddAutoMapper(typeof(PerfilesAutoMappers));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Bocha")
    .AddEntityFrameworkStores<BochaAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

//builder de autentificacion
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Imagenes")),
    RequestPath = "/Imagenes"
    //Te da un URL que te conceta con la carpeta de imagenes dentro del proyecto
});

app.MapControllers();

app.Run();

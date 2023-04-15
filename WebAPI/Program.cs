#region using 
using System.Text;
using APIs.Validations;
using Application;
using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using Application.ViewModels;
using Applications.InterfaceRepositories;
using Applications.InterfaceServices;
using Applications.Services;
using Applications.ViewModels.UserViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructures;
using Infrastructures.Mappers;
using Infrastructures.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebAPI.Validations;
#endregion
// TODO: separate file services life time, jwt, ... stuff

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));
builder.Services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// swagger documentation
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Student API",
        Version = "v1",
        Description = "API for student services",
        Contact = new OpenApiContact
        {
            Url = new Uri("https://nhonvo.github.io/portfolio/")
        }
    });

    // Add JWT authentication support in Swagger
    var securityScheme = new OpenApiSecurityScheme

    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    options.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            securityScheme, new[] { "Bearer" }
        }
    };

    options.AddSecurityRequirement(securityRequirement);
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IShippingCompanyRepository, ShippingCompanyRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// cache
builder.Services.AddMemoryCache();

// validation and JWT
{
    // server side
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddFluentValidationClientsideAdapters();
    // client side
    builder.Services.AddScoped<IValidator<LoginRequestViewModel>, LoginValidation>();
    builder.Services.AddScoped<IValidator<RegisterRequestViewModel>, RegisterValidation>();
    builder.Services.AddScoped<IValidator<CustomerDTO>, CustomerDTOValidation>();
    builder.Services.AddScoped<IValidator<CustomerUpdateRequest>, CustomerUpdateRequestValidation>();
    builder.Services.AddScoped<IValidator<UpdateRequestViewModel>, UpdateUserValidation>();
    // JWT
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    });
    builder.Services.AddAuthorization();
}
var app = builder.Build();
// cache

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

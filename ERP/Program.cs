using ERP.Application.DTOs.Category;
using ERP.Application.DTOs.Product;
using ERP.Application.DTOs.User;
using ERP.Application.Events.Products;
using ERP.Application.Interfaces;
using ERP.Application.Mappers;
using ERP.Application.Services;
using ERP.Application.Validators.CategoryValidator;
using ERP.Application.Validators.ProductValidator;
using ERP.Application.Validators.UserValidator;
using ERP.Domain.Entities;
using ERP.Domain.Interfaces;
using ERP.Domain.ValueObjects;
using ERP.Middleware;
using ERP.Persistence;
using ERP.Persistence.Repository;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Configurações do MediatR
builder.Services.AddMediatR(typeof(ProductOutOfStockEvent).Assembly);

//Repositorios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//helpers
builder.Services.AddScoped<HateoasLinkService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//User Service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<IValidator<CreateUserDTO> , CreateUserDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateUserDTO>, UpdateUserDTOValidator>();

//Product Service
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ProductMapper>();
builder.Services.AddScoped<IValidator<CreateProductDTO>, CreateProductDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateProductDTO>, UpdateProductDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateProductInventoryDTO>, UpdateProductInventoryDTOValidator>();

//Category Service
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<CategoryMapper>();
builder.Services.AddScoped<IValidator<CreateCategoryDTO>, CreateCategoryDTOValidator>();
builder.Services.AddScoped<IValidator<UpdateCategoryDTO>, UpdateCategoryDTOValidator>();
    

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Utiliza o middleware custom para apanhar exceptions
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
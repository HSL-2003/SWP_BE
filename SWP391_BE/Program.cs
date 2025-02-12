using Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Repo;
using Service;
using SWP391_BE.Abstraction.JWT;
using SWP391_BE.Abstraction.Logging;
using SWP391_BE.Abstraction.Utility;
using SWP391_BE.DTOs.Auth.LoginAdmin;
using SWP391_BE.DTOs.Auth.LoginUser;
using SWP391_BE.Mappings;
using SWP391_BE.RealJWT;
using SWP391_BE.RealLogger;
using SWP391_BE.RealUtility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register SkinCareManagementDbContext
builder.Services.AddDbContext<SkinCareManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SkinCareManagementDB")));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(LoginAdminHandle).Assembly,
    typeof(LoginUserHandle).Assembly
));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
builder.Services.AddScoped<IUtilityService, UtilityService>();
// ??ng k� IJWT v?i m?t l?p th?c thi
builder.Services.AddScoped<IJWT, JWTService>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add these lines in the service registration section
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddAutoMapper(typeof(OrderMappingProfile));

// Add these lines along with the other service registrations
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddAutoMapper(typeof(OrderDetailMappingProfile));

// Add these lines along with the other service registrations
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddAutoMapper(typeof(PromotionMappingProfile));

// Add these lines along with the other service registrations
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(typeof(ProductMappingProfile));

// Add these lines along with the other service registrations
builder.Services.AddScoped<ISkintypeRepository, SkintypeRepository>();
builder.Services.AddScoped<ISkintypeService, SkintypeService>();
builder.Services.AddAutoMapper(typeof(SkintypeMappingProfile));

// Add these lines along with the other service registrations
builder.Services.AddScoped<ISkinRoutineRepository, SkinRoutineRepository>();
builder.Services.AddScoped<ISkinRoutineService, SkinRoutineService>();
builder.Services.AddAutoMapper(typeof(SkinRoutineMappingProfile));

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

app.MapControllers();

app.Run();

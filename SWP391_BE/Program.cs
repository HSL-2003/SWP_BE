using Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repo;
using Service;
using SWP391_BE.Mappings;
using System.Text;
using Microsoft.AspNetCore.Authentication.Facebook;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register SkinCareManagementDbContext
builder.Services.AddDbContext<SkinCareManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SkinCareManagementDB")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add existing service registrations
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddAutoMapper(typeof(OrderMappingProfile));

builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddAutoMapper(typeof(OrderDetailMappingProfile));

builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddAutoMapper(typeof(PromotionMappingProfile));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(typeof(ProductMappingProfile));

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddAutoMapper(typeof(BrandMappingProfile));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddAutoMapper(typeof(CategoryMappingProfile));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(UserMappingProfile));

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddAutoMapper(typeof(RoleMappingProfile));

builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddAutoMapper(typeof(FeedbackMappingProfile));

builder.Services.AddScoped<IVolumeRepository, VolumeRepository>();
builder.Services.AddScoped<IVolumeService, VolumeService>();
builder.Services.AddAutoMapper(typeof(VolumeMappingProfile));

builder.Services.AddScoped<ISkinTypeRepository, SkinTypeRepository>();
builder.Services.AddScoped<ISkinTypeService, SkinTypeService>();
builder.Services.AddAutoMapper(typeof(SkinTypeMappingProfile));

builder.Services.AddScoped<ISkinRoutineRepository, SkinRoutineRepository>();
builder.Services.AddScoped<ISkinRoutineService, SkinRoutineService>();
builder.Services.AddAutoMapper(typeof(SkinRoutineMappingProfile));

// Add Payment related services
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<PayosService>();
// Add these lines along with the other service registrations
builder.Services.AddScoped<IDashboardReportRepository, DashboardReportRepository>();
builder.Services.AddScoped<IDashboardReportService, DashboardReportService>();
builder.Services.AddAutoMapper(typeof(DashboardMappingProfile));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed((host) => true);
    });
});
var tokenKey = builder.Configuration.GetSection("AppSettings:Token").Value;

Console.WriteLine($"Token Length: {tokenKey?.Length}");
Console.WriteLine($"Token Value: {tokenKey}");

if (string.IsNullOrEmpty(tokenKey))
{
    throw new InvalidOperationException("JWT Token Key is missing from configuration.");
}

if (tokenKey.Length < 64)
{
    throw new InvalidOperationException($"JWT Token Key is too short. Length: {tokenKey.Length}");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RoleClaimType = "role"
        };
    });
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie() // Lưu trạng thái đăng nhập bằng Cookie
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        googleOptions.CallbackPath = "/api/auth/google-login";
    })
    .AddFacebook(facebookOptions =>
    {
        IConfigurationSection facebookAuthNSection = builder.Configuration.GetSection("Authentication:Facebook");
        facebookOptions.AppId = facebookAuthNSection["AppId"];
        facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];
        facebookOptions.CallbackPath = "/api/auth/login-facebook";
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Middleware order is important!
app.UseRouting();
app.UseCors("AllowAll");  // Must be after UseRouting

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Remove UseHttpsRedirection if testing with HTTP
// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Error handling middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unhandled exception: {ex}");
        throw;
    }
});

app.Run(); 
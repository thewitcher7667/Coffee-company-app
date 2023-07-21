using Business_Logic_Layer.Services;
using Business_Logic_Layer.Services.Contracts;
using Data_Access_Layer.DataContext;
using Data_Access_Layer.Models;
using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Persentation_Layer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("cont"));
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Login";
    opt.LogoutPath = "/Logout";
    opt.AccessDeniedPath = "/Login";
});

builder.Services.Configure<PasswordHasherOptions>(opt => opt.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2);

builder.Services.AddTransient(typeof(IGenaricRespository<>),typeof(GenericRepository<>));

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IDepartmenServices, DepartmentServices>();
builder.Services.AddScoped<IRestaurantServices, RestaurantServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IItemBuyedServices, ItemBuyedServices>();
builder.Services.AddScoped<IResetServices, ResetServices>();

builder.Services.AddHostedService<BackgroundServices>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllers();

app.Run();

using Doctork.Application.Abstraction;
using Doctork.Application.Interfaces;
using Doctork.Application.Services;
using Doctork.Infrastructure.Helpers;
using Doctork.Infrastructure.Services;
using Doctork.Persistence.Helpers;
using Doctork.Persistence.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.Configure<Sender>(builder.Configuration.GetSection("Sender"));
builder.Services.AddCors();

var assemblies = Assembly.Load("Doctork.Application");
builder.Services.AddAutoMapper(assemblies);
builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(assemblies));

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddTransient(typeof(IAuthentication), typeof(Authentication));
builder.Services.AddTransient(typeof(IPasswordHasher), typeof(PasswordHasher));
builder.Services.AddTransient(typeof(IJwtService), typeof(JwtService));
builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddTransient(typeof(IImageService), typeof(ImageService));
builder.Services.AddTransient(typeof(IEmailSender), typeof(EmailSender));
builder.Services.AddTransient(typeof(ICacheService), typeof(MemoryCacheService));

builder.Services.AddTransient<IDbConnection>(options => 
new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllers();

app.Run();

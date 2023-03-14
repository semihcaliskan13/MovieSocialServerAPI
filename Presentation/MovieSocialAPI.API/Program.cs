using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MovieSocialAPI.Application;
//todo 0 validator using MovieSocialAPI.Application.Validators;
using MovieSocialAPI.Infrastructure;
using MovieSocialAPI.Infrastructure.Filters;
using MovieSocialAPI.Infrastructure.Services.Storage.Azure;
using MovieSocialAPI.Infrastructure.Services.Storage.Local;
using MovieSocialAPI.Persistence;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddStorage<AzureStorage>();
/*builder.Services.AddStorage<AzureStorage>();*///mimariye hangi storage kullanaca��n� belirtiyoruz.
//bu da enum ile olan y�ntem....  builder.Services.AddStorage(StorageType.Local);
builder.Services.AddPersistenceServices();//Buradaki fonksiyonlar� bu yap� sayesinde ekliyoruz, genel olarak IOC Container yap�lar� ve registiration.
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
//b�t�n sitelerden istek alabilir builder.Services.AddCors(options=>options.AddDefaultPolicy(policy=>policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:3000", "https://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials()//belirli bir urlden gelen istekleri al.
));

builder.Services.AddControllers(options =>
options.Filters.Add<ValidationFilter>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);


//bir cycle durumu movielist'de s�z konyusuydu bu ��zd�.
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);




builder.Services.AddFluentValidationAutoValidation();

//todo 1 validator builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();





builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.Cookie.Name = "access_token";
    })
    .AddJwtBearer("User",options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))

        };
        
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["access_token"];
                return Task.CompletedTask;
            }
        };
        //options.Events.OnMessageReceived = context =>
        //{

        //    if (context.Request.Cookies.ContainsKey("access_token"))
        //    {
        //        context.Token = context.Request.Cookies["access_token"];
        //    }

        //    return Task.CompletedTask;
        //};

    });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseStaticFiles();


app.UseHttpsRedirection();



app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();

using fiap2022.api;
using fiap2022.core.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(
    s => {
        s.SwaggerDoc("v1", new OpenApiInfo() { Title = "Api Fiap", Version = "V1" });
    }) ;

builder.Services.AddControllers(config =>
{

    config.RespectBrowserAcceptHeader = true;
}).AddXmlDataContractSerializerFormatters();


builder.Services.AddCors(x=> {
    x.AddPolicy("Default", b => {
        b.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var connection = @"Server=(localdb)\mssqllocaldb;Database=FiapDatabase;Trusted_Connection=True;ConnectRetryCount=0";
builder.Services.AddDbContext<DataContext>
    (o => o.UseSqlServer(connection));


builder.Services.AddAuthentication(
    x =>
    { x.DefaultAuthenticateScheme = "Jwt";
        x.DefaultChallengeScheme = "Jwt";
    })
    .AddJwtBearer("Jwt",
    o => {
        o.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidAudience = "clients-api",
            ValidIssuer = "api",
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Security.GetKey()),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5)



        };
        }
    );






var app = builder.Build();

app.Use(async (context, next) =>
{
    //logica

    context.Response.Headers.Add("api-version", "v.1.0");
    await next.Invoke();

});


app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API"));


app.UseRouting();


app.UseCors("Default");

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();

using AlumniNetworkAPI.Data;
using AlumniNetworkAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IGroupService), typeof(GroupService));
builder.Services.AddScoped<IUserService, UserSevice>();
builder.Services.AddScoped(typeof(IPostService), typeof(PostService));
builder.Services.AddScoped<ITopicService, TopicService>();

builder.Services.AddDbContext<AlumniDbContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionString"]));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKeyResolver = (token, securitytoken, kid, parameters) =>
            {
                var client = new HttpClient();
                var keyuri = builder.Configuration["TokenSecrets:KeyURI"];
                var response = client.GetAsync(keyuri).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(responseString);
                return keys.Keys;
            },
            ValidIssuers = new List<string>
            {
                builder.Configuration["TokenSecrets:IssuerURI"]
            },
            ValidAudience = "account"
        };
    });

builder.Services.AddDbContext<AlumniDbContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:LocalConnection"]));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AlumniNetworkAPI",
        Version = "v1",
        Description = "ASP.NET Core Web API for alumni, groups, topics, posts and events.",
        License = new OpenApiLicense
        {
            Name = "Use under MIT",
            Url = new Uri("https://opensource.org/licenses/MIT"),
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

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


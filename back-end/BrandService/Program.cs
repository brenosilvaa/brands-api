using BrandService.IoC;
using Microsoft.OpenApi.Models;
using System.Dynamic;
using System.Reflection;

const string ApiName = "BrandsService";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("AllowCors", builder =>
{
    builder.SetIsOriginAllowed(_ => true)
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();
}));

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = ApiName,
        Version = "v1",
        Description = $"API de Integra��o {ApiName}",
        Contact = new OpenApiContact
        {
            Name = "Admin",
            Email = "admin@brands.com",
        },
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";


    config.UseInlineDefinitionsForEnums();

    config.CustomSchemaIds(CustomSchemaIdStrategy);
});

var app = builder.Build();

app.UseCors("AllowCors");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseReDoc(c =>
{
    c.DocumentTitle = $"{ApiName} v1";
    c.SpecUrl = "/swagger/v1/swagger.json";
    c.RoutePrefix = string.Empty;
    c.ConfigObject.AdditionalItems.Add("theme", GetRedocTheme());
});

app.UseAuthentication();

app.UseAuthorization();

app.UseInfrastructure();
app.UseCors("AllowCors");

app.MapControllers();

app.Run();

static string CustomSchemaIdStrategy(Type currentClass)
{
    string returnedValue = currentClass.Name;

    if (returnedValue.EndsWith("Vm"))
        returnedValue = returnedValue.Replace("Vm", string.Empty);

    return returnedValue;
}

static dynamic GetRedocTheme()
{
    dynamic theme = new ExpandoObject();
    theme.colors = new ExpandoObject();
    theme.colors.primary = new ExpandoObject();
    theme.colors.primary.main = "#2196f3";
    return theme;
}
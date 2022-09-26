using System.Reflection;
using CommandQuery;
using CommandQuery.DependencyInjection;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Notino.HomeWork.API.CQS;
using Notino.HomeWork.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    var converter = new StringEnumConverter(new CamelCaseNamingStrategy());
    options.SerializerSettings.Converters.Add(converter);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.IncludeFields = true;
});

builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.CustomSchemaIds(type => type.FullName);
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nutino Home Work API", Version = "v1" });
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    });

// CQS pattern add  Queries
builder.Services.AddQueries(typeof(ConvertXmlToJsonQueryHandler).Assembly);
builder.Services.AddQueries(typeof(DownloadQueryHandler).Assembly);
builder.Services.AddQueries(typeof(ShowUrlQueryHandler).Assembly);
builder.Services.AddQueries(typeof(UploadCommandHandler).Assembly);

//CQS pattern add  commands
builder.Services.AddCommands(typeof(UploadCommandHandler).Assembly);

// add dependencies
builder.Services.AddSingleton<ISaveStringService, SaveStringService>();
builder.Services.AddSingleton<ILoadStringService, LoadStringService>();
builder.Services.AddScoped<IConvertorContentFormatFile, ConvertorContentFormatFile>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.Services.GetService<ICommandProcessor>()?.AssertConfigurationIsValid();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


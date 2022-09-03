using BasicAPISettings.Api.Configs;
using BasicAPISettings.Api.Configs.Auth;
using BasicAPISettings.Api.Configs.Autofac;
using BasicAPISettings.Api.Configs.Database;
using BasicAPISettings.Api.Configs.SupportedCultures;
using BasicAPISettings.Api.Configs.Swagger;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAppConfiguration();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.AddVersionedSwagger();

builder.Services.AddCors();
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAppServices();

var app = builder.Build();

//app.Migrate(); // descomente após adicionar as credenciais do banco de dados

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseHsts();


app.UseSupportedCultures(builder.Configuration);

app.UseVersionedSwagger(app.Services.GetRequiredService<IApiVersionDescriptionProvider>());

app.UseRouting();
app.UseCors(a => a.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

using CoreDataAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Azure.Storage.Blobs;

var azureAdConfiguration = new ConfigurationBuilder()
    .AddInMemoryCollection(new Dictionary<string, string?>
    {
        {"AzureAd:Instance","https://login.microsoftonline.com/"},
        {"AzureAd:Domain", Environment.GetEnvironmentVariable("ASPNETCORE_AZAD_DOMAIN")},
        {"AzureAd:TenantId", Environment.GetEnvironmentVariable("ASPNETCORE_AZAD_TENANT_ID")},
        {"AzureAd:ClientId", Environment.GetEnvironmentVariable("ASPNETCORE_AZAD_CLIENT_ID")},
        //{"AzureAd:ClientSecret", Environment.GetEnvironmentVariable("ASPNETCORE_AZAD_CLIENT_SECRET")},
        {"AzureAd:Audience", Environment.GetEnvironmentVariable("ASPNETCORE_AZAD_AUDIENCE")}
    })
    .Build();

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_SQL_CONNECT");
var blobStorageConnectionString = Environment.GetEnvironmentVariable("ASPNETCORE_AZBLOBS_CONNECT");
var blobServiceClient = new BlobServiceClient(blobStorageConnectionString);

builder.Services.AddSingleton(blobServiceClient);
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connectionString));

// Configurar la autenticación con Microsoft Identity usando ClientSecret y AddMicrosoftIdentityWebApi
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(azureAdConfiguration.GetSection("AzureAd"));

builder.Services.AddAuthentication()
    .AddJwtBearer("AzureAdClientSecret", options =>
    {
        options.Authority = $"{azureAdConfiguration["AzureAd:Instance"]}{azureAdConfiguration["AzureAd:TenantId"]}";
        options.Audience = azureAdConfiguration["AzureAd:Audience"];
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidAudience = azureAdConfiguration["AzureAd:ClientId"],
            ValidIssuer = $"{azureAdConfiguration["AzureAd:Instance"]}{azureAdConfiguration["AzureAd:TenantId"]}",
        };
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DefaultPolicy", policy =>
    {
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme, "AzureAdClientSecret");
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("ProductionPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("DevelopmentPolicy");
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseCors("ProductionPolicy");
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization("DefaultPolicy");

app.Run();

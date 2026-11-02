using AuthTemplate.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddAuthServices(builder.Configuration);

var app = builder.Build();

app.InitializeDatabase();
app.UsePresentation(builder.Environment);

app.Run();

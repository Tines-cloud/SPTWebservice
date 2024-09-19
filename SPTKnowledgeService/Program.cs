using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SPTGradeService.Repositories;
using SPTGradeService.Repositories.Impl;
using SPTGradeService.Services;
using SPTGradeService.Services.Impl;
using SPTKnowledgeService.Data;
using SPTKnowledgeService.Mappers;
using SPTKnowledgeService.Repositories;
using SPTKnowledgeService.Repositories.Impl;
using SPTKnowledgeService.Services;
using SPTKnowledgeService.Services.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure the database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the configuration settings
builder.Services.Configure<ApiGatewaySettings>(builder.Configuration.GetSection("ApiGateway"));

// Register HTTP clients with correct base addresses
builder.Services.AddHttpClient<ISubjectService, SubjectService>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<ApiGatewaySettings>>().Value;
    client.BaseAddress = new Uri(settings.SubjectServiceBaseUrl);
});

builder.Services.AddHttpClient<IUserService, UserService>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<ApiGatewaySettings>>().Value;
    client.BaseAddress = new Uri(settings.UserServiceBaseUrl);
});

// Register repositories
builder.Services.AddScoped<IStudySessionRepository, StudySessionRepository>();
builder.Services.AddScoped<IBreakRepository, BreakRepository>();
builder.Services.AddScoped<IKnowledgeRepository, KnowledgeRepository>();
builder.Services.AddScoped<IGradeRepository, GradeRepository>();

// Register services
builder.Services.AddScoped<IStudySessionService, StudySessionService>();
builder.Services.AddScoped<IBreakService, BreakService>();
builder.Services.AddScoped<IKnowledgeService, KnowledgeService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGradeService, GradeService>();

// Register mappers
builder.Services.AddScoped<KnowledgeMapper>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

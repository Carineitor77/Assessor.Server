using System.Reflection;
using Assessor.Server.Api.Extensions;
using Assessor.Server.Api.Filters;
using Assessor.Server.Api.Middlewares;
using Assessor.Server.Application.Common.Interfaces;
using Assessor.Server.Domain.Enums;
using Assessor.Server.Domain.Options;
using Assessor.Server.Infrastructure.HttpClients;
using Assessor.Server.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(FluentValidationFilter));
});
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(Assembly.Load("Assessor.Server.Application"));
});
builder.Services.Configure<OpenAiOptions>(builder.Configuration.GetSection("OpenAi"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<IAiHttpClient, OpenAiHttpClient>();
builder.Services.AddKeyedScoped<IFileReaderService, DocFileReaderService>(FileReaderTypes.Doc);
builder.Services.AddKeyedScoped<IFileReaderService, PdfFileReaderService>(FileReaderTypes.Pdf);
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

var app = builder.Build();

app.UseGlobalExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();

// TODO: add fluent validation
using Externo.Infrastructure;
using Externo.Repositories;
using Externo.UseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.Mail;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Externo API", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

#region Infrastructure

builder.Services.AddScoped(sp => new SmtpClient(builder.Configuration.GetValue<string>("SmtpSettings:Host"))
{
    Port = builder.Configuration.GetValue<int>("SmtpSettings:Port"),
    Credentials = new NetworkCredential(
        builder.Configuration.GetValue<string>("SmtpSettings:Username"),
        builder.Configuration.GetValue<string>("SmtpSettings:Password")),
    EnableSsl = true,
});
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddSingleton<IFila, Fila>();
builder.Services.AddDbContext<ExternoDbContext>(options => options.UseInMemoryDatabase("ExternoDb"));

#endregion Infrastructure

#region UseCases

builder.Services.AddTransient<IEnviarEmailUseCase, EnviarEmailUseCase>();

#endregion UseCases

#region Repositories

builder.Services.AddTransient<IEmailRepository, EmailRepository>();
builder.Services.AddTransient<ICobrancaRepository, CobrancaRepository>();

#endregion Repositories

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

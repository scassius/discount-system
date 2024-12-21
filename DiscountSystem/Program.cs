using DiscountSystem.Application.Services;
using DiscountSystem.Domain.Interfaces;
using DiscountSystem.Domain.Services;
using DiscountSystem.Infrastructure.Data;
using DiscountSystem.Infrastructure.Repositories;
using DiscountSystem.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DiscountSystem.API;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5001, o =>
    {
        o.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
        o.UseHttps();
    });

    options.ListenLocalhost(5000, o =>
    {
        o.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=discountcodes.db"));

builder.Services.AddScoped<IDiscountCodeRepository, DiscountCodeRepository>();
builder.Services.AddScoped<DiscountCodeGenerator>();
builder.Services.AddScoped<DiscountServiceApp>();
builder.Services.AddGrpc();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.MapGrpcService<DiscountServiceImpl>();
app.MapGet("/", () => "gRPC server is running.");

app.Run();
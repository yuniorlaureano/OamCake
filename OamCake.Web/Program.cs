using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using OamCake.Common;
using OamCake.Common.Helpers;
using OamCake.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<OamCakeContext>(options => options
        .UseSqlServer(builder.Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("OamCake.Web")));

builder.Services.AddScoped<IConnection, Connection>();
builder.Services.AddScoped<IDapperHelper, DapperHelper>();

builder.Services.AddAuthentication(Constants.AUTH_TYPE).AddCookie(Constants.AUTH_TYPE, options =>
{
    options.Cookie.Name = Constants.AUTH_TYPE;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(Constants.CLAIM_ADMIN, policy => policy.RequireClaim(Constants.CLAIM_ADMIN, Constants.CLAIM_ADMIN));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Media", "Photos")),
    RequestPath = new PathString("/photos")
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

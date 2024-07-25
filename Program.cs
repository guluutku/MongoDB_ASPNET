using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add Custom MongoDB Services
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection(nameof(MongoDBSettings.ConnectionString))
);

builder.Services.AddSingleton<IMongoDBSettings>(
    sp => sp.GetRequiredService<IOptions<MongoDBSettings>>().Value
);

builder.Services.AddSingleton<IMongoClient>(
  s => new MongoClient(builder.Configuration.GetValue<string>("MongoDBSettings:ConnectionString"))
);

builder.Services.AddScoped<IStudentService, StudentService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

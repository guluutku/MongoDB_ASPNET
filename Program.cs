using MongoDB.Driver;
using MongoDB.Bson;

MongoClient client = new MongoClient("mongodb+srv://gunuluutku:<password>@laerning.dvlyypw.mongodb.net/?appName=Laerning");

List<string> databases = client.ListDatabaseNames().ToList();

foreach(string database in databases) {
    Console.WriteLine(database);
}

var builder = WebApplication.CreateBuilder(args);

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

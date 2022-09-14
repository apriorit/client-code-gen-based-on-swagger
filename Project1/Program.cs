var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddNewtonsoftJson();

builder.Services.AddSwaggerDocument(settings =>
{
    settings.PostProcess = document =>
    {
        document.Info.Version = "v1";
        document.Info.Title = "Example API";
        document.Info.Description = "REST API for example.";
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultDevelopment",
        builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
            builder.WithExposedHeaders("Content-Disposition"); //to pass the names of the files that are downloaded in angular
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseCors("DefaultDevelopment");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

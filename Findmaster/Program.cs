using Findmaster.DataAccessLayer.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

var app = builder.Build();

app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<DatabaseContext>(opt => 
    opt.UseNpgsql("Host = localhost; Port = 5432; Database = usersdb; Username = postgres; Password = 1"));
    services.AddControllers();
    services.AddControllersWithViews();
}


void Configure(IApplicationBuilder app)
{
    app.UseDeveloperExceptionPage();

    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
    });


}

    if (app.Environment.IsDevelopment())
    {
    }

    app.UseSwagger();
    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.MapRazorPages();

    app.Run();

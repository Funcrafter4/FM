using Findmaster.Controllers;
using Findmaster.DataAccessLayer.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapControllers();
app.UseSwagger();
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
    services.AddRouting();
    
}


void Configure(IApplicationBuilder app)
{
    app.UseDeveloperExceptionPage();

    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.UseRouting();
    
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
}

    if (app.Environment.IsDevelopment())
    {
    }


    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();


    app.MapRazorPages();

    app.Run();

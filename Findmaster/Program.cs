using Findmaster.DataAccessLayer.DataContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DatabaseContext>(opt =>
opt.UseNpgsql("Host = abul.db.elephantsql.com; Port = 5432; Database = gepagwva; Username = gepagwva; Password = QCfayoFrlibAbMUqZiEBXwLKH5ZG0lXM"));
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddRouting();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

var app = builder.Build();
app.MapControllers();
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
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
    opt.UseNpgsql("Host = abul.db.elephantsql.com; Port = 5432; Database = xpichrsv; Username = xpichrsv; Password = qipQnRJQOoZOt6tnukvrY97p-krIakrB"));
    services.AddControllers();
    services.AddControllersWithViews();
    services.AddRouting();
    
}


void Configure(IApplicationBuilder app)
{
}

    if (app.Environment.IsDevelopment())
    {
    }


    app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

    app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();

    app.Run();

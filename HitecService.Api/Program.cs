using HitecService.Core.Middlewares;
using HitecService.Ioc;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddHealthChecks();


builder.Services.IocInjectAllDependencies();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI(s => { s.SwaggerEndpoint("/swagger/v1/swagger.json", "HitecService API"); });

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();
app.UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
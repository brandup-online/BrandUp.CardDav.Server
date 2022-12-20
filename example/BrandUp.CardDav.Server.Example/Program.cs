using BrandUp.CardDav.Server;
using BrandUp.CardDav.Server.Example.Domain.Context;
using BrandUp.CardDav.Server.Example.Domain.Repositories;
using BrandUp.CardDav.Transport.Binding.Providers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new RequestBinderProvider());
});

builder.Services.AddCradDavServer()
                .AddUsers<UserRepository>()
                .AddAddressBooks<AddressBookRepository>()
                .AddContacts<ContactRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongoDbContext<AppDocumentContext>(builder.Configuration.GetSection("MongoDb"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
app.MapControllers();

app.Run();

public partial class Program { }
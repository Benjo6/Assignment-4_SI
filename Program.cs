using System.Text.Json.Serialization;
using Assignment_4_SI.Configuration;
using Assignment_4_SI.Controllers;
using Assignment_4_SI.Mail;
using Assignment_4_SI.Models.Database.ActivityRepository;
using Assignment_4_SI.Models.Database.EventRepository;
using Assignment_4_SI.Models.Database.ReservationRepository;
using Assignment_4_SI.Models.Database.UserRepository;
using Assignment_4_SI.Models.Database.UUIDRepository;
using Assignment_4_SI.RabbitMQ.Configuration;
using Assignment_4_SI.RabbitMQ.Consumer;
using Assignment_4_SI.RabbitMQ.Producer;
using Assignment_4_SI.XML;
using FrontEndAPI.Models.Database;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMvc();
builder.Services.AddMvc().AddJsonOptions(options => {
    //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.JsonSerializerOptions.ReferenceHandler =ReferenceHandler.IgnoreCycles;
});

//mail config
builder.Services.AddSingleton<IEmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
builder.Services.AddTransient<IEmailService, EmailService>();

//rabbit mq
builder.Services.AddSingleton<IMQConfiguration>(builder.Configuration.GetSection("RabbitMQConfiguration").Get<MqConfiguration>());
builder.Services.AddSingleton<IMQConsumer, MQConsumer>();
builder.Services.AddScoped<IMQProducer, MQProducer>();
builder.Services.AddSingleton<IHostedService, ConsumerService>();

//system and message configuration
builder.Services.AddSingleton<IMessageTypeConfiguration>(builder.Configuration.GetSection("MessageTypes").Get<MessageTypeConfiguration>());
builder.Services.AddSingleton<ISystemConfiguration>(builder.Configuration.GetSection("Systems")
    .Get<SystemConfiguration>());

    
//controllers as service
builder.Services.AddTransient<UserController, UserController>();
builder.Services.AddTransient<ReservationController, ReservationController>();

//xml services
builder.Services.AddScoped<IXMLService, XMLService>(); 

//db context
builder.Services.AddDbContext<S2ITSP2_2_Context>(options => options.UseMySQL(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    x => x.MigrationsHistoryTable("__APIMigrationsHistory")));
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<IEventRepository, EventRepositoryImpl>();
builder.Services.AddScoped<IActivityRepository, ActivityRepositoryImpl>();
builder.Services.AddScoped<IReservationRepository, ReservationRepositoryImpl>();
builder.Services.AddScoped<IUUIDRepository,UUIDRepositoryImpl>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
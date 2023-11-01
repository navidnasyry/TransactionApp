using Nest;
using TransactionAppProject.Classes;
using TransactionAppProject.ApplicationExceptions;
using TransactionAppProject.Configs;
using TransactionAppProject.Interfaces;

// Read Environment Configs
var configFilePath = "app.config";
var builderObj = new ConfigurationBuilder();
var configObj = new ConfigurationsReader(builderObj, configFilePath);

// Connecting to Elasticsearch
var elasticObj = new ElasticClientFactory(configObj);
var elasticClient = elasticObj.GetElasticsearchClient();

var connectionChecker = new CheckElasticConnection(elasticClient);
if (!connectionChecker.CheckAllCheckers())
{
    throw new ConnectionFailedException(elasticObj.ToString());
}

// Create Elastic Repository
var elasticRepository = new ElasticClientRepository(elasticObj);


// Start WebApplication Setup
var builder = WebApplication.CreateBuilder(args);
Console.WriteLine(builder.Configuration);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<IElasticClientRepository>(x=>elasticRepository);
builder.Services.AddDependencyGroup();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.
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
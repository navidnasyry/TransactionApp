using TransactionAppProject.Classes;


// Read Environment Configs
var configFilePath = "app.config";
var configObj = new ReadConfigurations(configFilePath);

// Connecting to Elasticsearch
var elasticObj = new ElasticClientFactory(configObj);
var elasticClient = elasticObj.GetElasticsearchClient();

// Start WebApplication Setup
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
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
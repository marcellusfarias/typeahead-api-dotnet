﻿using TypeAheadApi.Data.Interfaces;
using TypeAheadApi.Data;
using TypeAheadApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITrie>(trie =>
{
    var trieFactory = new TrieFactory(10, trie.GetService<ILogger<TrieFactory>>()!, trie.GetService<ILogger<Trie>>()!);
    return trieFactory.Initialize(File.ReadAllText("./names.json"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(error => error.UseCustomErrors(app.Services.GetRequiredService<ILoggerFactory>()));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

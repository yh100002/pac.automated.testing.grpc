using POC.AutomatedTesting.gRPC.Services;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

/*
builder.Services.AddCodeFirstGrpc(options =>
{
    options.MaxReceiveMessageSize = 10 * 1024 * 1024; // 10 MB
    options.MaxSendMessageSize = 10 * 1024 * 1024; // 10 MB

});
*/

builder.Services.AddGrpcReflection();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<PacPlatformTeamMembersService>();
//app.MapCodeFirstGrpcReflectionService();
app.MapGrpcReflectionService();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

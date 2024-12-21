using Grpc.Net.Client;
using DiscountSystem.Client;

using var channel = GrpcChannel.ForAddress("http://localhost:5000", new GrpcChannelOptions
{
    Credentials = Grpc.Core.ChannelCredentials.Insecure
});

var client = new DiscountSystem.Client.DiscountService.DiscountServiceClient(channel);

var generateResponse = client.GenerateCodes(new GenerateRequest { Count = 100, Length = 8 });
Console.WriteLine($"Generate Codes: {(generateResponse.Result ? "Success" : "Failure")}");

var useResponse = await client.UseCodeAsync(new UseCodeRequest { Code = "ABCDEFGH" });
Console.WriteLine($"Use Code Result: {useResponse.Result}");
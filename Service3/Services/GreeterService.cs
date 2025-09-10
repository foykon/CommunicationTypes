using Grpc.Core;

namespace Service3.Services
{
    public class GreeterService : GreetService.GreetServiceBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }
        public override async Task SayHello(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
           await Task.Run(async () =>
           {
               for (int i = 0; i < 10; i++)
               {
                   await responseStream.WriteAsync(new HelloReply
                   {
                       Message = $"Hello {request.Name} - {i}"
                   });
                   await Task.Delay(1000);
               }
           });
        }
    }
}

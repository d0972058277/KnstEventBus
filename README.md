#### Reference
[tehmantra/saunter](https://github.com/tehmantra/saunter)  
[domaindrivendev/Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)  
[dotnet-architecture/eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers/tree/dotnet3-migration/dev-dotnet3)
***  
# KnstAsyncApi
### Register in Startup.cs ConfigureServices
```
services.AddAsyncApi(options =>
{
    options.AsyncApi = new AsyncApiDocument
    {
        Info = new Info("Async API", "1.0.0")
        {
            Description = "Description"
        }
    };
});
```
### Use middleware
```
app.UseAsyncApi();
```
### Add AsyncApi document mark  
[[AsyncApi]](https://github.com/d0972058277/KnstEventBus/blob/master/KnstAsyncApi/Attributes/AsyncApiAttribute.cs)  
[[Channel]](https://github.com/d0972058277/KnstEventBus/blob/master/KnstAsyncApi/Attributes/ChannelAttribute.cs)  
[[Message]](https://github.com/d0972058277/KnstEventBus/blob/master/KnstAsyncApi/Attributes/MessageAttribute.cs)  
[[Publish]](https://github.com/d0972058277/KnstEventBus/blob/master/KnstAsyncApi/Attributes/PublishAttribute.cs)  
[[Subscribe]](https://github.com/d0972058277/KnstEventBus/blob/master/KnstAsyncApi/Attributes/SubscribeAttribute.cs)  
```
/// <summary>
/// HelloWorld Channel(Topic) Summary
/// </summary>
[AsyncApi]
[Channel("pubsub/HelloWorld")]
[Message(typeof(HelloWorld))]
public class HelloWorldChannel : IChannel<HelloWorld>
{
    /// <summary>
    /// HelloWorld Pub Summary
    /// </summary>
    [Publish]
    public Task PublishAsync(HelloWorld @event) => Task.CompletedTask;

    /// <summary>
    /// HelloWorld Sub Summary
    /// </summary>
    [Subscribe]
    public Task SubscribeAsync() => Task.CompletedTask;

    public Task UnSubscribeAsync() => Task.CompletedTask;
}
```
### Visit your document  
https://{domain-host}/asyncapi/asyncapi.json  
```https://localhost:5001/asyncapi/asyncapi.json```

You can run my [toys](https://github.com/d0972058277/KnstEventBus/tree/master/Toys) to get example.

#### Reference and Thanks
[AsyncApi](https://www.asyncapi.com/)  
[tehmantra/saunter](https://github.com/tehmantra/saunter)  
[domaindrivendev/Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)  
[dotnet-architecture/eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers/tree/dotnet3-migration/dev-dotnet3)  
[RicoSuter/Namotion.Reflection](https://github.com/RicoSuter/Namotion.Reflection)  
[JeringTech/Javascript.NodeJS](https://github.com/JeringTech/Javascript.NodeJS)
***  
# [KnstAsyncApi](https://www.nuget.org/packages/KnstAsyncApi) ![Nuget](https://img.shields.io/nuget/v/KnstAsyncApi) & [KnstAsyncApiUI](https://www.nuget.org/packages/KnstAsyncApiUI) ![Nuget](https://img.shields.io/nuget/v/KnstAsyncApiUI)
#### Requirements  
* Node.js v12.16+
* npm v6.13.7+

### Install
```
dotnet add package KnstAsyncApi --version 1.1.1
dotnet add package KnstAsyncApiUI --version 1.0.8
```

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
services.AddAsyncApiUI();
```
### Use middleware
```
app.UseAsyncApi();
app.UseAsyncApiUI();
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
public class HelloWorldChannel : IPubChannel<HelloWorld>, ISubChannel
{
    /// <summary>
    /// HelloWorld Pub Summary
    /// </summary>
    [Publish]
    [Message(typeof(HelloWorld))]
    public Task PublishAsync(HelloWorld @event) => Task.CompletedTask;

    /// <summary>
    /// HelloWorld Sub Summary
    /// </summary>
    [Subscribe]
    [Message(typeof(HelloWorld))]
    public Task SubscribeAsync() => Task.CompletedTask;

    public Task UnSubscribeAsync() => Task.CompletedTask;
}
```
### Visit your document  
https://{domain-host}/asyncapi/asyncapi.json  
```https://localhost:5001/asyncapi/asyncapi.json```  
https://{domain-host}/asyncapi/index.html  
```https://localhost:5001/asyncapi/index.html```  

You can run my [toys](https://github.com/d0972058277/KnstEventBus/tree/master/Toys) to get example.  
![toys-asyncapi-document](https://raw.githubusercontent.com/d0972058277/KnstEventBus/master/Images/toys-asyncapi-document.png)

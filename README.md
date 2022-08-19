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

### Licenses
#### The Unlicense
This is free and unencumbered software released into the public domain.

Anyone is free to copy, modify, publish, use, compile, sell, or
distribute this software, either in source code form or as a compiled
binary, for any purpose, commercial or non-commercial, and by any
means.

In jurisdictions that recognize copyright laws, the author or authors
of this software dedicate any and all copyright interest in the
software to the public domain. We make this dedication for the benefit
of the public at large and to the detriment of our heirs and
successors. We intend this dedication to be an overt act of
relinquishment in perpetuity of all present and future rights to this
software under copyright law.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

For more information, please refer to <https://unlicense.org>

const path = require('path');
const Generator = require('@asyncapi/generator');
const generator = new Generator('@asyncapi/html-template', path.resolve(__dirname, 'wwwroot'));

async function generateAsyncApi(callback, url) {
    try {
        // https://raw.githubusercontent.com/asyncapi/asyncapi/2.0.0/examples/2.0.0/streetlights.yml
        await generator.generateFromString('{"asyncapi":"2.0.0","info":{"title":"Toys API","version":"1.0.0","description":"Knst Toys."},"defaultContentType":"application/json","channels":{"pubsub/HelloWorld":{"subscribe":{"operationId":"Toys.Channels.HelloWorlds.HelloWorldChannel.Subscribe","summary":"","tags":[],"bindings":{},"message":{"payload":{"$ref":"#/components/schemas/helloWorld"},"tags":[],"examples":[],"traits":[]},"traits":[]},"publish":{"operationId":"Toys.Channels.HelloWorlds.HelloWorldChannel.Publish","summary":"","tags":[],"bindings":{},"message":{"payload":{"$ref":"#/components/schemas/helloWorld"},"tags":[],"examples":[],"traits":[]},"traits":[]}}},"components":{"schemas":{"innerHelloWorld":{"type":"object","required":[],"properties":{"InnerMessage":{"type":"string"},"HelloWorldEnum":{"type":"string","enum":["Test1","Test2","Test3"]}}},"helloWorld":{"type":"object","required":[],"properties":{"Messages":{"type":"array","items":{"type":"string"}},"InnerHelloWorld":{"$ref":"#/components/schemas/innerHelloWorld"},"Id":{"type":"string","format":"uuid"},"CreationDate":{"type":"string","format":"date-time"}}}},"messages":{},"securitySchemes":{},"parameters":{},"correlationIds":{},"serverBindings":{},"channelBindings":{},"operationBindings":{},"messageBindings":{},"operationTraits":{},"messageTraits":{}},"tags":[]}');
        // await generator.generateFromURL(url);
        callback(null, 'done');
    } catch (e) {
        console.error(e);
        callback(e);
    }
}

module.exports = generateAsyncApi;

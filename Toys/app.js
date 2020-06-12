const path = require('path');
const Generator = require('@asyncapi/generator');
const generator = new Generator('@asyncapi/html-template', path.resolve(__dirname, 'wwwroot'));

async function generateAsyncApi(callback, url) {
    try {
        // https://raw.githubusercontent.com/asyncapi/asyncapi/2.0.0/examples/2.0.0/streetlights.yml
        await generator.generateFromURL(url);
        callback(null, 'done');
    } catch (e) {
        console.error(e);
        callback(e);
    }
}

module.exports = generateAsyncApi;
const path = require('path');
const Generator = require('@asyncapi/generator');

async function generateAsyncApi(url, directory) {
    try {
        // https://raw.githubusercontent.com/asyncapi/asyncapi/2.0.0/examples/2.0.0/streetlights.yml
        let generator = new Generator('@asyncapi/html-template', path.resolve(__dirname, directory));
        process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
        await generator.generateFromURL(url);
    } catch (e) {
        console.error(e);
        throw e;
    }
}

module.exports = generateAsyncApi;

const path = require('path');
const Generator = require('@asyncapi/generator');

async function generateAsyncApi(asyncapiDocument, directory) {
    try {
        let generator = new Generator('@asyncapi/html-template', path.resolve(__dirname, directory), { forceWrite=true });
        await generator.generateFromString(asyncapiDocument);
    } catch (e) {
        console.error(e);
        throw e;
    }
}

module.exports = generateAsyncApi;

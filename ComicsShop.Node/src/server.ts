import fastify from 'fastify';
import AppService from './app';

const server = fastify()
server.register(AppService);

server.listen({ port: 8094, host: '0.0.0.0' }, (err, address) => {
    if (err) {
        console.error(err)
        process.exit(1)
    }
    console.log(`Server listening at ${address}`)
})

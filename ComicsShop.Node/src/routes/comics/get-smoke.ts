import {FastifyInstance, RouteOptions} from 'fastify'

export default function getById(fastify: FastifyInstance): RouteOptions {
    return {
        method: 'GET',
        url: '/smoke',
        handler: async (request, reply) => {
            return reply.code(200).send("Hello world");
        }
    };
}


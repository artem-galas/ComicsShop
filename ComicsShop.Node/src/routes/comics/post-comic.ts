import { FastifyInstance, RouteOptions } from 'fastify';
import * as comicsRepo from '../../repositories/comics.repository';
import {
    comicsReplyMapper,
    ComicsCreateRequestSchema,
    ComicsReplySchema
} from '../../mappers/comics.mapper';

export default function postComic(fastify: FastifyInstance): RouteOptions {
    return {
        method: 'POST',
        url: '/comics',
        schema: {
            body: ComicsCreateRequestSchema,
            response: {
                '201': ComicsReplySchema,
            }
        },
        handler: async (request, reply) => {
            const { body } = request;
            const [{ Id: createdId }] = await comicsRepo.create(fastify, (body as Comics));
            const comic = await comicsRepo.getById(fastify, createdId);

            if (comic) {
                return reply.code(201).send(comicsReplyMapper(comic));
            }

            return reply.code(422).send(new Error('Bad request'));
        }
    }
}

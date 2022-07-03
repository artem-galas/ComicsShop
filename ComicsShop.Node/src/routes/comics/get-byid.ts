import {FastifyInstance, RouteOptions} from 'fastify'
import * as comicsRepo from '../../repositories/comics.repository';
import {
    ComicsByIdRequestParams,
    ComicsByIdRequestParamsSchema,
    comicsReplyMapper,
    ComicsReplySchema
} from '../../mappers/comics.mapper';

export default function getById(fastify: FastifyInstance): RouteOptions {
    return {
        method: 'GET',
        url: '/comics/:id',
        schema: {
            params: ComicsByIdRequestParamsSchema,
            response: {
                200: ComicsReplySchema,
            }
        },
        handler: async (request, reply) => {
            const {params} = request;

            const comics = await comicsRepo.getById(fastify, (params as ComicsByIdRequestParams).id);

            if (comics) {
                return comicsReplyMapper(comics);
            }

            return reply.code(404).send(new Error('Comics not found'));
        }
    };
}


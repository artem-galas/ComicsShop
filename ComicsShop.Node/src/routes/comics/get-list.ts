import {FastifyInstance, RouteOptions} from 'fastify';
import * as comicsRepo from '../../repositories/comics.repository';
import {comicsListReplyMapper, ComicsListReplySchema} from '../../mappers/comics.mapper';

export default function getList(fastify: FastifyInstance): RouteOptions {
    return {
        method: 'GET',
        url: '/comics',
        schema: {
            response: {
                '2xx': ComicsListReplySchema,
            }
        },
        handler: async () => {
            const comicsList = await comicsRepo.getAll(fastify);

            return comicsListReplyMapper(comicsList);
        }
    }
}

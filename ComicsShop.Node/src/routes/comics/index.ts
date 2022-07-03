import {FastifyInstance} from 'fastify'
import getList from './get-list';
import getById from './get-byid';
import postComic from './post-comic';
import smokeTesting from './get-smoke';

export default async function (fastify: FastifyInstance) {
    fastify.route(getList(fastify));
    fastify.route(getById(fastify));
    fastify.route(postComic(fastify));
    fastify.route(smokeTesting(fastify));
}

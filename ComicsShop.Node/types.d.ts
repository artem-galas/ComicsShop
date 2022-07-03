import { FastifyPlugin  } from 'fastify'
import {Knex} from 'knex';

declare module 'fastify' {
    interface FastifyInstance {
        knex: Knex,
    }
}

export type KnexJsOptions = Knex.Config;


declare const fastifyKnexJS: FastifyPlugin<KnexJsOptions>;

export default fastifyKnexJS;

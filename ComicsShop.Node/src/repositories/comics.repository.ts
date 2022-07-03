import {FastifyInstance} from 'fastify';
import { v4 as uuidv4} from 'uuid';

export async function getById(fastify: FastifyInstance, id: string) {
    return fastify.knex<Comics>('Comics')
        .where({Id: id})
        .first();
}

export async function getAll(fastify: FastifyInstance) {
    return fastify.knex<Comics>('Comics');
}

export async function updateById(fastify: FastifyInstance, id: string, data: Partial<Comics>) {
    return fastify.knex<Comics>('Comics')
        .where({Id: id})
        .update(data)
        .returning('Id')
}

export async function create(fastify: FastifyInstance, data: Comics) {
    const id = uuidv4();
    return fastify.knex<Comics>('Comics')
        .insert({...data, Id: id})
        .returning('Id');
}

export async function deleteById(fastify: FastifyInstance, id: string) {
    return fastify.knex<Comics>('Comics')
        .delete()
        .returning('Id')
}

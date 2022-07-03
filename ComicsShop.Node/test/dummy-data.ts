import {FastifyInstance} from 'fastify';

export async function generateTestDb(fastify: FastifyInstance) {
    await createComicsDb(fastify.knex);
    await generateComicsTable(fastify.knex);
}

export async function createComicsDb(knex: FastifyInstance['knex']) {
    return knex.schema.createTable('Comics', (t) => {
        t.string('Id', 36);
        t.double('Price');
        t.string('Title');
        t.string('Image');
        t.text('Description', 'TINYTEXT');
        t.primary(['Id']);
    });
}

export async function destroyTestDb(fastify: FastifyInstance) {
    return fastify.knex.schema.dropTable('Comics');
}

export async function generateComicsTable(knex: FastifyInstance['knex']) {
    return knex<Comics>('Comics')
        .insert([
            {
                Id: 'e9804a14-f08e-4a10-abb9-b54f8fcb3cea',
                Price: 2.99,
                Image: 'comics01.jsp',
                Title: '“I AM GOTHAM” Chapter One',
                Description: 'Comics 01 description',
            },
            {
                Id: '985c1976-1ca8-4509-874f-d350adbfc027',
                Price: 2.99,
                Title: 'I AM GOTHAM” Chapter Two',
                Image: 'comics02.jsp',
                Description: 'Comics 01 description',
            },
            {
                Id: 'ba3a26ba-be81-42eb-a6a0-831edf41fb1f',
                Price: 1.99,
                Title: 'PATH TO DOOM I',
                Image: 'superman01.jpg',
                Description: 'Superman description 01'
            },
            {
                Id: '39f8aaa3-151d-4163-8a61-9d6c62319f8d',
                Price: 4.99,
                Title: 'PATH TO DOOM II',
                Image: 'superman02.jpg',
                Description: 'Superman description 02'
            },
        ])
}

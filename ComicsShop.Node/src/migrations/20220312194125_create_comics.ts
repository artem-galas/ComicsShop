import {Knex} from 'knex';

export async function up(knex: Knex): Promise<void> {
    return knex.schema.createTable('Comics', (t) => {
        t.string('Id', 36);
        t.double('Price');
        t.string('Title');
        t.string('Image');
        t.text('Description', 'TINYTEXT');
        t.primary(['Id']);
    });
}


export async function down(knex: Knex): Promise<void> {
    return knex.schema.dropTable('Comics');
}


const data = require('./data.json');
const {defaultSettings} = require('./knexfile');
const knex = require('knex')(defaultSettings);
const { v4: uuidv4 } = require('uuid');

async function seedData() {
    try {
        var rows = await knex('Comics');

        if (rows > 0) {
            console.log("Seed is declined as data already exist");
            return;
        }

        console.log("Seed Started... 🦾");

        for (const d of data) {
            await knex('Comics').insert({
                ...d,
                Id: uuidv4(),
            });
        }

        console.log("Seed completed ✅");
    } catch (e) {
        console.log(e);
    }
}

seedData();
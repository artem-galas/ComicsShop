require('ts-node/register');
const path = require('path');
const BASE_PATH = path.join(__dirname, 'src');

export const defaultSettings = {
    client: 'mysql',
    connection: process.env.DB_CONNECTION_STRING,
    migrations: {
        directory: path.join(BASE_PATH, 'migrations')
    },
    seeds: {
        directory: path.join(BASE_PATH, 'seeds')
    }
};

export const knexConfig = {
    development: {
        ...defaultSettings
    },

    production: {
        ...defaultSettings
    },

    test: {
        ...defaultSettings,
        client: 'sqlite3',
        connection: { filename: ':memory:' },
        useNullAsDefault: true,
    }
};

module.exports = {
    ...knexConfig
};

const path = require('path');
const BASE_PATH = path.join(__dirname, 'src');

connectionString = 'mysql://comics_shop:password@127.0.0.1:8091/comics_shop' 

const defaultSettings = {
    client: 'mysql2',
    connection: connectionString,
};

module.exports = {
    defaultSettings
};
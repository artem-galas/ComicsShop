const autocannon = require('autocannon')

let servers = {
    rest: `http://localhost:8093`,
    node: `http://localhost:8094`,
};

const instance = autocannon({
    url: `${servers[process.env.SERVER]}/${process.env.ROUTE}`,
    method: 'GET',
    connections: 10,
    pipelining: 1,
    duration: 60,
    timeout: 20,
    warmup: {
        connections: 1,
        duration: 3
    }
}, autocannon.printResult)

autocannon.track(instance, { renderProgressBar: true });

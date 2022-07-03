import {build} from '../helper'
import {destroyTestDb, generateTestDb} from '../dummy-data';

const app = build();

describe('ComicsShop routes', () => {
    beforeEach(async () => {
        await generateTestDb(app);
    });

    afterEach(async () => {
        await destroyTestDb(app);
    })

    it('GET /comics returns list', async () => {
        const res = await app.inject({
            url: '/comics'
        })

        const { data } = res.json();

        expect(data).toHaveLength(4);
    })

    it('GET /comics/:id returns selected comics', async () => {
        const res = await app.inject({
            url: '/comics/e9804a14-f08e-4a10-abb9-b54f8fcb3cea'
        })

        const { data } = res.json();

        expect(data).toEqual({
            id: 'e9804a14-f08e-4a10-abb9-b54f8fcb3cea',
            price: 2.99,
            image: 'comics01.jsp',
            title: '“I AM GOTHAM” Chapter One',
            description: 'Comics 01 description',
        });
    });

    it('GET /comics/:id returns error if comics not found', async () => {
        const res = await app.inject({
            url: '/comics/random-uuid'
        })

        const { data, error } = res.json();

        expect(data).toBeNull();
        expect(res.statusCode).toBe(404);
    });
})


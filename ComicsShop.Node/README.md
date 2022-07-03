# Node JS ComicsShop
Simple [Fastify](https://www.fastify.io/) NodeJS application 

Application follow same structure as a C# one

- entities - type which represents DB entity
- mappers - functions/types for format response
- migrations - DB migrations files
- plugins - fastify plugins for connected DB and utility errors
- repositories - functions to manipulate with DB entity
- routes - route handler

## API
### `GET /comics`
✅ 200 - list of comics

### `GET /comics/:id`
✅ 200 - returns one comics' by ID
❌ 404 - if comics not found

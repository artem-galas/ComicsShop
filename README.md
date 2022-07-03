# .NET Minimal API vs Fastify

- [Fastify Doc](https://www.fastify.io/)
- [C# MinimalApi](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0)

This repo represents tree implementation of the same API

- ComicsShop.Rest - .NET 6 implements REST Api
- ComicsShop.Node - NodeJS implements REST Api
- SeedData - simple application for adding data to DB
- StressTest - using [autocannon](https://www.npmjs.com/package/autocannon) for perfomance testing

## API

### `GET /comics`
✅ 200 - list of comics

```json
{
  "success": true,
  "data": [
    {
      "id": "14f91395-484b-457a-9a3d-af7adb712c2d",
      "price": 2.99,
      "title": "PATH TO DOOM VI",
      "image": "https://res.cloudinary.com/dkzhgauk1/image/upload/v1579376879/Sups06_uwzn82.jpg",
      "description": "In the epic conclusion, the mystery of Black Zero deepens just as the Man of Steel makes a fateful decision that may stop Doomsday, but also risks the lives of those he loves most."
    },
    {
      "id": "22eed8d1-e20a-4fc6-98de-58f43565feb3",
      "price": 1.99,
      "title": "THE LIES #3",
      "image": "https://res.cloudinary.com/dkzhgauk1/image/upload/v1579376882/ww05_rabumy.jpg",
      "description": "Steve Trevor finds himself trapped in the heart of Urzkartaga’s darkness, with Wonder Woman and Cheetah the only hope of rescue for him and his men. But how far can Cheetah be trusted?"
    }
  ]
}
```
### `GET /comics/:id`
✅ 200 - returns one comics' by ID

```json
{
    "success": true,
    "data": {
        "id": "14f91395-484b-457a-9a3d-af7adb712c2d",
        "price": 2.99,
        "title": "PATH TO DOOM VI",
        "image": "https://res.cloudinary.com/dkzhgauk1/image/upload/v1579376879/Sups06_uwzn82.jpg",
        "description": "In the epic conclusion, the mystery of Black Zero deepens just as the Man of Steel makes a fateful decision that may stop Doomsday, but also risks the lives of those he loves most."
    }
}
```

❌ 400 - if GUID is not correct
❌ 404 - if comics not found

### Database
All Applications connected to same Database, which should be run beforehand

```bash
$ cd Database
$ docker-compose up -d
```
The command above will run Database MySQL server on port 8091.

Then you have to create Table in DB and configure user

- Connect to DB as a `root` user with password `root` 127.0.0.1:8091
- Simple create a table
```sql
CREATE TABLE `Comics` (
`Id` CHAR(36) NOT NULL,
`Price` DOUBLE,
`Title` CHAR(255),
`Image` CHAR(255),
`Description` TEXT,
PRIMARY KEY (`Id`)
) ENGINE=InnoDB;
```
- Seed table with data
```bash
$ cd Database
$ npm run seed
```

### Applications
Each App folder contains `docker-compose` files with will run server in `development` mode.

### Architecture
Each application have _similar_ architecture:

- Mappers - functions to transform one data type to another 
- Repositories - database communication layer
- Entities - class represents DB entity
- Endpoints - routes mapping
- Services - business logic

### Test results

#### .NET

```bash
Running 60s test @ http://localhost:8093/comics
10 connections

┌─────────┬──────┬──────┬───────┬───────┬─────────┬─────────┬────────┐
│ Stat    │ 2.5% │ 50%  │ 97.5% │ 99%   │ Avg     │ Stdev   │ Max    │
├─────────┼──────┼──────┼───────┼───────┼─────────┼─────────┼────────┤
│ Latency │ 3 ms │ 5 ms │ 13 ms │ 17 ms │ 6.04 ms │ 3.86 ms │ 191 ms │
└─────────┴──────┴──────┴───────┴───────┴─────────┴─────────┴────────┘
┌───────────┬─────────┬─────────┬─────────┬─────────┬─────────┬─────────┬─────────┐
│ Stat      │ 1%      │ 2.5%    │ 50%     │ 97.5%   │ Avg     │ Stdev   │ Min     │
├───────────┼─────────┼─────────┼─────────┼─────────┼─────────┼─────────┼─────────┤
│ Req/Sec   │ 485     │ 769     │ 1592    │ 1814    │ 1529.77 │ 253.93  │ 485     │
├───────────┼─────────┼─────────┼─────────┼─────────┼─────────┼─────────┼─────────┤
│ Bytes/Sec │ 8.37 MB │ 13.3 MB │ 27.5 MB │ 31.3 MB │ 26.4 MB │ 4.38 MB │ 8.37 MB │
└───────────┴─────────┴─────────┴─────────┴─────────┴─────────┴─────────┴─────────┘

92k requests in 60.03s, 1.58 GB read
```

### Fastify

```bash
Running 60s test @ http://localhost:8094/comics
10 connections

┌─────────┬──────┬───────┬───────┬───────┬──────────┬─────────┬───────┐
│ Stat    │ 2.5% │ 50%   │ 97.5% │ 99%   │ Avg      │ Stdev   │ Max   │
├─────────┼──────┼───────┼───────┼───────┼──────────┼─────────┼───────┤
│ Latency │ 6 ms │ 11 ms │ 21 ms │ 26 ms │ 11.48 ms │ 4.17 ms │ 91 ms │
└─────────┴──────┴───────┴───────┴───────┴──────────┴─────────┴───────┘
┌───────────┬─────────┬─────────┬─────────┬─────────┬─────────┬─────────┬─────────┐
│ Stat      │ 1%      │ 2.5%    │ 50%     │ 97.5%   │ Avg     │ Stdev   │ Min     │
├───────────┼─────────┼─────────┼─────────┼─────────┼─────────┼─────────┼─────────┤
│ Req/Sec   │ 636     │ 697     │ 841     │ 961     │ 834.95  │ 66.23   │ 636     │
├───────────┼─────────┼─────────┼─────────┼─────────┼─────────┼─────────┼─────────┤
│ Bytes/Sec │ 10.8 MB │ 11.9 MB │ 14.3 MB │ 16.4 MB │ 14.2 MB │ 1.13 MB │ 10.8 MB │
└───────────┴─────────┴─────────┴─────────┴─────────┴─────────┴─────────┴─────────┘

50k requests in 60.02s, 853 MB read
```
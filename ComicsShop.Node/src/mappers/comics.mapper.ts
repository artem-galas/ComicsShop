import {Static, Type} from '@sinclair/typebox'
import {apiResponseSchema} from './api.mapper';

export const ComicsByIdRequestParamsSchema = Type.Object({id: Type.String()});

export const ComicsSchema = Type.Object({
    id: Type.String(),
    price: Type.Number(),
    title: Type.Optional(Type.String({maxLength: 255})),
    image: Type.Optional(Type.String({maxLength: 255})),
    description: Type.Optional(Type.String()),
});

export const ComicsReplySchema = apiResponseSchema(ComicsSchema);

export const ComicsListReplySchema = apiResponseSchema(Type.Array(ComicsSchema));

export type ComicsReply = Static<typeof ComicsSchema>;
export type ComicsByIdRequestParams = Static<typeof ComicsByIdRequestParamsSchema>;

export function comicsReplyMapper(comics: Comics): ComicsReply {
    return {
        id: comics.Id,
        description: comics.Description,
        image: comics.Image,
        price: comics.Price,
        title: comics.Title,
    };
}

export function comicsListReplyMapper(comics: Comics[]): ComicsReply[] {
    return comics.map(c => comicsReplyMapper(c));
}

export const ComicsCreateRequestSchema = Type.Object({
    price: Type.Number(),
    title: Type.Optional(Type.String({maxLength: 255})),
    image: Type.Optional(Type.String({maxLength: 255})),
    description: Type.Optional(Type.String()),
})

export type ComicsCreateRequest = Static<typeof ComicsCreateRequestSchema>;

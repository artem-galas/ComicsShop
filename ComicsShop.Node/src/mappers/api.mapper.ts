import {Static, Type} from '@sinclair/typebox';
import {FastifyError} from 'fastify';

type ApiResponse<T = unknown> = {
    data: T,
    success: true
}

type ErrorApiResponse = Static<typeof ErrorReplySchema>

export function apiResponseSchema(schema: any) {
    return Type.Object({
        success: Type.Boolean(),
        data: schema,
    });
}

export const ErrorReplySchema = Type.Object({
    success: Type.Boolean(),
    data: Type.Null(),
    error: Type.String(),
})

export function isError(payload: unknown): payload is FastifyError {
    const errorResponse = payload as FastifyError;
    return errorResponse.message !== undefined;
}

export function successMapper<T = unknown>(payload: T): ApiResponse<T> {
    return {
        success: true,
        data: payload,
    }
}

export function errorMapper(payload: FastifyError): ErrorApiResponse {
    return {
        success: false,
        data: null,
        error: payload.message,
    }
}

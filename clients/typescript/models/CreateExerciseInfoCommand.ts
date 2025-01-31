/* tslint:disable */
/* eslint-disable */
/**
 * Training Plans Api - All Versions
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: all
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */

import { exists, mapValues } from '../runtime';
import type { BodyElements } from './BodyElements';
import {
    BodyElementsFromJSON,
    BodyElementsFromJSONTyped,
    BodyElementsToJSON,
} from './BodyElements';

/**
 * 
 * @export
 * @interface CreateExerciseInfoCommand
 */
export interface CreateExerciseInfoCommand {
    /**
     * 
     * @type {string}
     * @memberof CreateExerciseInfoCommand
     */
    name: string;
    /**
     * 
     * @type {string}
     * @memberof CreateExerciseInfoCommand
     */
    description: string;
    /**
     * 
     * @type {number}
     * @memberof CreateExerciseInfoCommand
     */
    authorId: number;
    /**
     * 
     * @type {Array<BodyElements>}
     * @memberof CreateExerciseInfoCommand
     */
    bodyElements?: Array<BodyElements> | null;
}

/**
 * Check if a given object implements the CreateExerciseInfoCommand interface.
 */
export function instanceOfCreateExerciseInfoCommand(value: object): boolean {
    let isInstance = true;
    isInstance = isInstance && "name" in value;
    isInstance = isInstance && "description" in value;
    isInstance = isInstance && "authorId" in value;

    return isInstance;
}

export function CreateExerciseInfoCommandFromJSON(json: any): CreateExerciseInfoCommand {
    return CreateExerciseInfoCommandFromJSONTyped(json, false);
}

export function CreateExerciseInfoCommandFromJSONTyped(json: any, ignoreDiscriminator: boolean): CreateExerciseInfoCommand {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'name': json['name'],
        'description': json['description'],
        'authorId': json['authorId'],
        'bodyElements': !exists(json, 'bodyElements') ? undefined : (json['bodyElements'] === null ? null : (json['bodyElements'] as Array<any>).map(BodyElementsFromJSON)),
    };
}

export function CreateExerciseInfoCommandToJSON(value?: CreateExerciseInfoCommand | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'name': value.name,
        'description': value.description,
        'authorId': value.authorId,
        'bodyElements': value.bodyElements === undefined ? undefined : (value.bodyElements === null ? null : (value.bodyElements as Array<any>).map(BodyElementsToJSON)),
    };
}


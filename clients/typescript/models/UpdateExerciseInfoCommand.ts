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
 * @interface UpdateExerciseInfoCommand
 */
export interface UpdateExerciseInfoCommand {
    /**
     * 
     * @type {number}
     * @memberof UpdateExerciseInfoCommand
     */
    updatedId: number;
    /**
     * 
     * @type {string}
     * @memberof UpdateExerciseInfoCommand
     */
    name: string;
    /**
     * 
     * @type {string}
     * @memberof UpdateExerciseInfoCommand
     */
    description: string;
    /**
     * 
     * @type {Array<BodyElements>}
     * @memberof UpdateExerciseInfoCommand
     */
    bodyElements?: Array<BodyElements> | null;
}

/**
 * Check if a given object implements the UpdateExerciseInfoCommand interface.
 */
export function instanceOfUpdateExerciseInfoCommand(value: object): boolean {
    let isInstance = true;
    isInstance = isInstance && "updatedId" in value;
    isInstance = isInstance && "name" in value;
    isInstance = isInstance && "description" in value;

    return isInstance;
}

export function UpdateExerciseInfoCommandFromJSON(json: any): UpdateExerciseInfoCommand {
    return UpdateExerciseInfoCommandFromJSONTyped(json, false);
}

export function UpdateExerciseInfoCommandFromJSONTyped(json: any, ignoreDiscriminator: boolean): UpdateExerciseInfoCommand {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'updatedId': json['updatedId'],
        'name': json['name'],
        'description': json['description'],
        'bodyElements': !exists(json, 'bodyElements') ? undefined : (json['bodyElements'] === null ? null : (json['bodyElements'] as Array<any>).map(BodyElementsFromJSON)),
    };
}

export function UpdateExerciseInfoCommandToJSON(value?: UpdateExerciseInfoCommand | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'updatedId': value.updatedId,
        'name': value.name,
        'description': value.description,
        'bodyElements': value.bodyElements === undefined ? undefined : (value.bodyElements === null ? null : (value.bodyElements as Array<any>).map(BodyElementsToJSON)),
    };
}


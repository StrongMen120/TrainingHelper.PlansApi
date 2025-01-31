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
import type { FileDetailsDto } from './FileDetailsDto';
import {
    FileDetailsDtoFromJSON,
    FileDetailsDtoFromJSONTyped,
    FileDetailsDtoToJSON,
} from './FileDetailsDto';
import type { UserDetailsDto } from './UserDetailsDto';
import {
    UserDetailsDtoFromJSON,
    UserDetailsDtoFromJSONTyped,
    UserDetailsDtoToJSON,
} from './UserDetailsDto';

/**
 * 
 * @export
 * @interface ExercisesInfoDetailsDto
 */
export interface ExercisesInfoDetailsDto {
    /**
     * 
     * @type {number}
     * @memberof ExercisesInfoDetailsDto
     */
    identifier: number;
    /**
     * 
     * @type {string}
     * @memberof ExercisesInfoDetailsDto
     */
    name: string;
    /**
     * 
     * @type {string}
     * @memberof ExercisesInfoDetailsDto
     */
    description: string;
    /**
     * 
     * @type {number}
     * @memberof ExercisesInfoDetailsDto
     */
    authorId: number;
    /**
     * 
     * @type {string}
     * @memberof ExercisesInfoDetailsDto
     */
    createdAt: string;
    /**
     * 
     * @type {UserDetailsDto}
     * @memberof ExercisesInfoDetailsDto
     */
    createdBy: UserDetailsDto;
    /**
     * 
     * @type {Array<BodyElements>}
     * @memberof ExercisesInfoDetailsDto
     */
    bodyElements?: Array<BodyElements> | null;
    /**
     * 
     * @type {Array<FileDetailsDto>}
     * @memberof ExercisesInfoDetailsDto
     */
    files?: Array<FileDetailsDto> | null;
    /**
     * 
     * @type {string}
     * @memberof ExercisesInfoDetailsDto
     */
    modifiedAt?: string | null;
    /**
     * 
     * @type {UserDetailsDto}
     * @memberof ExercisesInfoDetailsDto
     */
    modifiedBy?: UserDetailsDto;
}

/**
 * Check if a given object implements the ExercisesInfoDetailsDto interface.
 */
export function instanceOfExercisesInfoDetailsDto(value: object): boolean {
    let isInstance = true;
    isInstance = isInstance && "identifier" in value;
    isInstance = isInstance && "name" in value;
    isInstance = isInstance && "description" in value;
    isInstance = isInstance && "authorId" in value;
    isInstance = isInstance && "createdAt" in value;
    isInstance = isInstance && "createdBy" in value;

    return isInstance;
}

export function ExercisesInfoDetailsDtoFromJSON(json: any): ExercisesInfoDetailsDto {
    return ExercisesInfoDetailsDtoFromJSONTyped(json, false);
}

export function ExercisesInfoDetailsDtoFromJSONTyped(json: any, ignoreDiscriminator: boolean): ExercisesInfoDetailsDto {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'identifier': json['identifier'],
        'name': json['name'],
        'description': json['description'],
        'authorId': json['authorId'],
        'createdAt': json['createdAt'],
        'createdBy': UserDetailsDtoFromJSON(json['createdBy']),
        'bodyElements': !exists(json, 'bodyElements') ? undefined : (json['bodyElements'] === null ? null : (json['bodyElements'] as Array<any>).map(BodyElementsFromJSON)),
        'files': !exists(json, 'files') ? undefined : (json['files'] === null ? null : (json['files'] as Array<any>).map(FileDetailsDtoFromJSON)),
        'modifiedAt': !exists(json, 'modifiedAt') ? undefined : json['modifiedAt'],
        'modifiedBy': !exists(json, 'modifiedBy') ? undefined : UserDetailsDtoFromJSON(json['modifiedBy']),
    };
}

export function ExercisesInfoDetailsDtoToJSON(value?: ExercisesInfoDetailsDto | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'identifier': value.identifier,
        'name': value.name,
        'description': value.description,
        'authorId': value.authorId,
        'createdAt': value.createdAt,
        'createdBy': UserDetailsDtoToJSON(value.createdBy),
        'bodyElements': value.bodyElements === undefined ? undefined : (value.bodyElements === null ? null : (value.bodyElements as Array<any>).map(BodyElementsToJSON)),
        'files': value.files === undefined ? undefined : (value.files === null ? null : (value.files as Array<any>).map(FileDetailsDtoToJSON)),
        'modifiedAt': value.modifiedAt,
        'modifiedBy': UserDetailsDtoToJSON(value.modifiedBy),
    };
}


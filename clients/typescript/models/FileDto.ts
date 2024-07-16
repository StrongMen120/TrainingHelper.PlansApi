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
/**
 * 
 * @export
 * @interface FileDto
 */
export interface FileDto {
    /**
     * 
     * @type {string}
     * @memberof FileDto
     */
    identifier: string;
    /**
     * 
     * @type {string}
     * @memberof FileDto
     */
    photoId: string;
}

/**
 * Check if a given object implements the FileDto interface.
 */
export function instanceOfFileDto(value: object): boolean {
    let isInstance = true;
    isInstance = isInstance && "identifier" in value;
    isInstance = isInstance && "photoId" in value;

    return isInstance;
}

export function FileDtoFromJSON(json: any): FileDto {
    return FileDtoFromJSONTyped(json, false);
}

export function FileDtoFromJSONTyped(json: any, ignoreDiscriminator: boolean): FileDto {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'identifier': json['identifier'],
        'photoId': json['photoId'],
    };
}

export function FileDtoToJSON(value?: FileDto | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'identifier': value.identifier,
        'photoId': value.photoId,
    };
}


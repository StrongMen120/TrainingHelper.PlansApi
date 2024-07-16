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
import type { ExercisesInfoDto } from './ExercisesInfoDto';
import {
    ExercisesInfoDtoFromJSON,
    ExercisesInfoDtoFromJSONTyped,
    ExercisesInfoDtoToJSON,
} from './ExercisesInfoDto';
import type { UserDetailsDto } from './UserDetailsDto';
import {
    UserDetailsDtoFromJSON,
    UserDetailsDtoFromJSONTyped,
    UserDetailsDtoToJSON,
} from './UserDetailsDto';

/**
 * 
 * @export
 * @interface PlannedExercisesDto
 */
export interface PlannedExercisesDto {
    /**
     * 
     * @type {string}
     * @memberof PlannedExercisesDto
     */
    identifier: string;
    /**
     * 
     * @type {number}
     * @memberof PlannedExercisesDto
     */
    plansId: number;
    /**
     * 
     * @type {number}
     * @memberof PlannedExercisesDto
     */
    exerciseInfoId: number;
    /**
     * 
     * @type {number}
     * @memberof PlannedExercisesDto
     */
    series: number;
    /**
     * 
     * @type {Array<number>}
     * @memberof PlannedExercisesDto
     */
    reps: Array<number>;
    /**
     * 
     * @type {Array<number>}
     * @memberof PlannedExercisesDto
     */
    weight: Array<number>;
    /**
     * 
     * @type {number}
     * @memberof PlannedExercisesDto
     */
    rate: number;
    /**
     * 
     * @type {number}
     * @memberof PlannedExercisesDto
     */
    rpe: number;
    /**
     * 
     * @type {number}
     * @memberof PlannedExercisesDto
     */
    brakeSeconds: number;
    /**
     * 
     * @type {ExercisesInfoDto}
     * @memberof PlannedExercisesDto
     */
    exerciseInfo: ExercisesInfoDto;
    /**
     * 
     * @type {string}
     * @memberof PlannedExercisesDto
     */
    createdAt: string;
    /**
     * 
     * @type {UserDetailsDto}
     * @memberof PlannedExercisesDto
     */
    createdBy: UserDetailsDto;
    /**
     * 
     * @type {string}
     * @memberof PlannedExercisesDto
     */
    modifiedAt?: string | null;
    /**
     * 
     * @type {UserDetailsDto}
     * @memberof PlannedExercisesDto
     */
    modifiedBy?: UserDetailsDto;
}

/**
 * Check if a given object implements the PlannedExercisesDto interface.
 */
export function instanceOfPlannedExercisesDto(value: object): boolean {
    let isInstance = true;
    isInstance = isInstance && "identifier" in value;
    isInstance = isInstance && "plansId" in value;
    isInstance = isInstance && "exerciseInfoId" in value;
    isInstance = isInstance && "series" in value;
    isInstance = isInstance && "reps" in value;
    isInstance = isInstance && "weight" in value;
    isInstance = isInstance && "rate" in value;
    isInstance = isInstance && "rpe" in value;
    isInstance = isInstance && "brakeSeconds" in value;
    isInstance = isInstance && "exerciseInfo" in value;
    isInstance = isInstance && "createdAt" in value;
    isInstance = isInstance && "createdBy" in value;

    return isInstance;
}

export function PlannedExercisesDtoFromJSON(json: any): PlannedExercisesDto {
    return PlannedExercisesDtoFromJSONTyped(json, false);
}

export function PlannedExercisesDtoFromJSONTyped(json: any, ignoreDiscriminator: boolean): PlannedExercisesDto {
    if ((json === undefined) || (json === null)) {
        return json;
    }
    return {
        
        'identifier': json['identifier'],
        'plansId': json['plansId'],
        'exerciseInfoId': json['exerciseInfoId'],
        'series': json['series'],
        'reps': json['reps'],
        'weight': json['weight'],
        'rate': json['rate'],
        'rpe': json['rpe'],
        'brakeSeconds': json['brakeSeconds'],
        'exerciseInfo': ExercisesInfoDtoFromJSON(json['exerciseInfo']),
        'createdAt': json['createdAt'],
        'createdBy': UserDetailsDtoFromJSON(json['createdBy']),
        'modifiedAt': !exists(json, 'modifiedAt') ? undefined : json['modifiedAt'],
        'modifiedBy': !exists(json, 'modifiedBy') ? undefined : UserDetailsDtoFromJSON(json['modifiedBy']),
    };
}

export function PlannedExercisesDtoToJSON(value?: PlannedExercisesDto | null): any {
    if (value === undefined) {
        return undefined;
    }
    if (value === null) {
        return null;
    }
    return {
        
        'identifier': value.identifier,
        'plansId': value.plansId,
        'exerciseInfoId': value.exerciseInfoId,
        'series': value.series,
        'reps': value.reps,
        'weight': value.weight,
        'rate': value.rate,
        'rpe': value.rpe,
        'brakeSeconds': value.brakeSeconds,
        'exerciseInfo': ExercisesInfoDtoToJSON(value.exerciseInfo),
        'createdAt': value.createdAt,
        'createdBy': UserDetailsDtoToJSON(value.createdBy),
        'modifiedAt': value.modifiedAt,
        'modifiedBy': UserDetailsDtoToJSON(value.modifiedBy),
    };
}


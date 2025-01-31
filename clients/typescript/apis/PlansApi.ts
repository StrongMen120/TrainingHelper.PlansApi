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


import * as runtime from '../runtime';
import type {
  CreatePlansCommand,
  PlansDto,
  ProblemDetails,
  UpdatePlansCommand,
} from '../models';
import {
    CreatePlansCommandFromJSON,
    CreatePlansCommandToJSON,
    PlansDtoFromJSON,
    PlansDtoToJSON,
    ProblemDetailsFromJSON,
    ProblemDetailsToJSON,
    UpdatePlansCommandFromJSON,
    UpdatePlansCommandToJSON,
} from '../models';

export interface CreatePlansRequest {
    createPlansCommand?: CreatePlansCommand;
}

export interface DeletePlansRequest {
    identifier: number;
}

export interface GetAllUserPlansRequest {
    userId: number;
}

export interface GetPlanRequest {
    identifier: number;
}

export interface UpdatePlansRequest {
    identifier: number;
    updatePlansCommand?: UpdatePlansCommand;
}

/**
 * PlansApi - interface
 * 
 * @export
 * @interface PlansApiInterface
 */
export interface PlansApiInterface {
    /**
     * 
     * @summary Create plans
     * @param {CreatePlansCommand} [createPlansCommand] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof PlansApiInterface
     */
    createPlansRaw(requestParameters: CreatePlansRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<PlansDto>>;

    /**
     * Create plans
     */
    createPlans(createPlansCommand?: CreatePlansCommand, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<PlansDto>;

    /**
     * 
     * @summary Delete plans
     * @param {number} identifier 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof PlansApiInterface
     */
    deletePlansRaw(requestParameters: DeletePlansRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<PlansDto>>;

    /**
     * Delete plans
     */
    deletePlans(identifier: number, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<PlansDto>;

    /**
     * 
     * @summary Gets all user plans
     * @param {number} userId 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof PlansApiInterface
     */
    getAllUserPlansRaw(requestParameters: GetAllUserPlansRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Array<PlansDto>>>;

    /**
     * Gets all user plans
     */
    getAllUserPlans(userId: number, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Array<PlansDto>>;

    /**
     * 
     * @summary Gets plans
     * @param {number} identifier 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof PlansApiInterface
     */
    getPlanRaw(requestParameters: GetPlanRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<PlansDto>>;

    /**
     * Gets plans
     */
    getPlan(identifier: number, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<PlansDto>;

    /**
     * 
     * @summary Update plans
     * @param {number} identifier 
     * @param {UpdatePlansCommand} [updatePlansCommand] 
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof PlansApiInterface
     */
    updatePlansRaw(requestParameters: UpdatePlansRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<PlansDto>>;

    /**
     * Update plans
     */
    updatePlans(identifier: number, updatePlansCommand?: UpdatePlansCommand, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<PlansDto>;

}

/**
 * 
 */
export class PlansApi extends runtime.BaseAPI implements PlansApiInterface {

    /**
     * Create plans
     */
    async createPlansRaw(requestParameters: CreatePlansRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<PlansDto>> {
        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/plans`,
            method: 'POST',
            headers: headerParameters,
            query: queryParameters,
            body: CreatePlansCommandToJSON(requestParameters.createPlansCommand),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => PlansDtoFromJSON(jsonValue));
    }

    /**
     * Create plans
     */
    async createPlans(createPlansCommand?: CreatePlansCommand, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<PlansDto> {
        const response = await this.createPlansRaw({ createPlansCommand: createPlansCommand }, initOverrides);
        return await response.value();
    }

    /**
     * Delete plans
     */
    async deletePlansRaw(requestParameters: DeletePlansRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<PlansDto>> {
        if (requestParameters.identifier === null || requestParameters.identifier === undefined) {
            throw new runtime.RequiredError('identifier','Required parameter requestParameters.identifier was null or undefined when calling deletePlans.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/plans/{identifier}`.replace(`{${"identifier"}}`, encodeURIComponent(String(requestParameters.identifier))),
            method: 'DELETE',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => PlansDtoFromJSON(jsonValue));
    }

    /**
     * Delete plans
     */
    async deletePlans(identifier: number, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<PlansDto> {
        const response = await this.deletePlansRaw({ identifier: identifier }, initOverrides);
        return await response.value();
    }

    /**
     * Gets all user plans
     */
    async getAllUserPlansRaw(requestParameters: GetAllUserPlansRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<Array<PlansDto>>> {
        if (requestParameters.userId === null || requestParameters.userId === undefined) {
            throw new runtime.RequiredError('userId','Required parameter requestParameters.userId was null or undefined when calling getAllUserPlans.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/plans/{userId}`.replace(`{${"userId"}}`, encodeURIComponent(String(requestParameters.userId))),
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => jsonValue.map(PlansDtoFromJSON));
    }

    /**
     * Gets all user plans
     */
    async getAllUserPlans(userId: number, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<Array<PlansDto>> {
        const response = await this.getAllUserPlansRaw({ userId: userId }, initOverrides);
        return await response.value();
    }

    /**
     * Gets plans
     */
    async getPlanRaw(requestParameters: GetPlanRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<PlansDto>> {
        if (requestParameters.identifier === null || requestParameters.identifier === undefined) {
            throw new runtime.RequiredError('identifier','Required parameter requestParameters.identifier was null or undefined when calling getPlan.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/plan/{identifier}`.replace(`{${"identifier"}}`, encodeURIComponent(String(requestParameters.identifier))),
            method: 'GET',
            headers: headerParameters,
            query: queryParameters,
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => PlansDtoFromJSON(jsonValue));
    }

    /**
     * Gets plans
     */
    async getPlan(identifier: number, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<PlansDto> {
        const response = await this.getPlanRaw({ identifier: identifier }, initOverrides);
        return await response.value();
    }

    /**
     * Update plans
     */
    async updatePlansRaw(requestParameters: UpdatePlansRequest, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<runtime.ApiResponse<PlansDto>> {
        if (requestParameters.identifier === null || requestParameters.identifier === undefined) {
            throw new runtime.RequiredError('identifier','Required parameter requestParameters.identifier was null or undefined when calling updatePlans.');
        }

        const queryParameters: any = {};

        const headerParameters: runtime.HTTPHeaders = {};

        headerParameters['Content-Type'] = 'application/json';

        if (this.configuration && this.configuration.accessToken) {
            const token = this.configuration.accessToken;
            const tokenString = await token("BearerJWT", []);

            if (tokenString) {
                headerParameters["Authorization"] = `Bearer ${tokenString}`;
            }
        }
        const response = await this.request({
            path: `/api/v1/plans/{identifier}`.replace(`{${"identifier"}}`, encodeURIComponent(String(requestParameters.identifier))),
            method: 'PUT',
            headers: headerParameters,
            query: queryParameters,
            body: UpdatePlansCommandToJSON(requestParameters.updatePlansCommand),
        }, initOverrides);

        return new runtime.JSONApiResponse(response, (jsonValue) => PlansDtoFromJSON(jsonValue));
    }

    /**
     * Update plans
     */
    async updatePlans(identifier: number, updatePlansCommand?: UpdatePlansCommand, initOverrides?: RequestInit | runtime.InitOverrideFunction): Promise<PlansDto> {
        const response = await this.updatePlansRaw({ identifier: identifier, updatePlansCommand: updatePlansCommand }, initOverrides);
        return await response.value();
    }

}

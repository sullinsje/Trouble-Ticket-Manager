'use strict';

export class userAJAXRepository {
    constructor(baseUrl) {
        this.baseUrl = baseUrl;
    }

    async getUsers() {
        const response = await fetch(`${this.baseUrl}/all`);
        if (!response.ok) {
            throw new Error('Failed to fetch users');
        }
        return await response.json();
    }
}
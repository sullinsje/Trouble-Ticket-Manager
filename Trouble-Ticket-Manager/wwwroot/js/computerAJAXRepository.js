'use strict';

export class computerAJAXRepository {
    constructor(baseUrl) {
        this.baseUrl = baseUrl;
    }

    async getComputers() {
        const response = await fetch(`${this.baseUrl}/all`);
        if (!response.ok) {
            throw new Error('Failed to fetch computers');
        }
        return await response.json();
    }
}
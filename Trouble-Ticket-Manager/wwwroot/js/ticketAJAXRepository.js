'use strict';

export class ticketAJAXRepository {
    constructor(baseUrl) {
        this.baseUrl = baseUrl;
    }

    async getTickets() {
        const response = await fetch(`${this.baseUrl}/all`);
        if (!response.ok) {
            throw new Error('Failed to fetch tickets');
        }
        return await response.json();
    }
}
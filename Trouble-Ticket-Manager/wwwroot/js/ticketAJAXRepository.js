'use strict';

export class ticketAJAXRepository {
    constructor(baseUrl) {
        this.baseUrl = baseUrl;
    }

    async getAllTickets() {
        const response = await fetch(`${this.baseUrl}/all`);
        if (!response.ok) {
            throw new Error('Failed to fetch tickets');
        }
        return await response.json();
    }

    async getTicket(id) {
        const response = await fetch(`${this.baseUrl}/one/${id}`);
        if (!response.ok) {
            throw new Error('Failed to fetch tickets');
        }
        return await response.json();
    }

    async createTicket(formData) {
        const url = `${this.baseUrl}/Create`;

        try {
            const response = await fetch(url, {
                method: 'POST',
                body: formData
            });

            if (response.ok) {
                return true;
            } else {
                throw new Error(`Failed to create ticket.`);
            }
        } catch (error) {
            if (error instanceof TypeError && error.message.includes('fetch')) {
                throw new Error('A network error occurred while communicating with the server.');
            }
            throw error;
        }
    }

    async editTicket(formData, ticketId) {
        const url = `${this.baseUrl}/Edit/${ticketId}`;

        try {
            const response = await fetch(url, {
                method: 'PUT', 
                body: formData
            });

            if (response.ok) {
                return true;
            } else {
                const errorText = await response.text();
                console.error('Server error updating ticket:', errorText);
                throw new Error(`Failed to save changes. Status: ${response.status}. ${errorText}`);
            }
        } catch (error) {
            if (error instanceof TypeError && error.message.includes('fetch')) {
                throw new Error('A network error occurred while communicating with the server.');
            }
            throw error;
        }
    }
}
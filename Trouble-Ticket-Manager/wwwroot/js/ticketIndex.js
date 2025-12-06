import { ticketAJAXRepository } from './ticketAJAXRepository.js';

'use strict';

document.addEventListener('DOMContentLoaded', async () => {
    const repository = new ticketAJAXRepository('/api/ticketapi');
    try {
        const tickets = await repository.getTickets();
        const tableBody = document.getElementById('ticket-table-body');

        tickets.forEach(ticket => {
            const row = document.createElement('tr');

            const idCell = document.createElement('td');
            idCell.textContent = ticket.id;
            row.appendChild(idCell);

            const contactNameCell = document.createElement('td');
            contactNameCell.textContent = ticket.contactName;
            row.appendChild(contactNameCell);

            const submittedAtCell = document.createElement('td');
            submittedAtCell.textContent = ticket.submittedAt;
            row.appendChild(submittedAtCell);

            const isResolvedCell = document.createElement('td');
            isResolvedCell.textContent = ticket.isResolved ? 'Yes' : 'No';
            row.appendChild(isResolvedCell);

            const chargerGivenCell = document.createElement('td');
            chargerGivenCell.textContent = ticket.chargerGiven ? 'Yes' : 'No';
            row.appendChild(chargerGivenCell);

            const actionCell = document.createElement('td');

            const detailsLink = document.createElement('a');
            detailsLink.setAttribute('href', `/ticket/Details/${ticket.id}`);
            detailsLink.classList.add('btn', 'btn-info', 'btn-sm', 'me-1');
            detailsLink.textContent = 'Details';

            const editLink = document.createElement('a');
            editLink.setAttribute('href', `/ticket/Edit/${ticket.id}`);
            editLink.classList.add('btn', 'btn-warning', 'btn-sm', 'me-1');
            editLink.textContent = 'Edit';

            const deleteLink = document.createElement('a');
            deleteLink.setAttribute('href', `/ticket/Delete/${ticket.id}`);
            deleteLink.classList.add('btn', 'btn-danger', 'btn-sm', 'me-1');
            deleteLink.textContent = 'Delete';

            actionCell.appendChild(detailsLink);
            actionCell.appendChild(editLink);
            actionCell.appendChild(deleteLink);
            row.appendChild(actionCell);

            tableBody.appendChild(row);
        });
    } catch (error) {
        console.error('Error loading tickets:', error);
    }
});
import { ticketAJAXRepository } from './ticketAJAXRepository.js';

'use strict';

document.addEventListener('DOMContentLoaded', async () => {
    const repository = new ticketAJAXRepository('/api/ticketapi');
    const tableBody = document.getElementById('ticket-table-body');

    if (!tableBody) {
        console.error("The element with ID 'ticket-table-body' was not found.");
        return;
    }

    let tickets;
    try {
        tickets = await repository.getAllTickets();

        tickets.forEach(ticket => {
            const row = document.createElement('tr');

            const idCell = document.createElement('td');
            idCell.textContent = ticket.id;
            row.appendChild(idCell);

            const contactNameCell = document.createElement('td');
            contactNameCell.textContent = ticket.contact.name;
            row.appendChild(contactNameCell);

            const assetTags = ticket.ticketComputers
                .map(t => t.computerAssetTag)
                .join(', ');

            const assetTagCell = document.createElement('td');
            assetTagCell.textContent = assetTags;
            row.appendChild(assetTagCell);

            const submittedAtCell = document.createElement('td');
            submittedAtCell.textContent = ticket.submittedAt;
            row.appendChild(submittedAtCell);

            const isResolvedCell = document.createElement('td');
            isResolvedCell.textContent = ticket.isResolved ? 'Yes' : 'No';
            row.appendChild(isResolvedCell);

            const actionCell = document.createElement('td');

            const detailsLink = document.createElement('a');
            detailsLink.setAttribute('href', '#');
            detailsLink.setAttribute('data-id', ticket.id);
            detailsLink.classList.add('btn', 'btn-info', 'btn-sm', 'me-1', 'details-modal-trigger');
            detailsLink.textContent = 'Details';

            const editLink = document.createElement('a');
            editLink.setAttribute('href', `/ticket/Edit/${ticket.id}`);
            editLink.classList.add('btn', 'btn-warning', 'btn-sm', 'me-1');
            editLink.textContent = 'Edit';

            const deleteLink = document.createElement('a');
            deleteLink.setAttribute('href', `/ticket/Close/${ticket.id}`);
            deleteLink.classList.add('btn', 'btn-danger', 'btn-sm', 'me-1');
            deleteLink.textContent = 'Close';

            actionCell.appendChild(detailsLink);
            actionCell.appendChild(editLink);
            actionCell.appendChild(deleteLink);
            row.appendChild(actionCell);

            tableBody.appendChild(row);
        });

    } catch (error) {
        console.error('Error loading tickets:', error);
        return;
    }
    
    const modalElement = document.getElementById('ticketDetailsModal');
    if (!modalElement || typeof bootstrap === 'undefined' || !bootstrap.Modal) {
        console.error("Bootstrap Modal element or JS not found.");
        return;
    }
    const modal = new bootstrap.Modal(modalElement);
    const modalContentContainer = document.getElementById('modal-content-container');
    

    tableBody.addEventListener('click', function (e) {
        const targetLink = e.target.closest('.details-modal-trigger');

        if (targetLink) {
            e.preventDefault(); 

            const ticketId = targetLink.getAttribute('data-id');
            const url = `/Ticket/DetailsPartial/${ticketId}`;

            fetch(url)
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`Failed to load ticket details. Status: ${response.status}`);
                    }
                    return response.text();
                })
                .then(html => {
                    modalContentContainer.innerHTML = html;
                    modal.show();
                })
                .catch(error => {
                    console.error('AJAX Error loading ticket details:', error);
                    alert('Could not load ticket details. Please check server logs.');
                });
        }
    });
});
import { computerAJAXRepository } from './computerAJAXRepository.js';

'use strict';

document.addEventListener('DOMContentLoaded', async () => {
    const repository = new computerAJAXRepository('/api/computerapi');
    try {
        const computers = await repository.getComputers();
        const tableBody = document.getElementById('computer-table-body');

        computers.forEach(computer => {
            const row = document.createElement('tr');

            const assetTagCell = document.createElement('td');
            assetTagCell.textContent = computer.assetTag;
            row.appendChild(assetTagCell);

            const serviceTagCell = document.createElement('td');
            serviceTagCell.textContent = computer.serviceTag;
            row.appendChild(serviceTagCell);

            const modelCell = document.createElement('td');
            modelCell.textContent = computer.model;
            row.appendChild(modelCell);

            const underWarrantyCell = document.createElement('td');
            underWarrantyCell.textContent = computer.underWarranty ? 'Yes' : 'No';
            row.appendChild(underWarrantyCell);

            const userIdCell = document.createElement('td');
            userIdCell.textContent = computer.userId;
            row.appendChild(userIdCell);

            const actionCell = document.createElement('td');

            const detailsLink = document.createElement('a');
            detailsLink.setAttribute('href', `/Computer/Details/${computer.assetTag}`);
            detailsLink.classList.add('btn', 'btn-info', 'btn-sm', 'me-1');
            detailsLink.textContent = 'Details';

            const editLink = document.createElement('a');
            editLink.setAttribute('href', `/Computer/Edit/${computer.assetTag}`);
            editLink.classList.add('btn', 'btn-warning', 'btn-sm', 'me-1');
            editLink.textContent = 'Edit';

            const deleteLink = document.createElement('a');
            deleteLink.setAttribute('href', `/Computer/Delete/${computer.assetTag}`);
            deleteLink.classList.add('btn', 'btn-danger', 'btn-sm', 'me-1');
            deleteLink.textContent = 'Delete';

            actionCell.appendChild(detailsLink);
            actionCell.appendChild(editLink);
            actionCell.appendChild(deleteLink);
            row.appendChild(actionCell);

            tableBody.appendChild(row);
        });
    } catch (error) {
        console.error('Error loading computers:', error);
    }
});
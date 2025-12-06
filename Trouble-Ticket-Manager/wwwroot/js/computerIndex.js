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

            tableBody.appendChild(row);
        });
    } catch (error) {
        console.error('Error loading computers:', error);
    }
});
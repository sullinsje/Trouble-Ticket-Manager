import { ticketAJAXRepository } from './ticketAJAXRepository.js';

'use strict';

const repository = new ticketAJAXRepository('/api/TicketAPI');

document.addEventListener('DOMContentLoaded', function () {
    const contactDropdown = document.getElementById('contact-name-dropdown');

    const assetTagCheckboxContainer = document.getElementById('asset-tag-checkbox-list');

    const currentSelectedTags = Array.from(document.querySelectorAll('input[name="SelectedAssetTags"]'))
        .map(input => input.value);

    const createNewUserCheckbox = document.getElementById('create-new-user-checkbox');
    const newContactNameGroup = document.getElementById('new-contact-name-group');
    const existingContactGroup = document.getElementById('existing-contact-group');
    const newContactNameInput = document.getElementById('NewContactName');
    const newContactEmailInput = document.getElementById('NewContactEmail');
    const newContactBuildingInput = document.getElementById('NewContactBuilding');
    const newContactRoomInput = document.getElementById('NewContactRoom');

    const createNewComputerCheckbox = document.getElementById('create-new-computer-checkbox');
    const newComputerDetailsGroup = document.getElementById('new-computer-details-group');
    const existingComputerGroup = document.getElementById('existing-computer-group');
    const newAssetTagInput = document.getElementById('NewAssetTag');
    const computerServiceTagInput = document.getElementById('NewComputerServiceTag');
    const computerModelInput = document.getElementById('NewComputerModel');

    function populateContactDropdown() {
        fetch('/api/contactapi/all')
            .then(response => response.json())
            .then(contacts => {
                const selectedContactId = contactDropdown.value;

                contactDropdown.innerHTML = '<option value="">Select a Contact</option>';

                contacts.forEach(contact => {
                    const option = document.createElement('option');
                    option.value = contact.id;
                    option.textContent = contact.name;
                    contactDropdown.appendChild(option);
                });

                if (selectedContactId) {
                    contactDropdown.value = selectedContactId;
                }
            })
            .catch(error => {
                console.error('Error fetching contacts:', error);
                contactDropdown.innerHTML = '<option value="">Error loading contacts</option>';
            });
    }

    function populateAssetTagCheckboxes() {
        fetch('/api/computerAPI/all')
            .then(response => response.json())
            .then(computers => {
                assetTagCheckboxContainer.innerHTML = '';

                if (computers.length === 0) {
                    assetTagCheckboxContainer.innerHTML = '<p class="text-muted mb-0">No existing computers found.</p>';
                    return;
                }

                computers.forEach(computer => {
                    const div = document.createElement('div');
                    div.className = 'form-check';

                    const checkbox = document.createElement('input');
                    checkbox.type = 'checkbox';
                    checkbox.className = 'form-check-input';
                    checkbox.id = 'asset-' + computer.assetTag;

                    checkbox.name = 'SelectedAssetTags';
                    checkbox.value = computer.assetTag;

                    if (currentSelectedTags.includes(computer.assetTag)) {
                        checkbox.checked = true;
                    }

                    const label = document.createElement('label');
                    label.className = 'form-check-label';
                    label.htmlFor = checkbox.id;
                    label.textContent = `${computer.assetTag} (${computer.model})`;

                    div.appendChild(checkbox);
                    div.appendChild(label);
                    assetTagCheckboxContainer.appendChild(div);
                });
            })
            .catch(error => {
                console.error('Error fetching computers:', error);
                assetTagCheckboxContainer.innerHTML = '<p class="text-danger mb-0">Error loading computers.</p>';
            });
    }

    createNewComputerCheckbox.addEventListener('change', function () {
        if (this.checked) {
            newComputerDetailsGroup.style.display = 'block';
            newAssetTagInput.setAttribute('required', 'required');
        } else {
            newComputerDetailsGroup.style.display = 'none';

            newAssetTagInput.value = '';
            computerServiceTagInput.value = '';
            computerModelInput.value = '';

            newAssetTagInput.removeAttribute('required');
        }
    });

    function editFormSubmission() {
        document.getElementById('editForm').addEventListener('submit', async function (e) {
            e.preventDefault();

            const form = e.target;
            const formData = new FormData(form);

            formData.delete('SelectedAssetTags');

            assetTagCheckboxContainer.querySelectorAll('input[name="SelectedAssetTags"]:checked').forEach(cb => {
                formData.append('SelectedAssetTags', cb.value);
            });

            const ticketId = formData.get('Id');

            if (!ticketId) {
                alert('Error: Ticket ID is missing from the form.');
                return;
            }

            try {
                const success = await repository.editTicket(formData, ticketId);

                if (success) {
                    window.location.href = '/Ticket/Index';
                }
            } catch (error) {
                console.error('Ticket update failed:', error);
                alert(error.message);
            }
        });
    }

    populateContactDropdown();
    populateAssetTagCheckboxes();
    editFormSubmission();

    createNewUserCheckbox.addEventListener('change', function () {
        if (this.checked) {
            existingContactGroup.style.display = 'none';
            contactDropdown.value = '';
            contactDropdown.removeAttribute('required');

            newContactNameGroup.style.display = 'block';
            newContactNameInput.setAttribute('required', 'required');
        } else {
            existingContactGroup.style.display = 'block';
            contactDropdown.setAttribute('required', 'required');

            newContactNameGroup.style.display = 'none';

            newContactNameInput.value = '';
            newContactEmailInput.value = '';
            newContactBuildingInput.value = '';
            newContactRoomInput.value = '';

            newContactNameInput.removeAttribute('required');
        }
    });
});
document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('closeTicketForm');
    
    if (!form) return; 

    const ticketId = form.dataset.ticketId;
    const updateUrl = `/api/TicketAPI/Close/${ticketId}`;

    form.addEventListener('submit', async function (e) {
        e.preventDefault();
        
        try {
            const response = await fetch(updateUrl, {
                method: 'POST',
            });

            if (response.ok) {
                window.location.href = '/Ticket/Index'; 

            } else {
                const errorData = await response.json();
                alert(`Failed to close ticket: ${errorData.message || response.statusText}`);
            }
        } catch (error) {
            console.error('Network Error:', error);
            alert('An error occurred while communicating with the server.');
        }
    });
});
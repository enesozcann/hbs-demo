$(document).ready(function () {

	// Get appointment details.

	$(document).on('click', '.btn-detail', function () {
		var selectedAppointmentId = $(this).attr('data-id');
		var url = '/Doctor/Appointment/Detail/' + selectedAppointmentId;
		window.location.href = url;
	});

});
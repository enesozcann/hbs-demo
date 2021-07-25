$(document).ready(function () {
    //init datatable
	var dataTable = $('#doctorsTable').DataTable();

    //init start hospitalList 

     var $hospital = $('#hospitalList').select2({
        placeholder: "Lütfen hastane seçiniz.",
        theme: 'bootstrap4',
		allowClear: true
	});

	//init start polyclinicList

	$('#polyclinicList').select2({
		placeholder: "Lütfen poliklinik seçiniz.",
		theme: 'bootstrap4',
		allowClear: true,
		disabled: true
	});

	// Get polyclinic list by hospital id from GetDoctorList controller.

	$hospital.on("select2:select", function (e) {
		selectedHospital = this.value;
		$.ajax({
			type: 'POST',
			dataType: 'json',
			data: { hospitalId: selectedHospital },
			url: '/Doctor/GetPolyclinicList/',
			success: function (data) {
				const polyclinicListDto = jQuery.parseJSON(data);
				if (polyclinicListDto.ResultStatus === 0) {
					$('#polyclinicList').prop('disabled', false);
					$.each(polyclinicListDto.Polyclinics, function (i, item) {
						var newOption = new Option(item.Name, item.Id, true, false);
						$('#polyclinicList').append(newOption).trigger('change');
					});
					Swal.fire({
						title: 'İşlem Başarılı!',
						text: `${polyclinicListDto.Message}`,
						icon: 'success',
						confirmButtonText: 'Tamam'
					});
				} else {
					Swal.fire({
						icon: 'error',
						title: 'İşlem Başarısız!',
						text: `${polyclinicListDto.Message}`,
						confirmButtonText: 'Tamam'
					});
				}
			},
			error: function (err) {
				console.log(err);
				toastr.error(`${err.responseText}`, "Hata!");
			}
		});
	});

	// Get DoctorList by Polyclinic Id from GetDoctorList Controller

	$('#polyclinicList').on("select2:select", function (e) {
		selectedPolyclinic = this.value;
		$.ajax({
			type: 'POST',
			dataType: 'json',
			data: { polyclinicId: selectedPolyclinic },
			url: '/Doctor/GetDoctorList/',
			success: function (data) {
				dataTable.clear();
				$('#doctorsTable').hide();
				const doctorListDto = jQuery.parseJSON(data);
				if (doctorListDto.ResultStatus === 0) {
					$.each(doctorListDto.Doctors.$values,
						function (index, doctor) {
							const newTableRow = dataTable.row.add([
								doctor.Id,
								doctor.User.FirstName + " " + doctor.User.LastName,
								doctor.Policlinic.Name,
								doctor.User.Email,
								doctor.User.PhoneNumber,
								`<img src="/images/${doctor.User.Picture}" alt="${doctor.User.FirstName}" style="max-height:50px; max-width:50px;" />`,
								`
                                <button class="btn btn-primary btn-sm btn-detail" data-id="${doctor.Id}"><span class="fas fa-info"></span></button>
                                            `
							]).node();
							const jqueryTableRow = $(newTableRow);
							jqueryTableRow.attr('name', `${doctor.Id}`);
						});
					dataTable.draw();
					$('#doctorsTable').fadeIn(2000);
				}
				else {
					toastr.error(`${appointmentListDto.Message}`, 'İşlem Başarısız!')
				}
			},
			error: function (err) {
				$('.spinner-border').hide();
				$('#appointmentsTable').fadeIn(1000);
				toastr.error(`${err.responseText}`, 'Hata!')
			}
		});
	});

	$(document).on('click', '.btn-detail', function () {
		var selectedDoctorId = $(this).attr('data-id');
		var url = '/Doctor/Detail/' + selectedDoctorId;
		window.location.href = url;
	});
});
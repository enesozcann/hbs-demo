$(document).ready(function () {

    //init start hospitalList 

    $('#hospitalList').select2({
        placeholder: "Lütfen hastane seçiniz.",
        theme: 'bootstrap4',
		allowClear: true
	});
	// init end

	// Get polyclinic list by hospital id from GetPolyclinicList controller.

	$('#hospitalList').on("select2:select", function (e) {
		selectedHospital = this.value;
		$.ajax({
			type: 'POST',
			dataType: 'json',
			data: { hospitalId: selectedHospital },
			url: '/Appointment/GetPolyclinicList/',
			success: function (data) {
				const polyclinicListDto = jQuery.parseJSON(data);
				if (polyclinicListDto.ResultStatus === 0) {
					$('#polyclinicList').val(null).trigger('change');
					$('#polyclinicList').prop('disabled', false);
					$('#doctorList').prop('disabled', true);
					$.each(polyclinicListDto.Polyclinics, function (i, item) {
						var newOption = new Option(item.Name, item.Id, false, false);
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
	// Get list of polyclinics by hospital id and set to select2 polyclinicList is done.

	//init start polyclinicList 
	$('#polyclinicList').select2({
		placeholder: "Lütfen poliklinik seçiniz.",
		theme: 'bootstrap4',
		allowClear: true,
		disabled: true
	});
	// init end

	// Get doctor list by polyclinic id from GetDoctorList controller.

	$('#polyclinicList').on("select2:select", function (e) {
		selectedPolyclinic = this.value;
		$.ajax({
			type: 'POST',
			dataType: 'json',
			data: { polyclinicId: selectedPolyclinic },
			url: '/Appointment/GetDoctorList/',
			success: function (data) {
				const doctorListDto = jQuery.parseJSON(data);
				if (doctorListDto.ResultStatus === 0) {
					$('#doctorList').val(null).trigger('change');
					$('#doctorList').prop('disabled', false);
					$.each(doctorListDto.Doctors.$values, function (i, item) {
						var newOption = new Option("Dr. "+item.User.FirstName+" "+item.User.LastName, item.Id, false, false);
						$('#doctorList').append(newOption).trigger('change');
					});
				} else {
					Swal.fire({
						icon: 'error',
						title: 'İşlem Başarısız!',
						text: `${doctorListDto.Message}`,
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
	// Get list of doctors by polyclinic id and set to select2 doctorList is done.

	//init start doctorList 

	$('#doctorList').select2({
		placeholder: "Lütfen doktor seçiniz.",
		theme: 'bootstrap4',
		allowClear: true,
		disabled: true
	});
	// init end

    // init start jQuery UI - DatePicker with Timepicker addon.

    $(function (){
		$("#datepicker").datetimepicker({
			closeText: "Kapat",
			prevText: "&#x3C;geri",
			nextText: "ileri&#x3e",
			currentText: "Bugün",
			monthNames: ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
				"Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"],
			monthNamesShort: ["Oca", "Şub", "Mar", "Nis", "May", "Haz",
				"Tem", "Ağu", "Eyl", "Eki", "Kas", "Ara"],
			dayNames: ["Pazar", "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi"],
			dayNamesShort: ["Pz", "Pt", "Sa", "Ça", "Pe", "Cu", "Ct"],
			dayNamesMin: ["Pz", "Pt", "Sa", "Ça", "Pe", "Cu", "Ct"],
			weekHeader: "Hf",
			dateFormat: "dd.mm.yy",
			timeText: 'Saat',
			firstDay: 1,
			isRTL: false,
			showMonthAfterYear: false,
			yearSuffix: "",
			controlType: 'select',
			oneLine: true,
			stepMinute: 30,
			minDate: +1,
			maxDate: +30,
			defaultDate: +5,
			hourMin: 8,
			hourMax: 17,
			dateInput: false,
			beforeShowDay: $.datepicker.noWeekends,
        });
	});
	// init end
});
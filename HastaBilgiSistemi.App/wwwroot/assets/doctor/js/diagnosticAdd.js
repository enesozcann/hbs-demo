$(document).ready(function () {

	// Init currentDiagnosticTable as DataTable
	var currentDiagnosticTable = $('#currentDiagnosticTable').DataTable({
		paging: false,
		searching: false,
		ordering: false,
		info:false
	});

	// Init recipesTable as DataTable 
    var recipesTable = $('#recipesTable').DataTable({
		"lengthMenu": [2,4,6,8]
    });

	// Init diagnosticsTable as DataTable 
	$('#diagnosticsTable').DataTable();

    // Get DiagnosticAddPartial on modal then add new diagnostic with appointmentId, patientId. *start
    $(function () {
        // Get DiagnosticAddPartial on modal.
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-diagnostic-add', function () {
            const curpatientId = $('#patientId').attr('data-id');
            const curAppointmentId = $(this).attr('data-id');
            const url = '/Doctor/Appointment/DiagnosticAdd?patientId=' + curpatientId + '&&appointmentId=' + curAppointmentId;
			$.get(url).done(function (data) {
				placeHolderDiv.html(data);
				placeHolderDiv.find(".modal").modal('show');
			});
		});
        // Get DiagnosticAddPartial on modal. *end

        // Add new diagnostic and refresh currentDiagnosticTable *start
        placeHolderDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const form = $('#form-diagnostic-add');
            const actionUrl = form.attr('action');
            const dataToSend = new FormData(form.get(0));
            $.ajax({
                url: actionUrl,
                type: 'POST',
                data: dataToSend,
                processData: false,
                contentType: false,
                success: function (data) {
                    const diagnosticAddAjaxModel = jQuery.parseJSON(data);
                    const newFormBody = $('.modal-body', diagnosticAddAjaxModel.DiagnosticAddPartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = currentDiagnosticTable.row.add([
                            diagnosticAddAjaxModel.DiagnosticDto.Diagnostic.Name,
                            diagnosticAddAjaxModel.DiagnosticDto.Diagnostic.Detail,
                            `<button class="profile-card__button button--orange btn-diagnostic-edit" data-id="${diagnosticAddAjaxModel.DiagnosticDto.Diagnostic.Id}"><i class="fas fa-edit"></i> Güncelle</button>`,
                            `<button class="profile-card__button button--blue btn-medicine-add" data-id="${diagnosticAddAjaxModel.DiagnosticDto.Diagnostic.Id}"><i class="fas fa-capsules"></i> İlaç Ekle</button>`
                        ]).node();
                        const jqueryTableRow = $(newTableRow);
                        jqueryTableRow.attr('name', `${diagnosticAddAjaxModel.DiagnosticDto.Diagnostic.Id}`);
                        currentDiagnosticTable.row(newTableRow).draw();
                        toastr.success(`${diagnosticAddAjaxModel.DiagnosticDto.Message}`, 'Başarılı İşlem!');
                    }
                    else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `${text}\n`;
                        });
                        toastr.warning(summaryText);
                    }
                },
                error: function (err) {
                    console.log(err);
                    toastr.error(`${err.responseText}`, "Hata!");
                }
            });
        });
        // Add new diagnostic and refresh currentDiagnosticTable *end

    });
    // Get DiagnosticAddPartial on modal then add new diagnostic with appointmentId, patientId. *end

    // Get DiagnosticUpdatePartial on modal then update diagnostic with appointmentId, patientId. *start
    $(function () {     
        // Get DiagnosticUpdatePartial on modal.
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-diagnostic-edit', function () {
            const curDiagnostic = $(this).attr('data-id');
            const url = '/Doctor/Appointment/DiagnosticUpdate/' + curDiagnostic;
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });
        // Get DiagnosticUpdatePartial on modal. *end

        // Update diagnostic and refresh currentDiagnosticTable *start
        placeHolderDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const form = $('#form-diagnostic-update');
            const actionUrl = form.attr('action');
            const dataToSend = new FormData(form.get(0));
            $.ajax({
                url: actionUrl,
                type: 'POST',
                data: dataToSend,
                processData: false,
                contentType: false,
                success: function (data) {
                    currentDiagnosticTable.clear();
                    const diagnosticAddAjaxModel = jQuery.parseJSON(data);
                    const newFormBody = $('.modal-body', diagnosticAddAjaxModel.DiagnosticAddPartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = currentDiagnosticTable.row.add([
                            diagnosticAddAjaxModel.DiagnosticDto.Diagnostic.Name,
                            diagnosticAddAjaxModel.DiagnosticDto.Diagnostic.Detail,
                            `<button class="profile-card__button button--orange btn-diagnostic-edit" data-id="${diagnosticAddAjaxModel.DiagnosticDto.Diagnostic.Id}"><i class="fas fa-edit"></i> Güncelle</button>`,
                            `<button class="profile-card__button button--blue btn-medicine-add" data-id="${diagnosticAddAjaxModel.DiagnosticDto.Diagnostic.Id}"><i class="fas fa-capsules"></i> İlaç Ekle</button>`
                        ]).node();
                        const jqueryTableRow = $(newTableRow);
                        jqueryTableRow.attr('name', `${diagnosticAddAjaxModel.DiagnosticDto.Diagnostic.Id}`);
                        currentDiagnosticTable.row(newTableRow).draw();
                        toastr.success(`${diagnosticAddAjaxModel.DiagnosticDto.Message}`, 'Başarılı İşlem!');
                    }
                    else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `${text}\n`;
                        });
                        toastr.warning(summaryText);
                    }
                },
                error: function (err) {
                    console.log(err);
                    toastr.error(`${err.responseText}`, "Hata!");
                }
            });
        });
        // Update diagnostic and refresh currentDiagnosticTable *end

    });
    // Get DiagnosticUpdatePartial on modal then add new diagnostic with appointmentId, patientId. *end

    // Set Appointment IsActive variable to false.
    $(document).on('click', '.btn-appointment-end', function () {
        Swal.fire({
            title: 'Emin Misin?',
            text: "Randevuyu bitirmek istediğine emin misin? Bu işlem geri alınamaz!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, bitir!',
            cancelButtonText: 'Hayır, iptal!'
        }).then((result) => {
            if (result.isConfirmed) {
                var selectedAppointmentId = $(this).attr('data-id');
                $.ajax({
                    url: '/Doctor/Appointment/SetIsActiveToFalse/',
                    type: 'POST',
                    data: { 'id': selectedAppointmentId },
                    success: function (data) {
                        Swal.fire(
                            'Başarılı!',
                            'Randevu tamamlandı. 3 saniye içerisinde günün randevuları sayfasına yönlendirileceksin.',
                            'success'
                        )
                        setTimeout(function () {
                            window.location.href = '/Doctor/Appointment/Today';                    
                        },3000);
                    },
                    error: function (err) {
                        console.log(err);
                        toastr.error(`${err.responseText}`, "Hata!");
                    }
                });
            }
        })
       
    });

    // Get RecipeAddPartial on modal then add new recipe with medicineId, diagnosticId. *start
    $(function () {
        // Get RecipeAddPartial on modal.
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-medicine-add', function () {
            const curDiagnostic = $(this).attr('data-id');
            const url = '/Doctor/Appointment/RecipeAdd/' + curDiagnostic;
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });
        // Get RecipeAddPartial on modal. *end

        // Add recipes and refresh recipesTable *start
        placeHolderDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            selectedMedicineId = $("#SelectedMedicineId option:selected").val();
            $("#medicineId").val(selectedMedicineId);
            const form = $('#form-recipe-add');
            const actionUrl = form.attr('action');
            const dataToSend = new FormData(form.get(0));
            $.ajax({
                url: actionUrl,
                type: 'POST',
                data: dataToSend,
                processData: false,
                contentType: false,
                success: function (data) {
                    const recipeAddAjaxModel = jQuery.parseJSON(data);
                    const newFormBody = $('.modal-body', recipeAddAjaxModel.RecipeAddPartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = recipesTable.row.add([
                            recipeAddAjaxModel.RecipeDto.Recipe.Medicine.Name,
                            recipeAddAjaxModel.RecipeDto.Recipe.Medicine.Prospectus,
                            `<button class="profile-card__button button--gray btn-recipe-delete" data-id="${recipeAddAjaxModel.RecipeDto.Recipe.Id}"><i class="fas fa-edit"></i> Sil</button>`
                        ]).node();
                        const jqueryTableRow = $(newTableRow);
                        jqueryTableRow.attr('name', `${recipeAddAjaxModel.RecipeDto.Recipe.Id}`);
                        recipesTable.row(newTableRow).draw();
                        toastr.success(`${recipeAddAjaxModel.RecipeDto.Message}`, 'Başarılı İşlem!');
                    }
                    else {
                        let summaryText = "";
                        $('#validation-summary > ul > li').each(function () {
                            let text = $(this).text();
                            summaryText = `${text}\n`;
                        });
                        toastr.warning(summaryText);
                    }
                },
                error: function (err) {
                    console.log(err);
                    toastr.error(`${err.responseText}`, "Hata!");
                }
            });
        });
        // Add recipes and refresh recipesTable *end

    });
    // Get RecipeAddPartial on modal then add new recipe with medicineId, diagnosticId. *end

    // Delete Recipe *start
    $(document).on('click', '.btn-recipe-delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        const tableRow = $(`[name="${id}"]`);
        const Name = tableRow.find('td:eq(1)').text();
        Swal.fire({
            title: 'İlacı silmek istediğinize emin misiniz?',
            text: `${Name} adlı ila. silinecektir, bu işlem geri alınamaz!`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, Sil!',
            cancelButtonText: 'Hayır, İptal Et'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    data: { recipeId: id },
                    url: '/Doctor/Appointment/RecipeDelete/',
                    success: function (data) {
                        const recipeDto = jQuery.parseJSON(data);
                        if (recipeDto.ResultStatus === 0) {
                            Swal.fire({
                                title: 'İşlem Başarılı!',
                                text: `${recipeDto.Message}`,
                                icon: 'success',
                                confirmButtonText: 'Tamam'
                            });
                            recipesTable.row(tableRow).remove().draw();
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'İşlem Başarısız!',
                                text: `${recipeDto.Message}`,
                                confirmButtonText: 'Tamam'
                            });
                        }
                    },
                    error: function (err) {
                        console.log(err);
                        toastr.error(`${err.responseText}`, "Hata!");
                    }
                });
            }
        });
    });

});
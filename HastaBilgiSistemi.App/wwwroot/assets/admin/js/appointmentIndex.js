/*DataTable oluşturma işlemi burada başlıyor.*/

$(document).ready(function () {
    $('#appointmentsTable').DataTable({
        dom:
            "<'row'<'col-sm-2'l><'col-sm-6 text-right'B><'col-sm-4'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            {
                text: 'Ekle',
                attr: {
                    id: "btnAdd",
                },
                className: 'btn btn-success btn-md',
                action: function (e, dt, node, config) {
                }
            },
            {
                text: 'Yenile',
                className: 'btn btn-info btn-md',
                action: function (e, dt, node, config) {
                    $.ajax({
                        type: 'GET',
                        url: '/Admin/Appointment/GetAllAppointments/',
                        contentType: 'application/json',
                        beforeSend: function () {
                            $('#appointmentsTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const appointmentListDto = jQuery.parseJSON(data);
                            console.log(appointmentListDto);
                            if (appointmentListDto.ResultStatus === 0) {
                                $.each(userListDto.Users.$values,
                                    function (index, appointment) {
                                        const newTableRow = dataTable.row.add([
                                            appointment.Id,
                                            appointment.AppointmentDate,
                                            appointment.IsActive,
                                            appointment.CreatedDate,
                                            appointment.ModifiedDate,
                                            `
                                <button class="btn btn-primary btn-sm btn-update" data-id="${appointment.Id}"><span class="fas fa-edit"></span></button>
                                <button class="btn btn-danger btn-sm btn-delete" data-id="${appointment.Id}"><span class="fas fa-minus-circle"></span></button>
                                            `
                                        ]).node();
                                        const jqueryTableRow = $(newTableRow);
                                        jqueryTableRow.attr('name', `${appointment.Id}`);
                                    });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#appointmentsTable').fadeIn(1000);
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
                }
            }
        ]
    });

    /* DataTable oluşturma işlemi burada bitiyor.*/

    /* Randevu ekle modalı getirme buradan başlıyor.*/

    $(function () {
        const url = '/Admin/Appointment/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });

        /* Randevu ekle modal'ı getirme işlemi burada bitiyor.*/

        /* Randevu Post işlemi buradan başlıyor.*/
        placeHolderDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const form = $('#form-appointment-add');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function (data) {
                console.log(data);
                const appointmentAddAjaxModel = jQuery.parseJSON(data);
                console.log(appointmentAddAjaxModel);
                const newFormBody = $('.modal-body', appointmentAddAjaxModel.AppointmentAddPartial);
                placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                if (isValid) {
                    placeHolderDiv.find('.modal').modal('hide');
                    const newTableRow = `
                            <tr name="${appointmentAddAjaxModel.AppointmentDto.Appointment.Id}">
                                <td>${appointmentAddAjaxModel.AppointmentDto.Appointment.Id}</td>
                                <td>${covertToShortDate(appointmentAddAjaxModel.AppointmentDto.Appointment.AppointmentDate)}</td>
                                <td>${appointmentAddAjaxModel.AppointmentDto.Appointment.Doctor.User.FirstName}</td>
                                <td>${appointmentAddAjaxModel.AppointmentDto.Appointment.Patient.User.FirstName}</td>
                                <td>${appointmentAddAjaxModel.AppointmentDto.Appointment.Doctor.Policlinic.Name}</td>
                                <td>${appointmentAddAjaxModel.AppointmentDto.Appointment.Doctor.Policlinic.Hospital.Name}</td>
                                <td>${covertToShortDate(appointmentAddAjaxModel.AppointmentDto.Appointment.CreatedDate)}</td>
                                <td>${covertToShortDate(appointmentAddAjaxModel.AppointmentDto.Appointment.ModifiedDate)}</td>
                                <td>${appointmentAddAjaxModel.AppointmentDto.Appointment.IsActive ? "<span class='badge bg-success'>Aktif</span>" : "<span class='badge bg-danger'>Pasif</span>"}</td>
                                <td>
                                    <div class="btn-group btn-block btn-sm" role="group">
                                        <button class="btn icon btn-primary btn-edit" data-id="${appointmentAddAjaxModel.AppointmentDto.Appointment.Id}"><i class="fas fa-edit"></i></button>
                                        <button class="btn icon btn-danger btn-delete" data-id="${appointmentAddAjaxModel.AppointmentDto.Appointment.Id}"><i class="fas fa-times-circle"></i></button>
                                    </div>
                                </td>
                            </tr>`;
                    const newTableRowObject = $(newTableRow);
                    newTableRowObject.hide();
                    $('#appointmentsTable').append(newTableRowObject);
                    newTableRowObject.fadeIn(1000);
                    toastr.success(`${appointmentAddAjaxModel.AppointmentDto.Message}`, 'Başarılı İşlem!');
                }
                else {
                    let summaryText = "";
                    $('#validation-summary > ul > li').each(function () {
                        let text = $(this).text();
                        summaryText = `${text}\n`;
                    });
                    toastr.warning(summaryText);
                }
            });
        });
    });

    /* Randevu Post işlemi burada bitiyor.*/

    /* Randevu Silme burada başlıyor */

    $(document).on('click', '.btn-delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        const tableRow = $(`[name="${id}"]`);
        const appointmentDate = tableRow.find('td:eq(1)').text();
        Swal.fire({
            title: 'Randevuyu silmek istediğinize emin misiniz?',
            text: `${appointmentDate} tarihli randevu silinecektir, bu işlem geri alınamaz!`,
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
                    data: { appointmentId: id },
                    url: '/Admin/Appointment/Delete/',
                    success: function (data) {
                        const appointmentDto = jQuery.parseJSON(data);
                        if (appointmentDto.ResultStatus === 0) {
                            Swal.fire({
                                title: 'İşlem Başarılı!',
                                text: `${appointmentDto.Message}`,
                                icon: 'success',
                                confirmButtonText: 'Tamam'
                            });
                            tableRow.fadeOut(1000);
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'İşlem Başarısız!',
                                text: `${appointmentDto.Message}`,
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

    /* Randevu Silme burada bitiyor */

    /* Randevu Güncelleme için modal getir */
    $(function () {
        const url = '/Admin/Appointment/Update';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-edit', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { appointmentId: id }).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');
            }).fail(function () {
                toastr.error("Hata.");
            });
        });

        /* Randevu güncelle*/
        placeHolderDiv.on('click', '#btnUpdate', function (event) {
            event.preventDefault();
            const form = $('#form-appointment-update');
            const actionUrl = form.attr('action');
            const dataToSend = form.serialize();
            $.post(actionUrl, dataToSend).done(function (data) {
                const appointmentUpdateAjaxModel = jQuery.parseJSON(data);
                const newFormBody = $('.modal-body', appointmentUpdateAjaxModel.AppointmentAddPartial);
                placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                if (isValid) {
                    placeHolderDiv.find('.modal').modal('hide');
                    const newTableRow = `
                            <tr name="${appointmentUpdateAjaxModel.AppointmentDto.Appointment.Id}">
                                <td>${appointmentUpdateAjaxModel.AppointmentDto.Appointment.Id}</td>
                                <td>${covertToShortDate(appointmentUpdateAjaxModel.AppointmentDto.Appointment.AppointmentDate)}</td>
                                <td>${appointmentUpdateAjaxModel.AppointmentDto.Appointment.Doctor.User.FirstName}</td>
                                <td>${appointmentUpdateAjaxModel.AppointmentDto.Appointment.Patient.User.FirstName}</td>
                                <td>${appointmentUpdateAjaxModel.AppointmentDto.Appointment.Doctor.Policlinic.Name}</td>
                                <td>${appointmentUpdateAjaxModel.AppointmentDto.Appointment.Doctor.Policlinic.Hospital.Name}</td>
                                <td>${covertToShortDate(appointmentUpdateAjaxModel.AppointmentDto.Appointment.CreatedDate)}</td>
                                <td>${covertToShortDate(appointmentUpdateAjaxModel.AppointmentDto.Appointment.ModifiedDate)}</td>
                                <td>${appointmentUpdateAjaxModel.AppointmentDto.Appointment.IsActive ? "<span class='badge bg-success'>Aktif</span>" : "<span class='badge bg-danger'>Pasif</span>"}</td>
                                <td>
                                    <div class="btn-group btn-block btn-sm" role="group">
                                        <button class="btn icon btn-primary btn-edit" data-id="${appointmentUpdateAjaxModel.AppointmentDto.Appointment.Id}"><i class="fas fa-edit"></i></button>
                                        <button class="btn icon btn-danger btn-delete" data-id="${appointmentUpdateAjaxModel.AppointmentDto.Appointment.Id}"><i class="fas fa-times-circle"></i></button>
                                    </div>
                                </td>
                            </tr>`;
                    const newTableRowObject = $(newTableRow);
                    const appointmentTableRow = $(`[name="${appointmentUpdateAjaxModel.AppointmentDto.Appointment.Id}"]`);
                    newTableRowObject.hide();
                    appointmentTableRow.replaceWith(newTableRowObject);
                    newTableRowObject.fadeIn(1000);
                    toastr.success(`${appointmentUpdateAjaxModel.AppointmentDto.Message}`, "Güncelleme Başarılı!")
                } else {
                    let summaryText = "";
                    $('#validation-summary > ul > li').each(function () {
                        let text = $(this).text();
                        summaryText = `${text}\n`;
                    });
                    toastr.warning(summaryText);
                }
            }).fail(function (response) {
                console.log(response)
            });
        });
    });
});
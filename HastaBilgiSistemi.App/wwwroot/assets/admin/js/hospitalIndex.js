/*DataTable oluşturma işlemi burada başlıyor.*/

$(document).ready(function () {
    const dataTable = $('#hospitalsTable').DataTable({
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
                        url: '/Admin/Hospital/GetAllHospitals/',
                        contentType: 'application/json',
                        beforeSend: function () {
                            $('#hospitalsTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const hospitalListDto = jQuery.parseJSON(data);
                            console.log(hospitalListDto)
                            dataTable.clear();
                            if (hospitalListDto.Data.ResultStatus === 0) {
                                $.each(hospitalListDto.Data.Hospitals.$values, function (index, hospital) {
                                    const newTableRow = dataTable.row.add([
                                        hospital.Id,
                                        hospital.Name,
                                        hospital.Address,
                                        covertToShortDate(hospital.CreatedDate),
                                        covertToShortDate(hospital.ModifiedDate),
                                        hospital.IsActive ? "<span class='badge bg-success'>Aktif</span>" : "<span class='badge bg-danger'>Pasif</span>",
                                        `<div class="btn-group btn-block btn-sm" role="group">
                                                <button class="btn icon btn-primary btn-edit" data-id="${hospital.Id}"><i class="fas fa-edit"></i></button>
                                                <button class="btn icon btn-danger btn-delete" data-id="${hospital.Id}"><i class="fas fa-times-circle"></i></button>
                                        </div>`
                                  ]).node();
                                    const jqueryTableRow = $(newTableRow);
                                    jqueryTableRow.attr('name', `${hospital.Id}`);
                                });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#hospitalsTable').fadeIn(1000);
                            }
                            else {
                                toastr.error(`${hospitalListDto.Message}`, 'İşlem Başarısız!')
                            }
                        },
                        error: function (err) {
                            $('.spinner-border').hide();
                            $('#hospitalsTable').fadeIn(1000);
                            toastr.error(`${err.responseText}`, 'Hata!')
                        }
                    });
                }
            }
        ]
    });
   
    /* DataTable oluşturma işlemi burada bitiyor.*/

    /* Kullanıcı ekle modalı getirme buradan başlıyor.*/

    $(function () {
        const url = '/Admin/Hospital/Add/';
        const placeHolderDiv = $('#modalPlaceHolder');
        $('#btnAdd').click(function () {
            $.get(url).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find(".modal").modal('show');
            });
        });

        /* Kullanıcı ekle modal'ı getirme işlemi burada bitiyor.*/

        /* Kullanıcı Post işlemi buradan başlıyor.*/
        placeHolderDiv.on('click', '#btnSave', function (event) {
            event.preventDefault();
            const form = $('#form-hospital-add');
            const actionUrl = form.attr('action');
            const dataToSend = new FormData(form.get(0));
            $.ajax({
                url: actionUrl,
                type: 'POST',
                data: dataToSend,
                processData: false,
                contentType: false,
                success: function(data) {
                    const hospitalAddAjaxModel = jQuery.parseJSON(data);
                    const newFormBody = $('.modal-body', hospitalAddAjaxModel.HospitalAddPartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = dataTable.row.add([
                            hospitalAddAjaxModel.HospitalDto.Hospital.Id,
                            hospitalAddAjaxModel.HospitalDto.Hospital.Name,
                            hospitalAddAjaxModel.HospitalDto.Hospital.Address,
                            covertToShortDate(hospitalAddAjaxModel.HospitalDto.Hospital.CreatedDate),
                            covertToShortDate(hospitalAddAjaxModel.HospitalDto.Hospital.ModifiedDate),
                            hospitalAddAjaxModel.HospitalDto.Hospital.IsActive ? "<span class='badge bg-success'>Aktif</span>" : "<span class='badge bg-danger'>Pasif</span>",
                            `<div class="btn-group btn-block btn-sm" role="group">
                                    <button class="btn icon btn-primary btn-edit" data-id="${hospitalAddAjaxModel.HospitalDto.Hospital.Id}"><i class="fas fa-edit"></i></button>
                                    <button class="btn icon btn-danger btn-delete" data-id="${hospitalAddAjaxModel.HospitalDto.Hospital.Id}"><i class="fas fa-times-circle"></i></button>
                             </div>`
                        ]).node();
                        const jqueryTableRow = $(newTableRow);
                        jqueryTableRow.attr('name', `${hospitalAddAjaxModel.HospitalDto.Hospital.Id}`);
                        dataTable.row(newTableRow).draw();
                        toastr.success(`${hospitalAddAjaxModel.HospitalDto.Message}`, 'Başarılı İşlem!');
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
                error: function(err) {
                    console.log(err);
                    toastr.error(`${err.responseText}`, "Hata!");
                }
            });
        });
    });

    /* Kullanıcı Post işlemi burada bitiyor.*/

    /* Kullanıcı Silme burada başlıyor */

    $(document).on('click', '.btn-delete', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        const tableRow = $(`[name="${id}"]`);
        const Name = tableRow.find('td:eq(1)').text();
        Swal.fire({
            title: 'Hastaneyi silmek istediğinize emin misiniz?',
            text: `${Name} adlı hastane silinecektir, bu işlem geri alınamaz!`,
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
                    data: { hospitalId: id },
                    url: '/Admin/Hospital/Delete/',
                    success: function (data) {
                        const hospitalDto = jQuery.parseJSON(data);
                        if (hospitalDto.ResultStatus === 0) {
                            Swal.fire({
                                title: 'İşlem Başarılı!',
                                text: `${hospitalDto.Message}`,
                                icon: 'success',
                                confirmButtonText: 'Tamam'
                            });
                            dataTable.row(tableRow).remove().draw();
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'İşlem Başarısız!',
                                text: `${hospitalDto.Message}`,
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

    /* Kullanıcı Silme burada bitiyor */

    /* Kullanıcı Güncelleme için modal getir */
    $(function () {
        const url = '/Admin/Hospital/Update';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-edit', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { hospitalId: id }).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');
            }).fail(function () {
                toastr.error("Bir hata oluştu.");
            });
        });

        /* Kullanıcı güncelle*/
        placeHolderDiv.on('click', '#btnUpdate', function (event) {
            event.preventDefault();
            const form = $('#form-hospital-update');
            const actionUrl = form.attr('action');
            const dataToSend = new FormData(form.get(0));
            $.ajax({
                url: actionUrl,
                type: 'POST',
                data: dataToSend,
                processData: false,
                contentType: false,
                success: function (data) {
                    const hospitalUpdateAjaxModel = jQuery.parseJSON(data);
                    console.log(hospitalUpdateAjaxModel);
                    const newFormBody = $('.modal-body', hospitalUpdateAjaxModel.HospitalUpdatePartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        const id = hospitalUpdateAjaxModel.HospitalDto.Hospital.Id;
                        const tableRow = $(`[name="${id}"]`);
                        placeHolderDiv.find('.modal').modal('hide');
                        dataTable.row(tableRow).data([
                            hospitalUpdateAjaxModel.HospitalDto.Hospital.Id,
                            hospitalUpdateAjaxModel.HospitalDto.Hospital.Name,
                            hospitalUpdateAjaxModel.HospitalDto.Hospital.Address,
                            covertToShortDate(hospitalUpdateAjaxModel.HospitalDto.Hospital.CreatedDate),
                            covertToShortDate(hospitalUpdateAjaxModel.HospitalDto.Hospital.ModifiedDate),
                            hospitalUpdateAjaxModel.HospitalDto.Hospital.IsActive ? "<span class='badge bg-success'>Aktif</span>" : "<span class='badge bg-danger'>Pasif</span>",
                            `<div class="btn-group btn-block btn-sm" role="group">
                                    <button class="btn icon btn-primary btn-edit" data-id="${hospitalUpdateAjaxModel.HospitalDto.Hospital.Id}"><i class="fas fa-edit"></i></button>
                                    <button class="btn icon btn-danger btn-delete" data-id="${hospitalUpdateAjaxModel.HospitalDto.Hospital.Id}"><i class="fas fa-times-circle"></i></button>
                             </div>`
                        ]);
                        tableRow.attr("name", `${id}`);
                        dataTable.row(tableRow).invalidate();
                        toastr.success(`${hospitalUpdateAjaxModel.HospitalDto.Message}`, 'Başarılı İşlem!');
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
    });
});
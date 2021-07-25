/*DataTable oluşturma işlemi burada başlıyor.*/

$(document).ready(function () {
    const dataTable = $('#usersTable').DataTable({
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
                        url: '/Admin/User/GetAllUsers/',
                        contentType: 'application/json',
                        beforeSend: function () {
                            $('#usersTable').hide();
                            $('.spinner-border').show();
                        },
                        success: function (data) {
                            const userListDto = jQuery.parseJSON(data);
                            console.log(userListDto);
                            dataTable.clear();
                            if (userListDto.ResultStatus === 0) {
                                $.each(userListDto.Users.$values, function (index, user) {
                                  const newTableRow =  dataTable.row.add([
                                        user.Id,
                                        user.UserName,
                                        user.FirstName,
                                        user.LastName,
                                        user.Email,
                                        user.PhoneNumber,
                                        `<img src="/images/${user.Picture}" alt="${user.UserName}" style="max-height:50px; max-width:50px;"/>`,
                                        `<div class="btn-group btn-block btn-sm" role="group">
                                                <button class="btn icon btn-primary btn-edit" data-id="${user.Id}"><i class="fas fa-edit"></i></button>
                                                <button class="btn icon btn-danger btn-delete" data-id="${user.Id}"><i class="fas fa-times-circle"></i></button>
                                        </div>`
                                  ]).node();
                                    const jqueryTableRow = $(newTableRow);
                                    jqueryTableRow.attr('name', `${user.Id}`);
                                });
                                dataTable.draw();
                                $('.spinner-border').hide();
                                $('#usersTable').fadeIn(1000);
                            }
                            else {
                                toastr.error(`${userListDto.Message}`, 'İşlem Başarısız!')
                            }
                        },
                        error: function (err) {
                            $('.spinner-border').hide();
                            $('#usersTable').fadeIn(1000);
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
        const url = '/Admin/User/Add/';
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
            const form = $('#form-user-add');
            const actionUrl = form.attr('action');
            const dataToSend = new FormData(form.get(0));
            $.ajax({
                url: actionUrl,
                type: 'POST',
                data: dataToSend,
                processData: false,
                contentType: false,
                success: function(data) {
                    const userAddAjaxModel = jQuery.parseJSON(data);
                    const newFormBody = $('.modal-body', userAddAjaxModel.UserAddPartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        placeHolderDiv.find('.modal').modal('hide');
                        const newTableRow = dataTable.row.add([
                            userAddAjaxModel.UserDto.User.Id,
                            userAddAjaxModel.UserDto.User.UserName,
                            userAddAjaxModel.UserDto.User.FirstName,
                            userAddAjaxModel.UserDto.User.LastName,
                            userAddAjaxModel.UserDto.User.Email,
                            userAddAjaxModel.UserDto.User.PhoneNumber,
                            `<img src="/images/${userAddAjaxModel.UserDto.User.Picture}" alt="${userAddAjaxModel.UserDto.User.UserName}" style="max-height:50px; max-width:50px;"/>`,
                            `<div class="btn-group btn-block btn-sm" role="group">
                                    <button class="btn icon btn-primary btn-edit" data-id="${userAddAjaxModel.UserDto.User.Id}"><i class="fas fa-edit"></i></button>
                                    <button class="btn icon btn-danger btn-delete" data-id="${userAddAjaxModel.UserDto.User.Id}"><i class="fas fa-times-circle"></i></button>
                             </div>`
                        ]).node();
                        const jqueryTableRow = $(newTableRow);
                        jqueryTableRow.attr('name', `${userAddAjaxModel.UserDto.User.Id}`);
                        dataTable.row(newTableRow).draw();
                        toastr.success(`${userAddAjaxModel.UserDto.Message}`, 'Başarılı İşlem!');
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
        const userName = tableRow.find('td:eq(1)').text();
        Swal.fire({
            title: 'Kullanıcıyı silmek istediğinize emin misiniz?',
            text: `${userName} adlı kullanıcı silinecektir, bu işlem geri alınamaz!`,
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
                    data: { userId: id },
                    url: '/Admin/User/Delete/',
                    success: function (data) {
                        const userDto = jQuery.parseJSON(data);
                        if (userDto.ResultStatus === 0) {
                            Swal.fire({
                                title: 'İşlem Başarılı!',
                                text: `${userDto.Message}`,
                                icon: 'success',
                                confirmButtonText: 'Tamam'
                            });
                            dataTable.row(tableRow).remove().draw();
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'İşlem Başarısız!',
                                text: `${userDto.Message}`,
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
        const url = '/Admin/User/Update';
        const placeHolderDiv = $('#modalPlaceHolder');
        $(document).on('click', '.btn-edit', function (event) {
            event.preventDefault();
            const id = $(this).attr('data-id');
            $.get(url, { userId: id }).done(function (data) {
                placeHolderDiv.html(data);
                placeHolderDiv.find('.modal').modal('show');
            }).fail(function () {
                toastr.error("Hata.");
            });
        });

        /* Kullanıcı güncelle*/
        placeHolderDiv.on('click', '#btnUpdate', function (event) {
            event.preventDefault();
            const form = $('#form-user-update');
            const actionUrl = form.attr('action');
            const dataToSend = new FormData(form.get(0));
            $.ajax({
                url: actionUrl,
                type: 'POST',
                data: dataToSend,
                processData: false,
                contentType: false,
                success: function (data) {
                    const userUpdateAjaxModel = jQuery.parseJSON(data);
                    const newFormBody = $('.modal-body', userUpdateAjaxModel.UserUpdatePartial);
                    placeHolderDiv.find('.modal-body').replaceWith(newFormBody);
                    const isValid = newFormBody.find('[name="IsValid"]').val() === 'True';
                    if (isValid) {
                        const id = userUpdateAjaxModel.UserDto.User.Id;
                        const tableRow = $(`[name="${id}"]`);
                        placeHolderDiv.find('.modal').modal('hide');
                        dataTable.row(tableRow).data([
                            userUpdateAjaxModel.UserDto.User.Id,
                            userUpdateAjaxModel.UserDto.User.UserName,
                            userUpdateAjaxModel.UserDto.User.FirstName,
                            userUpdateAjaxModel.UserDto.User.LastName,
                            userUpdateAjaxModel.UserDto.User.Email,
                            userUpdateAjaxModel.UserDto.User.PhoneNumber,
                            `<img src="/images/${userUpdateAjaxModel.UserDto.User.Picture}" alt="${userUpdateAjaxModel.UserDto.User.UserName}" style="max-height:50px; max-width:50px;"/>`,
                            `<div class="btn-group btn-block btn-sm" role="group">
                                    <button class="btn icon btn-primary btn-edit" data-id="${userUpdateAjaxModel.UserDto.User.Id}"><i class="fas fa-edit"></i></button>
                                    <button class="btn icon btn-danger btn-delete" data-id="${userUpdateAjaxModel.UserDto.User.Id}"><i class="fas fa-times-circle"></i></button>
                             </div>`
                        ]);
                        tableRow.attr("name", `${id}`);
                        dataTable.row(tableRow).invalidate();
                        toastr.success(`${userUpdateAjaxModel.UserDto.Message}`, 'Başarılı İşlem!');
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
// init appointmentsTable to datatable.
$(document).ready(function () {
    $('#appointmentsTable').DataTable();
    $(document).on('click', '.btn-cancel', function (event) {
        event.preventDefault();
        const id = $(this).attr('data-id');
        const tableRow = $(`[name="${id}"]`);
        const appointmentDate = tableRow.find('td:eq(1)').text();
        Swal.fire({
            title: 'Randevuyu iptal istediğinize emin misiniz?',
            text: `${appointmentDate} tarihli randevu iptal edilecektir, bu işlem geri alınamaz!`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, Onaylıyorum!',
            cancelButtonText: 'Hayır, Vazgeçtim'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    data: { appointmentId: id },
                    url: '/Appointment/Cancel/',
                    success: function (data) {
                        const appointmentDto = jQuery.parseJSON(data);
                        if (appointmentDto.ResultStatus === 0) {
                            if ($('span').attr('data-id') == appointmentDto.Id) {
                                $(this).html("test");
                            }
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
});
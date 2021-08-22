
function addPage() {
    //if (!$("#AddOrEditForm").valid()) return false;
    Swal.showLoading();
    var form = $('#AddOrEditForm')[0];
    var formData = new FormData(form);
    $.ajax({
        type: 'POST',
        url: '/sayfa-ekle',
        data: formData,
        processData: false,
        contentType: false,
        dataType: 'json',
        async: true,
        success: function (response) {
            Swal.hideLoading();
            if (response.ec == 0) {
                Swal.fire({
                    title: 'Başarılı!',
                    text: 'Statik Sayfa Kayıt/güncelleme başarıyla gerçekleşti.',
                    icon: 'success',
                    confirmButtonText: 'Tamam'
                }).then(function () {
                    window.location.href = "/statik-sayfalar";
                });
            } else if (response.ec == -1) {
                Swal.fire({
                    title: 'Hata!',
                    text: 'Kayıt/Güncelleme sırasında hata oluştu.',
                    icon: 'warning',
                    confirmButtonText: 'Tamam'
                });
            } else {

            }
        },
    });
}

$(document).ready(function () {
    $('#summernote').summernote();
});
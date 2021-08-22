$(document).ready(function () {
    $("#AddOrEditForm").validate({
        rules: {
            Title: {
                required: true
            },
            CategoryId: {
                required: true
            }
        },
        messages: {
            Title: {
                required: "Lütfen Başlık Giriniz.",
            },
            CategoryId: {
                required: "Lütfen Kategori Seçiniz.",
            }
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        },
        submitHandler: function (form) {
            if ($(form).valid())
                form.submit();
            return false;
        },
        success: function (element) {
            element.text('').addClass('valid')
                .closest('.form-group').removeClass('error').addClass('success');
        }
    });
    //$('#summernote').summernote({
    //    height: 400
    //});
});

function addNews() {
    if (!$("#AddOrEditForm").valid()) return false;
    Swal.showLoading();
    var form = $('#AddOrEditForm')[0];
    var formData = new FormData(form);
    $.ajax({
        type: 'POST',
        url: '/haber-ekle',
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
                    text: 'Haber Kayıt/güncelleme başarıyla gerçekleşti.',
                    icon: 'success',
                    confirmButtonText: 'Tamam'
                }).then(function () {
                    window.location.href = "/haberler";
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
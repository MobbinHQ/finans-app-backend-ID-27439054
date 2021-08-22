$(document).ready(function () {
    $("#AddOrEditForm").validate({
        rules: {
            Name: {
                required: true
            }
        },
        messages: {
            Name: {
                required: "Lütfen Kategori Adı Giriniz.",
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
});

function addProductCategory() {
    if (!$("#AddOrEditForm").valid()) return false;
    Swal.showLoading();
    var form = $('#AddOrEditForm')[0];
    var formData = new FormData(form);
    $.ajax({
        type: 'POST',
        url: '/haber-kategori-ekle',
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
                    text: 'Haber Kategorisi Kayıt/güncelleme başarıyla gerçekleşti.',
                    icon: 'success',
                    confirmButtonText: 'Tamam'
                }).then(function () {
                    window.location.href = "/haber-kategorileri";
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
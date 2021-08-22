$(document).ready(function () {
    var selector = document.getElementById("Phone");
    var im = new Inputmask("(999) 999 99 99");
    im.mask(selector);
    debugger;
    $.validator.addMethod("pnumber", function (pn, element) {
        pn = pn.split("_").join("");
        pn = pn.split(" ").join("");
        pn = pn.replace("(", "");
        pn = pn.replace(")", "");
        if (pn.length < 10) {
            return false;
        } else {
            return true;
        }
    }, "");
    $("#AddOrEditForm").validate({
        rules: {
            Name: {
                required: true
            },
            Surname: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            Phone: {
                required: true,
                pnumber: true
            }
        },
        messages: {
            Name: {
                required: "Lütfen İsim Giriniz.",
            },
            Surname: {
                required: "Lütfen Soyisim Giriniz.",
            },
            Phone: {
                required: "Lütfen Telefon Numarasını Giriniz.",
                pnumber: "Telefon Numarası Eksik veya Hatalı"
            },
            Email: {
                required: "Lütfen Email Adresini Giriniz.",
                email: "Email Adresi Hatalı"
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

function addOrEditUser() {
    if (!$("#AddOrEditForm").valid()) return false;

    Swal.showLoading();
    var form = $('#AddOrEditForm')[0];
    var formData = new FormData(form);
    //var dosya = $('input[type=file]');
    //formData.append("file", dosya[0].files[0]);
    $.ajax({
        type: 'POST',
        url: '/kullanici-ekle',
        data: formData,
        processData: false,
        contentType: false,
        dataType: 'json',
        success: function (response) {
            Swal.hideLoading();
            if (response.ec == 0) {
                Swal.fire({
                    title: 'Başarılı!',
                    text: 'Kayıt/güncelleme başarıyla gerçekleşti.',
                    icon: 'success',
                    confirmButtonText: 'Tamam'
                }).then(function () {
                    window.location.href = "/kullanicilar";
                });
            } else if (response.ec == 1) {
                Swal.fire({
                    title: 'Hata!',
                    text: 'Email adresi başka bir kullanıcıya ait.',
                    icon: 'warning',
                    confirmButtonText: 'Tamam'
                });
            } else if (response.ec == -1) {
                Swal.fire({
                    title: 'Hata!',
                    text: 'Kayıt/Güncelleme sırasında hata oluştu.',
                    icon: 'warning',
                    confirmButtonText: 'Tamam'
                });
            }
        },
    });
}
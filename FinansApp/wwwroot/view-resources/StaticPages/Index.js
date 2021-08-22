$(document).ready(function () {
    //$('#summernote').summernote();
});
function selectPage() {
    var id = document.getElementById("page").value;
    if (id == null || id == "") {
        console.log("1");
        document.getElementById("smDiv").classList.add("d-none");
        return;
    }
    $.ajax({
        type: "GET",
        url: "/sayfa-getir",
        data: { id: id },
        async: true,
        success: function (data) {
            document.getElementById("smDiv").classList.remove("d-none");
            if (data != null) {
                var markupStr = data.text;
                $("#summernote").summernote('code', markupStr);
            } else {
                $("#summernote").summernote('code', '');
            }
        },
        dataType: 'json'
    });
}
function savePage() {
    var id = document.getElementById("page").value;
    var messageData = $('#summernote').summernote('code');
    $.ajax({
        type: "POST",
        url: "/icerik-kaydet",
        data: { id: id, text: messageData },
        async: true,
        success: function (data) {
            document.getElementById("smDiv").classList.remove("d-none");
        },
        dataType: 'json'
    });
}

var table = $('#PageTable').DataTable({
    "processing": true,
    "serverSide": true,
    "filter": true,
    "ajax": {
        "url": "/sayfalar-liste",
        "type": "POST",
        "datatype": "json"
    },
    columnDefs: [
        {
            targets: 0,
            data: null,
            visible: false,
        },
        {
            targets: 1,
            data: 'name',
            className: 'control',
            sortable: false,
            autoWidth: true,
        },
        {
            targets: 2,
            data: null,
            sortable: false,
            autoWidth: true,
            defaultContent: '',
            render: (data, type, row, meta) => {
                return [
                    `   <button type="button" class="btn btn-sm bg-secondary text-white edit-productCategory" data-productCategory-id="${row.id}">`,
                    `       <i class="fas fa-pencil-alt"></i> Düzenle`,
                    '   </button>',
                    `   <button type="button" class="btn btn-sm bg-danger text-white delete-productCategory" data-productCategory-id="${row.id}" data-productCategory-name="${row.name}">`,
                    `       <i class="fas fa-trash"></i> Sil`,
                    '   </button>'
                ].join('');
            }
        }
    ],
    "language": {
        "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Turkish.json"
    }
});

$(document).on('click', '.edit-productCategory', function () {
    var productCategoryId = $(this).attr("data-productCategory-id");
    window.location.href = "/sayfa-duzenle/" + productCategoryId;
});
$(document).on('click', '.delete-productCategory', function () {
    var productCategoryId = $(this).attr("data-productCategory-id");
    var productCategoryName = $(this).attr('data-productCategory-name');

    deleteProductCategory(productCategoryId, productCategoryName);
});

function deleteProductCategory(productCategoryId, productCategoryName) {
    Swal.fire({
        title: productCategoryName,
        text: "Silmek istediğinize emin misiniz ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet , Sil!',
        cancelButtonText: 'İptal'
    }).then((result) => {

        if (result.value) {
            $.ajax({
                type: "POST",
                url: "/sayfa-sil",
                data: { id: productCategoryId },
                success: function (data) {
                    if (data.ec == 0) {
                        Swal.fire(
                            'Silindi!',
                            'Statik sayfa silme işlemi başarılı.',
                            'success'
                        );
                        table.ajax.reload();
                    } else {
                        Swal.fire(
                            'Hata!',
                            'Statik sayfa silme sırasında hata oluştu..',
                            'warning'
                        );
                    }
                }
            });
        }
    });
}
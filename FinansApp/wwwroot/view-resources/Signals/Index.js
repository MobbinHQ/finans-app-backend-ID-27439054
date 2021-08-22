var table = $('#RestTable').DataTable({
    "processing": true,
    "serverSide": true,
    "filter": true,
    "ajax": {
        "url": "/sinyal-liste",
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
            data: 'buySell',
            className: 'control',
            sortable: false,
            autoWidth: true,
        },
        {
            targets: 2,
            data: 'entry',
            className: 'control',
            sortable: false,
            autoWidth: true,
        },
        {
            targets: 3,
            data: 'tp',
            className: 'control',
            sortable: false,
            autoWidth: true,
        },
        {
            targets: 4,
            data: 'sl',
            autoWidth: true,
            sortable: false
        },
        {
            targets: 5,
            data: null,
            sortable: false,
            autoWidth: true,
            defaultContent: '',
            render: (data, type, row, meta) => {
                return [
                    `   <button type="button" class="btn btn-sm bg-secondary text-white edit-productCategory" data-productCategory-id="${row.id}">`,
                    `       <i class="fas fa-pencil-alt"></i> Düzenle`,
                    '   </button>',
                    `   <button type="button" class="btn btn-sm bg-danger text-white delete-productCategory" data-productCategory-id="${row.id}" data-productCategory-name="${row.buySell}">`,
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
    window.location.href = "/sinyal-duzenle/" + productCategoryId;
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
                url: "/sinyal-sil",
                data: { id: productCategoryId },
                success: function (data) {
                    if (data.ec == 0) {
                        Swal.fire(
                            'Silindi!',
                            'Sinyal silme işlemi başarılı.',
                            'success'
                        );
                        table.ajax.reload();
                    } else if (data.ec == -1) {
                        Swal.fire(
                            'Hata!',
                            'Sinyal silme sırasında hata oluştu..',
                            'warning'
                        );
                    } else {
                        Swal.fire(
                            'Hata!',
                            'Bir şeyler ters gitti..',
                            'warning'
                        );
                    }
                }
            });
        }
    });
}
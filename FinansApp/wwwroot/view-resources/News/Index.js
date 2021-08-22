var table = $('#RestTable').DataTable({
    "processing": true,
    "serverSide": true,
    "filter": true,
    "ajax": {
        "url": "/haber-liste",
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
            data: 'title',
            className: 'control',
            sortable: false,
            autoWidth: true,
        },
        {
            targets: 2,
            data: 'isActive',
            autoWidth: true,
            sortable: false,
            render: data => `<input type="checkbox" disabled ${data ? 'checked' : ''}>`
        },
        {
            targets: 3,
            data: 'createDate',
            autoWidth: true,
            sortable: false,
            render: function (data, type, row) {
                return data ? moment(data).format('DD.MM.YYYY HH:mm') : '';
            }
        },
        {
            targets: 4,
            data: null,
            sortable: false,
            autoWidth: true,
            defaultContent: '',
            render: (data, type, row, meta) => {
                return [
                    `   <button type="button" class="btn btn-sm bg-secondary text-white edit-productCategory" data-productCategory-id="${row.id}">`,
                    `       <i class="fas fa-pencil-alt"></i> Düzenle`,
                    '   </button>',
                    `   <button type="button" class="btn btn-sm bg-danger text-white delete-productCategory" data-productCategory-id="${row.id}" data-productCategory-name="${row.categoryName}">`,
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
    window.location.href = "/haber-duzenle/" + productCategoryId;
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
                url: "/haber-sil",
                data: { id: productCategoryId },
                success: function (data) {
                    if (data.ec == 0) {
                        Swal.fire(
                            'Silindi!',
                            'Haber silme işlemi başarılı.',
                            'success'
                        );
                        table.ajax.reload();
                    } else if (data.ec == -1) {
                        Swal.fire(
                            'Hata!',
                            'Haber silme sırasında hata oluştu..',
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
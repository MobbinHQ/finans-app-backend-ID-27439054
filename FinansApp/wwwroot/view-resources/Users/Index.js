var table = $('#UserTable').DataTable({
    "processing": true,
    "serverSide": true,
    "filter": true,
    "ajax": {
        "url": "/kullanici-liste",
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
            data: 'surname',
            autoWidth: true,
            sortable: false
        },
        {
            targets: 3,
            data: 'email',
            autoWidth: true,
            sortable: false
        },
        {
            targets: 4,
            data: 'phone',
            autoWidth: true,
            sortable: false
        },
        {
            targets: 5,
            data: 'isActive',
            autoWidth: true,
            sortable: false,
            render: data => `<input type="checkbox" disabled ${data ? 'checked' : ''}>`
        },
        {
            targets: 6,
            data: null,
            sortable: false,
            autoWidth: false,
            autoWidth: true,
            defaultContent: '',
            render: (data, type, row, meta) => {
                return [
                    `   <button type="button" class="btn btn-sm bg-secondary text-white edit-user" data-user-id="${row.id}">`,
                    `       <i class="fas fa-pencil-alt"></i> Düzenle`,
                    '   </button>',
                    `   <button type="button" class="btn btn-sm bg-danger text-white delete-user" data-user-id="${row.id}" data-user-name="${row.name}">`,
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
$(document).on('click', '.edit-user', function () {
    var userId = $(this).attr("data-user-id");
    window.location.href = "/kullanici-duzenle/" + userId;
});
$(document).on('click', '.delete-user', function () {
    var userId = $(this).attr("data-user-id");
    var userName = $(this).attr('data-user-name');

    deleteuser(userId, userName);
});

function deleteuser(userId, userName) {
    Swal.fire({
        title: userName,
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
                url: "/kullanici-sil",
                data: { id: userId },
                success: function (data) {
                    if (data) {
                        Swal.fire(
                            'Silindi!',
                            'Kullanıcı silme işlemi başarılı.',
                            'success'
                        );
                        table.ajax.reload();
                    } else {
                        Swal.fire(
                            'Hata!',
                            'Kullanıcı silme sırasında hata oluştu..',
                            'warning'
                        );
                    }

                }
            });

        }
    })

}
function ShowAlert(message, type, url) {
    var mapper = {
        '1': ['error', 'Lỗi'],
        '2': ['success', 'Thành công'],
        '3': ['info', 'Thông tin']
    }
    swal({
        title: mapper[type][1],
        text: message,
        type: mapper[type][0]
    }, function () {
        if (url != null) {
            window.location.href = url;
        }
    });
}

function ShowConfirm(message, ajax) {
    swal({
        title: message,
        type: "warning",
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: "Đồng ý",
        confirmButtonClass: 'btn btn-success',
        cancelButtonText: "Không",
        showCancelButton: true,
        closeOnConfirm: false
    }, ajax
    );
}
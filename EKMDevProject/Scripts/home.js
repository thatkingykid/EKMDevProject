$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Home/RedirectUser",
        success: function (data) {
            $('#siteContent').html(data);
        },
        error: function (data) {
            console.log(data);
        }
    })
})

$(document).on('ready', '#admin-item-table', function () {
    $(this).DataTable();
})

function writeAndShowError(message) {
    var p = $('<p>').addClass('text-danger');
    p.text(message);
    $('#error-alert').prepend(p).removeClass('hidden');
}

function writeAndShowSuccess(message) {
    var p = $('<p>').addClass('text-success');
    p.text(message);
    $('#success-alert').prepend(p).removeClass('hidden');
}
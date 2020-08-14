$(document).on('click', '#register', function () {
    $.ajax({
        type: "GET",
        url: "/Login/Register",
        success: function (data) {
            $('#siteContent').html(data);
        }
    })
})

function LoginSuccess() {
    $.ajax({
        type: "GET",
        url: "/Home/Index",
        success: function (data) {
            $('#siteContent').html(data);
        }
    })
}

function LoginFailure() {
    writeAndShowError('Please submit a valid username or password')
}

function RegisterSuccess() {
    writeAndShowSuccess('User successfully added');
    $.ajax({
        type: "GET",
        url: "/Login/Index",
        success: function (data) {
            $('#siteContent').html(data);
        }
    })
}

function RegisterFailure(data) {
    writeAndShowError(data.responseJSON['Message']);
}
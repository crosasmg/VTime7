var urlBase = '/fasi/dli/forms/autologin.aspx';

$(document).ready(function () {
    $("#btnStart").click(function (e) {
        var username = $('#txtUsername').val();
        var password = $('#txtPassword').val();

        param = JSON.stringify({
            username: username,
            password: password
        });

        var urlAction = urlBase + "/TokenGenerate";

        $.ajax({
            url: urlAction,
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
			async: false,
            data: param,           
            success: function (data) {
                if (data.d.IsValid === true) {
                    window.location.href = '/generated/form/AutoServAgentes.aspx?InMotionGITToken=' + data.d.Token;
                }
                else {
                    notification.control.error(null, 'Error al intentar validar las credenciales');
                }
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
        e.preventDefault();
    });
});
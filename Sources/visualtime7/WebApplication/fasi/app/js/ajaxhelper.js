var ajaxJsonHelper = new function () {
    this.ajax = function (url, type, data, success, error, complete, async) {
        // Si no está definido la function de error, entonces le agrega una generica
        if (!error) {
            error = function (jqXHR, textStatus, errorThrown) {
                // Si el error es de no autenticado
                $.LoadingOverlay("hide");
                if (jqXHR.status == 401)
                    notification.swal.infoCallback(dict.NotAuthorized[generalSupport.LanguageName()], dict.ClickToRefreshPage[generalSupport.LanguageName()], function () { securitySupport.Logout(masterSupport.user.userId, true); });
                else if (jqXHR.status != 200)
                    generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
            };
        }

        if (async == undefined) {
            async = true;
        }

        $.ajax({
            url: url,
            type: type,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            async: async,
            cache: false,
            data: data,
            success: success,
            error: error,
            complete: complete,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
            }
        });
    };

    this.get = function (url, data, success, error, complete) {
        this.ajax(url, 'GET', data, success, error, complete);
    };

    this.post = function (url, data, success, error, complete, async) {
        this.ajax(url, 'POST', data, success, error, complete, async);
    };

    this.put = function (url, data, success, error, complete) {
        this.ajax(url, 'PUT', data, success, error, complete);
    };

    this.delete = function (url, data, success, error, complete) {
        this.ajax(url, 'DELETE', data, success, error, complete);
    };
};
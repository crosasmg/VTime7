var notification = {
    swal: {
        success: function (title, message) {
            swal(title, message, "success");
        },
        success: function (title, message, timer, callback) {
            swal({
                title: title,
                text: message,
                type: "success",
                timer: timer
            }, callback);
        },
        info: function (title, message) {
            swal({
                title: title,
                text: message,
                type: "info",
                html: true
            });
        },
        infoCallback: function (title, message, callback) {
            swal({
                title: title,
                text: message,
                type: "info",
                showCancelButton: false,
                confirmButtonText: 'OK',
                closeOnConfirm: true
            }, callback);
        },
        warning: function (title, message) {
            swal(title, message, "warning");
        },
        error: function (title, message) {
            swal(title, message, "error");
        },
        deleteRowConfirmation: function (callback) {
            swal({
                title: dict.DeleteRowConfirmation[generalSupport.LanguageName()],
                text: null,
                type: "warning",
                showCancelButton: true,
                cancelButtonText: dict.CancelNo[generalSupport.LanguageName()],
                confirmButtonColor: "#ec4758",
                confirmButtonText: dict.DeleteYes[generalSupport.LanguageName()],
                closeOnConfirm: true
            }, callback);
        },
        deleteConfirmation: function (text, callback) {
            swal({
                title: dict.AreYouSure[generalSupport.LanguageName()],
                text: text,
                type: "warning",
                showCancelButton: true,
                cancelButtonText: dict.Cancel[generalSupport.LanguageName()],
                confirmButtonColor: "#ec4758",
                confirmButtonText: dict.Delete[generalSupport.LanguageName()],
                closeOnConfirm: true
            }, callback);
        },
        continueConfirmation: function (title, message, callback) {
            swal({
                title: title,
                text: message,
                type: "warning",
                showCancelButton: true,
                cancelButtonText: dict.No[generalSupport.LanguageName()],
                confirmButtonColor: "#18a689",
                confirmButtonText: dict.Yes[generalSupport.LanguageName()],
                closeOnConfirm: true
            }, callback);
        }
    },
    toastr: {
        success: function (title, message) {
            toastr.success(message, title, { positionClass: "toast-bottom-right" });
        },
        info: function (title, message) {
            toastr.info(message, title, { positionClass: "toast-bottom-right" });
        },
        warning: function (title, message) {
            toastr.warning(message, title, { positionClass: "toast-bottom-right" });
        },
        error: function (title, message) {
            toastr.error(message, title, { positionClass: "toast-bottom-right" });
        }
    },
    control: {
        success: function (ctrolId, message) {
            toastr.success(message, null, { positionClass: "toast-bottom-right" });
        },
        info: function (ctrolId, message) {
            toastr.info(message, null, { positionClass: "toast-bottom-right" });
        },
        warning: function (ctrolId, message) {
            toastr.warning(message, null, { positionClass: "toast-bottom-right" });
        },
        error: function (ctrolId, message) {
            if (ctrolId !== null) {
                var options = {};
                options[ctrolId] = message;
                $('#' + ctrolId).closest("form").validate().showErrors(options);
            }
            else
                toastr.error(message, null, { positionClass: "toast-bottom-right" });
        }
    },
    alert: {
        success: function (title, message) {
            notification.alert.showAlert(message, 'success', title);
        },
        info: function (title, message) {
            notification.alert.showAlert(message, 'info', title);
        },
        warning: function (title, message) {
            notification.alert.showAlert(message, 'warning', title);
        },
        error: function (title, message) {
            notification.alert.showAlert(message, 'danger', title);
        },
        showAlert: function (message, type, title, closeDelay) {
            if ($("#alerts-container").length == 0)
                $("body").append($('<div id="alerts-container" style="position: fixed; width: 50%; left: 25%; top: 10%;">'));

            // default to alert-info; other options include success, warning, danger
            type = type || "info";

            // create the alert div
            var alert = $('<div class="alert alert-' + type + ' fade in">').append($('<button type="button" class="close" data-dismiss="alert">').append("&times;"));

            if (title)
                alert.append('<strong>' + title + ' </strong>');
            alert.append(message);

            // add the alert div to top of alerts-container, use append() to add to bottom
            $("#alerts-container").prepend(alert);

            // if closeDelay was passed - set a timeout to close the alert
            if (closeDelay)
                window.setTimeout(function () { alert.alert("close") }, closeDelay);
        }
    },
    splash: {
        success: function (title, message) {
            notification.splash.showSplash(message, 'success', title);
        },
        info: function (title, message) {
            notification.splash.showSplash(message, 'info', title);
        },
        warning: function (title, message) {
            notification.splash.showSplash(message, 'warning', title);
        },
        error: function (title, message) {
            notification.splash.showSplash(message, 'danger', title);
        },
        showSplash: function (message, type, title) {
            var mainElement = $("form[id$='MainForm']");
            mainElement.css("display", "none");

            // default to alert-info; other options include success, warning, danger
            type = type || "info";

            // create the alert div
            var alert = $('<div class="alert alert-' + type + ' fade in">');

            if (title)
                alert.append('<strong>' + title + ' </strong>');
            alert.append(message);
            mainElement.parent().prepend(alert);
        }
    }
};
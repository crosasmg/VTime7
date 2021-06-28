//Fields
var isSoaked, isPopup;
var urlBase = window.location.protocol + '//' + window.location.host + '/dropthings/ErrorHandler.aspx';
//Fields

$(document).ready(function () {
    $(window).on('load', function () {
        if (isSoaked === false && isPopup === false ) {
            var detailValue = getParameterByName('detail');
            if (detailValue !== undefined && detailValue !== '') {
                window.location.href = urlBase + '?detail=' + detailValue;
            } else {
                window.location.href = urlBase;
            }
        } else {
            MessageConfiguration();
        }
    });
});

//Methods

//Get QueryString from URL.
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function MessageConfiguration() {
    var keyValue = getParameterByName('CustomCode');
    var detailValue = getParameterByName('detail')
    var param = JSON.stringify({ code: keyValue, detail: detailValue });
    var urlAction = urlBase + "/MessageConfiguration";
    $.ajax({
        url: urlAction,
        data: param,
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            var root = result.d;
            $("#lblError").text(root.Message);
            if (root.DetailVisible === true) {
                $("#lblErrorDetail").text(detailValue);
                $("#lblErrorDetail").show();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("fail: " + errorThrown);
        }
    });
}

//Methods

(function () {
    isSoaked = window != window.parent;
    isPopup = (window.opener != null);
    $("#lblErrorDetail").hide();
})();
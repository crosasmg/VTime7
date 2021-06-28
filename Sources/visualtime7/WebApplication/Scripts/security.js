// Iterator for the block of each connString section
var i = 1;
var $loading;

function connString(connObject) {
    var versionOracle = connObject.ConnType.indexOf('Oracle');
    var versionSql = connObject.ConnType.indexOf('SqlClient');
	var versionOther = ((versionOracle === -1 && versionSql === -1) ? (1): (-1));
    var html =
            '<div class="col-md-6 form-horizontal">' +
                '<h4 class="form-signin-heading">' + connObject.ConnName + ':</h4>' +
                '<div class="form-group">' +
                    '<label for="dataSource-' + i + '" class="col-sm-3 control-label">' + ((versionOther === -1) ? ('Data Source') : ('Data Source|Server Name')) + '</label>' +
                    '<div class="col-sm-9">' +
                        '<input id="dataSource-' + i + '" type="text" class="form-control dataSource" placeholder="' + ((versionOracle === -1) ? (((versionOther === -1)) ? ('192.168.0.1\\SQLFRONTOFFICE') : ('192.168.0.1:4020')) :  ('TIME')) + '">' +
                    '</div>' +    
                '</div>';
    if (versionOracle === -1) {
        html += '<div class="form-group">' +
                    '<label for="initialCatalog-' + i + '" class="col-sm-3 control-label">' + ((versionOther === -1) ? ('Initial Catalog') : ('Initial Catalog|Catalog')) + '</label>' +
                    '<div class="col-sm-9">' +
                        '<input id="initialCatalog-' + i + '" type="text" class="form-control initialCatalog" placeholder="FrontOffice">' +
                    '</div>' +
                '</div>';
    }
        html +='<div class="form-group">' +
                    '<label for="userId-' + i + '" class="col-sm-3 control-label">User ID</label>' +
                    '<div class="col-sm-9">' +
                        '<input id="userId-' + i + '" type="text" class="form-control userId" placeholder="vtapps">' +
                    '</div>' +
                '</div>' +
                '<div class="form-group">' +
                    '<label for="password-' + i + '" class="col-sm-3 control-label">Password</label>' +
                    '<div class="col-sm-9">' +
                        '<input id="password-' + i + '" type="password" class="form-control pass1" placeholder="vt">' +
                    '</div>' +
                '</div>' +
                '<div class="form-group">' +
                    '<label for="password2-' + i + '" class="col-sm-3 control-label">Password</label>' +
                    '<div class="col-sm-9">' +
                        '<input id="password2-' + i + '" type="password" class="form-control pass2" placeholder="apps">' +
                    '</div>' +
                '</div>' +
                '<div class="col-md-12 text-right">' +
                    '<input type="button" data-conn="' + connObject.ConnName + '" class="btnEncrypt btn btn-primary" value="Actualizar" />' +
                '</div>' +
            '</div>';
    return html;
}

var index = {
    Init: function() {
        $.ajax({
            url: "Security.aspx/GetConnStrings",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                $.each(data.d, function(index, entry) {
                //data.d.forEach(function (index) { // incompatibilidad con IE7
                    if (entry.ConnType.indexOf('Oracle') !== -1)
                        $(".oracleContainer").append(connString(entry));
                    else if(entry.ConnType.indexOf('SqlClient') !== -1)
                        $(".sqlServerContainer").append(connString(entry));
                    else
                    	$(".otrasContainer").append(connString(entry));
                    i++;
                });
            },
            error: function (error) {
                alert("Ha ocurrido un error cargando los strings de conexión.");
            }
        }).done(index.RenderAfterInit);

        $(".btnDecrypt").click(function () {
            $.ajax({
                url: "Security.aspx/DecryptConfig",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) { 
                    alert("Valores por defecto restaurados.");
                },
                error: function (error) {
                    alert(error.responseJSON.Message);
                }
            });
        });

        $("#EncryptAll").click(function() {
            $.each($(".btnEncrypt"), function () {
                var catalog = $(this).parent().parent().find(".initialCatalog").val();
                var connObj = {
                    connName: $(this).data("conn"),
                    initialCatalog: (typeof(catalog) === 'undefined') ? "" : catalog,
                    dataSource: $(this).parent().parent().find(".dataSource").val(),
                    userId: $(this).parent().parent().find(".userId").val(),
                    pass1: $(this).parent().parent().find(".pass1").val(),
                    pass2: $(this).parent().parent().find(".pass2").val()
                };

                $.ajax({
                    type: "POST",
                    url: "Security.aspx/EncryptConfig",
                    data: JSON.stringify(connObj),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        alert("Credenciales actualizadas para la conexión " + connObj.connName);
                    },
                    error: function(error) {
                        alert(error.responseJSON.Message);
                    }
                });
            });
        });
    },
    RenderAfterInit: function() {
        $(".btnEncrypt").click(function() {
            var catalog = $(this).parent().parent().find(".initialCatalog").val();
            var connObj = {
                connName: $(this).data("conn"),
                initialCatalog: (typeof(catalog) === 'undefined') ? "" : catalog,
                dataSource: $(this).parent().parent().find(".dataSource").val(),
                userId: $(this).parent().parent().find(".userId").val(),
                pass1: $(this).parent().parent().find(".pass1").val(),
                pass2: $(this).parent().parent().find(".pass2").val()
            };

            $.ajax({
                type: "POST",
                url: "Security.aspx/EncryptConfig",
                data: JSON.stringify(connObj),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    alert("Credenciales actualizadas para la conexión " + connObj.connName);
                },
                error: function(error) {
                    alert(error.responseJSON.Message);
                }
            });
        });
    }
}

$(function () {
	$loading = $('.loadingDiv').hide();
	
	jQuery.ajaxSetup({
	    beforeSend: function() {
	        $loading.show();
	    },
	    complete: function(){
	        $loading.hide();
	    }
	});

    index.Init();
}); 
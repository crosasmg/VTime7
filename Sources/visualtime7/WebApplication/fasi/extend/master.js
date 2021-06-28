var masterSupport = new function () {
    //Obtiene la lista de menús del usuario
    this.getMenu = function () {
        $.ajax({
            type: "GET",
            url: constants.fasiApi.fasi + 'PagesByUserId?userId=' + masterSupport.user.userId + '&languageId=' + localStorage.getItem('languageId'),
            dataType: "json",
            async: false,
            cache: false,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Bearer " + masterSupport.user.token);
            },
            success: function (data) {
                masterSupport.loadMenu(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
            }
        });
    };

    // Carga el menú en la página con los datos del servidor
    this.loadMenu = function (data) {
        if (data !== undefined && data !== null && data.Successfully) {
            var sideMenu = $('#side-menu');

            var pageId = masterSupport.getUrlParameter('pageId');
            $.each(data.Data, function (index, item) {
                sideMenu.append(masterSupport.makeMenuItem(index, item, pageId, 1));
            });
        }
    };

    // Crea un item en el menú, su llamada puede ser recursiva para menús que continen sub menú
    this.makeMenuItem = function (index, item, pageId, level) {
        var htmlItem = '';
        var isActive = false;
        // Verifica se el menú contiene sub menús
        var hasSubItems = item.Children && item.Children.length > 0;

        // Cuando el id es mayor que zero significa que es de la página default
        var href = (item.Id > 0)
            ? constants.defaultPage + '?pageId=' + item.Id + '"'
            : (hasSubItems)
                ? '#'
                : item.Url;

        // Verifica si el menú está activo (seleccionado)
        if ((index === 0 && level === 1 && pageId === '' && window.location.pathname === constants.defaultPage)
            || (pageId !== '' && pageId === item.Id.toString())
            || (item.Url && window.location.pathname === item.Url.split('?')[0])) {
            isActive = true;

            $('#menuItemName').val(item.Title);
            $('#menuOrder').val(item.Order);
            $('#btnSaveMenuItem').click(function (event) { masterSupport.btnSaveMenuItemClick(event, item.Id); });
            $('#btnDeleteMenuItem').click(function (event) { masterSupport.btnDeleteMenuItemClick(event, item.Id); });

            if (item.LayoutType < 1 || item.LayoutType > 3 || !item.LayoutType)
                item.LayoutType = 3;

            $("#columns" + item.LayoutType).attr('checked', true);
        }

        htmlItem = '<li id="' + item.Id + '" {activeClass} data-layout="' + item.LayoutType + '">' +
            '<a href="' + href + '">' +
            (item.Icon ? '<i class="' + item.Icon + '"></i>' : '') +
            '<span class="nav-label">' + item.Title + '</span>' +
            (hasSubItems ? '<span class="fa arrow"></span>' : '') +
            '</a>';

        if (hasSubItems) {
            var classLevel = (level === 1 ? "nav-second-level" : "nav-third-level");

            htmlItem += '<ul class="nav ' + classLevel + (isActive ? '' : ' collapse') + '">';

            $.each(item.Children, function (indexSubItem, subItem) {
                htmlItem += masterSupport.makeMenuItem(indexSubItem, subItem, pageId, ++level);
            });
            htmlItem += '</ul>';
        }
        else htmlItem += '</li>';

        htmlItem = htmlItem.replace('{activeClass}', (isActive || htmlItem.indexOf('class="active"') !== -1 ? 'class="active"' : ''));
        return htmlItem;
    };

    // Obtiene el usuario, sea autenticado o anónimo
    this.getUser = function () {
        // Si no existe lenguaje seleccionada le pone la default
        if (!localStorage.getItem('languageId')) {
            localStorage.setItem('languageId', constants.defaultLanguageId);
            localStorage.setItem('languageName', constants.defaultLanguageName);
        }

        // Obtiene el código del usuario
        $.ajax({
            type: "GET",
            url: '/fasi/wmethods/User.aspx/GetUserInformation',
            contentType: "application/json; charset=utf-8",
            async: false,
            dataType: "json",
            success: function (data) {
                masterSupport.user = {
                    userId: data.d.userId,
                    companyId: data.d.companyId,
                    isAnonymous: data.d.isAnonymous,
                    schemeCode: data.d.schemeCode,
                    token: data.d.token,
                    clientId: data.d.clientId,
                    producerId: data.d.producerId,
                    firstNameAndSecondLastName: data.d.firstNameAndSecondLastName,
                    languageID: data.d.languageID,
                    languageName: data.d.languageName,
                    type: data.d.type
                };

                if (!masterSupport.user.isAnonymous) {
                    if (!localStorage.getItem('languageId')) {
                        localStorage.setItem('languageId', masterSupport.user.languageID);
                        localStorage.setItem('languageName', masterSupport.user.languageName);
                    }

                    $('#profileDropdown').css('display', '');
                    $('#userName').html(masterSupport.user.firstNameAndSecondLastName);
                    $('#userType').html(masterSupport.user.type);

                    // Habilita las funcionalidades de usuario autenticado
                    $('#signIcon').prop('class', 'fa fa-sign-out');
                    $('#signLink').on('click', function () { securitySupport.Logout(masterSupport.user.userId, true); });
                    $('#signIcon').prop('title', dict.LogOut[localStorage.getItem('languageName')]);

                    if (window.location.pathname === constants.defaultPage) {
                        $("#small-chat").css('display', '');
                        $('#menuConfiguration').css('display', '');
                    }
                    $(".theme-config-box").prop('title', dict.Configuration[localStorage.getItem('languageName')]);
                } else {
                    // Si es usuario anónimo se crea un nuevo usuario
                    if (masterSupport.user.isAnonymous && masterSupport.user.token !== "") {
                        $.ajax({
                            type: "POST",
                            url: constants.fasiApi.members + 'UserAnonymous',
                            async: false,
                            cache: false,
                            dataType: "json",
                            beforeSend: function (xhr) {
                                xhr.setRequestHeader("Authorization", "Bearer " + masterSupport.user.token);
                            },
                            success: function (data) {
                                masterSupport.user.userId = data.Data;
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
                            }
                        });
                    }

                    $('#signLink').on('click', function () { window.location.replace(constants.logInPage); });
                    $('#signIcon').prop('title', dict.LogIn[localStorage.getItem('languageName')]);
                }
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            }
        });
    };

    // Carga las opciones de lenguaje
    this.loadLanguage = function () {
        ajaxJsonHelper.get(constants.fasiApi.fasi + 'Language?languageId=' + localStorage.getItem('languageId'), null,
            function (data) {
                if (data && data.length > 0) {
                    var languageSelection = $('#languageSelection');
                    languageSelection.html('');

                    $.each(data, function (index, item) {
                        if (index !== 0)
                            languageSelection.append('<li class="divider"></li>');

                        languageSelection.append('<li>' +
                            '<a href="javascript:masterSupport.changeLanguage(' + item.Code + ',\'' + item.CultureCode + '\')">' +
                            '<div>' + item.Description + '</div>' +
                            '</a>' +
                            '</li>');
                    });
                }
                translator.initialize(localStorage.getItem('languageName'));
            },
            function () {
                translator.initialize(localStorage.getItem('languageName'));
            });
    };

    // Cambia la lenguaje
    this.changeLanguage = function (languageId, languageName) {
        if (languageId != localStorage.getItem('languageId')) {
            localStorage.setItem('languageId', languageId);
            localStorage.setItem('languageName', languageName);
            window.location.reload(true);
        }
    };

    // Obtiene el valor de parámetros de la url (por nombre)
    this.getUrlParameter = function (name) {
        name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
        var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
        var results = regex.exec(location.search);
        return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
    };

    // Control de visibilidad de la configuración de página
    this.controlActions = function () {
        $('.spin-icon').click(function () {
            $(".theme-config-box").toggleClass("show");
        });
    };

    // Agrega un nuevo item al menú
    this.createMenuItem = function () {
        ajaxJsonHelper.post(constants.fasiApi.fasi + 'PageAdd?userId=' + masterSupport.user.userId + '&languageId=' + localStorage.getItem('languageId'), null,
            function (data) {
                if (data !== undefined && data !== null && data.Successfully) {
                    window.location.replace(constants.defaultPage + '?pageId=' + data.Data.Id);
                }
            });
    };

    // Guarda los datos de la página
    this.btnSaveMenuItemClick = function (event, pageId) {
        var formInstance = $("#menuItemMainForm");
        var fvalidate = formInstance.validate();

        if (formInstance.valid()) {
            var layoutType = $('input:radio[name=Columns]:checked').val();
            if ($('#' + pageId).data('layout') != layoutType)
                defaultPage.resetGridStackColumns(layoutType);

            ajaxJsonHelper.post(constants.fasiApi.fasi + "PageChange?pageId=" + pageId + "&newTitle=" + encodeURIComponent($('#menuItemName').val()) + "&languageId=" + localStorage.getItem('languageId') + "&layoutType=" + layoutType + '&order=' + $('#menuOrder').val(), null,
                function (data) {
                    if (data !== undefined && data !== null && data.Successfully) {
                        window.location.replace(constants.defaultPage + '?pageId=' + pageId);
                    }
                });
        }
        else
            generalSupport.NotifyErrorValidate(fvalidate);
        event.preventDefault();
    };

    // Elimina una página
    this.btnDeleteMenuItemClick = function (event, pageId) {
        ajaxJsonHelper.delete(constants.fasiApi.fasi + 'PageDelete?pageId=' + pageId, null,
            function (data) {
                if (data !== undefined && data !== null && data.Successfully) {
                    window.location.replace(constants.defaultPage);
                }
            });
        event.preventDefault();
    };

    // Configuración del jquery.validate
    this.validateSetup = function () {
        var requiredMesage = dict.RequiredField[localStorage.getItem('languageName')];

        $("#menuItemMainForm").validate({
            rules: {
                PageName: { required: true },
                Columns: { required: true },
                MenuOrder: { required: true }
            },
            messages: {
                PageName: { required: requiredMesage },
                Columns: { required: requiredMesage },
                MenuOrder: { required: requiredMesage }
            }
        });
    };

    // Inicializa los tooltips con el texto traducido
    this.initializeToolTips = function () {
        $('#languageIcon').prop('title', dict.Language[localStorage.getItem('languageName')]);
        $('#helpIcon').prop('title', dict.Help[localStorage.getItem('languageName')]);
        //$('#alertsIcon').prop('title', dict.Alerts[localStorage.getItem('languageName')]);

        $('[data-toggle="tooltip"]').tooltip();

        // Carga la versión del archivo upgrade
        $.ajax({
            type: "GET",
            url: '/upgrade.inf',
            contentType: "text/html; charset=utf-8",
            success: function (data) {
                $('[data-trn-key="Copyright"]').prop('title', data);
                $('[data-trn-key="Copyright"]').tooltip();
            }
        });
    };

    // Define el título de la página
    this.setPageTitle = function (pageTitle) {
        var headerPageTitle = $('#pageTitle');
        headerPageTitle.html(pageTitle);
        headerPageTitle[0].parentNode.parentNode.style.display = '';
        $('.navbar')[0].classList.remove('white-bg');
    };

    // Se inicializa la busca por transacciones
    this.initializeTransactionSearch = function () {
        if (!masterSupport.user.isAnonymous) {
            $('#top-search').select2({
                placeholder: dict.SearchTransaction[localStorage.getItem('languageName')] + '...',
                language: localStorage.getItem('languageName'),
                maximumInputLength: 20,
                minimumInputLength: 3,
                ajax: {
                    type: "GET",
                    url: constants.fasiApi.backoffice + 'GetTransaction',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    delay: 250,
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + masterSupport.user.token);
                    },
                    data: function (params) {
                        // Se formatan los datos que se envía por parámetro
                        var query = {
                            userCode: params.term ? params.term : '',
                            userSchema: masterSupport.user.schemeCode,
                            transactionCode: ''
                        }
                        return query;
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        if (jqXHR.status == 401)
                            notification.swal.infoCallback(dict.ExpiredSession[localStorage.getItem('languageName')], dict.ClickToRefreshPage[localStorage.getItem('languageName')], function () { window.location.reload(true); });
                        else generalSupport.ErrorHandler(jqXHR, textStatus, errorThrown);
                    },
                    processResults: function (response) {
                        if (response.Successfully) {
                            var data = new Array();
                            // Se formatea los datos que recibe el componente
                            $.each(response.Data.Items, function (index, obj) {
                                data.push({ id: obj.split('-')[0].trim(), text: obj });
                            });

                            return {
                                results: data,
                                pagination: {
                                    more: false
                                }
                            };
                        }
                    }
                }
            }).on('change.select2', function (e) {
                if ($('#top-search').val() != null) {
                    masterSupport.openSelectedTransaction();
                    $("#top-search").val(null);
                    $('#top-search').trigger('change');
                }
            });
        }
    };

    // Abre la transacción seleccionada
    this.openSelectedTransaction = function () {
        $.LoadingOverlay("show");
        ajaxJsonHelper.get(constants.fasiApi.backoffice + 'MakeURLIfAllowed',
            {
                windowLogicalCode: $('#top-search').val(),
                schemaCode: masterSupport.user.schemeCode,
                companyId: masterSupport.user.companyId
            },
            function (response) {
                $.LoadingOverlay("hide");

                if (response.Successfully) {
                    var lstrURL = response.Data.Url.substr(response.Data.Url.indexOf('sCodispl=') + 9);
                    var lintLength = lstrURL.indexOf('&');
                    var lstrCodispl = lstrURL.substr(0, lintLength);
                    var win = open(window.location.protocol + '//' + window.location.host + response.Data.Url, 'Transaccion' + lstrCodispl.replace('-', '_'), 'toolbar=no,resizable=no,location=no,directories=no,status=yes,menubar=no,copyhistory=no,width=750,height=450,left=20,top=20');
                    if (win != null) {
                        win.moveTo(0, 0);
                        win.resizeTo(window.screen.availWidth, window.screen.availHeight);
                    }
                }
                else if (response.Reason.length > 0)
                    notification.swal.error('', response.Reason);
            });
    };

    this.NotificationClean = function (type, subType) {
        $.LoadingOverlay("show");
        ajaxJsonHelper.put(constants.fasiApi.notifications + 'NotificationsUpdate',
            JSON.stringify({
                Type: type,
                SubType: subType
            }),
            function (response) {
                $.LoadingOverlay("hide");
                if (response.Successfully) {
                    masterSupport.makeMessageMenu();
                }
            });
    };

    this.refreshMenu = function () {
        masterSupport.makeMessageMenu();
        //Timer
        try {
            $.periodic({ period: 30000, decay: 1.2, max_period: 60000 }, function () {
                masterSupport.makeMessageMenu();
            });
        } catch (e) {
            console.log(e);
        }
    };

    // Monta el menu de mensajes
    this.makeMessageMenu = function () {
        if (!masterSupport.user.isAnonymous) {
            $('#divNotifications').show();
            var messageContainer = $('#NotificationMessageContainer');
            $('#NotificationMessageContainer li').remove();
            ajaxJsonHelper.get(constants.fasiApi.notifications + 'GetNotifications', null,
                function (response) {
                    if (response && response.Count > 0) {
                        var count = parseInt($('#alertsCount').html(), 0);
                        if (isNaN(count)) {
                            count = 0;
                        }
                        if (count !== parseInt(response.Count)) {
                            $('#alertsCount').addClass('animated rubberBand');
                            window.setTimeout(function () {
                                $('#alertsCount').removeClass('animated rubberBand');
                            }, 3000);
                        }
                        $('#OptionCountInfo').removeClass('disabled');
                        $('#alertsIcon').css({ 'color': 'red' });
                        $('#alertsCount').show();
                    } else {
                        $('#OptionCountInfo').addClass('disabled');
                        $('#alertsCount').addClass('animated rubberBand');
                        $('#alertsIcon').css({ 'color': 'grey' });
                        $('#alertsCount').hide();
                    }

                    $('#alertsCount').html(response.Count);

                    $.each(response.Items, function (index, item) {
                        if (index > 0)
                            messageContainer.append('<li class="divider"></li>');

                        // Notificación
                        var html = '<li>' +
                            '<a href="{url}">' +
                            '<div>' +
                            '<i class="fa {icon}"></i> {message}' +
                            '<span class="pull-right text-muted small">{datetime}</span>' +
                            '</div>' +
                            '</a>' +
                            '</li>'

                        var lastUpdateOn = moment(new Date(item.LastUpdateOn));
                        var CurrentDate = moment();

                        var days = CurrentDate.diff(lastUpdateOn, 'days', false)

                        var messageDay = days + " " + dict.Day[localStorage.getItem('languageName')] + (days <= 1 ? "" : 's');

                        html = html.replace('{url}', item.Url ? (item.Url !== '@clear@' ? item.Url : 'javascript:masterSupport.NotificationClean(' + item.Type + ',' + item.SubType + ')') : '#');
                        html = html.replace('{icon}', item.Icon);
                        html = html.replace('{message}', item.Count && item.Count >= 0 ? item.Message.replace('{count}', item.Count) : item.Message);
                        html = html.replace('{datetime}', messageDay);

                        messageContainer.append(html);
                    });

                    // Agrega el último articulo con el link de ver todas las notificaciones
                    //if (response && response.Count > 0) {
                    //    messageContainer.append('<li class="divider"></li>' +
                    //        '<li>' +
                    //        '<div class="text-center link-block">' +
                    //        '<a href="#">' +
                    //        '<strong>See All Alerts</strong>' +
                    //        '<i class="fa fa-angle-right"></i>' +
                    //        '</a>' +
                    //        '</div>' +
                    //        '</li>');
                    //}
                });
        }
    };
};

$(document).ready(function () {
    masterSupport.getUser();
    if (masterSupport.user.token !== "") {
        var InMotionGITToken = generalSupport.GetParameterByName('InMotionGITToken');
        if (InMotionGITToken !== null) {
            if (!securitySupport.UserCheckEquals(InMotionGITToken, masterSupport.user.userId)) {
                securitySupport.Logout(masterSupport.user.userId, false);
                securitySupport.AutoLogIn(InMotionGITToken, constants.defaultLanguageId);
                masterSupport.getUser();
            }
        }
        // masterSupport.getMenu();
        // masterSupport.controlActions();
        // masterSupport.loadLanguage();
        // masterSupport.validateSetup();
        // masterSupport.initializeToolTips();
        // masterSupport.initializeTransactionSearch();
        // masterSupport.refreshMenu();
    } else {
        notification.swal.error(dict.STSFailureTitle[localStorage.getItem('languageName')],
            dict.STSFailureMessage[localStorage.getItem('languageName')]);
    }
});
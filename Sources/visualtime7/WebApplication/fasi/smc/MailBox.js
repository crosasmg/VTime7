app.smc = app.smc || {};
app.smc.mailbox = (function () {

    var currentView = 'inboxlnk';
    var currentTitle;
    var receiverPlaceHolder;
    var important;
    var urlVars = [];
    var userDefaultSMC;

    MailBoxMode = function () {
        $("#DetailView").hide();
        $("#ComposeView").hide();
        $("#MailBoxView").show("slide", { direction: "right" });
    };

    ComposeMode = function () {
        $("#MailBoxView").hide();
        $("#DetailView").hide();
        $("#ComposeView").show("slide", { direction: "right" });
        $("#SUBJECTCONTENT").val('');
        $('#BODYCONTENT').summernote('code', '');
        ajaxJsonHelper.get(constants.fasiApi.smc + 'Usuarios?filter=', null,
            function (data) {
                var data2 = $.map(data.Data, function (obj) {
                    obj.id = obj.Code;
                    obj.text = obj.Description;
                    return obj;
                });
                $('#RECEIVER').select2({
                    minimumResultsForSearch: 20,
                    placeholder: receiverPlaceHolder,
                    data: data2
                });
                if (!masterSupport.user.isEmployee) {
                    $('#RECEIVER').val(userDefaultSMC).trigger('change');
                    $('#RECEIVER').prop("disabled", true);
                }  
                else
                    $('#RECEIVER').val(null).trigger('change');
            }
        );
    };

    ComposeModeDraft = function (id) {
        $("#MailBoxView").hide();
        $("#DetailView").hide();
        $("#ComposeView").show("slide", { direction: "right" });
        ajaxJsonHelper.get(constants.fasiApi.smc + 'Message?id=' + id, null,
            function (data) {
                $('#DRAFTID').val(id);
                $("#SUBJECTCONTENT").val(data.Data.SUBJECT);
                $('#BODYCONTENT').summernote('code', data.Data.BODY);
                var receiver = data.Data.SMCRECEIVERS.map(function (item) {
                    return item.RECEIVER;
                });
                ajaxJsonHelper.get(constants.fasiApi.smc + 'Usuarios?filter=', null,
                    function (data) {
                        var data2 = $.map(data.Data, function (obj) {
                            obj.id = obj.Code;
                            obj.text = obj.Description;
                            return obj;
                        });
                        $('#RECEIVER').select2({
                            minimumResultsForSearch: 20,
                            placeholder: receiverPlaceHolder,
                            data: data2
                        });
                        if (!masterSupport.user.isEmployee) {
                            $('#RECEIVER').val(userDefaultSMC).trigger('change');
                            $('#RECEIVER').prop("disabled", true);
                        }
                        else
                            $('#RECEIVER').val(receiver).trigger('change');
                    }
                );
            }
        );
    };

    Discard = function () {
        if ($('#DRAFTID').val() != '') {
            $('#DRAFTID').val(null);
        } else {

        }
        MailBoxMode();
        Load();
    };

    GetIdCheck = function () {
        var ids = [];
        var tableTr = $(".table-mail tbody tr").filter(function (idx, el) {
            if ($(el).find('div.icheckbox_square-green').hasClass('checked')) {
                return $(this);
            }
        });
        $(tableTr).each(function () {
            ids.push($(this).attr("data-id"));
        });
        return ids;
    };

    MarkImportant = function () {
        var ids = GetIdCheck();
        ajaxJsonHelper.post(constants.fasiApi.smc + 'MarkImportant', JSON.stringify(ids), function (data) {
            if (data.Successfully === true) {
                MailBoxMode();
                currentView = this.id;
                Load();
            }
        }, function (error) {
            console.log(error)
        });
    };

    MarkRead = function () {
        var ids = GetIdCheck();
        ajaxJsonHelper.post(constants.fasiApi.smc + 'MarkRead', JSON.stringify(ids), function (data) {
            if (data.Successfully === true) {
                MailBoxMode();
                currentView = this.id;
                Load();
            }
        }, function (error) {
            console.log(error)
        });
    };

    MoveTrash = function () {
        var ids = GetIdCheck();
        ajaxJsonHelper.post(constants.fasiApi.smc + 'MoveTrash', JSON.stringify(ids), function (data) {
            if (data.Successfully === true) {
                MailBoxMode();
                currentView = this.id;
                Load();
            }
        }, function (error) {
            console.log(error)
        });
    };

    RFMail = function (isReply) {
        var typeText = isReply ? 'RE: ' : 'RV: ';
        $("#MailBoxView").hide();
        $("#DetailView").hide();
        $("#ComposeView").show("slide", { direction: "right" });
        if ($('#SUBJECT').html().indexOf(typeText) == -1)
            $("#SUBJECTCONTENT").val(typeText + $('#SUBJECT').html());
        else
            $("#SUBJECTCONTENT").val($('#SUBJECT').html());
        $('#BODYCONTENT').summernote('code', '<p><br></p>' + $('#BODY').html());
        ajaxJsonHelper.get(constants.fasiApi.smc + 'Usuarios?filter=', null,
            function (data) {
                var data2 = $.map(data.Data, function (obj) {
                    obj.id = obj.Code;
                    obj.text = obj.Description;
                    return obj;
                });
                $('#RECEIVER').select2({
                    minimumResultsForSearch: 20,
                    placeholder: receiverPlaceHolder,
                    data: data2
                });
                if (isReply)
                    $('#RECEIVER').val($('#SENDERID').html()).trigger('change');
            }
        );
    };

    ShowDetail = function (id, method) {
        ajaxJsonHelper.get(constants.fasiApi.smc + method + '?id=' + id, null,
            function (data) {
                if (data.Successfully) {
                    $('#MESSAGEID').html(id);
                    $('#SENDERID').html(data.Data.SENDER);
                    $('#SUBJECT').html(data.Data.SUBJECT);
                    $('#SENDERNAME').html(data.Data.FIRSTNAME);
                    $('#RECEIVED').html(generalSupport.ToJavaScriptDateCustom(data.Data.RECEIVED, 'DD/MM/YYYY'));
                    $('#BODY').html(data.Data.BODY);

                    $("#MailBoxView").hide();
                    $("#ComposeView").hide();
                    $("#DetailView").show("slide", { direction: "right" });
                }
                else {
                    //TODO: MOSTRAR MENSAJE CON EL ERROR
                    console.log(data.Reason);
                }
            }
        );
    };

    Load = function () {
        $(".table-mail tbody").html('');
        ajaxJsonHelper.get(constants.fasiApi.smc + 'MailBoxInformation', null,
            function (data) {
                data = data.Data;
  
                $("#inboxCount").html(data.InBox);                
                $("#SendMailCount").html(data.SendMail);
                $("#DraftCount").html(data.Drafts);
                $("#TrashCount").html(data.Trash);
                switch (currentView) {
                    case 'inboxlnk':
                        currentTitle += ' (' + data.InBox + ')';
                        break;
                    case 'sendedlnk':
                        currentTitle += ' (' + data.SendMail + ')';
                        break;
                    case 'draftlnk':
                        currentTitle += ' (' + data.Drafts + ')';
                        break;
                    case 'deletedlnk':
                        currentTitle += ' (' + data.Trash + ')';
                        break;
                }
                $('#BoxCurrentFolder').html(currentTitle);
            }
        );
        switch (currentView) {
            case 'inboxlnk':
                ajaxJsonHelper.get(constants.fasiApi.smc + 'InBox', null,
                    function (data) {
                        data.Data.forEach(function (element) {
                            AddRowTableMail(element);
                        });
                    });
                break;
            case 'sendedlnk':
                ajaxJsonHelper.get(constants.fasiApi.smc + 'SendItems', null,
                    function (data) {
                        data.Data.forEach(function (element) {
                            AddRowTableMail(element);
                        });
                    });
                break;
            case 'draftlnk':
                ajaxJsonHelper.get(constants.fasiApi.smc + 'Drafts', null,
                    function (data) {
                        data.Data.forEach(function (element) {
                            AddRowTableMail(element);
                        });
                    });
                break;
            case 'deletedlnk':
                ajaxJsonHelper.get(constants.fasiApi.smc + 'Trash', null,
                    function (data) {
                        data.Data.forEach(function (element) {
                            AddRowTableMail(element);
                        });
                    });
                break;
        }
    };

    SearchMessage = function () {
        var typeSearch;
        switch (currentView) {
            case 'inboxlnk':
                typeSearch = 1;
                break;
            case 'sendedlnk':
                typeSearch = 2;
                break;
            case 'draftlnk':
                typeSearch = 3;
                break;
            case 'deletedlnk':
                typeSearch = 4;
                break;
        }
        ajaxJsonHelper.get(constants.fasiApi.smc + 'SearchMessage?typeSearch=' + typeSearch + '&textSearch=' + $('#search').val(), null,
            function (data) {
                $(".table-mail tbody").html('');
                data.Data.forEach(function (element) {
                    AddRowTableMail(element);
                });
            });
        event.preventDefault();
    };

    SendMail = function (state) {
        if ($('#DRAFTID').val() != '') {
            SenMailDraft(state);
            return;
        }
        var item = DataConstruct(0, state);
        ajaxJsonHelper.post(constants.fasiApi.smc + 'Create', JSON.stringify(item), function (data) {
            if (data.Successfully === true) {
                MailBoxMode();
                currentView = this.id;
                Load();
            }
        }, function (error) {
            console.log(error)
        });
    };

    SenMailDraft = function (state) {
        var item = DataConstruct($('#DRAFTID').val(), state);
        ajaxJsonHelper.put(constants.fasiApi.smc + 'Update', JSON.stringify(item), function (data) {
            if (data.Successfully === true) {
                $('#DRAFTID').val(null);
                MailBoxMode();
                currentView = this.id;
                Load();
            }
        }, function (error) {
            console.log(error)
        });
    }

    DataConstruct = function (id, state) {
        var smcReceiver = new Array();
        var dateSended = new moment();
        $("#RECEIVER").val().forEach(function (value, index, array) {
            smcReceiver.push({
                "Id": id,
                "Receiver": value,
                "Unread": true,
                "MailJobId": "probando",
                "State": true
            });
        });
        var item = {
            Id: id,
            Receiver: 0,
            Subject: $("#SUBJECTCONTENT").val(),
            Body: $('#BODYCONTENT').summernote('code'),
            Priority: false,
            State: state,
            Unread: true,
            SourceId: id,
            Sended: dateSended.format("YYYY-MM-DDTHH:mm:ss"),
            MailJobId: "string",
            SmcReceivers: smcReceiver
        };
        return item;
    }

    Trash = function () {
        ajaxJsonHelper.put(constants.fasiApi.smc + 'ChangeStateMessage?id=' + $("#MESSAGEID").html() + '&state=3', null, function (data) {
            if (data.Successfully === true) {
                MailBoxMode();
                currentView = this.id;
                Load();
            }
        }, function (error) {
            console.log(error)
        });
    };

    SelectTypeDetail = function (id, state) {
        if (state == 1) {
            ComposeModeDraft(id);
        }
        else {
            ShowDetail(id, 'Message');
        }
    };

    GetUrlVars = function () {
        var hash = [];
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            urlVars.push(hash[0]);
            urlVars[hash[0]] = hash[1];
        }
    };

    GetDefaultUserId = function () {
        ajaxJsonHelper.get(constants.fasiApi.smc + 'GetDefaultUserId', null,
            function (data) {
                userDefaultSMC = data.Data;
            });
    };

    Init_Controls = function () {
        $('.folder-list li').click(function (e) {
            MailBoxMode();
            currentView = this.id;
            currentTitle = this.textContent.substring(0, this.textContent.lastIndexOf(' '));
            Load();
            event.preventDefault();
        });
        $('.compose-mail').click(function () {
            $('#DRAFTID').val(null);
            ComposeMode();
            event.preventDefault();
        });

        $(".table-mail tbody").on('click', '.mail-ontact', function (e) {
            SelectTypeDetail($(this).closest("tr").data("id"), $(this).closest("tr").find('td.mail-state').text());
        });

        $(".table-mail tbody").on('click', '.mail-subject', function (e) {
            SelectTypeDetail($(this).closest("tr").data("id"), $(this).closest("tr").find('td.mail-state').text());
        });

        $('#Refresh').click(function () {
            currentView = 'inboxlnk';
            Load();
        });

        $('#Send').click(function () {
            SendMail(2);
            event.preventDefault();
        });

        $('.summernote').summernote();

        GetUrlVars();
    };

    AddRowTableMail = function (data) {
        var row = '<tr class="@unread" data-id="@id">' +
            ' <td class="check-mail">' +
            '  <div class="icheckbox_square-green" style="position: relative;">' +
            '  <input type="checkbox" class="i-checks" style="position: absolute; opacity: 0;">' +
            '  <ins class="iCheck-helper" style="position: absolute; top: 0%; left: 0%; display: block; width: 100%; height: 100%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;"></ins>' +
            '  </div>' +
            ' </td>' +
            ' <td class="mail-ontact">@from@label</td>' +
            ' <td class="mail-subject">@subject</td>' +
            ' <td class="">@attach</td>' +
            ' <td class="text-right mail-date">@received</td>' +
            ' <td hidden class="mail-state">@state</td>' +
            '</tr>';
        row = row.replace(/@id/g, data.ID);
        if (data.UNREAD)
            row = row.replace('@unread', 'unread');
        else
            row = row.replace('@unread', 'read');
        row = row.replace('@from', data.FIRSTNAME);
        if (data.PRIORITY)
            row = row.replace('@label', '<span class="label label-warning pull-right">' + important + '</span>');
        else
            row = row.replace('@label', '');
        row = row.replace('@subject', data.SUBJECT);
        if (data.ATTACH)
            row = row.replace('@attach', '<i class="fa fa-paperclip">');
        else
            row = row.replace('@attach', '');
        row = row.replace('@received', generalSupport.ToJavaScriptDateCustom(data.SENDED, 'MMMM DD YYYY'));
        if (data.STATE)
            row = row.replace('@state', data.STATE);
        $(".table-mail tbody").append(row);
        $("div.icheckbox_square-green")
            .mouseenter(function () {
                $(this).addClass("hover");
            })
            .mouseleave(function () {
                $(this).removeClass("hover");
            })
            .click(function (e) {
                if ($(this).hasClass('checked')) {
                    $(this).removeClass("checked");
                } else {
                    $(this).addClass("checked");
                }
                e.preventDefault();
                e.stopImmediatePropagation();
            });
    };

    return {
        Init: function () {
            moment.locale(generalSupport.UserContext().languageName);
            generalSupport.TranslateInit(generalSupport.GetCurrentName(), function () {
                if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
                    masterSupport.setPageTitle($.i18n.t('app.title'));
                    currentTitle = $.i18n.t('app.form.InboxTitle');
                    receiverPlaceHolder = $.i18n.t('app.form.ReceiverMessage');
                    important = $.i18n.t('app.form.Important');
                }
            });
            Init_Controls();
            GetDefaultUserId();
            currentView = 'inboxlnk';
            Load();
            if (urlVars["id"] != undefined) {
                ShowDetail(urlVars["id"], 'ViewMessage');
            }
        }
    };
})();
$(function ($) {
    securitySupport.IsConnected();
});
$(document).ready(function () {
    app.smc.mailbox.Init();
});

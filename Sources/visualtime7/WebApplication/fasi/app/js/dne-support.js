// Summernote
var richTextOptions = {
    height: 150,
    minHeight: 150,
    maxHeight: 150,
    toolbar: [
        ['style', ['bold', 'italic', 'underline', 'clear']],
        ['font', ['strikethrough', 'superscript', 'subscript', 'fontname']],
        ['fontsize', ['fontsize']],
        ['color', ['color']],
        ['para', ['ul', 'ol', 'paragraph']]
    ],
    dialogsInBody: true,
    disableDragAndDrop: true
}

/**
 * Main class to manage properties of the DNE Control.
 */
var DNESupport = new function () {
    var isUserSuscriptor = false, provider = "", urlService = "";
    var notesCounter = 0, attachmentsCounter = 0, DNECounter = 0;

    /**
     * Formato para el popup de edición del recurso en la grilla.
     */
    this.DNEEditCommandFormatter = function (value, row) {
        return '<a href="javascript:void(0)" class="" title=' + $.i18n.t('DNEControl.form.Edit') + '>' + value + '</a>';
    }

    /**
     * Baja el contenido del archivo a la máquina cliente.
     */
    this.DNEDownloadFile = function (content, filename) {
        generalSupport.base64ToArrayBuffer(content, filename);
    }

    /**
     * Event for handling the add attachment event.
     */
    this.DNEAttachmentAddModalEvent = function (selector) {
        $('#' + selector + 'CreateBtn').off().click(function (e) {
            var addModal = $('#' + selector + 'AddModal');
            addModal.find('#' + selector + 'DNEFileDescription, #' + selector + 'DNEFileExpirationDate').val('');
            addModal.find('#' + selector + 'DNEFileInput').fileinput('refresh').fileinput('enable').fileinput('clear');
            addModal.modal('show');
        });
    }

    /**
     * Indica si el usuario es suscriptor.
     */
    this.DNEIsUserSuscriptor = function () {
        return $.ajax({
            type: "POST",
            url: "/fasi/wmethods/User.aspx/IsUserSuscriptor",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                isUserSuscriptor = data.d;
            }
        });
    }

    /**
     * Todas las peticiones al servicio dependen del proveedor.
     */
    this.DNEGetProvider = function () {
        return $.ajax({
            type: "POST",
            url: "/fasi/wmethods/General.aspx/SettingValue",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ "name": "DNEProvider" }),
            success: function (data) {
                provider = data.d;
            }
        });
    }

    /**
     * Gets the endpoint of the service.
     */
    this.DNEGetUrl = function () {
        return $.ajax({
            type: "POST",
            url: "/fasi/wmethods/General.aspx/SettingValue",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ "name": "DNE.URL" }),
            success: function (data) {
                urlService = data.d;
            }
        });
    }

    /**
     * Evento para el botón refresh. Obtiene nuevamente la información del servidor.
     */
    this.DNERefreshButtonEvent = function (selector, selectNotesOnly, tags, isTemporary) {
        $('#' + selector + 'RefreshBtn').off().click(function (e) {
            DNESupport.DNERetrieveData(selector, selectNotesOnly, tags, isTemporary);
        });
    }

    /**
     * Obtiene la información del servicio de acuerdo a la secuencia y tags.
     */
    this.DNERetrieveData = function (selector, selectNotesOnly, tags, isTemporary) {
        $.LoadingOverlay("show");

        var operation = DNESupport.DNEGetRetrieveOperation(isTemporary);
        var sequenceId = $("#" + selector + "SequenceId").val();

        return $.ajax({
            type: "POST",
            url: urlService + operation, //"GetActiveResourceSequenceAndFormTemporals", //operation,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            crossDomain: true,
            headers: {
                'Accept-Language': generalSupport.LanguageName()
            },
            data: JSON.stringify({
                sequenceId: (sequenceId === "") ? 0 : sequenceId,
                shortTags: tags,
                selectNotesOnly: selectNotesOnly,
                provider: provider
            }),
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + app.user.token);
            },
            success: function (data) {
                //console.log(data);

                if (selectNotesOnly === 0) {
                    // Attachments
                    DNESupport.DNEAttachmentAddModalEvent(selector);
                    $('#' + selector + 'Tbl').bootstrapTable('load', data);
                } else {
                    // Notes
                    DNESupport.DNEDrawNotes(selector, data, isTemporary);
                }
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            },
            complete: function () {
                $.LoadingOverlay("hide");
            }
        });
    }

    /**
     * Luego de obtener las notas, se deben pintar en el contenedor.
     */
    this.DNEDrawNotes = function (selector, resource, isTemporary) {
        var notes = "";
        var expirationDate = "", updateDate = "", creationDate = "";
        $.each(resource, function (index, value) {
            updateDate = ((value.UpdateDate !== null) ? generalSupport.ToJavaScriptDateCustom(value.UpdateDate, generalSupport.DateFormat()) : "");
            creationDate = ((value.CreationDate !== null) ? generalSupport.ToJavaScriptDateCustom(value.CreationDate, generalSupport.DateFormat()) : "");
            expirationDate = ((value.ExpirationDate !== null) ? generalSupport.ToJavaScriptDateCustom(value.ExpirationDate, generalSupport.DateFormat()) : "");
            notes += '<li class="' + (index & 1 === 1 ? 'info' : 'success') + '-element note-element-form-designer" id="' + value.ConsequenceId + '">' +
                '<div class="note">' +
                '<h4 class="noteDescription">' + value.Description + '</h4>' +
                '<div class="noteContent">' + ((value.Note !== null) ? value.Note.Content : "") + '</div>' +
                '</div>' +
                '<div class="note-footer-element-form-designer">' +
                '<hr>' +
                '<i class="fa fa-clock-o"></i> ' + $.i18n.t('DNEControl.form.UpdateDate') + ': ' + updateDate +
                '<br><i class="fa fa-user"></i> ' + $.i18n.t('DNEControl.form.UpdateUser') + ': ' + value.UpdateUserName +
                '<br><i class="fa fa-clock-o"></i> ' + $.i18n.t('DNEControl.form.CreationDate') + ': ' + creationDate +
                '<br><i class="fa fa-user"></i> ' + $.i18n.t('DNEControl.form.CreationUser') + ': ' + value.CreatorUserName +
                '<br><i class="fa fa-clock-o expirationDate" data-expirationdate="' + expirationDate + '"></i> ' + $.i18n.t('DNEControl.form.ExpirationDate') + ': ' + expirationDate +
                '<a href="#" class="pull-right btn btn-xs btn-danger ' + selector + 'NoteDelete"><i class="fa fa-trash"></i> ' + $.i18n.t('DNEControl.form.Delete') + '</a>' +
                '<a href="#" class="pull-right btn btn-xs btn-primary ' + selector + 'NoteEdit"><i class="fa fa-pencil-square-o"></i> ' + $.i18n.t('DNEControl.form.Edit') + '</a>' +
                '<a href="#" class="pull-right btn btn-xs btn-default ' + selector + 'NoteSee"><i class="fa fa-search"></i> ' + $.i18n.t('DNEControl.form.See') + '</a>' +
                '</div>' +
                '</li>';
        });

        $('#' + selector + 'NoteList').empty().append(notes);
        $('.' + selector + 'NoteEdit').off().click(function (e) {
            DNESupport.DNENoteEditEvent(selector, resource, $(this).parent().parent());
        });
        $('.' + selector + 'NoteDelete').off().click(function (e) {
            DNESupport.DNENoteDeleteEvent(selector, $(this).parent().parent().attr('id'), isTemporary)
        });
        $('.' + selector + 'NoteSee').off().click(function () {
            DNESupport.DNENoteSeeEvent(selector, $(this).parent().parent())
        });
    }

    /**
     * Handler para ver las notas.
     */
    this.DNENoteSeeEvent = function (selector, noteSeeSelected) {
        var noteSeeModal = $('#' + selector + 'SeeModal');
        noteSeeModal.find('#' + selector + 'SeeModalNoteContent').html(noteSeeSelected.find('.noteContent').html());
        noteSeeModal.css("word-break", "break-all");
        noteSeeModal.modal('show');
    }

    /**
     * Handler para edicion de notas.
     */
    this.DNENoteEditEvent = function (selector, retrievedNotes, noteEditSelected) {
        var noteEditModal = $('#' + selector + 'EditModal');
        var noteEditModalDescription = noteEditModal.find('#' + selector + 'EditModalDescription');
        var noteEditModalExpirationDate = noteEditModal.find('#' + selector + 'EditExpirationDate');
        var noteEditModalContent = noteEditModal.find('#' + selector + 'EditModalNoteContent');
        var composedResourceKey = {
            SequenceId: $("#" + selector + "SequenceId").val(),
            ConsequenceId: noteEditSelected.attr('id')
        };

        noteEditModalDescription.val(noteEditSelected.find('.noteDescription').text());
        noteEditModalExpirationDate.val(noteEditSelected.find('.expirationDate').data('expirationdate'));
        noteEditModalContent.summernote('code', noteEditSelected.find('.noteContent').html());
        noteEditModal.modal('show');

        $('#' + selector + 'EditModalBtn').off().click(function (e) {
            var resource = retrievedNotes.filter(function (note) { return note.Note.ConsequenceId.toString() === composedResourceKey.ConsequenceId; })[0];
            resource.Description = noteEditModalDescription.val();
            resource.Note.Content = noteEditModalContent.summernote('code');

            if (resource.Description === "") {
                toastr.error($.i18n.t('DNEControl.validation.DescriptionCannotBeEmpty'), '', { positionClass: "toast-bottom-right" });
            } else {
                if (resource.Description.length > 100) {
                    toastr.error($.i18n.t('DNEControl.validation.DescriptionCannotBeEmpty'), '', { positionClass: "toast-bottom-right" });
                } else {
                    // if expiration date if after today, then.
                    // Date can be empty.
                    if ((noteEditModalExpirationDate.val() === "") || (moment(noteEditModalExpirationDate.val(), generalSupport.DateFormat()).isAfter(moment(new Date())))) {
                        try {
                            resource.ExpirationDate = ((noteEditModalExpirationDate.val() !== "") ? generalSupport.jsDateToWCF(noteEditModalExpirationDate.val(), generalSupport.DateFormat()) : null);
                        } catch (e) {
                            resource.ExpirationDate = "";
                            console.log("invalid date");
                        }

                        DNESupport.DNEEditResource(resource).done(function () {
                            DNESupport.DNERefreshGrid(selector);
                            noteEditModal.modal('hide');
                        });
                    } else {
                        toastr.error($.i18n.t('DNEControl.validation.InvalidExpirationDate'), '', { positionClass: "toast-bottom-right" });
                    }
                }
            }
        });
    }

    /**
     * Handler para borrado de notas.
     */
    this.DNENoteDeleteEvent = function (selector, consequenceId, isTemporary) {
        var noteDeleteModal = $('#' + selector + 'DeleteModal');
        var composedResourceKey = {
            SequenceId: $("#" + selector + "SequenceId").val(),
            ConsequenceId: consequenceId
        };

        noteDeleteModal.modal('show');
        $('#' + selector + 'DeleteModalBtn').off().click(function (e) {
            DNESupport.DNEDeleteResource(composedResourceKey, DNESupport.DNEGetDeleteOperation(isTemporary)).done(function () {
                DNESupport.DNERefreshGrid(selector);
                noteDeleteModal.modal('hide');
            });
        });
    }

    /**
     * Handler para agregado de notas.
     */
    this.DNEAddNoteEvent = function (selector, tags, isTemporary) {
        var noteSelector = $('#' + selector + 'AddModalNoteContent');

        noteSelector.off().summernote(richTextOptions);
        $('#' + selector + 'CreateBtn').off().click(function (e) {
            var addModal = $('#' + selector + 'AddModal');
            addModal.find('#' + selector + 'AddModalDescription').val('');
            addModal.find('#' + selector + 'AddModalExpirationDate').val('');
            noteSelector.summernote('code', '');
            addModal.modal('show');

            $('#' + selector + 'AddModalBtn').off().click(function () {
                var sequenceOnSelector = $("#" + selector + "SequenceId").val();
                var note = {
                    SequenceId: sequenceOnSelector,
                    Content: noteSelector.summernote('code'),
                }
                var resource = {
                    SequenceId: sequenceOnSelector,
                    Description: $('#' + selector + 'AddModalDescription').val(),
                    ResourceTypeId: 5, // enum note
                    ClientAssociatedCompany: "1",
                    ClientAssociatedPerson: "1",
                    Name: $('#' + selector + 'AddModalDescription').val(),
                    Tags: tags,
                    Note: note,
                    ExpirationDate: addModal.find('#' + selector + 'AddModalExpirationDate').val()
                }

                if (resource.Description === "") {
                    toastr.error($.i18n.t('DNEControl.validation.DescriptionCannotBeEmpty'), '', { positionClass: "toast-bottom-right" });
                } else {
                    if (resource.Description.length > 100) {
                        toastr.error($.i18n.t('DNEControl.validation.DescriptionCannotBeEmpty'), '', { positionClass: "toast-bottom-right" });
                    } else {
                        // if expiration date if after today, then.
                        // Date can be empty.
                        if ((resource.ExpirationDate === "") || (moment(resource.ExpirationDate, generalSupport.DateFormat()).isAfter(moment(new Date())))) {
                            try {
                                resource.ExpirationDate = ((resource.ExpirationDate !== "") ? generalSupport.jsDateToWCF(resource.ExpirationDate, generalSupport.DateFormat()) : null);
                            } catch (e) {
                                resource.ExpirationDate = "";
                                console.log("invalid date");
                            }

                            if (sequenceOnSelector.toString() !== "0") {
                                DNESupport.DNEAddResource(selector, resource, DNESupport.DNEGetAddOperation(isTemporary)).done(function () {
                                    addModal.modal('hide');
                                    DNESupport.DNERefreshGrid(selector);
                                });
                            } else {
                                DNESupport.DNEGetSequenceId(selector).done(function (data) {
                                    note.SequenceId = data;
                                    resource.SequenceId = data;

                                    DNESupport.DNEAddResource(selector, resource, DNESupport.DNEGetAddOperation(isTemporary)).done(function () {
                                        addModal.modal('hide');
                                        DNESupport.DNERefreshGrid(selector);
                                    });
                                });
                            }
                        } else {
                            toastr.error($.i18n.t('DNEControl.validation.InvalidExpirationDate'), '', { positionClass: "toast-bottom-right" });
                        }
                    }
                }
            });
        });
    }

    /**
     * Retorna un valor de la secuencia si éste es cero.
     */
    this.DNEGetSequenceId = function (selector) {
        return $.ajax({
            type: "GET",
            url: urlService + "GenerateSequence",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { provider: provider },
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + app.user.token);
            },
            success: function (data) {
                $('#' + selector + 'SequenceId').val(data);
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            },
            complete: function () {
                $.LoadingOverlay("hide");
            }
        });
    }

    /**
     * Loads the language text on the address control.
     */
    this.LoadTextOnForm = function (selector, notes) {
        $('#' + selector + 'Tbl').find("th[data-field='Name']").find('div').first().html($.i18n.t("DNEControl.form.Name"));
        $('#' + selector + 'Tbl').find("th[data-field='Description']").find('div').first().html($.i18n.t("DNEControl.form.Description"));
        $('#' + selector + 'Tbl').find("th[data-field='ResourceTypeId']").find('div').first().html($.i18n.t("DNEControl.form.ResourceTypeId"));
        $('#' + selector + 'Tbl').find("th[data-field='UpdateUserCode']").find('div').first().html($.i18n.t("DNEControl.form.UpdateUserCode"));
        $('#' + selector + 'Tbl').find("th[data-field='UpdateDate']").find('div').first().html($.i18n.t("DNEControl.form.UpdateDate"));
        $('#' + selector + 'Tbl').find("th[data-field='CreatorUserCode']").find('div').first().html($.i18n.t("DNEControl.form.CreatorUserCode"));
        $('#' + selector + 'Tbl').find("th[data-field='CreationDate']").find('div').first().html($.i18n.t("DNEControl.form.CreationDate"));
        $('#' + selector + 'Tbl').find("th[data-field='Download']").find('div').first().html($.i18n.t("DNEControl.form.Download"));

        $('#' + selector + 'CreateBtn').append(' ' + $.i18n.t("DNEControl.form.Add"));
        $('#' + selector + 'RefreshBtn').append(' ' + $.i18n.t("DNEControl.form.Refresh"));
        $('#' + selector + 'SignatureWrapper').find("h1").append(' ' + $.i18n.t("DNEControl.form.ESignature"));
        $('#' + selector + 'SignatureWrapper').find("p").append(' ' + $.i18n.t("DNEControl.form.ESignature-description"));
        $('#' + selector + 'Signature-add').append(' ' + $.i18n.t("DNEControl.form.ESignature-add"));
        $('#' + selector + 'Signature-clear').append(' ' + $.i18n.t("DNEControl.form.ESignature-clear"));
        document.getElementById(selector + 'CreateBtn').title = $.i18n.t("DNEControl.form.Add");
        document.getElementById(selector + 'RefreshBtn').title = $.i18n.t("DNEControl.form.Refresh");

        if (notes) {
            $('#' + selector + 'DeleteModal').find("h4.modal-title").html($.i18n.t("DNEControl.form.ConfirmElimination"));
            $('#' + selector + 'DeleteModal').find("p.error-text").html($.i18n.t("DNEControl.form.SureWantToEliminate"));
            $('#' + selector + 'DeleteModalBtn').append(' ' + $.i18n.t("DNEControl.form.Delete"));
            document.getElementById(selector + 'DeleteModalBtn').title = $.i18n.t("DNEControl.form.Delete");
            $('#' + selector + 'DeleteModal').find(".modal-footer").find(".btn").last().append(' ' + $.i18n.t("DNEControl.form.Cancel"));

            $('#' + selector + 'AddModal').find("h4.modal-title").html($.i18n.t("DNEControl.form.Add"));
            $('#' + selector + 'AddModalDescriptionLabel').html($.i18n.t("DNEControl.form.Description"));
            document.getElementById(selector + 'AddModalDescription').title = $.i18n.t("DNEControl.form.Description");
            // $('#' + selector + 'AddModalNoteContentLabel').html($.i18n.t("DNEControl.form.Content"));
            // document.getElementById(selector + 'AddModalNoteContent').title = $.i18n.t("DNEControl.form.Content");
            $('#' + selector + 'AddModalBtn').append(' ' + $.i18n.t("DNEControl.form.Save"));
            document.getElementById(selector + 'AddModalBtn').title = $.i18n.t("DNEControl.form.Save");
            $('#' + selector + 'AddModal').find(".modal-footer").find(".btn").last().append(' ' + $.i18n.t("DNEControl.form.Close"));

            $('#' + selector + 'EditModal').find("h4.modal-title").html($.i18n.t("DNEControl.form.Edit"));
            $('#' + selector + 'EditModalDescriptionLabel').html($.i18n.t("DNEControl.form.Description"));
            document.getElementById(selector + 'EditModalDescription').title = $.i18n.t("DNEControl.form.Description");
            $('#' + selector + 'EditExpirationDateLabel').html($.i18n.t("DNEControl.form.ExpirationDate"));
            document.getElementById(selector + 'EditExpirationDate').title = $.i18n.t("DNEControl.form.ExpirationDate");
            // $('#' + selector + 'EditModalNoteContentLabel').html($.i18n.t("DNEControl.form.Content"));
            // document.getElementById(selector + 'EditModalNoteContent').title = $.i18n.t("DNEControl.form.Content");
            $('#' + selector + 'EditModalBtn').append(' ' + $.i18n.t("DNEControl.form.Update"));
            document.getElementById(selector + 'EditModalBtn').title = $.i18n.t("DNEControl.form.Update");
            $('#' + selector + 'EditModal').find(".modal-footer").find(".btn").last().append(' ' + $.i18n.t("DNEControl.form.Close"));

            $('#' + selector + 'SeeModal').find("h4.modal-title").html($.i18n.t("DNEControl.form.FullNote"));
            // $('#' + selector + 'SeeModalNoteContent').html($.i18n.t("DNEControl.form.Content"));
            // document.getElementById(selector + 'SeeModalNoteContent').title = $.i18n.t("DNEControl.form.Content");
            $('#' + selector + 'SeeModal').find(".modal-footer").find(".btn").last().append(' ' + $.i18n.t("DNEControl.form.Close"));
        } else {
            $('#' + selector + 'RemoveBtn').append(' ' + $.i18n.t("DNEControl.form.Delete"));
            document.getElementById(selector + 'RemoveBtn').title = $.i18n.t("DNEControl.form.Delete");

            $('#' + selector + 'Delete').find("h4.modal-title").html($.i18n.t("DNEControl.form.ConfirmElimination"));
            $('#' + selector + 'Delete').find("p.error-text").html($.i18n.t("DNEControl.form.SureWantToEliminate"));
            $('#' + selector + 'DeleteAction').append(' ' + $.i18n.t("DNEControl.form.Delete"));
            document.getElementById(selector + 'DeleteAction').title = $.i18n.t("DNEControl.form.Delete");
            $('#' + selector + 'Delete').find(".modal-footer").find(".btn").last().append(' ' + $.i18n.t("DNEControl.form.Cancel"));

            $('#' + selector + 'AddModal').find("h4.modal-title").html($.i18n.t("DNEControl.form.Add"));
            $('#' + selector + 'AddForm').find('#' + selector + 'DNEFileDescriptionLabel').html($.i18n.t("DNEControl.form.Description"));
            document.getElementById(selector + 'DNEFileDescription').title = $.i18n.t("DNEControl.form.Description");
            $('#' + selector + 'AddForm').find('#' + selector + 'DNEFileExpirationDateLabel').html($.i18n.t("DNEControl.form.ExpirationDate"));
            document.getElementById(selector + 'DNEFileExpirationDate').title = $.i18n.t("DNEControl.form.ExpirationDate");
            $('#' + selector + 'AddForm').find('#' + selector + 'DNEFileInputLabel').html($.i18n.t("DNEControl.form.File"));
            $('#' + selector + 'AddModal').find(".modal-footer").find(".btn").last().append(' ' + $.i18n.t("DNEControl.form.Close"));

            $('#' + selector + 'EditForm').find("h4.modal-title").html($.i18n.t("DNEControl.form.Detail"));
            $('#' + selector + 'EditFormDescriptionLabel').html($.i18n.t("DNEControl.form.Description"));
            document.getElementById(selector + 'EditFormDescription').title = $.i18n.t("DNEControl.form.Description");
            $('#' + selector + 'EditFormUpdateUsercodeLabel').html($.i18n.t("DNEControl.form.UpdateUserName"));
            document.getElementById(selector + 'EditFormUpdateUsercode').title = $.i18n.t("DNEControl.form.UpdateUserName");
            $('#' + selector + 'EditFormUpdateDateLabel').html($.i18n.t("DNEControl.form.UpdateDate"));
            document.getElementById(selector + 'EditFormUpdateDate').title = $.i18n.t("DNEControl.form.UpdateDate");
            $('#' + selector + 'EditFormCreatorUsercodeLabel').html($.i18n.t("DNEControl.form.CreatorUserName"));
            document.getElementById(selector + 'EditFormCreatorUsercode').title = $.i18n.t("DNEControl.form.CreatorUserName");
            $('#' + selector + 'EditFormCreationDateLabel').html($.i18n.t("DNEControl.form.CreationDate"));
            document.getElementById(selector + 'EditFormCreationDate').title = $.i18n.t("DNEControl.form.CreationDate");
            $('#' + selector + 'EditFormExpirationDateLabel').html($.i18n.t("DNEControl.form.ExpirationDate"));
            document.getElementById(selector + 'EditFormExpirationDate').title = $.i18n.t("DNEControl.form.ExpirationDate");
            $('#' + selector + 'SaveEditForm').append(' ' + $.i18n.t("DNEControl.form.Update"));
            document.getElementById(selector + 'SaveEditForm').title = $.i18n.t("DNEControl.form.Update");
            $('#' + selector + 'EditForm').find(".modal-footer").find(".btn").last().append(' ' + $.i18n.t("DNEControl.form.Close"));
        }
    }

	/**
     * Gets the language for the control.
     */
    this.LoadDefaultValues = function (selector, sequenceId, tags, onlyNotes, isTemporary, alwaysRefreshLanguage, isVisible, isEnabled, showSignature) {
        $("#" + selector + "SequenceId").val(sequenceId);
        $("#" + selector + "SequenceId").data('isTemporary', isTemporary);
        $("#" + selector + "AddModalDescription, #" + selector + "EditModalDescription, #" + selector + "EditFormDescription").attr('maxlength', '100');

        // calendar initiators
        $('#' + selector + 'EditFormExpirationDate-group, #' + selector + 'DNEFileExpirationDate-group, #' + selector + 'EditExpirationDate-group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: generalSupport.LanguageName()
        });

        DNESupport.DNERetrieveData(selector, onlyNotes, tags, isTemporary).done(function () {
            DNESupport.DNERefreshButtonEvent(selector, onlyNotes, tags, isTemporary);

            // Refresh the data on the table inside the requirements tab on underwriting panel
            if (onlyNotes) { // notes
                DNESupport.DNEAddNoteEvent(selector, tags, isTemporary);

                if (notesCounter === 0) {
                    DNESupport.LoadTextOnForm(selector, true);

                    var expirationDateOnAddForm = DNESupport.GetExpirationDateOnAdd(selector);
                    $("#" + selector + "AddForm").find(".row").find(".col-md-12").first().after(expirationDateOnAddForm);
                    $('#' + selector + 'AddModalExpirationDate-group').datetimepicker({
                        format: generalSupport.DateFormat(),
                        locale: generalSupport.LanguageName()
                    });

                    notesCounter++;
                }

                DNESupport.DNENotesVisible(selector, isVisible);
                DNESupport.DNENotesEnable(selector, isEnabled);
            } else { // documents
                if ((alwaysRefreshLanguage) || (attachmentsCounter === 0)) {
                    DNESupport.DNEGridEvents(selector, true, isTemporary);
                    DNESupport.LoadTextOnForm(selector, false);
                }

                if ((attachmentsCounter === 0) && (!alwaysRefreshLanguage)) { // normal behavior of the control.
                    DNESupport.DNEGridEvents(selector, true, isTemporary);
                    attachmentsCounter++;
                }

                DNESupport.DNEFileEventInitialization(selector, tags, isTemporary);
                DNESupport.DNEAttachmentsVisible(selector, isVisible);
                DNESupport.DNEAttachmentsEnable(selector, isEnabled);
            }

            DNESupport.initializeSignature(selector, tags, isTemporary, showSignature);
        });
    }

    /**
     * Se inicializan variables locales ya que no en todas las funciones se disponen de ellas cuando se llaman internamente.
     */
    this.DNEInitialization = function (selector, sequenceId, tags, onlyNotes, isTemporary, supporObj, alwaysRefreshLanguage, isVisible, isEnabled, showSignature) {
        // Default value true is null or undefined, otherwise, sets the value.
        isVisible = (isVisible === null || isVisible === undefined) ? true : isVisible;
        isEnabled = (isEnabled === null || isEnabled === undefined) ? true : isEnabled;
        showSignature = (isVisible === null || isVisible === undefined) ? false : showSignature;
        
        // Language should only be loaded once.
        if (DNECounter === 0) {
            // Load the JSON File
            $.when(
                DNESupport.DNEGetProvider(),
                DNESupport.DNEGetUrl(),
                DNESupport.DNEIsUserSuscriptor(),
                $.ajax("/fasi/locales/DNEControlBase." + generalSupport.LanguageName() + ".json")).done(function (v1, v2, v3, DNEResource) {
                    $.i18n.addResourceBundle(generalSupport.LanguageName(), 'translation', DNEResource[0], true, true);

                    DNESupport.LoadDefaultValues(selector, sequenceId, tags, onlyNotes, isTemporary, alwaysRefreshLanguage, isVisible, isEnabled, showSignature);
                    DNECounter++;
                });
        } else {
            DNESupport.LoadDefaultValues(selector, sequenceId, tags, onlyNotes, isTemporary, alwaysRefreshLanguage, isVisible, isEnabled, showSignature);
        }
    }

    /**
     * Return the snippet of code for the expiration date on add modals.
     */
    this.GetExpirationDateOnAdd = function (selector) {
        return '<div class="col-md-12">' +
            '<div class="form-group">' +
            '<div class="col-md-4 text-left">' +
            '<label id="' + selector + 'AddModalExpirationDateLabel" class="control-label"' +
            'for="' + selector + 'AddModalExpirationDate">' + $.i18n.t("DNEControl.form.ExpirationDate") + '</label>' +
            '</div>' +
            '<div class="col-md-8">' +
            '<div class="input-group date" id="' + selector + 'AddModalExpirationDate-group">' +
            '<input id="' + selector + 'AddModalExpirationDate" name="' + selector + 'AddModalExpirationDate"' +
            'type="text" class="form-control" size="10" maxlength="10" title="' + $.i18n.t("DNEControl.form.ExpirationDate") + '">' +
            '<span class="input-group-addon">' +
            '<span class="glyphicon glyphicon-calendar"></span>' +
            '</span>' +
            '</div>' +
            '</div>' +
            '</div>' +
            '</div>';
    }

    /**
     * Eventos de la grilla para los attachments.
     */
    this.DNEGridEvents = function (selector, enable, isTemporary) {
        var table = $('#' + selector + 'Tbl');
        table.bootstrapTable().off();
        if (enable) {
            table.on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function (row, element) {
                $('#' + selector + 'RemoveBtn').prop('disabled', !table.bootstrapTable('getSelections').length);
            }).on('click-cell.bs.table', function (field, value, row, element) {
                if (value === "Download")
                    DNESupport.DNERequestResourceContent(element);
            });

            $('#' + selector + 'Delete').find('#' + selector + 'DeleteAction').off().click(function (e) {
                DNESupport.DNEDeleteResourceFromTable(selector, isTemporary);
                e.preventDefault(); // cancel default behavior
            });

            $('#' + selector + 'RemoveBtn').prop('disabled', !table.bootstrapTable('getSelections').length);
        } else {
            $('#' + selector + 'Delete').find('#' + selector + 'DeleteAction').off();
        }

        // Detalle siempre esta disponible.
        table.on('click-cell.bs.table', function (field, value, row, element) {
            if (value === "Name")
                DNESupport.DNEEditResourceEvent(selector, element);
        });
    }

    /**
     * Retorna la operación de borrado de acuerdo a si es temporal o no.
     */
    this.DNEGetDeleteOperation = function (isTemporary) {
        return (isTemporary === true) ? "DeleteResourceTemporarily" : "DeleteResource";
    }

    /**
     * Retorna la operación de agregado de acuerdo a si es temporal o no.
     */
    this.DNEGetAddOperation = function (isTemporary) {
        return (isTemporary === true) ? "AddResourceWithTemporaryState" : "AddResource";
    }

    /**
     * Retorna la operación de obtener de acuerdo a si es temporal o no.
     */
    this.DNEGetRetrieveOperation = function (isTemporary) {
        if (isUserSuscriptor)
            return "GetActiveResourceSequenceAndMyTemporals";
        else
            return (isTemporary === true) ? "GetOwnResourceSequenceTemporaryStateOnly" : "GetOwnResourceSequenceActiveAndTemporaryState";
    }

    /**
     * Inicialización del evento para el fileinput.
     */
    this.DNEFileEventInitialization = function (selector, tags, isTemporary) {
        var fileInput = $('#' + selector + 'DNEFileInput');
        fileInput.off().fileinput('destroy').fileinput({
            uploadUrl: '/fasi/handlers/DNEUploadFileHandler.ashx',
            language: generalSupport.LanguageName(),
            // se configura la cantidad máxima de archivos permitidos
            maxFileCount: 5,
            // se configura los botones a mostrar en cada archivo cargado
            fileActionSettings: {
                showRemove: false,
                showUpload: true,
                showDownload: true,
                showZoom: false,
                showDrag: false
            },
            uploadExtraData: function () {
                return {
                    selector: selector,
                    description: $('#' + selector + 'DNEFileDescription').val(),
                    expirationDate: $('#' + selector + 'DNEFileExpirationDate').val(),
                    sequenceId: $("#" + selector + "SequenceId").val(),
                    tags: JSON.stringify(tags),
                    operationName: DNESupport.DNEGetAddOperation(isTemporary),
                    provider: provider,
                    token: app.user.token,
                    formatDate: generalSupport.DateFormat(),
                    extensionNotAllowedMessage: $.i18n.t('DNEControl.validation.ExtensionNotAllowed'),
                    expirationDateErrorMessage: $.i18n.t('DNEControl.validation.InvalidExpirationDate')
                }
            }
        }).on('filepreajax', function (event, previewId, index) {
            //console.log($('#' + selector + 'DNEFileExpirationDate').val());
        }).on('fileuploaded', function (event, data, previewId, index) {
            // Luego que se suben todos los archivos se limpia el contenido
            if (data.filescount === 1) {
                fileInput.fileinput('refresh').fileinput('enable').fileinput('clear');
                $('#' + selector + 'DNEFileDescription').val('');
                $('#' + selector + 'DNEFileExpirationDate').val('');
            }

            var sequenceOnSelector = $("#" + selector + "SequenceId");

            if (sequenceOnSelector.val().toString() === "0")
                sequenceOnSelector.val(data.response.id);

            // grid always have to be refreshed.
            DNESupport.DNERefreshGrid(selector);
        }).on('fileerror', function (event, data, msg) {
            //console.log("id: ", data.id, " index: ", data.index, " file: ", data.file, " reader: ", data.reader, " files: ", data.files, " message: ", msg);
        });
    }

    /**
     * Borra el / los recursos seleccionados.
     */
    this.DNEDeleteResourceFromTable = function (selector, isTemporary) {
        var keys = $.map($('#' + selector + 'Tbl').bootstrapTable('getSelections'), function (row) {
            return {
                SequenceId: row.SequenceId,
                ConsequenceId: row.ConsequenceId
            };
        });

        var operation = DNESupport.DNEGetDeleteOperation(isTemporary);

        $.each(keys, function (index, value) {
            DNESupport.DNEDeleteResource(value, operation).done(function (v1) {
                DNESupport.DNERefreshGrid(selector);
            });
        });

        $('#' + selector + 'Btn').prop('disabled', true);
    }

    /**
     * Calls the service to add the resouce.
     */
    this.DNEAddResource = function (selector, resource, operation) {
        $.LoadingOverlay("show");
        return $.ajax({
            type: "POST",
            url: urlService + operation,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                resource: resource,
                provider: provider
            }),
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + app.user.token);
            },
            success: function (data) { },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            },
            complete: function () {
                $.LoadingOverlay("hide");
            }
        });
    }

    /**
     * Calls the service to delete the resource.
     */
    this.DNEDeleteResource = function (key, operation) {
        $.LoadingOverlay("show");
        return $.ajax({
            type: "POST",
            url: urlService + operation,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                composedResourceKey: key,
                provider: provider
            }),
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + app.user.token);
            },
            success: function () { },
            error: function (qXHR, textStatus, errorThrown) {
                //generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                toastr.error($.i18n.t('DNEControl.form.AccessDenied'), '', { positionClass: "toast-bottom-right" });
            },
            complete: function () {
                $.LoadingOverlay("hide");
            }
        });
    }

    /**
     * Solicita el contenido del recurso al servicio, devuelve un arreglo de bytes.
     */
    this.DNERequestResourceContent = function (resource) {
        var composedResourceKey = {
            SequenceId: resource.SequenceId,
            ConsequenceId: resource.ConsequenceId
        };
        $.LoadingOverlay("show");
        $.ajax({
            type: "POST",
            url: urlService + "GetResourceContent",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                composedResourceKey: composedResourceKey,
                provider: provider
            }),
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + app.user.token);
            },
            success: function (data) {
                DNESupport.DNEDownloadFile(data, resource.Name)
            },
            error: function (qXHR, textStatus, errorThrown) {
                generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
            },
            complete: function () {
                $.LoadingOverlay("hide");
            }
        });
    }

    /**
     * Evento para edición de adjuntos.
     */
    this.DNEEditResourceEvent = function (selector, resource) {
        var editModal = $('#' + selector + 'EditForm');
        editModal.find('#' + selector + 'EditFormDescription').val(resource.Description);
        editModal.find('#' + selector + 'EditFormUpdateUsercode').val(resource.UpdateUserName).attr('disabled', true);
        editModal.find('#' + selector + 'EditFormCreatorUsercode').val(resource.CreatorUserName).attr('disabled', true);
        editModal.find('#' + selector + 'EditFormUpdateDate').val((resource.UpdateDate !== null) ? generalSupport.ToJavaScriptDateCustom(resource.UpdateDate, generalSupport.DateFormat()) : "").attr('disabled', true);
        editModal.find('#' + selector + 'EditFormCreationDate').val((resource.CreationDate !== null) ? generalSupport.ToJavaScriptDateCustom(resource.CreationDate, generalSupport.DateFormat()) : "").attr('disabled', true);
        editModal.find('#' + selector + 'EditFormExpirationDate').val((resource.ExpirationDate !== null) ? generalSupport.ToJavaScriptDateCustom(resource.ExpirationDate, generalSupport.DateFormat()) : "");

        // Unbinds the on click event of the save.
        editModal.modal('show').on('hidden.bs.modal', function (e) {
            $('#' + selector + 'SaveEditForm').off();
        });

        $('#' + selector + 'SaveEditForm').off().click(function (e) {
            // Solo se puede cambiar el valor de la descripción del recurso y fecha de expiración.
            resource.Description = $('#' + selector + 'EditFormDescription').val();
            var temporalExpDate = $('#' + selector + 'EditFormExpirationDate').val();

            if (resource.Description === "") {
                toastr.error($.i18n.t('DNEControl.validation.DescriptionCannotBeEmpty'), '', { positionClass: "toast-bottom-right" });
            } else {
                if (resource.Description.length > 100) {
                    toastr.error($.i18n.t('DNEControl.validation.DescriptionCannotBeEmpty'), '', { positionClass: "toast-bottom-right" });
                } else {
                    // if expiration date if after today, then.
                    // Date can be empty.
                    if ((temporalExpDate === "") || (moment(temporalExpDate, generalSupport.DateFormat()).isAfter(moment(new Date())))) {
                        resource.ExpirationDate = ((temporalExpDate !== "") ? generalSupport.jsDateToWCF(temporalExpDate, generalSupport.DateFormat()) : null);
                        DNESupport.DNEEditResource(resource).done(function () {
                            DNESupport.DNERefreshGrid(selector);
                            editModal.modal('hide');
                        });
                    } else {
                        toastr.error($.i18n.t('DNEControl.validation.InvalidExpirationDate'), '', { positionClass: "toast-bottom-right" });
                    }
                }
            }
        });
    }

    /**
     * Envía la petición del recurso.
     */
    this.DNEEditResource = function (resource) {
        $.LoadingOverlay("show");
        return $.ajax({
            type: "POST",
            url: urlService + "UpdateResource",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                updatedResourceDTO: resource,
                provider: provider
            }),
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + app.user.token);
            },
            success: function () { },
            error: function (qXHR, textStatus, errorThrown) {
                // generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                toastr.error($.i18n.t('DNEControl.form.AccessDenied'), '', { positionClass: "toast-bottom-right" });
            },
            complete: function () {
                $.LoadingOverlay("hide");
            }
        });
    }

    /**
     * Emula un click al botón refresh.
     */
    this.DNERefreshGrid = function (selector) {
        $('#' + selector + 'RefreshBtn').click();
    }

    /**
     * En true, al menos una nota es requerida.
     */
    this.DNENotesRequired = function (selector, validate) {
        if (validate) {
            if ($('#' + selector + 'NoteList').find(".note").length >= 1) {
                return true;
            } else {
                swal($.i18n.t('DNEControl.form.ErrorForm'), $.i18n.t('DNEControl.form.NoteRequired'), $.i18n.t('DNEControl.form.Error'));
                return false;
            }
        } else {
            return true;
        }
    }

    /**
     * En true, mustra las notas.
     */
    this.DNENotesVisible = function (selector, visible) {
        if (visible) {
            $('#' + selector + 'NoteList').parent().show();
        } else {
            $('#' + selector + 'NoteList').parent().hide();
        }
    }

    /**
     * En false, deshabilita los botones de las notas.
     */
    this.DNENotesEnable = function (selector, enable) {
        if (enable) {
            $('#' + selector + 'NoteList').parent().find(".btn, a").attr('disabled', false);
            $('.' + selector + 'NoteDelete').attr('disabled', false);
        } else {
            $('#' + selector + 'NoteList').parent().find(".btn, a").attr('disabled', true);
            $('#' + selector + 'Tbl').off();
        }
        $('#' + selector + 'SeeModal').find(".btn.btn-default").attr('disabled', false);
        $('.' + selector + 'NoteSee').attr('disabled', false);
    }

    /**
     * En true, al menos un attachment es requerido.
     */
    this.DNEAttachmentsRequired = function (selector, validate) {
    }

    /**
     * En true, mustra los attachments.
     */
    this.DNEAttachmentsVisible = function (selector, visible) {
        if (visible) {
            $('#' + selector + 'toolbar').parent().parent().parent().show();
        } else {
            $('#' + selector + 'toolbar').parent().parent().parent().hide();
        }
    }

    /**
     * En false, deshabilita los botones de los attachments.
     */
    this.DNEAttachmentsEnable = function (selector, enable, isTemporary) {
        if (isTemporary === undefined) // It is undefined if its called from the form designer.
            isTemporary = $("#" + selector + "SequenceId").data('isTemporary');

        if (enable) {
            $('#' + selector + 'toolbar').parent().parent().parent().find(".btn, a, i").attr('disabled', false);
            DNESupport.DNEGridEvents(selector, true, isTemporary);
            var editModal = $('#' + selector + 'EditForm');
            editModal.find('#' + selector + 'EditFormDescription').attr('disabled', false);
            editModal.find('#' + selector + 'EditFormExpirationDate').attr('disabled', false);
            editModal.find('#' + selector + 'SaveEditForm').attr('disabled', false);
        } else {
            $('#' + selector + 'toolbar').parent().parent().parent().find(".btn, a, i").attr('disabled', true);
            DNESupport.DNEGridEvents(selector, false, isTemporary);
            var editModal = $('#' + selector + 'EditForm');
            editModal.find('#' + selector + 'EditFormDescription').attr('disabled', true);
            editModal.find('#' + selector + 'EditFormExpirationDate').attr('disabled', true);
            editModal.find('#' + selector + 'SaveEditForm').attr('disabled', true);
        }
    }

    /**
     * Should return all tags.
     */
    this.DNEGetTags = function () {
        //var tagsSelector = $('#tags').find('option');
        //var tags = [];
        //$.each(tagsSelector, function (index, value) {
        //    var ShortTagDTO = {
        //        Content: 2899,
        //        TagTypeId: 3
        //    };
        //    tags.push(ShortTagDTO);
        //});

        //console.log(tags);
        //return tags;
    }

    /**
     * Returns the array of bytes of the img.
     */
    this.base64ToArrayBuffer = function (base64) {
        var binary_string = window.atob(base64);
        var len = binary_string.length;
        var bytes = new Uint8Array(len);
        for (var i = 0; i < len; i++) {
            bytes[i] = binary_string.charCodeAt(i);
        }
        return bytes;
    }

    /**
     * Initializes the signature control.
     */
    this.initializeSignature = function (selector, tags, isTemporary, showSignature) {
        if (showSignature) {
            $("#" + selector + "SignatureWrapper").show();
            
            window.requestAnimFrame = (function (callback) {
                return window.requestAnimationFrame ||
                    window.webkitRequestAnimationFrame ||
                    window.mozRequestAnimationFrame ||
                    window.oRequestAnimationFrame ||
                    window.msRequestAnimaitonFrame ||
                    function (callback) {
                        window.setTimeout(callback, 1000 / 60);
                    };
            })();

            var canvas = document.getElementById(selector + "Signature-canvas");
            canvas.width = "620";
            canvas.height = "160";
            var ctx = canvas.getContext("2d");
            ctx.strokeStyle = "#222222";
            ctx.lineWidth = 4;

            var drawing = false;
            var mousePos = {
                x: 0,
                y: 0
            };
            var lastPos = mousePos;

            canvas.addEventListener("mousedown", function (e) {
                drawing = true;
                lastPos = getMousePos(canvas, e);
            }, false);

            canvas.addEventListener("mouseup", function (e) {
                drawing = false;
            }, false);

            canvas.addEventListener("mousemove", function (e) {
                mousePos = getMousePos(canvas, e);
            }, false);

            // Add touch event support for mobile
            canvas.addEventListener("touchstart", function (e) {

            }, false);

            canvas.addEventListener("touchmove", function (e) {
                var touch = e.touches[0];
                var me = new MouseEvent("mousemove", {
                    clientX: touch.clientX,
                    clientY: touch.clientY
                });
                canvas.dispatchEvent(me);
            }, false);

            canvas.addEventListener("touchstart", function (e) {
                mousePos = getTouchPos(canvas, e);
                var touch = e.touches[0];
                var me = new MouseEvent("mousedown", {
                    clientX: touch.clientX,
                    clientY: touch.clientY
                });
                canvas.dispatchEvent(me);
            }, false);

            canvas.addEventListener("touchend", function (e) {
                var me = new MouseEvent("mouseup", {});
                canvas.dispatchEvent(me);
            }, false);

            function getMousePos(canvasDom, mouseEvent) {
                var rect = canvasDom.getBoundingClientRect();
                return {
                    x: mouseEvent.clientX - rect.left,
                    y: mouseEvent.clientY - rect.top
                }
            }

            function getTouchPos(canvasDom, touchEvent) {
                var rect = canvasDom.getBoundingClientRect();
                return {
                    x: touchEvent.touches[0].clientX - rect.left,
                    y: touchEvent.touches[0].clientY - rect.top
                }
            }

            function renderCanvas() {
                if (drawing) {
                    ctx.moveTo(lastPos.x, lastPos.y);
                    ctx.lineTo(mousePos.x, mousePos.y);
                    ctx.stroke();
                    lastPos = mousePos;
                }
            }

            // Prevent scrolling when touching the canvas
            document.body.addEventListener("touchstart", function (e) {
                if (e.target == canvas) {
                    e.preventDefault();
                }
            }, false);
            document.body.addEventListener("touchend", function (e) {
                if (e.target == canvas) {
                    e.preventDefault();
                }
            }, false);
            document.body.addEventListener("touchmove", function (e) {
                if (e.target == canvas) {
                    e.preventDefault();
                }
            }, false);

            (function drawLoop() {
                requestAnimFrame(drawLoop);
                renderCanvas();
            })();

            function clearCanvas() {
                canvas.width = canvas.width;
            }

            // Set up the UI
            var submitBtn = document.getElementById(selector + "Signature-add");
            var clearBtn = document.getElementById(selector + "Signature-clear");
            clearBtn.addEventListener("click", function (e) {
                clearCanvas();
            }, false);
            submitBtn.addEventListener("click", function (e) {
                DNESupport.addSignatureToDNE(selector, canvas.toDataURL(), tags, isTemporary);
            }, false);

            $("#" + selector + "Signature-canvas").css({"border":"2px dotted #CCCCCC", "border-radius":"15px", "cursor":"crosshair"});
        } else {
            $("#" + selector + "SignatureWrapper").hide();
        }
    }

    /**
     * Adds the signature to dne
     */
    this.addSignatureToDNE = function (selector, dataImg, tags, isTemporary) {
        var sequenceOnSelector = $("#" + selector + "SequenceId").val();
        var dneImage = {
            SequenceId: sequenceOnSelector,
            OriginalImage: Array.from(DNESupport.base64ToArrayBuffer(dataImg.replace("data:image/png;base64,", "")))
        };
        var resource = {
            SequenceId: sequenceOnSelector,
            Description: "E-Signature",
            ResourceTypeId: 1, // image
            ClientAssociatedCompany: "1",
            ClientAssociatedPerson: "1",
            Name: "E-Signature.png",
            Tags: tags,
            Image: dneImage,
            ExpirationDate: null,
        };
    
        if (sequenceOnSelector.toString() !== "0") {
            DNESupport.DNEAddResource(selector, resource, DNESupport.DNEGetAddOperation(isTemporary)).done(function () {
                DNESupport.DNERefreshGrid(selector);
            });
        } else {
            DNESupport.DNEGetSequenceId(selector).done(function (data) {
                dneImage.SequenceId = data;
                resource.SequenceId = data;

                DNESupport.DNEAddResource(selector, resource, DNESupport.DNEGetAddOperation(isTemporary)).done(function () {
                    DNESupport.DNERefreshGrid(selector);
                });
            });
        }
    }
}

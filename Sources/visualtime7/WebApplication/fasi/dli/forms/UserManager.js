var UserManagerSupport = new function () {

    this.currentRow = {};
    this.newIndex = -1;
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    this.ClientIdLkp = [];
    this.ProducerIdLkp = [];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#UserManagerFormId').val(),
            User_User: generalSupport.NormalizeProperties($('#UserTbl').bootstrapTable('getData'), 'CreationDate,LastLoginDate,LastLockedOutDate'),
            EmailOld: $('#EmailOld').val(),
            EmailChangeResult: $('#EmailChangeResult').is(':checked'),
            Type: generalSupport.NumericValue('#Type', -99999, 99999)
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#UserManagerFormId').val(data.InstanceFormId);
        $('#EmailOld').val(data.EmailOld);
        $('#EmailChangeResult').prop("checked", data.EmailChangeResult);
        AutoNumeric.set('#Type', data.Type);

        UserManagerSupport.LookUpForRolAssiged(source);
        UserManagerSupport.LookUpForGroupAssiged(source);
        UserManagerSupport.LookUpForClientId(source);
        UserManagerSupport.LookUpForProducerId(source);
        UserManagerSupport.LookUpForSupervisors(source);

        $('#UserTbl').bootstrapTable('refreshOptions', { ajax: UserManagerSupport.UserTblRequest });
        if (data.User_User !== null)
            $('#UserTbl').bootstrapTable('load', data.User_User);

    };

    this.ControlBehaviour = function () {
                 $('#ClientId').select2({  
	        language: generalSupport.LanguageName(), dropdownParent: $("#UserPopup"),
placeholder: '',
          width: '100%',
          language: generalSupport.LanguageName(),
	        ajax: {
	               type: "GET",
                 url: constants.fasiApi.base + 'BackOffice/v1/ClientsLkpPagination',
	               contentType: "application/json; charset=utf-8",
                 delay: 250,                 
	               dataType: 'json',
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
	               
	            data: function (params) {
                    var count = 10;
                    // Se formatan los datos que se envía por parámetro
                    var query = {
                        startIndex: params.page ? (((params.page * count) - count) + 1) : 0 + 1,
                        endIndex: params.page ? (params.page * count) : count,
                        filter: params.term ? params.term : ''  
                    };
                    return $.param(query);
                },
                processResults: function (response) {
                    // Se formatea los datos que recibe el componente
                    var data = $.map(response.Data.Items, function (obj) {
                        obj.id = obj.Code;
                        obj.text = obj.Description;

                        return obj;
                    });

                    return {
                        results: data,
                        pagination: {
                            more: data.length >= 9
                        }
                    };
                }
            },
            templateResult: function (item) {
                if (item.id) return item.id + ' - ' + item.text;
                return item.text;
            },
            templateSelection: function (item) {
                if (item.id) return item.id + ' - ' + item.text;
                return item.text;
            }
        });
                 $('#ProducerId').select2({  
	        language: generalSupport.LanguageName(), dropdownParent: $("#UserPopup"),
placeholder: '',
          width: '100%',
          language: generalSupport.LanguageName(),
	        ajax: {
	               type: "GET",
                 url: constants.fasiApi.base + 'BackOffice/v1/ProducerLkpPagination',
	               contentType: "application/json; charset=utf-8",
                 delay: 250,                 
	               dataType: 'json',
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
	               
	            data: function (params) {
                    var count = 10;
                    // Se formatan los datos que se envía por parámetro
                    var query = {
                        startIndex: params.page ? (((params.page * count) - count) + 1) : 0 + 1,
                        endIndex: params.page ? (params.page * count) : count,
                        filter: params.term ? params.term : ''  
                    };
                    return $.param(query);
                },
                processResults: function (response) {
                    // Se formatea los datos que recibe el componente
                    var data = $.map(response.Data.Items, function (obj) {
                        obj.id = obj.Code;
                        obj.text = obj.Description;

                        return obj;
                    });

                    return {
                        results: data,
                        pagination: {
                            more: data.length >= 9
                        }
                    };
                }
            },
            templateResult: function (item) {
                if (item.id) return item.id + ' - ' + item.text;
                return item.text;
            },
            templateSelection: function (item) {
                if (item.id) return item.id + ' - ' + item.text;
                return item.text;
            }
        });



      new AutoNumeric('#UserId', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      new AutoNumeric('#SecurityLevel', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
      new AutoNumeric('#Type', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });



  this.LookUpForRolAssiged = function (defaultValue, source) {
        var ctrol = $('#RolAssiged');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            
            app.core.AsyncWebMethod('/fasi/dli/forms/UserManagerActions.aspx/LookUpForRolAssiged', false,
             JSON.stringify({
					       id: $('#UserManagerFormId').val(),
					       value: defaultValue
				       }),
				       function (data) {              
                    ctrol.children().remove();                 
                    generalSupport.Select2Load('RolAssiged', data.d.Data, 'ROLEID', 'ROLENAME', defaultValue, null, null);                                  
             });
        }
        else
            if (defaultValue !== null) {
                generalSupport.Select2ItemsRefresh('RolAssiged', defaultValue);
            }
            else {
                ctrol.val('').trigger('change')
            }
    };
  this.LookUpForGroupAssiged = function (defaultValue, source) {
        var ctrol = $('#GroupAssiged');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            
            app.core.AsyncWebMethod('/fasi/dli/forms/UserManagerActions.aspx/LookUpForGroupAssiged', false,
             JSON.stringify({
					       id: $('#UserManagerFormId').val(),
					       value: defaultValue
				       }),
				       function (data) {              
                    ctrol.children().remove();                 
                    generalSupport.Select2Load('GroupAssiged', data.d.Data, 'GROUPID', 'DESCRIPTION', defaultValue, null, null);                                  
             });
        }
        else
            if (defaultValue !== null) {
                generalSupport.Select2ItemsRefresh('GroupAssiged', defaultValue);
            }
            else {
                ctrol.val('').trigger('change')
            }
    };
  this.LookUpForSupervisors = function (defaultValue, source) {
        var ctrol = $('#Supervisors');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            
            app.core.AsyncWebMethod('/fasi/dli/forms/UserManagerActions.aspx/LookUpForSupervisors', false,
             JSON.stringify({
					       id: $('#UserManagerFormId').val(),
					       value: defaultValue
				       }),
				       function (data) {              
                    ctrol.children().remove();                 
                    generalSupport.Select2Load('Supervisors', data.d.Data, 'USERID', 'USERNAME', defaultValue, null, null);                                  
             });
        }
        else
            if (defaultValue !== null) {
                generalSupport.Select2ItemsRefresh('Supervisors', defaultValue);
            }
            else {
                ctrol.val('').trigger('change')
            }
    };
   this.GetCheckComboxSelectedValues = function (componentId) {
        var selectedItems = new Array();
        $('#' + componentId + ' option:selected').each(function (index, item) {
            selectedItems.push({ Text: $(item).text(), Value: $(item).val() });
        })
        return selectedItems;
    };

        $('#CreationDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#CreationDate_group');
        $('#LastLoginDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#LastLoginDate_group');
        $('#LastLockedOutDate_group').datetimepicker({
            format: generalSupport.DateFormat(),
            locale: moment.locale()
        });
        generalSupport.SetCalendarPosition('#LastLockedOutDate_group');


    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               UserManagerSupport.ObjectToInput(data.d.Data.Instance, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };


    this.CallRenderLookUps = function (data) {
          if (data.d.Success === true && data.d.Data.LookUps) {

              data.d.Data.LookUps.forEach(function (elementSource) {
              generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items, ((elementSource.Key == 'RolAssiged' || elementSource.Key == 'GroupAssiged' || elementSource.Key == 'Supervisors') ? true : false));
 
              });
          }
     };



    this.User_Item1_Actions = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#UserSaveBtn'));          
            var UnloockedUserResult;
            var errors;
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Members/v1/PasswordRecovery?userId=' + generalSupport.NumericValue('#UserId', -999999999, 999999999),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         PasswordRecoveryResult = data.Successfully;
PasswordRecoveryMessage = data.Reason;

        if (PasswordRecoveryResult === true){
            var message3 = $.i18n.t('app.form.User_Message_Notify_MenuItem13');
            notification.control.info(null, message3);
            }            
            else {
            var message4 = $.i18n.t('app.form.User_Message_Notify_MenuItem14');
            notification.control.error(null, message4);

                }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };
    this.User_Item2_Actions = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#UserSaveBtn'));          
            var localEmailOld;
            var ChangeEmail;
            $('#EmailOld').val($('#Email').val());

    $('#popup0Popup').modal('show');

    };
    this.User_Item3_Actions = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#UserSaveBtn'));          
            var ResultApproval;
            var errors;
               $.ajax({
                    type: "POST",
                    url: constants.fasiApi.base + 'Members/v1/UserApproval?userId=' + generalSupport.NumericValue('#UserId', -999999999, 999999999),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         ResultApproval = data.Successfully;

        if (ResultApproval === true){
            var message3 = $.i18n.t('app.form.User_Message_Notify_MenuItem33');
            notification.swal.success($.i18n.t('app.form.User_Title_Notify_MenuItem33'), message3);
                    AutoNumeric.set('#Type', 2);

            $('#UserTbl').bootstrapTable('refresh');
            }            
            else {
            var message6 = $.i18n.t('app.form.User_Message_Notify_MenuItem36');
            notification.swal.error($.i18n.t('app.form.User_Title_Notify_MenuItem36'), message6);

                }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };
    this.User_Item4_Actions = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#UserSaveBtn'));          
            var UnlookedUser;
            var errors;
               $.ajax({
                    type: "POST",
                    url: constants.fasiApi.base + 'Members/v1/UserLockedChangeByUserId?userId=' + generalSupport.NumericValue('#UserId', -999999999, 999999999) + '&state=false',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         UnlookedUser = data.Successfully;

        if (UnlookedUser === true){
            var message3 = $.i18n.t('app.form.User_Message_Notify_MenuItem43');
            notification.swal.success($.i18n.t('app.form.User_Title_Notify_MenuItem43'), message3);
            row.IsLockedOut = false; 
$('#UserTbl').bootstrapTable('updateByUniqueId', { id: row.UserId, row: row });
            }            
            else {
            var message5 = $.i18n.t('app.form.User_Message_Notify_MenuItem45');
            notification.swal.error($.i18n.t('app.form.User_Title_Notify_MenuItem45'), message5);

                }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };
    this.User_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#UserSaveBtn'));          
            var UpdateResult;
            var errors;
            var reason;
            var stringEmpty;
        stringEmpty = "";
               $.ajax({
                    type: "PUT",
                    url: constants.fasiApi.base + 'Members/v1/UserUpdate',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ UserId: generalSupport.NumericValue('#UserId', -999999999, 999999999), UserName: $('#UserName').val(), Email: $('#Email').val(), RolAssiged: generalSupport.Select2GetValue('RolAssiged', true), ClientId: $('#ClientId').val(), ProducerId: $('#ProducerId').val(), IsAdministrator: $('#IsAdministrator').is(':checked'), AllowScheduler: $('#AllowScheduler').is(':checked'), GroupAssiged: generalSupport.Select2GetValue('GroupAssiged', true), IsLockedOut: $('#IsLockedOut').is(':checked'), PasswordNeverExpires: $('#PasswordNeverExpires').is(':checked') }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         UpdateResult = data.Successfully;
reason = data.Reason;

            if (UpdateResult === true && reason === stringEmpty){
                $('#UserTbl').bootstrapTable('updateByUniqueId', { id: row.UserId, row: row });
                $modal.modal('hide');
                var message5 = $.i18n.t('app.form.User_Message_Notify_update5');
                notification.toastr.success($.i18n.t('app.form.User_Title_Notify_update5'), message5);
                }                
                else {
                var message6 = $.i18n.t('app.form.User_Message_Notify_update6');
                message6 = message6.replace(/{{reason}}/g, reason);
                notification.toastr.error($.i18n.t('app.form.User_Title_Notify_update6'), message6);

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };
    this.User_BeforeShowPopup = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#UserSaveBtn'));          
        $('#UserId').prop('disabled', true);
        $('#UserIdLabel').prop('disabled', true);
        $('#IsEmployeeWrap').prop('disabled', true);
        $('#IsEmployeeLabel').prop('disabled', true);
        $('#SecurityLevel').prop('disabled', true);
        $('#SecurityLevelLabel').prop('disabled', true);
        if (row.UserId != 0){
            $('#UserName').prop('disabled', true);
            $('#UserNameLabel').prop('disabled', true);
            $('#Email').prop('disabled', true);
            $('#EmailLabel').prop('disabled', true);

            }

    };
    this.User_delete = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#UserSaveBtn'));          
            var UserDeleteResult;
            var errors;
            var reason;
            var stringEmpty;
        stringEmpty = "";
               $.ajax({
                    type: "DELETE",
                    url: constants.fasiApi.base + 'Members/v1/UserDeleteById?userId=' + generalSupport.NumericValue('#UserId', -999999999, 999999999) + '&DeleteAllRelatedData=true',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         UserDeleteResult = data.Successfully;
reason = data.Reason;

            if (UserDeleteResult === true && reason === stringEmpty){
                var message4 = $.i18n.t('app.form.User_Message_Notify_delete4');
                notification.swal.success($.i18n.t('app.form.User_Title_Notify_delete4'), message4);
                $('#UserTbl').bootstrapTable('remove', {field: 'UserId', values: [generalSupport.NumericValue('#UserId', -999999999, 999999999)]});
                }                
                else {
                var message6 = $.i18n.t('app.form.User_Message_Notify_delete6');
                notification.toastr.error($.i18n.t('app.form.User_Title_Notify_delete6'), message6);

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown, true);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };

    this.ControlActions =   function () {

        $('#btnEmailChange').click(function (event) {
            var formInstance = $("#UserManagerMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
            var EmailChangeMessage;
            var errors;
            var EmailChangeResult;
                var btnLoading = Ladda.create(document.querySelector('#btnEmailChange'));
                btnLoading.start();
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Members/v1/EmailChange?userId=' + generalSupport.NumericValue('#UserId', -999999999, 999999999) + '&email=' + $('#EmailOld').val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         EmailChangeMessage = data.Reason;
EmailChangeResult = data.Successfully;

                    if (EmailChangeResult === EmailChangeResult){
                        $('#popup0Popup').modal('hide');
                        $('#UserTbl').bootstrapTable('refresh');
                        var message4 = $.i18n.t('app.form.btnEmailChange_Message_Notify_click4');
                        notification.swal.success($.i18n.t('app.form.btnEmailChange_Title_Notify_click4'), message4);
                        }                        
                        else {
                        var message5 = $.i18n.t('app.form.btnEmailChange_Message_Notify_click5');
                        notification.swal.error($.i18n.t('app.form.btnEmailChange_Title_Notify_click5'), message5);

                            }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#btnAllUser').click(function (event) {
            var formInstance = $("#UserManagerMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#btnAllUser'));
                btnLoading.start();
                        AutoNumeric.set('#Type', 1);

                $('#UserTbl').bootstrapTable('refresh');
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });
        $('#btnPendingApproval').click(function (event) {
            var formInstance = $("#UserManagerMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#btnPendingApproval'));
                btnLoading.start();
                        AutoNumeric.set('#Type', 2);

                $('#UserTbl').bootstrapTable('refresh');
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();

        $.validator.addMethod("EmailOld_Validate1", function (value, element) {
            var result = true;
            if (!this.optional(element)) {
            var EmailExit;
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Members/v1/UserNameEmailExist?email=' + $('#EmailOld').val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({  }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         EmailExit = data.Successfully;

                if (EmailExit === true){
                    result = false;
            }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
            }
            return result;
        });

        $("#UserManagerMainForm").validate({
            errorPlacement: function (error, element) {
                var name = $(element).attr("name");
                var $obj = $("#" + name + "_validate");
                if ($obj.length) {
                    error.appendTo($obj);
                }
                else {
                    error.insertAfter(element);
                }
            },
           onkeyup: function(element) {
                if ($(element).attr('name') !== 'EmailOld') {
                    $.validator.defaults.onkeyup.apply(this,arguments);
                }
            },
            rules: {
                EmailOld: {
                    required: true,
                    maxlength: 80,
                    email: true,
                    EmailOld_Validate1: true
                },
                Type: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                }
            },
            messages: {
                EmailOld: {
                    required: $.i18n.t('app.form.EmailOld_RequiredMessage'),
                    maxlength: $.i18n.t('app.form.EmailOld_maxlength'),
                    email: $.i18n.t('app.form.EmailOld_Email'),
                    EmailOld_Validate1: $.i18n.t('app.form.EmailOld_Validate1')
                },
                Type: {
                    AutoNumericMinValue: 'El valor mínimo permitido es -99999',
                    AutoNumericMaxValue: 'El valor máximo permitido es 99999'
                }
            }
        });
        $("#UserEditForm").validate().destroy();
        $("#UserEditForm").validate({
            rules: {
                UserId: {
                    AutoNumericMinValue: -999999999,
                    AutoNumericMaxValue: 999999999
                },
                UserName: {
                    maxlength: 80
                },
                Email: {
                    maxlength: 80
                },
                SecurityLevel: {
                    AutoNumericMinValue: 1,
                    AutoNumericMaxValue: 9
                },
                ClientId: {
                },
                ProducerId: {
                },
                CreationDate: {
                    DatePicker: true
                },
                LastLoginDate: {
                    DatePicker: true
                },
                LastLockedOutDate: {
                    DatePicker: true
                }

            },
            messages: {
                UserId: {
                    AutoNumericMinValue: $.i18n.t('app.validation.UserId.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.UserId.AutoNumericMaxValue')
                },
                UserName: {
                    maxlength: $.i18n.t('app.validation.UserName.maxlength')
                },
                Email: {
                    maxlength: $.i18n.t('app.validation.Email.maxlength')
                },
                SecurityLevel: {
                    AutoNumericMinValue: $.i18n.t('app.validation.SecurityLevel.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.SecurityLevel.AutoNumericMaxValue')
                },
                ClientId: {
                },
                ProducerId: {
                },
                CreationDate: {
                    DatePicker: $.i18n.t('app.validation.CreationDate.DatePicker')
                },
                LastLoginDate: {
                    DatePicker: $.i18n.t('app.validation.LastLoginDate.DatePicker')
                },
                LastLockedOutDate: {
                    DatePicker: $.i18n.t('app.validation.LastLockedOutDate.DatePicker')
                }

            }
        });

    };
   this.LookUpForRolAssigedFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = generalSupport.Select2GetDescription('RolAssiged', true, value);
        }
        return result;
    };
   this.LookUpForGroupAssigedFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = generalSupport.Select2GetDescription('GroupAssiged', true, value);
        }
        return result;
    };
    this.LookUpForClientIdFormatter = function (value, row, index) {
        var result = '';
        if (value === null || value === undefined || value === 0 || value === '') {
            result = '';
        } else {
            UserManagerSupport.ClientIdLkp.forEach(function (elementSource) {
                if (elementSource.Key === value)
                    result = value + ' - ' + elementSource.Value;
            });
            if (result === '') {
                result = '<div id="ClientIdLkp_' + row.UserId + '" >' +
                    '<div class="sk-spinner sk-spinner-wave" style="height:15px !important;" > ' +
                    '<div class="sk-rect1" ></div> ' +
                    '<div class="sk-rect2" ></div > ' +
                    '<div class="sk-rect3" ></div > ' +
                    '<div class="sk-rect4" ></div > ' +
                    '<div class="sk-rect5" ></div > ' +
                    '</div>' +
                    '</div>';

                var name = "ClientIdLkp_" + row.UserId;

                app.core.AsyncGet(constants.fasiApi.backoffice  + 'ClientByIdLkp?Id=' + row.ClientId, true, false,
                    JSON.stringify({}),
                    function (data) {
                        if (data !== '') {
                            result = data;
                            $('#' + name + ' div').replaceWith(value + ' - ' + result);
                        } else {
                            $('#' + name + ' div').replaceWith('');
                            data = "";
                        }
                        UserManagerSupport.ClientIdLkp.push({ Key: value, Value: data });
                    });
            }
        }
        return result;
    };
   this.LookUpForClientId = function (defaultValue, source) {
        if (defaultValue) {
        var select = $('#ClientId');
        
        $.ajax({
                type: "GET",
                url: constants.fasiApi.backoffice  + 'ClientByIdLkp?Id=' + defaultValue,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: JSON.stringify({  }),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                }).then(function (response) {
                if (response) {
                    // Se crea el "option" y lo agrega
                    var option = new Option(response, defaultValue, true, true);
                    select.append(option).trigger('change');

                    // Se llama de forma manual el evento de selección
                    select.trigger({
                        type: 'select2:select',
                        params: {
                            data: option
                        }
                    });
                }
            });
        }
    };
    this.LookUpForProducerIdFormatter = function (value, row, index) {
        var result = '';
        if (value === null || value === undefined || value === 0 || value === '') {
            result = '';
        } else {
            UserManagerSupport.ProducerIdLkp.forEach(function (elementSource) {
                if (elementSource.Key === value)
                    result = value + ' - ' + elementSource.Value;
            });
            if (result === '') {
                result = '<div id="ProducerIdLkp_' + row.UserId + '" >' +
                    '<div class="sk-spinner sk-spinner-wave" style="height:15px !important;" > ' +
                    '<div class="sk-rect1" ></div> ' +
                    '<div class="sk-rect2" ></div > ' +
                    '<div class="sk-rect3" ></div > ' +
                    '<div class="sk-rect4" ></div > ' +
                    '<div class="sk-rect5" ></div > ' +
                    '</div>' +
                    '</div>';

                var name = "ProducerIdLkp_" + row.UserId;

                app.core.AsyncGet(constants.fasiApi.backoffice  + 'ProducerByIdLkp?Id=' + row.ProducerId, true, false,
                    JSON.stringify({}),
                    function (data) {
                        if (data !== '') {
                            result = data;
                            $('#' + name + ' div').replaceWith(value + ' - ' + result);
                        } else {
                            $('#' + name + ' div').replaceWith('');
                            data = "";
                        }
                        UserManagerSupport.ProducerIdLkp.push({ Key: value, Value: data });
                    });
            }
        }
        return result;
    };
   this.LookUpForProducerId = function (defaultValue, source) {
        if (defaultValue) {
        var select = $('#ProducerId');
        
        $.ajax({
                type: "GET",
                url: constants.fasiApi.backoffice  + 'ProducerByIdLkp?Id=' + defaultValue,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: JSON.stringify({  }),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                }).then(function (response) {
                if (response) {
                    // Se crea el "option" y lo agrega
                    var option = new Option(response, defaultValue, true, true);
                    select.append(option).trigger('change');

                    // Se llama de forma manual el evento de selección
                    select.trigger({
                        type: 'select2:select',
                        params: {
                            data: option
                        }
                    });
                }
            });
        }
    };
   this.LookUpForSupervisorsFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = generalSupport.Select2GetDescription('Supervisors', true, value);
        }
        return result;
    };

    this.UserTblRequest = function (params) {
        $.ajax({
             type: "GET",
             url: constants.fasiApi.base + 'Members/v1/UserAllByPage?type=' + generalSupport.NumericValue('#Type', -99999, 99999) + '&startIndex=' + (((params.data.offset !== undefined) ? params.data.offset : 0)+1) + '&endIndex=' + (((params.data.offset !== undefined) ? params.data.offset : 0)+((params.data.limit !== undefined) ? params.data.limit : 0)) + '&filter=' + ((params.data.search !== undefined) ? params.data.search : ''),
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             data: JSON.stringify({  }),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                   success: function (data) {
                if (data.Successfully === true) {
                    params.success({
                        total: data.Data.Count,
                        rows: data.Data.Items
                    });
                }
                else
                    generalSupport.NotifyFail(data.Reason, data.Code);
              },
               error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
             });
    };
    this.UserTblSetup = function (table) {
        UserManagerSupport.LookUpForRolAssiged('');
        UserManagerSupport.LookUpForGroupAssiged('');
        UserManagerSupport.LookUpForClientId('');
        UserManagerSupport.LookUpForProducerId('');
        UserManagerSupport.LookUpForSupervisors('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'UserId',
            sidePagination: 'server',
            search: true,
            showColumns: true,
            showExport: true,
            exportDataType: 'all',
            exportOptions: {
            onCellHtmlData: function(cell, row, col, data) {
                    var result = "";
                    if (data != "") {
                        var html = $.parseHTML(data);

                        $.each(html, function() {
                            if (typeof $(this).html() === 'undefined')
                                result += $(this).text();
                            else if (typeof $(this).attr('class') === 'undefined' || $(this).hasClass('th-inner') === true)
                                result += $(this).html();
                            else if($(this).hasClass('update edit') === true)
                                result += $(this).html();
                            else if (typeof $(this).attr('class') === 'undefined' || $(this).hasClass('row-fluid') === true)
                                if (this.children.length !== 0) {
                                    $.each(this.children, function () {
                                        if ($(this).attr('class') === 'undefined' || $(this).hasClass('control-label') === true) {
                                            result += $(this).text();
                                        }
                                    });
                                }
                        });
                    }
                    return result;
                },
                maxNestedTables: 0,
                jspdf: {                          // jsPDF / jsPDF-AutoTable related options
                    orientation:      'l',
                    unit:             'mm',
                    format:           'a4',         // One of jsPDF page formats or 'bestfit' for automatic paper format selection
                    margins:          {left: 5, right: 5, top: 10, bottom: 10},
                    split: 10,
                    autotable: {
                      styles: {
                        fontSize:     9,
                        fillColor:    255,          // Color value or 'inherit' to use css background-color from html table
                        fontStyle:    'normal',     // 'normal', 'bold', 'italic', 'bolditalic' or 'inherit' to use css font-weight and font-style from html table
                        overflow:     'linebreak',  // 'visible', 'hidden', 'ellipsize' or 'linebreak'
                        cellWidth:    'auto',
                      }
                  }
                },
                date: {
                    html: generalSupport.DateFormat()
                },
                mso: {
                    xslx: {
                        fileFormat: 'xlsx',
                        formatId: {
                            date: 14,
                            numbers: 0
                        }
                    }
                },
                numbers: {
                    html: {
                        decimalMark: generalSupport.DecimalCharacter(),
                        thousandsSeparator: generalSupport.DigitGroupSeparator()
                    }
                }
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'pdf', 'xlsx'],
            toolbar: '#Usertoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'UserManagerSupport.selected_Formatter'
            }, {
                field: 'UserId',
                title: $.i18n.t('app.form.UserTbl_UserId_Title'),
                formatter: 'UserManagerSupport.UserId_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'UserName',
                title: $.i18n.t('app.form.UserTbl_UserName_Title'),
                events: 'UserActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'Email',
                title: $.i18n.t('app.form.UserTbl_Email_Title'),
                sortable: false,
                halign: 'center'
            }, {
                field: 'IsEmployee',
                title: $.i18n.t('app.form.UserTbl_IsEmployee_Title'),
                formatter: 'UserManagerSupport.IsEmployee_IsCheck',
                sortable: false,
                halign: 'center'
            }, {
                field: 'IsApproved',
                title: $.i18n.t('app.form.UserTbl_IsApproved_Title'),
                formatter: 'UserManagerSupport.IsApproved_IsCheck',
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'IsAdministrator',
                title: $.i18n.t('app.form.UserTbl_IsAdministrator_Title'),
                formatter: 'UserManagerSupport.IsAdministrator_IsCheck',
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'AllowScheduler',
                title: $.i18n.t('app.form.UserTbl_AllowScheduler_Title'),
                formatter: 'UserManagerSupport.AllowScheduler_IsCheck',
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'IsLockedOut',
                title: $.i18n.t('app.form.UserTbl_IsLockedOut_Title'),
                formatter: 'UserManagerSupport.IsLockedOut_IsCheck',
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'PasswordNeverExpires',
                title: $.i18n.t('app.form.UserTbl_PasswordNeverExpires_Title'),
                formatter: 'UserManagerSupport.PasswordNeverExpires_IsCheck',
                sortable: false,
                halign: 'center',
                visible: false
            }, {
                field: 'RolAssiged',
                title: $.i18n.t('app.form.UserTbl_RolAssiged_Title'),
                formatter: 'UserManagerSupport.LookUpForRolAssigedFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'GroupAssiged',
                title: $.i18n.t('app.form.UserTbl_GroupAssiged_Title'),
                formatter: 'UserManagerSupport.LookUpForGroupAssigedFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'SecurityLevel',
                title: $.i18n.t('app.form.UserTbl_SecurityLevel_Title'),
                formatter: 'UserManagerSupport.SecurityLevel_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'ClientId',
                title: $.i18n.t('app.form.UserTbl_ClientId_Title'),
                formatter: 'UserManagerSupport.LookUpForClientIdFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'ProducerId',
                title: $.i18n.t('app.form.UserTbl_ProducerId_Title'),
                formatter: 'UserManagerSupport.LookUpForProducerIdFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'CreationDate',
                title: $.i18n.t('app.form.UserTbl_CreationDate_Title'),
                formatter: 'UserManagerSupport.UserCreationDate_ColumnFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'LastLoginDate',
                title: $.i18n.t('app.form.UserTbl_LastLoginDate_Title'),
                formatter: 'UserManagerSupport.UserLastLoginDate_ColumnFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'LastLockedOutDate',
                title: $.i18n.t('app.form.UserTbl_LastLockedOutDate_Title'),
                formatter: 'UserManagerSupport.UserLastLockedOutDate_ColumnFormatter',
                sortable: false,
                halign: 'center',
                align: 'center'
            }, {
                field: 'Supervisors',
                title: $.i18n.t('app.form.UserTbl_Supervisors_Title'),
                formatter: 'UserManagerSupport.LookUpForSupervisorsFormatter',
                sortable: false,
                halign: 'center'
            }]
        });

        table.bootstrapTable('refreshOptions', {
            contextMenu: '#UserContextMenu',
            contextMenuButton: '',
            beforeContextMenuRow: function (e, row, buttonElement) {
                UserManagerSupport.UserRowToInput(row);
                if (!buttonElement ) {
                    if (row.IsApproved === true)
                        $('#UserContextMenu').find('[data-item="User_Item1"]').show();
                    else
                        $('#UserContextMenu').find('[data-item="User_Item1"]').hide();
                    if (row.IsApproved === true)
                        $('#UserContextMenu').find('[data-item="User_Item2"]').show();
                    else
                        $('#UserContextMenu').find('[data-item="User_Item2"]').hide();
                    if (row.IsApproved === false)
                        $('#UserContextMenu').find('[data-item="User_Item3"]').show();
                    else
                        $('#UserContextMenu').find('[data-item="User_Item3"]').hide();
                    if (row.IsLockedOut === true)
                        $('#UserContextMenu').find('[data-item="User_Item4"]').show();
                    else
                        $('#UserContextMenu').find('[data-item="User_Item4"]').hide();

                    table.bootstrapTable('showContextMenu', {
                        event: e,
                        contextMenu: '#UserContextMenu',
                        buttonElement: buttonElement
                    });
                }
                return false;
            },
            onContextMenuItem: function (row, $el) {
                UserManagerSupport.UserRowToInput(row);
                switch ($el.data("item")) {
                    case 'User_Item1':
                        UserManagerSupport.User_Item1_Actions(row, null);
                        break;
                    case 'User_Item2':
                        UserManagerSupport.User_Item2_Actions(row, null);
                        break;
                    case 'User_Item3':
                        UserManagerSupport.User_Item3_Actions(row, null);
                        break;
                    case 'User_Item4':
                        UserManagerSupport.User_Item4_Actions(row, null);
                        break;
                }
            }
        });

        $('#UserTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#UserTbl');
            $('#UserRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#UserRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#UserTbl').bootstrapTable('getSelections'), function (row) {		
                UserManagerSupport.UserRowToInput(row);
                UserManagerSupport.User_delete(row, null);
                
                return row.UserId;
            });

            $('#UserRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#UserCreateBtn').click(function () {
            var formInstance = $("#UserEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            UserManagerSupport.UserShowModal($('#UserPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#UserPopup').find('#UserSaveBtn').click(function () {
            var formInstance = $("#UserEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#UserPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';
                else
                   UserManagerSupport.newIndex = UserManagerSupport.newIndex - 1;
                   
                var caption = $('#UserSaveBtn').html();
                $('#UserSaveBtn').html('Procesando...');
                $('#UserSaveBtn').prop('disabled', true);

                UserManagerSupport.currentRow.UserId = generalSupport.NumericValue('#UserId', -999999999, 999999999);
                UserManagerSupport.currentRow.UserName = $('#UserName').val();
                UserManagerSupport.currentRow.Email = $('#Email').val();
                UserManagerSupport.currentRow.IsEmployee = $('#IsEmployee').is(':checked');
                UserManagerSupport.currentRow.IsApproved = $('#IsApproved').is(':checked');
                UserManagerSupport.currentRow.IsAdministrator = $('#IsAdministrator').is(':checked');
                UserManagerSupport.currentRow.AllowScheduler = $('#AllowScheduler').is(':checked');
                UserManagerSupport.currentRow.IsLockedOut = $('#IsLockedOut').is(':checked');
                UserManagerSupport.currentRow.PasswordNeverExpires = $('#PasswordNeverExpires').is(':checked');
                UserManagerSupport.currentRow.RolAssiged = generalSupport.Select2GetValue('RolAssiged', true);
                UserManagerSupport.currentRow.GroupAssiged = generalSupport.Select2GetValue('GroupAssiged', true);
                UserManagerSupport.currentRow.SecurityLevel = generalSupport.NumericValue('#SecurityLevel', 1, 9);
                UserManagerSupport.currentRow.ClientId = $('#ClientId').val();
                UserManagerSupport.currentRow.ProducerId = $('#ProducerId').val();
                UserManagerSupport.currentRow.CreationDate = generalSupport.DatePickerValue('#CreationDate');
                UserManagerSupport.currentRow.LastLoginDate = generalSupport.DatePickerValue('#LastLoginDate');
                UserManagerSupport.currentRow.LastLockedOutDate = generalSupport.DatePickerValue('#LastLockedOutDate');
                UserManagerSupport.currentRow.Supervisors = generalSupport.Select2GetValue('Supervisors', true);

                $('#UserSaveBtn').prop('disabled', false);
                $('#UserSaveBtn').html(caption);

                if (wm === 'Update') {
                    UserManagerSupport.User_update(UserManagerSupport.currentRow, $modal);
                }
                else {                    
                    $('#UserTbl').bootstrapTable('append', UserManagerSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.UserShowModal = function (md, title, row) {
        var formInstance = $("#UserEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { UserId: 0, UserName: '', Email: '', IsEmployee: null, IsApproved: null, IsAdministrator: null, AllowScheduler: null, IsLockedOut: null, PasswordNeverExpires: null, RolAssiged: '', GroupAssiged: '', SecurityLevel: 0, ClientId: '', ProducerId: '', CreationDate: null, LastLoginDate: null, LastLockedOutDate: null, Supervisors: '' };

        md.data('id', row.UserId);
        md.find('.modal-title').text(title);

        UserManagerSupport.UserRowToInput(row);
        $('#IsApproved').prop('disabled', true);
        $('#CreationDate').prop('disabled', true);
        $('#LastLoginDate').prop('disabled', true);
        $('#LastLockedOutDate').prop('disabled', true);
        $('#Supervisors').prop('disabled', true);
        UserManagerSupport.User_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.UserRowToInput = function (row) {
        UserManagerSupport.currentRow = row;
        AutoNumeric.set('#UserId', row.UserId);
        $('#UserName').val(row.UserName);
        $('#Email').val(row.Email);
        $('#IsEmployee').prop("checked", row.IsEmployee);
        $('#IsApproved').prop("checked", row.IsApproved);
        $('#IsAdministrator').prop("checked", row.IsAdministrator);
        $('#AllowScheduler').prop("checked", row.AllowScheduler);
        $('#IsLockedOut').prop("checked", row.IsLockedOut);
        $('#PasswordNeverExpires').prop("checked", row.PasswordNeverExpires);
        UserManagerSupport.LookUpForRolAssiged(row.RolAssiged, '');
        UserManagerSupport.LookUpForGroupAssiged(row.GroupAssiged, '');
        AutoNumeric.set('#SecurityLevel', row.SecurityLevel);
        UserManagerSupport.LookUpForClientId(row.ClientId, '');
        $('#ClientId').trigger('change');
        UserManagerSupport.LookUpForProducerId(row.ProducerId, '');
        $('#ProducerId').trigger('change');
        $('#CreationDate').val(generalSupport.ToJavaScriptDateCustom(row.CreationDate, generalSupport.DateFormat()));
        $('#LastLoginDate').val(generalSupport.ToJavaScriptDateCustom(row.LastLoginDate, generalSupport.DateFormat()));
        $('#LastLockedOutDate').val(generalSupport.ToJavaScriptDateCustom(row.LastLockedOutDate, generalSupport.DateFormat()));
        UserManagerSupport.LookUpForSupervisors(row.Supervisors, '');

    };





    this.UserId_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999999999,
            decimalPlaces: 0,
            minimumValue: -999999999
        });
      };
    this.SecurityLevel_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: -9
        });
      };


	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#UserTbl *').prop('disabled'),
                  checked: value
                 }
      };
	    this.IsEmployee_IsCheck = function (value, row, index) {
        var icon = "";
        
        if (value === false) {
            icon = "glyphicon glyphicon-unchecked";
        } else {
            icon = "glyphicon glyphicon-check";
        }
       
        return '<div class="text-center" >' +
            '<i class="' + icon + '"></i>' +
            '</div>';
      };
	    this.IsApproved_IsCheck = function (value, row, index) {
        var icon = "";
        
        if (value === false) {
            icon = "glyphicon glyphicon-unchecked";
        } else {
            icon = "glyphicon glyphicon-check";
        }
       
        return '<div class="text-center" >' +
            '<i class="' + icon + '"></i>' +
            '</div>';
      };
	    this.IsAdministrator_IsCheck = function (value, row, index) {
        var icon = "";
        
        if (value === false) {
            icon = "glyphicon glyphicon-unchecked";
        } else {
            icon = "glyphicon glyphicon-check";
        }
       
        return '<div class="text-center" >' +
            '<i class="' + icon + '"></i>' +
            '</div>';
      };
	    this.AllowScheduler_IsCheck = function (value, row, index) {
        var icon = "";
        
        if (value === false) {
            icon = "glyphicon glyphicon-unchecked";
        } else {
            icon = "glyphicon glyphicon-check";
        }
       
        return '<div class="text-center" >' +
            '<i class="' + icon + '"></i>' +
            '</div>';
      };
	    this.IsLockedOut_IsCheck = function (value, row, index) {
        var icon = "";
        
        if (value === false) {
            icon = "glyphicon glyphicon-unchecked";
        } else {
            icon = "glyphicon glyphicon-check";
        }
       
        return '<div class="text-center" >' +
            '<i class="' + icon + '"></i>' +
            '</div>';
      };
	    this.PasswordNeverExpires_IsCheck = function (value, row, index) {
        var icon = "";
        
        if (value === false) {
            icon = "glyphicon glyphicon-unchecked";
        } else {
            icon = "glyphicon glyphicon-check";
        }
       
        return '<div class="text-center" >' +
            '<i class="' + icon + '"></i>' +
            '</div>';
      };

    this.UserCreationDate_ColumnFormatter = function (value, row, index, field) {
        return tableHelperSupport.DateFormatter(value, row,index);
    };
    this.UserLastLoginDate_ColumnFormatter = function (value, row, index, field) {
        return tableHelperSupport.DateFormatter(value, row,index);
    };
    this.UserLastLockedOutDate_ColumnFormatter = function (value, row, index, field) {
        return tableHelperSupport.DateFormatter(value, row,index);
    };


  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('UserManager', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        UserManagerSupport.ValidateSetup();
        
        

    UserManagerSupport.ControlBehaviour();
    UserManagerSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/UserManagerActions.aspx/Initialization", false,
            JSON.stringify({
                id: $('#UserManagerFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  UserManagerSupport.CallRenderLookUps(data);
                
                $('#EmailOld').val(generalSupport.URLStringValue('EmailOld'));
    AutoNumeric.set('#Type', generalSupport.URLNumericValue('Type'));

                $("#UserTblPlaceHolder").replaceWith('<table id="UserTbl"></table>');
    UserManagerSupport.UserTblSetup($('#UserTbl'));

                new AutoNumeric('#Type', 1);
        $('#UserTbl').bootstrapTable('refreshOptions', { ajax: UserManagerSupport.UserTblRequest });

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: $("#UserManagerMainForm"),
        CallBack: UserManagerSupport.Init
    });
});

window.UserActionEvents = {
    'click .update': function (e, value, row, index) {
        UserManagerSupport.UserShowModal($('#UserPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};

var RoleManagerSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#RoleManagerFormId').val(),
            Role_Role: generalSupport.NormalizeProperties($('#RoleTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#RoleManagerFormId').val(data.InstanceFormId);


        $('#RoleTbl').bootstrapTable('refreshOptions', { ajax: RoleManagerSupport.RoleTblRequest });
        if (data.Role_Role !== null)
            $('#RoleTbl').bootstrapTable('load', data.Role_Role);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Id', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      new AutoNumeric('#SecurityLevel', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: 0
        });






    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               RoleManagerSupport.ObjectToInput(data.d.Data.Instance, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };


    this.CallRenderLookUps = function (data) {
          if (data.d.Success === true && data.d.Data.LookUps) {

              data.d.Data.LookUps.forEach(function (elementSource) {
              generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items, false);
 
              });
          }
     };



    this.Role_insert = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#RoleSaveBtn'));          
            var ReleAddResult;
            var errors;
            var ReleAddMessage;
               $.ajax({
                    type: "POST",
                    url: constants.fasiApi.base + 'Members/v1/RoleAdd',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ Id: generalSupport.NumericValue('#Id', -99999, 99999), Name: $('#Name').val(), IsBackOfficeSource: $('#IsBackOfficeSource').is(':checked'), SecurityLevel: generalSupport.NumericValue('#SecurityLevel', 1, 9) }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         ReleAddResult = data.Successfully;
ReleAddMessage = data.Reason;
        AutoNumeric.set('#Id', data.Data.Id);

            if (ReleAddResult === true){
                $('#RoleTbl').bootstrapTable('append', row);
                $modal.modal('hide');
                var message4 = $.i18n.t('app.form.Role_Message_Notify_insert4');
                notification.swal.success($.i18n.t('app.form.Role_Title_Notify_insert4'), message4);
        data = app.core.SyncWebMethod("/fasi/dli/forms/RoleManagerActions.aspx/Clean5e21d681d528473c81c3aec7253ef516", false,
               JSON.stringify({  }));
               

                }                
                else {
                var message6 = $.i18n.t('app.form.Role_Message_Notify_insert6');
                notification.swal.error($.i18n.t('app.form.Role_Title_Notify_insert6'), message6);

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };
    this.Role_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#RoleSaveBtn'));          
            var ReleUpdateResult;
            var errors;
               $.ajax({
                    type: "PUT",
                    url: constants.fasiApi.base + 'Members/v1/RoleUpdate',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ Id: generalSupport.NumericValue('#Id', -99999, 99999), Name: $('#Name').val(), IsBackOfficeSource: $('#IsBackOfficeSource').is(':checked'), SecurityLevel: generalSupport.NumericValue('#SecurityLevel', 1, 9) }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                         ReleUpdateResult = data.Successfully;
        $('#IsBackOfficeSource').prop("checked", data.Data);

            if (ReleUpdateResult === true){
                var message3 = $.i18n.t('app.form.Role_Message_Notify_update3');
                notification.swal.success($.i18n.t('app.form.Role_Title_Notify_update3'), message3);
        data = app.core.SyncWebMethod("/fasi/dli/forms/RoleManagerActions.aspx/Clean6b4b847d024f427ba5aea4397cdf59df", false,
               JSON.stringify({  }));
               

                    $('#RoleTbl').bootstrapTable('updateByUniqueId', { id: row.Id, row: row });
                    $modal.modal('hide');
                }                
                else {
                var message6 = $.i18n.t('app.form.Role_Message_Notify_update6');
                notification.swal.error($.i18n.t('app.form.Role_Title_Notify_update6'), message6);

                    }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };
    this.Role_delete = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#RoleSaveBtn'));          
            var RoleRemoveResult;
            var errors;
            var roleInUser;
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Members/v1/RoleInUser?RoleId=' + generalSupport.NumericValue('#Id', -99999, 99999),
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
                         roleInUser = data.Data;

            if (roleInUser === false){
               $.ajax({
                    type: "DELETE",
                    url: constants.fasiApi.base + 'Members/v1/RoleRemove?Id=' + generalSupport.NumericValue('#Id', -99999, 99999),
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
                                     if (data.Successfully === true) {
           RoleRemoveResult = data.Successfully;

                               if (RoleRemoveResult === true){
                        $('#RoleTbl').bootstrapTable('remove', {field: 'Id', values: [generalSupport.NumericValue('#Id', -99999, 99999)]});
                        var message6 = $.i18n.t('app.form.Role_Message_Notify_delete6');
                        notification.swal.success($.i18n.t('app.form.Role_Title_Notify_delete6'), message6);
        data = app.core.SyncWebMethod("/fasi/dli/forms/RoleManagerActions.aspx/Clean8a7886f19915466caf50a1c5e03590bb", false,
               JSON.stringify({  }));
               

                            var message8 = $.i18n.t('app.form.Role_Message_Notify_delete8');
                            notification.swal.error($.i18n.t('app.form.Role_Title_Notify_delete8'), message8);
                            }                            
                            else {
                        var message9 = $.i18n.t('app.form.Role_Message_Notify_delete9');
                        notification.swal.error($.i18n.t('app.form.Role_Title_Notify_delete9'), message9);

                                }

            }
            else
           generalSupport.NotifyFail(data.Reason, data.Code, true);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown, true);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });                        }                        
                        else {
                var message11 = $.i18n.t('app.form.Role_Message_Notify_delete11');
                notification.swal.error($.i18n.t('app.form.Role_Title_Notify_delete11'), message11);

                            }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown, true);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };
    this.Role_BeforeShowPopup = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#RoleSaveBtn'));          
        if (row.Id === 0){
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Members/v1/RoleIndex',
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
                                     if (data.Successfully === true) {
                   AutoNumeric.set('#Id', data.Data);

           
            }
            else
           generalSupport.NotifyFail(data.Reason, data.Code);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
            }

    };

    this.ControlActions =   function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#RoleManagerMainForm").validate({
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

            rules: {

            },
            messages: {

            }
        });
        $("#RoleEditForm").validate().destroy();
        $("#RoleEditForm").validate({
            rules: {
                Id: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999
                },
                Name: {
                    required: true,
                    maxlength: 15
                },
                SecurityLevel: {
                    AutoNumericMinValue: 1,
                    AutoNumericMaxValue: 9
                }

            },
            messages: {
                Id: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Id.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Id.AutoNumericMaxValue')
                },
                Name: {
                    required: $.i18n.t('app.validation.Name.required'),
                    maxlength: $.i18n.t('app.validation.Name.maxlength')
                },
                SecurityLevel: {
                    AutoNumericMinValue: $.i18n.t('app.validation.SecurityLevel.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.SecurityLevel.AutoNumericMaxValue')
                }

            }
        });

    };

    this.RoleTblRequest = function (params) {
        $.ajax({
             type: "GET",
             url: constants.fasiApi.base + 'Members/v1/Roles?startIndex=' + (((params.data.offset !== undefined) ? params.data.offset : 0)+1) + '&endIndex=' + (((params.data.offset !== undefined) ? params.data.offset : 0)+((params.data.limit !== undefined) ? params.data.limit : 0)) + '&filter=' + ((params.data.search !== undefined) ? params.data.search : ''),
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
    this.RoleTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Id',
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
                }
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'pdf', 'xlsx'],
            toolbar: '#Roletoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'RoleManagerSupport.selected_Formatter'
            }, {
                field: 'Id',
                title: $.i18n.t('app.form.RoleTbl_Id_Title'),
                formatter: 'RoleManagerSupport.Id_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'Name',
                title: $.i18n.t('app.form.RoleTbl_Name_Title'),
                events: 'RoleActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'SecurityLevel',
                title: $.i18n.t('app.form.RoleTbl_SecurityLevel_Title'),
                formatter: 'RoleManagerSupport.SecurityLevel_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'IsBackOfficeSource',
                title: $.i18n.t('app.form.RoleTbl_IsBackOfficeSource_Title'),
                formatter: 'RoleManagerSupport.IsBackOfficeSource_IsCheck',
                sortable: false,
                halign: 'center'
            }]
        });


        $('#RoleTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#RoleTbl');
            $('#RoleRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#RoleRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#RoleTbl').bootstrapTable('getSelections'), function (row) {		
                RoleManagerSupport.RoleRowToInput(row);
                RoleManagerSupport.Role_delete(row, null);
                
                return row.Id;
            });

            $('#RoleRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#RoleCreateBtn').click(function () {
            var formInstance = $("#RoleEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            RoleManagerSupport.RoleShowModal($('#RolePopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#RolePopup').find('#RoleSaveBtn').click(function () {
            var formInstance = $("#RoleEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#RolePopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#RoleSaveBtn').html();
                $('#RoleSaveBtn').html('Procesando...');
                $('#RoleSaveBtn').prop('disabled', true);

                RoleManagerSupport.currentRow.Id = generalSupport.NumericValue('#Id', -99999, 99999);
                RoleManagerSupport.currentRow.Name = $('#Name').val();
                RoleManagerSupport.currentRow.SecurityLevel = generalSupport.NumericValue('#SecurityLevel', 1, 9);
                RoleManagerSupport.currentRow.IsBackOfficeSource = $('#IsBackOfficeSource').is(':checked');

                $('#RoleSaveBtn').prop('disabled', false);
                $('#RoleSaveBtn').html(caption);

                if (wm === 'Update') {
                    RoleManagerSupport.Role_update(RoleManagerSupport.currentRow, $modal);
                }
                else {                    
                    RoleManagerSupport.Role_insert(RoleManagerSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.RoleShowModal = function (md, title, row) {
        var formInstance = $("#RoleEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { Id: 0, Name: '', SecurityLevel: 0, IsBackOfficeSource: null };

        md.data('id', row.Id);
        md.find('.modal-title').text(title);

        RoleManagerSupport.RoleRowToInput(row);
        $('#IsBackOfficeSource').prop('disabled', true);
        RoleManagerSupport.Role_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.RoleRowToInput = function (row) {
        RoleManagerSupport.currentRow = row;
        AutoNumeric.set('#Id', row.Id);
        $('#Name').val(row.Name);
        AutoNumeric.set('#SecurityLevel', row.SecurityLevel);
        $('#IsBackOfficeSource').prop("checked", row.IsBackOfficeSource);

    };





    this.Id_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.SecurityLevel_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 9,
            decimalPlaces: 0,
            minimumValue: 0
        });
      };


	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#RoleTbl *').prop('disabled'),
                  checked: value
                 }
      };
	    this.IsBackOfficeSource_IsCheck = function (value, row, index) {
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



  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('RoleManager', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        RoleManagerSupport.ValidateSetup();
        
        

    RoleManagerSupport.ControlBehaviour();
    RoleManagerSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/RoleManagerActions.aspx/Initialization", false,
            JSON.stringify({
                id: $('#RoleManagerFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  RoleManagerSupport.CallRenderLookUps(data);
                
            
                $("#RoleTblPlaceHolder").replaceWith('<table id="RoleTbl"></table>');
    RoleManagerSupport.RoleTblSetup($('#RoleTbl'));

                    $('#RoleTbl').bootstrapTable('refreshOptions', { ajax: RoleManagerSupport.RoleTblRequest });

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: $("#RoleManagerMainForm"),
        CallBack: RoleManagerSupport.Init
    });
});

window.RoleActionEvents = {
    'click .update': function (e, value, row, index) {
        RoleManagerSupport.RoleShowModal($('#RolePopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};

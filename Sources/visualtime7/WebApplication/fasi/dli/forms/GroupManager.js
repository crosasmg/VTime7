var GroupManagerSupport = new function () {

    this.currentRow = {};
    this.newIndex = -1;
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#GroupManagerFormId').val(),
            UserGroup_UserGroup: generalSupport.NormalizeProperties($('#UserGroupTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#GroupManagerFormId').val(data.InstanceFormId);

        GroupManagerSupport.LookUpForOwnerId(source);

        $('#UserGroupTbl').bootstrapTable('refreshOptions', { ajax: GroupManagerSupport.UserGroupTblRequest });
        if (data.UserGroup_UserGroup !== null)
            $('#UserGroupTbl').bootstrapTable('load', data.UserGroup_UserGroup);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Id', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });






    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               GroupManagerSupport.ObjectToInput(data.d.Data.Instance, source);
            
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



    this.UserGroup_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#UserGroupSaveBtn'));          
            var UpdateResult;
            var errors;
               $.ajax({
                    type: "PUT",
                    url: constants.fasiApi.base + 'Members/v1/GroupUpdate',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ Id: generalSupport.NumericValue('#Id', -99999, 99999), OwnerId: parseInt(0 + $('#OwnerId').val(), 10), Description: $('#Description').val() }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                                     if (data.Successfully === true) {
           UpdateResult = data.Data;

                       if (UpdateResult === true){
                $('#UserGroupTbl').bootstrapTable('updateByUniqueId', { id: row.Id, row: row });
                $modal.modal('hide');
                var message4 = $.i18n.t('app.form.UserGroup_Message_Notify_update4');
                notification.toastr.success($.i18n.t('app.form.UserGroup_Title_Notify_update4'), message4);
        data = app.core.SyncWebMethod("/fasi/dli/forms/GroupManagerActions.aspx/Cleanaf7cbf44c1744de08b19dead5caaf81b", false,
               JSON.stringify({  }));
               

                }                
                else {
                var message6 = $.i18n.t('app.form.UserGroup_Message_Notify_update6');
                notification.swal.error($.i18n.t('app.form.UserGroup_Title_Notify_update6'), message6);

                    }

            }
            else
           generalSupport.NotifyFail(data.Reason, data.Code);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };
    this.UserGroup_delete = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#UserGroupSaveBtn'));          
            var GroupDeleteResult;
            var errors;
            var ExisteRelationInDiary;
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.diary + 'GroupsUsedInTask?Ids=' + generalSupport.NumericValue('#Id', -99999, 99999),
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
                         ExisteRelationInDiary = data;

            if (ExisteRelationInDiary === false){
               $.ajax({
                    type: "DELETE",
                    url: constants.fasiApi.base + 'Members/v1/GroupRemove?Id=' + generalSupport.NumericValue('#Id', -99999, 99999),
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
           GroupDeleteResult = data.Data;

                               if (GroupDeleteResult === true){
                        $('#UserGroupTbl').bootstrapTable('remove', {field: 'Id', values: [generalSupport.NumericValue('#Id', -99999, 99999)]});
                        var message6 = $.i18n.t('app.form.UserGroup_Message_Notify_delete6');
                        notification.toastr.success($.i18n.t('app.form.UserGroup_Title_Notify_delete6'), message6);
        data = app.core.SyncWebMethod("/fasi/dli/forms/GroupManagerActions.aspx/Clean4737a973c8804e17a613f227f65a2a3b", false,
               JSON.stringify({  }));
               

                            var message8 = $.i18n.t('app.form.UserGroup_Message_Notify_delete8');
                            notification.swal.error($.i18n.t('app.form.UserGroup_Title_Notify_delete8'), message8);
                            }                            
                            else {
                        var message9 = $.i18n.t('app.form.UserGroup_Message_Notify_delete9');
                        notification.toastr.error($.i18n.t('app.form.UserGroup_Title_Notify_delete9'), message9);

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
                var message11 = $.i18n.t('app.form.UserGroup_Message_Notify_delete11');
                notification.swal.error($.i18n.t('app.form.UserGroup_Title_Notify_delete11'), message11);

                            }

                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown, true);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };
    this.UserGroup_insert = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#UserGroupSaveBtn'));          
            var RoleAddResult;
            var errors;
            var GroupIndexLast;
               $.ajax({
                    type: "POST",
                    url: constants.fasiApi.base + 'Members/v1/GroupsAdd',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    data: JSON.stringify({ Id: generalSupport.NumericValue('#Id', -99999, 99999), OwnerId: parseInt(0 + $('#OwnerId').val(), 10), Description: $('#Description').val() }),
                    headers: {
                        'Accept-Language': localStorage.getItem('languageName')
                    },
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                    success: function (data) {
                                     if (data.Successfully === true) {
           RoleAddResult = data.Successfully;

                       if (RoleAddResult === true){
                $('#UserGroupTbl').bootstrapTable('append', row);
                $modal.modal('hide');
                var message4 = $.i18n.t('app.form.UserGroup_Message_Notify_insert4');
                notification.toastr.success($.i18n.t('app.form.UserGroup_Title_Notify_insert4'), message4);
        data = app.core.SyncWebMethod("/fasi/dli/forms/GroupManagerActions.aspx/Clean7a52486485fb451a9da6eb0b3ee4295c", false,
               JSON.stringify({  }));
               

                }                
                else {
                var message6 = $.i18n.t('app.form.UserGroup_Message_Notify_insert6');
                notification.swal.error($.i18n.t('app.form.UserGroup_Title_Notify_insert6'), message6);

                    }

            }
            else
           generalSupport.NotifyFail(data.Reason, data.Code);
                    },
                    error: function (qXHR, textStatus, errorThrown) {
                        generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                    }
                }).done(function () { if ($('#btnLoading').length > 0){ btnLoading.stop(); } });
    };
    this.UserGroup_BeforeShowPopup = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#UserGroupSaveBtn'));          
            var GroupIndeResult;
        if (row.Id === 0){
            $('#Id').prop('disabled', true);
            $('#IdLabel').prop('disabled', true);
               $.ajax({
                    type: "GET",
                    url: constants.fasiApi.base + 'Members/v1/GroupIndex',
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


        $("#GroupManagerMainForm").validate({
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
        $("#UserGroupEditForm").validate().destroy();
        $("#UserGroupEditForm").validate({
            rules: {
                Id: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                Description: {
                    required: true,
                    maxlength: 39
                },
                OwnerId: {
                }

            },
            messages: {
                Id: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Id.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Id.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.Id.required')
                },
                Description: {
                    required: $.i18n.t('app.validation.Description.required'),
                    maxlength: $.i18n.t('app.validation.Description.maxlength')
                },
                OwnerId: {
                }

            }
        });

    };
    this.LookUpForOwnerIdFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#OwnerId>option[value='" + value + "']").text();
        }
        return result;
    };
   this.LookUpForOwnerId = function (defaultValue, source) {
        var ctrol = $('#OwnerId');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));
            return $.ajax({
                type: "GET",
                url: constants.fasiApi.base + 'Members/v1/UsersLkp?userType=1&' + 'Ids=*',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                data: JSON.stringify({  }),
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("Authorization", "Bearer " + app.user.token);
                    },
                success: function (data) {
                    ctrol.children().remove();
                    if (data.Successfully === true) {
                        ctrol.append($('<option />').val(0).text(''));
                        data.Data.forEach(function (element) {
                            ctrol.append($('<option />').val(element.Code).text(element.Description));
                        });

                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);
						                if(source !== 'Initialization')
                              ctrol.change();
                              
                            
                            
                    }
                    else
                        generalSupport.NotifyFail(data.d.Reason, data.d.Code);
                },
                error: function (qXHR, textStatus, errorThrown) {
                    generalSupport.ErrorHandler(qXHR, textStatus, errorThrown);
                }
            });
        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);
					         
                   if(source !== 'Initialization')
                      ctrol.change();
				   }
    };

    this.UserGroupTblRequest = function (params) {
        $.ajax({
             type: "GET",
             url: constants.fasiApi.base + 'Members/v1/Groups?startIndex=' + (((params.data.offset !== undefined) ? params.data.offset : 0)+1) + '&endIndex=' + (((params.data.offset !== undefined) ? params.data.offset : 0)+((params.data.limit !== undefined) ? params.data.limit : 0)) + '&filter=' + ((params.data.search !== undefined) ? params.data.search : ''),
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
    this.UserGroupTblSetup = function (table) {
        GroupManagerSupport.LookUpForOwnerId('');
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
            toolbar: '#UserGrouptoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'GroupManagerSupport.selected_Formatter'
            }, {
                field: 'Id',
                title: $.i18n.t('app.form.UserGroupTbl_Id_Title'),
                formatter: 'GroupManagerSupport.Id_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right',
                visible: false
            }, {
                field: 'Description',
                title: $.i18n.t('app.form.UserGroupTbl_Description_Title'),
                events: 'UserGroupActionEvents',
                formatter: 'tableHelperSupport.EditCommandFormatter',
                sortable: false,
                halign: 'center'
            }, {
                field: 'OwnerId',
                title: $.i18n.t('app.form.UserGroupTbl_OwnerId_Title'),
                formatter: 'GroupManagerSupport.LookUpForOwnerIdFormatter',
                sortable: false,
                halign: 'center'
            }]
        });


        $('#UserGroupTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#UserGroupTbl');
            $('#UserGroupRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#UserGroupRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#UserGroupTbl').bootstrapTable('getSelections'), function (row) {		
                GroupManagerSupport.UserGroupRowToInput(row);
                GroupManagerSupport.UserGroup_delete(row, null);
                
                return row.Id;
            });

            $('#UserGroupRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#UserGroupCreateBtn').click(function () {
            var formInstance = $("#UserGroupEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            GroupManagerSupport.UserGroupShowModal($('#UserGroupPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#UserGroupPopup').find('#UserGroupSaveBtn').click(function () {
            var formInstance = $("#UserGroupEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#UserGroupPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';
                else
                   GroupManagerSupport.newIndex = GroupManagerSupport.newIndex - 1;
                   
                var caption = $('#UserGroupSaveBtn').html();
                $('#UserGroupSaveBtn').html('Procesando...');
                $('#UserGroupSaveBtn').prop('disabled', true);

                GroupManagerSupport.currentRow.Id = generalSupport.NumericValue('#Id', -99999, 99999);
                GroupManagerSupport.currentRow.Description = $('#Description').val();
                GroupManagerSupport.currentRow.OwnerId = parseInt(0 + $('#OwnerId').val(), 10);

                $('#UserGroupSaveBtn').prop('disabled', false);
                $('#UserGroupSaveBtn').html(caption);

                if (wm === 'Update') {
                    GroupManagerSupport.UserGroup_update(GroupManagerSupport.currentRow, $modal);
                }
                else {                    
                    GroupManagerSupport.UserGroup_insert(GroupManagerSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.UserGroupShowModal = function (md, title, row) {
        var formInstance = $("#UserGroupEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { Id: 0, Description: '', OwnerId: 0 };

        md.data('id', row.Id);
        md.find('.modal-title').text(title);

        GroupManagerSupport.UserGroupRowToInput(row);

        GroupManagerSupport.UserGroup_BeforeShowPopup(row, md);
        md.appendTo("body");
        md.modal('show');
    };

    this.UserGroupRowToInput = function (row) {
        GroupManagerSupport.currentRow = row;
        AutoNumeric.set('#Id', row.Id);
        $('#Description').val(row.Description);
        GroupManagerSupport.LookUpForOwnerId(row.OwnerId, '');
        $('#OwnerId').trigger('change');

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


	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#UserGroupTbl *').prop('disabled'),
                  checked: value
                 }
      };



  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('GroupManager', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        GroupManagerSupport.ValidateSetup();
        
        

    GroupManagerSupport.ControlBehaviour();
    GroupManagerSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/GroupManagerActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), false,
            JSON.stringify({
                id: $('#GroupManagerFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  GroupManagerSupport.CallRenderLookUps(data);
                
            
                $("#UserGroupTblPlaceHolder").replaceWith('<table id="UserGroupTbl"></table>');
    GroupManagerSupport.UserGroupTblSetup($('#UserGroupTbl'));

                    $('#UserGroupTbl').bootstrapTable('refreshOptions', { ajax: GroupManagerSupport.UserGroupTblRequest });

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: $("#GroupManagerMainForm"),
        CallBack: GroupManagerSupport.Init
    });
});

window.UserGroupActionEvents = {
    'click .update': function (e, value, row, index) {
        GroupManagerSupport.UserGroupShowModal($('#UserGroupPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};

var CustomMenuSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#CustomMenuFormId').val(),
            UserCustomMenu_Grid_UserCustomMenu_Item: generalSupport.NormalizeProperties($('#UserCustomMenu_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#CustomMenuFormId').val(data.InstanceFormId);

        CustomMenuSupport.LookUpForCodispl(source);

        CustomMenuSupport.UserCustomMenu_GridTblRequest();
        if (data.UserCustomMenu_Grid_UserCustomMenu_Item !== null)
            $('#UserCustomMenu_GridTbl').bootstrapTable('load', data.UserCustomMenu_Grid_UserCustomMenu_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#OptionOrder', {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: -999
        });






    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               CustomMenuSupport.ObjectToInput(data.d.Data.Instance, source);
            
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



    this.UserCustomMenu_Grid_insert = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#UserCustomMenu_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/CustomMenuActions.aspx/UserCustomMenu_Grid1InsertCommandActionUserCustomMenu", false,
               JSON.stringify({ USERID0: app.user.userId, CODISPL2: row.Codispl, OPTIONORDER3: row.OptionOrder }));
               

        if (data.d.Success === true){
            $('#UserCustomMenu_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.UserCustomMenu_Grid_Message_Notify_insert4');
            notification.toastr.success($.i18n.t('app.form.UserCustomMenu_Grid_Title_Notify_insert4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.UserCustomMenu_Grid_Message_Notify_insert5');
            notification.swal.error($.i18n.t('app.form.UserCustomMenu_Grid_Title_Notify_insert5'), message5);

                }

    };
    this.UserCustomMenu_Grid_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#UserCustomMenu_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/CustomMenuActions.aspx/UserCustomMenu_Grid1UpdateCommandActionUserCustomMenu", false,
               JSON.stringify({ OPTIONORDER1: row.OptionOrder,                 UserCustomMenuUserID2: app.user.userId, UserCustomMenuCodispl3: row.Codispl }));
               

        if (data.d.Success === true){
            $('#UserCustomMenu_GridTbl').bootstrapTable('updateByUniqueId', { id: row.Codispl, row: row });
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.UserCustomMenu_Grid_Message_Notify_update4');
            notification.toastr.success($.i18n.t('app.form.UserCustomMenu_Grid_Title_Notify_update4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.UserCustomMenu_Grid_Message_Notify_update5');
            notification.swal.error($.i18n.t('app.form.UserCustomMenu_Grid_Title_Notify_update5'), message5);

                }

    };
    this.UserCustomMenu_Grid_delete = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#UserCustomMenu_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/CustomMenuActions.aspx/UserCustomMenu_Grid1DeleteCommandActionUserCustomMenu", false,
               JSON.stringify({                 UserCustomMenuUserID1: app.user.userId, UserCustomMenuCodispl2: row.Codispl }));
               

        if (data.d.Success === true){
            $('#UserCustomMenu_GridTbl').bootstrapTable('remove', {field: 'Codispl', values: [$('#Codispl').val()]});
            var message4 = $.i18n.t('app.form.UserCustomMenu_Grid_Message_Notify_delete4');
            notification.toastr.success($.i18n.t('app.form.UserCustomMenu_Grid_Title_Notify_delete4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.UserCustomMenu_Grid_Message_Notify_delete5');
            notification.toastr.error($.i18n.t('app.form.UserCustomMenu_Grid_Title_Notify_delete5'), message5);

                }

    };

    this.ControlActions =   function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#CustomMenuMainForm").validate({
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
        $("#UserCustomMenu_GridEditForm").validate().destroy();
        $("#UserCustomMenu_GridEditForm").validate({
            rules: {
                Codispl: {
                    required: true                },
                OptionOrder: {
                    AutoNumericMinValue: -999,
                    AutoNumericMaxValue: 999,
                    required: true
                }

            },
            messages: {
                Codispl: {
                    required: $.i18n.t('app.validation.Codispl.required')                },
                OptionOrder: {
                    AutoNumericMinValue: $.i18n.t('app.validation.OptionOrder.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.OptionOrder.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.OptionOrder.required')
                }

            }
        });

    };
    this.LookUpForCodisplFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Codispl>option[value='" + value + "']").text();
        }
        return '<a href="javascript:void(0)" class="update edit" data-modal-title="Editar" title="Haga click para editar">' + result + '</a>';
    };
    this.LookUpForCodispl = function (defaultValue, source) {
        var ctrol = $('#Codispl');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/CustomMenuActions.aspx/LookUpForCodispl", false,
                JSON.stringify({ id: $('#CustomMenuFormId').val() }),
                function (data) {
                    ctrol.children().remove();
                        
                        $.each(data.d.Data, function () {
                            ctrol.append($('<option />').val(this['Code']).text(this['Description']));
                        });
                        if (defaultValue !== null)
                            ctrol.val(defaultValue);
                        else
                            ctrol.val(0);

                        if (source !== 'Initialization')
                            ctrol.change();
                            
                            
                });

        }
        else
            if (typeof defaultValue !== 'undefined' && defaultValue !== null)
                if (defaultValue.toString() !== (ctrol.val() || '0').toString()) {
                    ctrol.val(defaultValue);

                    if (source !== 'Initialization')
                        ctrol.change();
                }
    };

    this.UserCustomMenu_GridTblSetup = function (table) {
        CustomMenuSupport.LookUpForCodispl('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Codispl',
            sortable: true,
            sidePagination: 'client',
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
                maxNestedTables: 0
            },
            exportTypes: ['json', 'xml', 'csv', 'txt', 'excel'],
            toolbar: '#UserCustomMenu_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'CustomMenuSupport.selected_Formatter'
            }, {
                field: 'Codispl',
                title: $.i18n.t('app.form.UserCustomMenu_GridTbl_Codispl_Title'),
                events: 'UserCustomMenu_GridActionEvents',
                formatter: 'CustomMenuSupport.LookUpForCodisplFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'OptionOrder',
                title: $.i18n.t('app.form.UserCustomMenu_GridTbl_OptionOrder_Title'),
                formatter: 'CustomMenuSupport.OptionOrder_FormatterMaskData',
                sortable: true,
                halign: 'center'
            }]
        });


        $('#UserCustomMenu_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#UserCustomMenu_GridTbl');
            $('#UserCustomMenu_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#UserCustomMenu_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#UserCustomMenu_GridTbl').bootstrapTable('getSelections'), function (row) {		
                CustomMenuSupport.UserCustomMenu_GridRowToInput(row);
                CustomMenuSupport.UserCustomMenu_Grid_delete(row, null);
                
                return row.Codispl;
            });

            $('#UserCustomMenu_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#UserCustomMenu_GridCreateBtn').click(function () {
            var formInstance = $("#UserCustomMenu_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            CustomMenuSupport.UserCustomMenu_GridShowModal($('#UserCustomMenu_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#UserCustomMenu_GridPopup').find('#UserCustomMenu_GridSaveBtn').click(function () {
            var formInstance = $("#UserCustomMenu_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#UserCustomMenu_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#UserCustomMenu_GridSaveBtn').html();
                $('#UserCustomMenu_GridSaveBtn').html('Procesando...');
                $('#UserCustomMenu_GridSaveBtn').prop('disabled', true);

                CustomMenuSupport.currentRow.Codispl = $('#Codispl').val();
                CustomMenuSupport.currentRow.OptionOrder = generalSupport.NumericValue('#OptionOrder', -999, 999);

                $('#UserCustomMenu_GridSaveBtn').prop('disabled', false);
                $('#UserCustomMenu_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    CustomMenuSupport.UserCustomMenu_Grid_update(CustomMenuSupport.currentRow, $modal);
                }
                else {                    
                    CustomMenuSupport.UserCustomMenu_Grid_insert(CustomMenuSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.UserCustomMenu_GridShowModal = function (md, title, row) {
        var formInstance = $("#UserCustomMenu_GridEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { Codispl: '', OptionOrder: 0 };

        md.data('id', row.Codispl);
        md.find('.modal-title').text(title);

        CustomMenuSupport.UserCustomMenu_GridRowToInput(row);
        $('#Codispl').prop('disabled', (row.Codispl !== ''));

        md.appendTo("body");
        md.modal('show');
    };

    this.UserCustomMenu_GridRowToInput = function (row) {
        CustomMenuSupport.currentRow = row;
        CustomMenuSupport.LookUpForCodispl(row.Codispl, '');
        $('#Codispl').trigger('change');
        AutoNumeric.set('#OptionOrder', row.OptionOrder);

    };
    this.UserCustomMenu_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/CustomMenuActions.aspx/UserCustomMenu_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: '',
                UserCustomMenuUserID1: app.user.userId
              }),
              function (data) {
                  $('#UserCustomMenu_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.OptionOrder_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 999,
            decimalPlaces: 0,
            minimumValue: -999
        });
      };


	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#UserCustomMenu_GridTbl *').prop('disabled'),
                  checked: value
                 }
      };



  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('CustomMenu', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        CustomMenuSupport.ValidateSetup();
        
        

    CustomMenuSupport.ControlBehaviour();
    CustomMenuSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/CustomMenuActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), false,
            JSON.stringify({
                id: $('#CustomMenuFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  CustomMenuSupport.CallRenderLookUps(data);
                
            
                $("#UserCustomMenu_GridTblPlaceHolder").replaceWith('<table id="UserCustomMenu_GridTbl"></table>');
    CustomMenuSupport.UserCustomMenu_GridTblSetup($('#UserCustomMenu_GridTbl'));

                    CustomMenuSupport.UserCustomMenu_GridTblRequest();

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        IsConnected: true,
        Element: $("#CustomMenuMainForm"),
        CallBack: CustomMenuSupport.Init
    });
});

window.UserCustomMenu_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        CustomMenuSupport.UserCustomMenu_GridShowModal($('#UserCustomMenu_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};

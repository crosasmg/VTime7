var GruposPorRolSupport = new function () {

    this.currentRow = {};
    this.newIndex = -1;
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#GruposPorRolFormId').val(),
            GrupoXRol_Grid_GrupoXRol_Item: generalSupport.NormalizeProperties($('#GrupoXRol_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#GruposPorRolFormId').val(data.InstanceFormId);

        GruposPorRolSupport.LookUpForId_Rol(source);
        GruposPorRolSupport.LookUpForId_Grupo_Acceso(source);

        GruposPorRolSupport.GrupoXRol_GridTblRequest();
        if (data.GrupoXRol_Grid_GrupoXRol_Item !== null)
            $('#GrupoXRol_GridTbl').bootstrapTable('load', data.GrupoXRol_Grid_GrupoXRol_Item);

    };

    this.ControlBehaviour = function () {



      new AutoNumeric('#Id_Compania', {
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
               GruposPorRolSupport.ObjectToInput(data.d.Data.Instance, source);
            
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



    this.GrupoXRol_Grid_insert = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#GrupoXRol_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/GruposPorRolActions.aspx/GrupoXRol_Grid1InsertCommandActionGrupoXRol", false,
               JSON.stringify({ ID_ROL1: row.Id_Rol, ID_GRUPO_ACCESO2: row.Id_Grupo_Acceso, ID_COMPANIA3: row.Id_Compania, EXTERNALUSER4: row.ExternalUser }));
               

        if (data.d.Success === true){
            $('#GrupoXRol_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.GrupoXRol_Grid_Message_Notify_insert4');
            notification.toastr.success($.i18n.t('app.form.GrupoXRol_Grid_Title_Notify_insert4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.GrupoXRol_Grid_Message_Notify_insert5');
            notification.swal.error($.i18n.t('app.form.GrupoXRol_Grid_Title_Notify_insert5'), message5);

                }

    };
    this.GrupoXRol_Grid_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#GrupoXRol_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/GruposPorRolActions.aspx/GrupoXRol_Grid1UpdateCommandActionGrupoXRol", false,
               JSON.stringify({ EXTERNALUSER1: row.ExternalUser, GrupoXRolIdRol2: row.Id_Rol, GrupoXRolIdGrupoAcceso3: row.Id_Grupo_Acceso, GrupoXRolIdCompania4: row.Id_Compania }));
               

        if (data.d.Success === true){
            $('#GrupoXRol_GridTbl').bootstrapTable('updateByUniqueId', { id: row.Unique_Key, row: row });
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.GrupoXRol_Grid_Message_Notify_update4');
            notification.toastr.success($.i18n.t('app.form.GrupoXRol_Grid_Title_Notify_update4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.GrupoXRol_Grid_Message_Notify_update5');
            notification.swal.error($.i18n.t('app.form.GrupoXRol_Grid_Title_Notify_update5'), message5);

                }

    };
    this.GrupoXRol_Grid_delete = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#GrupoXRol_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/GruposPorRolActions.aspx/GrupoXRol_Grid1DeleteCommandActionGrupoXRol", false,
               JSON.stringify({ GrupoXRolIdRol1: row.Id_Rol, GrupoXRolIdGrupoAcceso2: row.Id_Grupo_Acceso, GrupoXRolIdCompania3: row.Id_Compania, GrupoXRolExternalUser4: row.ExternalUser }));
               

        if (data.d.Success === true){
            $('#GrupoXRol_GridTbl').bootstrapTable('uncheckAll');
            app.core.AsyncWebMethod("/fasi/dli/forms/GruposPorRolActions.aspx/GrupoXRol_GridTblDataLoad", false,
                JSON.stringify({
                    filter: $("div.bootstrap-table > div.fixed-table-toolbar > div.search > input.form-control").val()
                }),
                function (data) {
                    $('#GrupoXRol_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);
                });
            var message4 = $.i18n.t('app.form.GrupoXRol_Grid_Message_Notify_delete4');
            notification.toastr.success($.i18n.t('app.form.GrupoXRol_Grid_Title_Notify_delete4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.GrupoXRol_Grid_Message_Notify_delete5');
            notification.toastr.error($.i18n.t('app.form.GrupoXRol_Grid_Title_Notify_delete5'), message5);

                }

    };

    this.ControlActions =   function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();
        $.validator.addMethod('multiKeyValidateGrupoXRol_Grid', function (value, element)
        {
            return tableHelperSupport.MultipleKeyColumnValidate($('#GrupoXRol_GridTbl'), $('#GrupoXRol_GridEditForm'), GruposPorRolSupport.currentRow.Unique_Key);
        });


        $("#GruposPorRolMainForm").validate({
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
        $("#GrupoXRol_GridEditForm").validate().destroy();
        $("#GrupoXRol_GridEditForm").validate({
            rules: {
                Id_Rol: {
                    required: true,
                    multiKeyValidateGrupoXRol_Grid: true
                },
                Id_Grupo_Acceso: {
                    required: true,
                    multiKeyValidateGrupoXRol_Grid: true
                },
                Id_Compania: {
                    AutoNumericMinValue: -99999,
                    AutoNumericMaxValue: 99999,
                    required: true
                },
                ExternalUser: {
                    required: true                }

            },
            messages: {
                Id_Rol: {
                    required: $.i18n.t('app.validation.Id_Rol.required'),
                    multiKeyValidateGrupoXRol_Grid: $.i18n.t('app.validation.GrupoXRol_Grid_multiKeyValidate')                },
                Id_Grupo_Acceso: {
                    required: $.i18n.t('app.validation.Id_Grupo_Acceso.required'),
                    multiKeyValidateGrupoXRol_Grid: $.i18n.t('app.validation.GrupoXRol_Grid_multiKeyValidate')                },
                Id_Compania: {
                    AutoNumericMinValue: $.i18n.t('app.validation.Id_Compania.AutoNumericMinValue'),
                    AutoNumericMaxValue: $.i18n.t('app.validation.Id_Compania.AutoNumericMaxValue'),
                    required: $.i18n.t('app.validation.Id_Compania.required')
                },
                ExternalUser: {
                    required: $.i18n.t('app.validation.ExternalUser.required')                }

            }
        });

    };
    this.LookUpForId_RolFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Id_Rol>option[value='" + value + "']").text();
        }
        return '<a href="javascript:void(0)" class="update edit" data-modal-title="Editar" title="Haga click para editar">' + result + '</a>';
    };
    this.LookUpForId_Rol = function (defaultValue, source) {
        var ctrol = $('#Id_Rol');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/GruposPorRolActions.aspx/LookUpForId_Rol", false,
                JSON.stringify({ id: $('#GruposPorRolFormId').val() }),
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
    this.LookUpForId_Grupo_AccesoFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Id_Grupo_Acceso>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForId_Grupo_Acceso = function (defaultValue, source) {
        var ctrol = $('#Id_Grupo_Acceso');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/GruposPorRolActions.aspx/LookUpForId_Grupo_Acceso", false,
                JSON.stringify({ id: $('#GruposPorRolFormId').val() }),
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
    this.LookUpForExternalUserFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#ExternalUser>option[value='" + value + "']").text();
        }
        return result;
    };

    this.GrupoXRol_GridTblSetup = function (table) {
        GruposPorRolSupport.LookUpForId_Rol('');
        GruposPorRolSupport.LookUpForId_Grupo_Acceso('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 20,
            uniqueId: 'Unique_Key',
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
            toolbar: '#GrupoXRol_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'GruposPorRolSupport.selected_Formatter'
            }, {
                field: 'Id_Rol',
                title: $.i18n.t('app.form.GrupoXRol_GridTbl_Id_Rol_Title'),
                events: 'GrupoXRol_GridActionEvents',
                formatter: 'GruposPorRolSupport.LookUpForId_RolFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Id_Grupo_Acceso',
                title: $.i18n.t('app.form.GrupoXRol_GridTbl_Id_Grupo_Acceso_Title'),
                formatter: 'GruposPorRolSupport.LookUpForId_Grupo_AccesoFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Id_Compania',
                title: $.i18n.t('app.form.GrupoXRol_GridTbl_Id_Compania_Title'),
                formatter: 'GruposPorRolSupport.Id_Compania_FormatterMaskData',
                sortable: true,
                halign: 'center',
                visible: false
            }, {
                field: 'ExternalUser',
                title: $.i18n.t('app.form.GrupoXRol_GridTbl_ExternalUser_Title'),
                formatter: 'GruposPorRolSupport.LookUpForExternalUserFormatter',
                sortable: true,
                halign: 'center'
            }]
        });


        $('#GrupoXRol_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#GrupoXRol_GridTbl');
            $('#GrupoXRol_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#GrupoXRol_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#GrupoXRol_GridTbl').bootstrapTable('getSelections'), function (row) {		
                GruposPorRolSupport.GrupoXRol_GridRowToInput(row);
                GruposPorRolSupport.GrupoXRol_Grid_delete(row, null);
                
                return row.Unique_Key;
            });

            $('#GrupoXRol_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#GrupoXRol_GridCreateBtn').click(function () {
            var formInstance = $("#GrupoXRol_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            GruposPorRolSupport.GrupoXRol_GridShowModal($('#GrupoXRol_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#GrupoXRol_GridPopup').find('#GrupoXRol_GridSaveBtn').click(function () {
            var formInstance = $("#GrupoXRol_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#GrupoXRol_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';
                else
                   GruposPorRolSupport.newIndex = GruposPorRolSupport.newIndex - 1;
                   
                var caption = $('#GrupoXRol_GridSaveBtn').html();
                $('#GrupoXRol_GridSaveBtn').html('Procesando...');
                $('#GrupoXRol_GridSaveBtn').prop('disabled', true);

                GruposPorRolSupport.currentRow.Unique_Key = GruposPorRolSupport.newIndex;
                GruposPorRolSupport.currentRow.Id_Rol = parseInt(0 + $('#Id_Rol').val(), 10);
                GruposPorRolSupport.currentRow.Id_Grupo_Acceso = parseInt(0 + $('#Id_Grupo_Acceso').val(), 10);
                GruposPorRolSupport.currentRow.Id_Compania = generalSupport.NumericValue('#Id_Compania', -99999, 99999);
                GruposPorRolSupport.currentRow.ExternalUser = $('#ExternalUser').val();

                $('#GrupoXRol_GridSaveBtn').prop('disabled', false);
                $('#GrupoXRol_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    GruposPorRolSupport.GrupoXRol_Grid_update(GruposPorRolSupport.currentRow, $modal);
                }
                else {                    
                    GruposPorRolSupport.GrupoXRol_Grid_insert(GruposPorRolSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.GrupoXRol_GridShowModal = function (md, title, row) {
        var formInstance = $("#GrupoXRol_GridEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { Id_Rol: 0, Id_Grupo_Acceso: 0, Id_Compania: 1, ExternalUser: '' };

        md.data('id', row.Unique_Key);
        md.find('.modal-title').text(title);

        GruposPorRolSupport.GrupoXRol_GridRowToInput(row);
        $('#Id_Rol').prop('disabled', (row.Id_Rol !== 0));
        $('#Id_Grupo_Acceso').prop('disabled', (row.Id_Rol !== 0));
        $('#Id_Compania').prop('disabled', (row.Id_Rol === 0));

        md.appendTo("body");
        md.modal('show');
    };

    this.GrupoXRol_GridRowToInput = function (row) {
        GruposPorRolSupport.currentRow = row;
        GruposPorRolSupport.LookUpForId_Rol(row.Id_Rol, '');
        $('#Id_Rol').trigger('change');
        GruposPorRolSupport.LookUpForId_Grupo_Acceso(row.Id_Grupo_Acceso, '');
        $('#Id_Grupo_Acceso').trigger('change');
        AutoNumeric.set('#Id_Compania', row.Id_Compania);
        $('#ExternalUser').val(row.ExternalUser);
        $('#ExternalUser').trigger('change');

    };
    this.GrupoXRol_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/GruposPorRolActions.aspx/GrupoXRol_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#GrupoXRol_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };





    this.Id_Compania_FormatterMaskData = function (value, row, index) {          
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
                  disabled: $('#GrupoXRol_GridTbl *').prop('disabled'),
                  checked: value
                 }
      };



  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('GruposPorRol', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        GruposPorRolSupport.ValidateSetup();
        
        

    GruposPorRolSupport.ControlBehaviour();
    GruposPorRolSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/GruposPorRolActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), false,
            JSON.stringify({
                id: $('#GruposPorRolFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  GruposPorRolSupport.CallRenderLookUps(data);
                
            
                $("#GrupoXRol_GridTblPlaceHolder").replaceWith('<table id="GrupoXRol_GridTbl"></table>');
    GruposPorRolSupport.GrupoXRol_GridTblSetup($('#GrupoXRol_GridTbl'));

                    GruposPorRolSupport.GrupoXRol_GridTblRequest();

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: $("#GruposPorRolMainForm"),
        CallBack: GruposPorRolSupport.Init
    });
});

window.GrupoXRol_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        GruposPorRolSupport.GrupoXRol_GridShowModal($('#GrupoXRol_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};

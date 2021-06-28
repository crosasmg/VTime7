var AccesosPorGrupoSeguridadSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#AccesosPorGrupoSeguridadFormId').val(),
            AccesoXGrupo_Grid_AccesoXGrupo_Item: generalSupport.NormalizeProperties($('#AccesoXGrupo_GridTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#AccesosPorGrupoSeguridadFormId').val(data.InstanceFormId);

        AccesosPorGrupoSeguridadSupport.LookUpForId_Acceso(source);
        AccesosPorGrupoSeguridadSupport.LookUpForId_Grupo_Acceso(source);

        AccesosPorGrupoSeguridadSupport.AccesoXGrupo_GridTblRequest();
        if (data.AccesoXGrupo_Grid_AccesoXGrupo_Item !== null)
            $('#AccesoXGrupo_GridTbl').bootstrapTable('load', data.AccesoXGrupo_Grid_AccesoXGrupo_Item);

    };

    this.ControlBehaviour = function () {









    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               AccesosPorGrupoSeguridadSupport.ObjectToInput(data.d.Data.Instance, source);
            
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



    this.AccesoXGrupo_Grid_insert = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#AccesoXGrupo_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/AccesosPorGrupoSeguridadActions.aspx/AccesoXGrupo_Grid1InsertCommandActionAccesoXGrupo", false,
               JSON.stringify({ ID_ACCESO1: row.Id_Acceso, ID_GRUPO_ACCESO2: row.Id_Grupo_Acceso }));
               

        if (data.d.Success === true){
            $('#AccesoXGrupo_GridTbl').bootstrapTable('append', row);
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.AccesoXGrupo_Grid_Message_Notify_insert4');
            notification.toastr.success($.i18n.t('app.form.AccesoXGrupo_Grid_Title_Notify_insert4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.AccesoXGrupo_Grid_Message_Notify_insert5');
            notification.swal.error($.i18n.t('app.form.AccesoXGrupo_Grid_Title_Notify_insert5'), message5);

                }

    };
    this.AccesoXGrupo_Grid_update = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#AccesoXGrupo_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/AccesosPorGrupoSeguridadActions.aspx/AccesoXGrupo_Grid1UpdateCommandActionAccesoXGrupo", false,
               JSON.stringify({ ID_ACCESO1: row.Id_Acceso, ID_GRUPO_ACCESO2: row.Id_Grupo_Acceso, AccesoXGrupoIdAcceso3: row.Id_Acceso, AccesoXGrupoIdGrupoAcceso4: row.Id_Grupo_Acceso }));
               

        if (data.d.Success === true){
            $('#AccesoXGrupo_GridTbl').bootstrapTable('updateByUniqueId', { id: row.Id_Acceso, row: row });
            $modal.modal('hide');
            var message4 = $.i18n.t('app.form.AccesoXGrupo_Grid_Message_Notify_update4');
            notification.toastr.success($.i18n.t('app.form.AccesoXGrupo_Grid_Title_Notify_update4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.AccesoXGrupo_Grid_Message_Notify_update5');
            notification.swal.error($.i18n.t('app.form.AccesoXGrupo_Grid_Title_Notify_update5'), message5);

                }

    };
    this.AccesoXGrupo_Grid_delete = function (row, $modal) {
          var data;
          var btnLoading = Ladda.create(document.querySelector('#AccesoXGrupo_GridSaveBtn'));          
        data = app.core.SyncWebMethod("/fasi/dli/forms/AccesosPorGrupoSeguridadActions.aspx/AccesoXGrupo_Grid1DeleteCommandActionAccesoXGrupo", false,
               JSON.stringify({ AccesoXGrupoIdAcceso1: row.Id_Acceso, AccesoXGrupoIdGrupoAcceso2: row.Id_Grupo_Acceso }));
               

        if (data.d.Success === true){
            $('#AccesoXGrupo_GridTbl').bootstrapTable('remove', {field: 'Id_Acceso', values: [parseInt(0 + $('#Id_Acceso').val(), 10)]});
            var message4 = $.i18n.t('app.form.AccesoXGrupo_Grid_Message_Notify_delete4');
            notification.toastr.success($.i18n.t('app.form.AccesoXGrupo_Grid_Title_Notify_delete4'), message4);
            }            
            else {
            var message5 = $.i18n.t('app.form.AccesoXGrupo_Grid_Message_Notify_delete5');
            notification.toastr.error($.i18n.t('app.form.AccesoXGrupo_Grid_Title_Notify_delete5'), message5);

                }

    };

    this.ControlActions =   function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#AccesosPorGrupoSeguridadMainForm").validate({
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
        $("#AccesoXGrupo_GridEditForm").validate().destroy();
        $("#AccesoXGrupo_GridEditForm").validate({
            rules: {
                Id_Acceso: {
                    required: true                },
                Id_Grupo_Acceso: {
                    required: true                }

            },
            messages: {
                Id_Acceso: {
                    required: $.i18n.t('app.validation.Id_Acceso.required')                },
                Id_Grupo_Acceso: {
                    required: $.i18n.t('app.validation.Id_Grupo_Acceso.required')                }

            }
        });

    };
    this.LookUpForId_AccesoFormatter = function (value, row, index) {
        var result = '';
        if (value === 0 || value === "") {
            result = '';
        } else {
            result = $("#Id_Acceso>option[value='" + value + "']").text();
        }
        return result;
    };
    this.LookUpForId_Acceso = function (defaultValue, source) {
        var ctrol = $('#Id_Acceso');
        if (ctrol.children().length === 0) {
            ctrol.children().remove();
            ctrol.append($('<option />').val('0').text(' Cargando...'));

            app.core.AsyncWebMethod("/fasi/dli/forms/AccesosPorGrupoSeguridadActions.aspx/LookUpForId_Acceso", false,
                JSON.stringify({ id: $('#AccesosPorGrupoSeguridadFormId').val() }),
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

            app.core.AsyncWebMethod("/fasi/dli/forms/AccesosPorGrupoSeguridadActions.aspx/LookUpForId_Grupo_Acceso", false,
                JSON.stringify({ id: $('#AccesosPorGrupoSeguridadFormId').val() }),
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

    this.AccesoXGrupo_GridTblSetup = function (table) {
        AccesosPorGrupoSeguridadSupport.LookUpForId_Acceso('');
        AccesosPorGrupoSeguridadSupport.LookUpForId_Grupo_Acceso('');
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 20,
            uniqueId: 'Id_Acceso',
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
            toolbar: '#AccesoXGrupo_Gridtoolbar',
            columns: [{
                field: 'selected',
                checkbox: true,
                formatter: 'AccesosPorGrupoSeguridadSupport.selected_Formatter'
            }, {
                field: 'Id_Acceso',
                title: $.i18n.t('app.form.AccesoXGrupo_GridTbl_Id_Acceso_Title'),
                formatter: 'AccesosPorGrupoSeguridadSupport.LookUpForId_AccesoFormatter',
                sortable: true,
                halign: 'center'
            }, {
                field: 'Id_Grupo_Acceso',
                title: $.i18n.t('app.form.AccesoXGrupo_GridTbl_Id_Grupo_Acceso_Title'),
                formatter: 'AccesosPorGrupoSeguridadSupport.LookUpForId_Grupo_AccesoFormatter',
                sortable: true,
                halign: 'center'
            }]
        });


        $('#AccesoXGrupo_GridTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#AccesoXGrupo_GridTbl');
            $('#AccesoXGrupo_GridRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#AccesoXGrupo_GridRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#AccesoXGrupo_GridTbl').bootstrapTable('getSelections'), function (row) {		
                AccesosPorGrupoSeguridadSupport.AccesoXGrupo_GridRowToInput(row);
                AccesosPorGrupoSeguridadSupport.AccesoXGrupo_Grid_delete(row, null);
                
                return row.Id_Acceso;
            });

            $('#AccesoXGrupo_GridRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#AccesoXGrupo_GridCreateBtn').click(function () {
            var formInstance = $("#AccesoXGrupo_GridEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            AccesosPorGrupoSeguridadSupport.AccesoXGrupo_GridShowModal($('#AccesoXGrupo_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#AccesoXGrupo_GridPopup').find('#AccesoXGrupo_GridSaveBtn').click(function () {
            var formInstance = $("#AccesoXGrupo_GridEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#AccesoXGrupo_GridPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#AccesoXGrupo_GridSaveBtn').html();
                $('#AccesoXGrupo_GridSaveBtn').html('Procesando...');
                $('#AccesoXGrupo_GridSaveBtn').prop('disabled', true);

                AccesosPorGrupoSeguridadSupport.currentRow.Id_Acceso = parseInt(0 + $('#Id_Acceso').val(), 10);
                AccesosPorGrupoSeguridadSupport.currentRow.Id_Grupo_Acceso = parseInt(0 + $('#Id_Grupo_Acceso').val(), 10);

                $('#AccesoXGrupo_GridSaveBtn').prop('disabled', false);
                $('#AccesoXGrupo_GridSaveBtn').html(caption);

                if (wm === 'Update') {
                    AccesosPorGrupoSeguridadSupport.AccesoXGrupo_Grid_update(AccesosPorGrupoSeguridadSupport.currentRow, $modal);
                }
                else {                    
                    AccesosPorGrupoSeguridadSupport.AccesoXGrupo_Grid_insert(AccesosPorGrupoSeguridadSupport.currentRow, $modal);
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.AccesoXGrupo_GridShowModal = function (md, title, row) {
        var formInstance = $("#AccesoXGrupo_GridEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { Id_Acceso: 0, Id_Grupo_Acceso: 0 };

        md.data('id', row.Id_Acceso);
        md.find('.modal-title').text(title);

        AccesosPorGrupoSeguridadSupport.AccesoXGrupo_GridRowToInput(row);
        $('#Id_Acceso').prop('disabled', (row.Id_Acceso !== 0));
        $('#Id_Grupo_Acceso').prop('disabled', (row.Id_Acceso !== 0));

        md.appendTo("body");
        md.modal('show');
    };

    this.AccesoXGrupo_GridRowToInput = function (row) {
        AccesosPorGrupoSeguridadSupport.currentRow = row;
        AccesosPorGrupoSeguridadSupport.LookUpForId_Acceso(row.Id_Acceso, '');
        $('#Id_Acceso').trigger('change');
        AccesosPorGrupoSeguridadSupport.LookUpForId_Grupo_Acceso(row.Id_Grupo_Acceso, '');
        $('#Id_Grupo_Acceso').trigger('change');

    };
    this.AccesoXGrupo_GridTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/AccesosPorGrupoSeguridadActions.aspx/AccesoXGrupo_GridTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#AccesoXGrupo_GridTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };







	    this.selected_Formatter = function (value, row, index) {          
          return {
                  disabled: $('#AccesoXGrupo_GridTbl *').prop('disabled'),
                  checked: value
                 }
      };



  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('AccesosPorGrupoSeguridad', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        AccesosPorGrupoSeguridadSupport.ValidateSetup();
        
        

    AccesosPorGrupoSeguridadSupport.ControlBehaviour();
    AccesosPorGrupoSeguridadSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/AccesosPorGrupoSeguridadActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), false,
            JSON.stringify({
                id: $('#AccesosPorGrupoSeguridadFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  AccesosPorGrupoSeguridadSupport.CallRenderLookUps(data);
                
            
                $("#AccesoXGrupo_GridTblPlaceHolder").replaceWith('<table id="AccesoXGrupo_GridTbl"></table>');
    AccesosPorGrupoSeguridadSupport.AccesoXGrupo_GridTblSetup($('#AccesoXGrupo_GridTbl'));

                    AccesosPorGrupoSeguridadSupport.AccesoXGrupo_GridTblRequest();

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: $("#AccesosPorGrupoSeguridadMainForm"),
        CallBack: AccesosPorGrupoSeguridadSupport.Init
    });
});

window.AccesoXGrupo_GridActionEvents = {
    'click .update': function (e, value, row, index) {
        AccesosPorGrupoSeguridadSupport.AccesoXGrupo_GridShowModal($('#AccesoXGrupo_GridPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};

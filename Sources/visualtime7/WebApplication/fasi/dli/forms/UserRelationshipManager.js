var UserRelationshipManagerSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#UserRelationshipManagerFormId').val()
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#UserRelationshipManagerFormId').val(data.InstanceFormId);


        if (data.grid4_CreditCard !== null)
            $('#grid4Tbl').bootstrapTable('load', data.grid4_CreditCard);
        UserRelationshipManagerSupport.ClientTblRequest();
        if (data.Client_Client !== null)
            $('#ClientTbl').bootstrapTable('load', data.Client_Client);

    };

    this.ControlBehaviour = function () {









    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               UserRelationshipManagerSupport.ObjectToInput(data.d.Data.Instance, source);
            
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




    this.ControlActions =   function () {


    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#UserRelationshipManagerMainForm").validate({
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

    };

    this.ClientTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
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
            detailView: true,
            onExpandRow: UserRelationshipManagerSupport.ClientTblExpandRow,
            columns: [{
                field: 'CustomNumeric',
                title: $.i18n.t('app.form.ClientTbl_CustomNumeric_Title'),
                formatter: 'UserRelationshipManagerSupport.CustomNumeric_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'CustomString',
                title: $.i18n.t('app.form.ClientTbl_CustomString_Title'),
                sortable: false,
                halign: 'center'
            }, {
                field: 'CustomStringEx',
                title: $.i18n.t('app.form.ClientTbl_CustomStringEx_Title'),
                sortable: false,
                halign: 'center'
            }, {
                field: 'eMailAddressDefault',
                title: $.i18n.t('app.form.ClientTbl_eMailAddressDefault_Title'),
                sortable: false,
                halign: 'center'
            }]
        });




    };


    this.ClientTblRequest = function (params) {
        
            app.core.AsyncWebMethod("/fasi/dli/forms/UserRelationshipManagerActions.aspx/ClientTblDataLoad", false,
              JSON.stringify({
                                               filter: ''
              }),
              function (data) {
                  $('#ClientTbl').bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

              });
        
    };
    this.grid4TblSetup = function (table) {
   
        table.bootstrapTable({   
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
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

            columns: [{
                field: 'UserCode',
                title: $.i18n.t('app.form.grid4Tbl_UserCode_Title'),
                formatter: 'UserRelationshipManagerSupport.numeric5_FormatterMaskData',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'CreditCardNumber',
                title: $.i18n.t('app.form.grid4Tbl_CreditCardNumber_Title'),
                sortable: false,
                halign: 'center'
            }, {
                field: 'CreditCardTypeDescription',
                title: $.i18n.t('app.form.grid4Tbl_CreditCardTypeDescription_Title'),
                sortable: false,
                halign: 'center'
            }, {
                field: 'BankCodeDescription',
                title: $.i18n.t('app.form.grid4Tbl_BankCodeDescription_Title'),
                sortable: false,
                halign: 'center'
            }, {
                field: 'RecordStatus',
                title: $.i18n.t('app.form.grid4Tbl_RecordStatus_Title'),
                sortable: false,
                halign: 'center'
            }]
        });
  

        UserRelationshipManagerSupport.$el = table;
        UserRelationshipManagerSupport.grid4TblRequest();

      };

    this.grid4TblRequest = function (params) {
        var table = this.$el;
        var row = $('#' + table.data("tblparentid")).bootstrapTable('getData')[table.data("parentid")];
        
        app.core.AsyncWebMethod('/fasi/dli/forms/UserRelationshipManagerActions.aspx/grid4TblDataLoad', false,
             JSON.stringify({
					                  filter: '',
                USERGROUPSOWNERID1: row.CustomNumeric,
                GROUPMEMBERSUSERID2: row.CustomNumeric
				       }),
				       function (data) { 
                  table.bootstrapTable('load', data.d.Data !== null ? data.d.Data : []);

               });
    };
    this.ClientTblExpandRow = function (index, row, $detail) {
       var tblparentid = $detail.parent().parent().parent()[0].id;
       var html = [];


        html.push('<table id="grid4Tbl-' + index + '" data-tblparentid="' + tblparentid + '" data-parentid="' + index + '"><caption data-i18n="app.form.grid4_Title" >Supervisados</caption></table>');
        

        $detail.html(html.join(""));

        UserRelationshipManagerSupport.grid4TblSetup($detail.find('#grid4Tbl-' + index));


    };





    this.CustomNumeric_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };
    this.numeric5_FormatterMaskData = function (value, row, index) {          
          return AutoNumeric.format(value,  {
            decimalCharacter: generalSupport.DecimalCharacter(),
            digitGroupSeparator: generalSupport.DigitGroupSeparator(),
            maximumValue: 99999,
            decimalPlaces: 0,
            minimumValue: -99999
        });
      };





  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('UserRelationshipManager', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        UserRelationshipManagerSupport.ValidateSetup();
        
        

    UserRelationshipManagerSupport.ControlBehaviour();
    UserRelationshipManagerSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/UserRelationshipManagerActions.aspx/Initialization", false,
            JSON.stringify({
                id: $('#UserRelationshipManagerFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  UserRelationshipManagerSupport.CallRenderLookUps(data);
                
            
                $("#ClientTblPlaceHolder").replaceWith('<table id="ClientTbl"><caption>' + $.i18n.t('app.form.Client_Title') + '</caption></table>');
    UserRelationshipManagerSupport.ClientTblSetup($('#ClientTbl'));

                    UserRelationshipManagerSupport.ClientTblRequest();

            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: ['Administrador'],
        Element: $("#UserRelationshipManagerMainForm"),
        CallBack: UserRelationshipManagerSupport.Init
    });
});


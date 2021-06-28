var CrystalReportServiceSampleSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#CrystalReportServiceSampleFormId').val(),
            ClientClientID: $('#ClientID').val(),
            Hobbies_ClientHobby: generalSupport.NormalizeProperties($('#HobbiesTbl').bootstrapTable('getData'), ''),
            Sports_ClientSport: generalSupport.NormalizeProperties($('#SportsTbl').bootstrapTable('getData'), '')
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#CrystalReportServiceSampleFormId').val(data.InstanceFormId);
        $('#ClientID').val(data.ClientClientID);
        $('#CompleteClientName').html(data.ClientCompleteClientName);
        $('#result').html(data.result);


        if (data.Hobbies_ClientHobby !== null)
            $('#HobbiesTbl').bootstrapTable('load', data.Hobbies_ClientHobby);
        if (data.Sports_ClientSport !== null)
            $('#SportsTbl').bootstrapTable('load', data.Sports_ClientSport);

    };

    this.ControlBehaviour = function () {









    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               if (source == 'Initialization')
					         CrystalReportServiceSampleSupport.ObjectToInput(data.d.Data.Instance, source);
				       else
                   CrystalReportServiceSampleSupport.ObjectToInput(data.d.Data, source);
            
            if (data.d.DataBehavior !== null)
                generalSupport.ServerBehavior(data.d.DataBehavior);
        }
        else
            generalSupport.NotifyFail(data.d.Reason, data.d.Code);
    };

    this.Initialization = function () {
        app.core.AsyncWebMethod("/fasi/dli/forms/CrystalReportServiceSampleActions.aspx/Initialization"+ (window.location.href.split("?")[1] ? "?" + window.location.href.split("?")[1] : ""), true,
            JSON.stringify({
                id: $('#CrystalReportServiceSampleFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
                if (data.d.Success)
                    $('#CrystalReportServiceSampleFormId').val(data.d.Data.Instance.InstanceFormId);
                    
                if (data.d.Success === true && data.d.Data.LookUps) {
                    data.d.Data.LookUps.forEach(function (elementSource) {
                        generalSupport.RenderLookUp(elementSource.Key, data.d.Data.Instance[elementSource.Key], 'Initialization', elementSource.Items);
                    });
                }
                


    $("#HobbiesTblPlaceHolder").replaceWith('<table id="HobbiesTbl"><caption>ClientHobby</caption></table>');
    CrystalReportServiceSampleSupport.HobbiesTblSetup($('#HobbiesTbl'));
    $("#SportsTblPlaceHolder").replaceWith('<table id="SportsTbl"><caption>ClientSport</caption></table>');
    CrystalReportServiceSampleSupport.SportsTblSetup($('#SportsTbl'));





                CrystalReportServiceSampleSupport.ActionProcess(data, 'Initialization');

                if (generalSupport.URLStringValue('fromid') !== '' && window.history.replaceState)                    
                    window.history.replaceState({}, null, '/fasi/dli/forms/' + location.pathname.substring(location.pathname.lastIndexOf("/") + 1) + '?id=' + $('#CrystalReportServiceSampleFormId').val());
 
              
          

            });
    };




    this.ControlActions =   function () {

        $('#button2').click(function (event) {
                var formInstance = $("#CrystalReportServiceSampleMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button2'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/CrystalReportServiceSampleActions.aspx/button2Click", false,
                          JSON.stringify({
                                        instance: CrystalReportServiceSampleSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    CrystalReportServiceSampleSupport.ActionProcess(data, 'button2Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#button3').click(function (event) {
                var formInstance = $("#CrystalReportServiceSampleMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button3'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/CrystalReportServiceSampleActions.aspx/button3Click", false,
                          JSON.stringify({
                                        instance: CrystalReportServiceSampleSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    CrystalReportServiceSampleSupport.ActionProcess(data, 'button3Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });
        $('#button0').click(function (event) {
                var formInstance = $("#CrystalReportServiceSampleMainForm");
                var fvalidate = formInstance.validate();
                
                if (formInstance.valid()) {
                    var btnLoading = Ladda.create(document.querySelector('#button0'));
                    btnLoading.start();

                    app.core.AsyncWebMethod("/fasi/dli/forms/CrystalReportServiceSampleActions.aspx/button0Click", false,
                          JSON.stringify({
                                        instance: CrystalReportServiceSampleSupport.InputToObject()
                          }),
                    function (data) {
                         btnLoading.stop();

                    CrystalReportServiceSampleSupport.ActionProcess(data, 'button0Click');
                    },
                    function () {
                         btnLoading.stop();
                    });
               }
                else
                     generalSupport.NotifyErrorValidate(fvalidate);
                     event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#CrystalReportServiceSampleMainForm").validate({
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
                ClientID: {
                    required: true,
                    maxlength: 14
                },
                CompleteClientName: {
                    maxlength: 63
                },
                result: {
                    maxlength: 60
                }
            },
            messages: {
                ClientID: {
                    required: 'The field is required.',
                    maxlength: 'The field allows 14 maximum characters'
                },
                CompleteClientName: {
                    maxlength: 'The field allows 63 maximum characters'
                },
                result: {
                    maxlength: 'The field allows 60 maximum characters'
                }
            }
        });
        $("#HobbiesEditForm").validate().destroy();
        $("#HobbiesEditForm").validate({
            rules: {
                Hobby: {
                    required: true,
                    maxlength: 5
                },
                HobbyDescription: {
                    maxlength: 15
                }

            },
            messages: {
                Hobby: {
                    required: 'The field is required.',
                    maxlength: 'The field allows 5 maximum characters'
                },
                HobbyDescription: {
                    maxlength: 'The field allows 15 maximum characters'
                }

            }
        });
        $("#SportsEditForm").validate().destroy();
        $("#SportsEditForm").validate({
            rules: {
                Sport: {
                    required: true,
                    maxlength: 5
                },
                SportDescription: {
                    maxlength: 15
                }

            },
            messages: {
                Sport: {
                    required: 'The field is required.',
                    maxlength: 'The field allows 5 maximum characters'
                },
                SportDescription: {
                    maxlength: 'The field allows 15 maximum characters'
                }

            }
        });

    };

    this.HobbiesTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Hobby',
            toolbar: '#Hobbiestoolbar',
            columns: [{
                field: 'Hobby',
                title: 'Code',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'HobbyDescription',
                title: 'Description',
                sortable: false,
                halign: 'center'
            }]
        });


        $('#HobbiesTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#HobbiesTbl');
            $('#HobbiesRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#HobbiesRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#HobbiesTbl').bootstrapTable('getSelections'), function (row) {		
                CrystalReportServiceSampleSupport.HobbiesRowToInput(row);
                
                
                return row.Hobby;
            });
            
          $('#HobbiesTbl').bootstrapTable('remove', {
                field: 'Hobby',
                values: ids
           });

            $('#HobbiesRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#HobbiesCreateBtn').click(function () {
            var formInstance = $("#HobbiesEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            CrystalReportServiceSampleSupport.HobbiesShowModal($('#HobbiesPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#HobbiesPopup').find('#HobbiesSaveBtn').click(function () {
            var formInstance = $("#HobbiesEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#HobbiesPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#HobbiesSaveBtn').html();
                $('#HobbiesSaveBtn').html('Procesando...');
                $('#HobbiesSaveBtn').prop('disabled', true);

                CrystalReportServiceSampleSupport.currentRow.Hobby = parseInt(0 + $('#Hobby').val(), 10);
                CrystalReportServiceSampleSupport.currentRow.HobbyDescription = $('#HobbyDescription').val();

                $('#HobbiesSaveBtn').prop('disabled', false);
                $('#HobbiesSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#HobbiesTbl').bootstrapTable('updateByUniqueId', { id: CrystalReportServiceSampleSupport.currentRow.Hobby, row: CrystalReportServiceSampleSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#HobbiesTbl').bootstrapTable('append', CrystalReportServiceSampleSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.HobbiesShowModal = function (md, title, row) {
        var formInstance = $("#HobbiesEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { Hobby: 0, HobbyDescription: '' };

        md.data('id', row.Hobby);
        md.find('.modal-title').text(title);

        CrystalReportServiceSampleSupport.HobbiesRowToInput(row);


        md.appendTo("body");
        md.modal('show');
    };

    this.HobbiesRowToInput = function (row) {
        CrystalReportServiceSampleSupport.currentRow = row;
        $('#Hobby').val(row.Hobby);
        $('#HobbyDescription').val(row.HobbyDescription);

    };
    this.SportsTblSetup = function (table) {
        table.bootstrapTable({
            maintainSelected: true,
            locale: generalSupport.LanguageName() + '-CR',
            pagination: true,
            pageSize: 10,
            uniqueId: 'Sport',
            toolbar: '#Sportstoolbar',
            columns: [{
                field: 'Sport',
                title: 'Code',
                sortable: false,
                halign: 'center',
                align: 'right'
            }, {
                field: 'SportDescription',
                title: 'Description',
                sortable: false,
                halign: 'center'
            }]
        });


        $('#SportsTbl').on('check.bs.table uncheck.bs.table check-all.bs.table uncheck-all.bs.table', function () {
            var $table = $('#SportsTbl');
            $('#SportsRemoveBtn').prop('disabled', !$table.bootstrapTable('getSelections').length);
        });

        $('#SportsRemoveBtn').click(function () {
	
            notification.swal.deleteRowConfirmation(
                function () {
		
                    var ids = $.map($('#SportsTbl').bootstrapTable('getSelections'), function (row) {		
                CrystalReportServiceSampleSupport.SportsRowToInput(row);
                
                
                return row.Sport;
            });
            
          $('#SportsTbl').bootstrapTable('remove', {
                field: 'Sport',
                values: ids
           });

            $('#SportsRemoveBtn').prop('disabled', true);

                });
            event.preventDefault(); // cancel default behavior
        });

        $('#SportsCreateBtn').click(function () {
            var formInstance = $("#SportsEditForm");
            var fvalidate = formInstance.validate();
            fvalidate.resetForm();
            CrystalReportServiceSampleSupport.SportsShowModal($('#SportsPopup').modal({ show: false }), $(this).attr('data-modal-title'));
        });

        $('#SportsPopup').find('#SportsSaveBtn').click(function () {
            var formInstance = $("#SportsEditForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var $modal = $('#SportsPopup');
                var wm = 'Create';
                if ($modal.data('id'))
                    wm = 'Update';

                var caption = $('#SportsSaveBtn').html();
                $('#SportsSaveBtn').html('Procesando...');
                $('#SportsSaveBtn').prop('disabled', true);

                CrystalReportServiceSampleSupport.currentRow.Sport = parseInt(0 + $('#Sport').val(), 10);
                CrystalReportServiceSampleSupport.currentRow.SportDescription = $('#SportDescription').val();

                $('#SportsSaveBtn').prop('disabled', false);
                $('#SportsSaveBtn').html(caption);

                if (wm === 'Update') {
                    $('#SportsTbl').bootstrapTable('updateByUniqueId', { id: CrystalReportServiceSampleSupport.currentRow.Sport, row: CrystalReportServiceSampleSupport.currentRow });
                    $modal.modal('hide');
                }
                else {                    
                    $('#SportsTbl').bootstrapTable('append', CrystalReportServiceSampleSupport.currentRow);
                    $modal.modal('hide');
                }
            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
        });

    };

    this.SportsShowModal = function (md, title, row) {
        var formInstance = $("#SportsEditForm");
        var fvalidate = formInstance.validate();
        
        fvalidate.resetForm();
        
        row = row || { Sport: 0, SportDescription: '' };

        md.data('id', row.Sport);
        md.find('.modal-title').text(title);

        CrystalReportServiceSampleSupport.SportsRowToInput(row);


        md.appendTo("body");
        md.modal('show');
    };

    this.SportsRowToInput = function (row) {
        CrystalReportServiceSampleSupport.currentRow = row;
        $('#Sport').val(row.Sport);
        $('#SportDescription').val(row.SportDescription);

    };










  this.Init = function(){
    
    moment.locale(generalSupport.UserContext().languageName);
    
    if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage)
        masterSupport.setPageTitle('Client information report');
        

    CrystalReportServiceSampleSupport.ControlBehaviour();
    CrystalReportServiceSampleSupport.ControlActions();
    CrystalReportServiceSampleSupport.ValidateSetup();

    CrystalReportServiceSampleSupport.Initialization();


  };
};

$(document).ready(function () {
   CrystalReportServiceSampleSupport.Init();
});

window.HobbiesActionEvents = {
    'click .update': function (e, value, row, index) {
        CrystalReportServiceSampleSupport.HobbiesShowModal($('#HobbiesPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};
window.SportsActionEvents = {
    'click .update': function (e, value, row, index) {
        CrystalReportServiceSampleSupport.SportsShowModal($('#SportsPopup').modal({ show: false }), $(this).attr('data-modal-title'), row);
    }
};

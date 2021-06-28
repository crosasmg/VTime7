var TermsSupport = new function () {

    this.currentRow = {};
    this.extImagen = [".jpg", ".jpeg", ".gif", ".png", ".tiff", ".tif", ".bmp"];
    
    this.InputToObject = function () {
        var data = {
            InstanceFormId: $('#TermsFormId').val()
        };
        return data;
    };

    this.ObjectToInput = function (data, source) {
        source = 'Initialization';
        $('#TermsFormId').val(data.InstanceFormId);



    };

    this.ControlBehaviour = function () {









    };

    this.ActionProcess = function (data, source) {
        if (data.d.Success === true) {
            if (data.d.Data !== null)
               TermsSupport.ObjectToInput(data.d.Data.Instance, source);
            
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

        $('#btnPrint').click(function (event) {
            var formInstance = $("#TermsMainForm");
            var fvalidate = formInstance.validate();

            if (formInstance.valid()) {
                var data;
                var btnLoading = Ladda.create(document.querySelector('#btnPrint'));
                btnLoading.start();
                window.print();
                btnLoading.stop();

            }
            else
                generalSupport.NotifyErrorValidate(fvalidate);
            event.preventDefault();
        });

    };

    this.ValidateSetup = function () {
        generalSupport.ExtendValidators();


        $("#TermsMainForm").validate({
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











  this.Init = function(){
    
    moment.locale(app.user.languageName);
    
   generalSupport.TranslateInit('Terms', function () {
        if (typeof masterSupport !== 'undefined' && typeof constants !== 'undefined' && window.location.pathname !== constants.defaultPage) {
            masterSupport.setPageTitle($.i18n.t('app.title'));
        }
        
        TermsSupport.ValidateSetup();
        
        

    TermsSupport.ControlBehaviour();
    TermsSupport.ControlActions();
    

    app.core.AsyncWebMethod("/fasi/dli/forms/TermsActions.aspx/Initialization", false,
            JSON.stringify({
                id: $('#TermsFormId').val(),
                urlid: generalSupport.URLStringValue('id'),
                fromid: generalSupport.URLStringValue('fromid')
            }),
            function (data) {
               if (data.d.Success === true) {
                  TermsSupport.CallRenderLookUps(data);
                
            
            
            
            
            
            
             }
});

   }, 'dli/forms');
  };
};

$(document).ready(function () {
    app.security.PageSetup({
        Pathname: window.location.pathname,
        roles: [],
        Element: $("#TermsMainForm"),
        CallBack: TermsSupport.Init
    });
});


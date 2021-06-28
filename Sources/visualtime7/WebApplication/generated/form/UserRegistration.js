function AsyncPostBack(s, e) {
    Sys.WebForms.PageRequestManager.getInstance().beginAsyncPostBack();
}

function Confirmation_Actions(s, e) {
    var popupWindow = popControl.GetWindowByName('pwUno');

    popupWindow.popupControl.HideWindow(popupWindow);
}

function UserNameValueChanged(s, e) {
    UserName_Actions(s, e);
}

function IntermediaryValueChanged(s, e) {
    Intermediary_Actions(s, e);
}

function ClientValueChanged(s, e) {
    Client_Actions(s, e);
}

function EmailValueChanged(s, e) {
    Email_Actions(s, e);
}

function EmailVerificationValueChanged(s, e) {
    EmailVerification_Actions(s, e);
}

function AcceptConditionsCheckedChanged(s, e) {
    AcceptConditions_Actions(s, e);
}

function btnSeeTermsClick(s, e) {
    btnSeeTerms_Actions(s, e);
}

function btnRegisterClick(s, e) {
    btnRegister_Actions(s, e);
}

function Intermediary_Actions(s, e) {
    var existe;

    $.ajax({
        type: "POST",
        url: window.location.protocol + "//" + window.location.host + "/generated/form/UserRegistrationPopup.aspx/IntermediaryExist",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ code: text1.GetValue() }),
        success: function (data) {
            existe = data.d.Result;
            if (existe != true) {
                text1.SetIsValid(false);
                text1.SetErrorText('El código de intermediario no existe');
            }
        },
        error: function (error) {
            alert(error.responseJSON.Message);
        }
    });
}

function Client_Actions(s, e) {
    var existe;

    $.ajax({
        type: "POST",
        url: window.location.protocol + "//" + window.location.host + "/generated/form/UserRegistrationPopup.aspx/ClientExist",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ email: Email.GetValue(), sclient: text3.GetValue() }),
        success: function (data) {
            existe = data.d.Result;
            if (existe != true) {
                text3.SetIsValid(false);
                text3.SetErrorText('El código de cliente no existe');
            }
        },
        error: function (error) {
            alert(error.responseJSON.Message);
        }
    });
}


function UserName_Actions(s, e) {
    var existe;

    $.ajax({
        type: "POST",
        url: window.location.protocol + "//" + window.location.host + "/generated/form/UserRegistrationPopup.aspx/Exist35598edad2a14d1cbb03ddae3b84de0f",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ USERNAME: UserName.GetValue() }),
        success: function (data) {
            existe = data.d.Result;
            if (existe == true) {
                UserName.SetIsValid(false);
                UserName.SetErrorText('Usuario ya existe');
            }
        },
        error: function (error) {
            alert(error.responseJSON.Message);
        }
    });
}


function Email_Actions(s, e) {
    var existe;

    $.ajax({
        type: "POST",
        url: window.location.protocol + "//" + window.location.host + "/generated/form/UserRegistrationPopup.aspx/ExistEmail0347c7f44c9049379f116072478a1ca2",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ EMAIL: Email.GetValue() }),
        success: function (data) {
            existe = data.d.Result;
            if (existe == true) {
                Email.SetIsValid(false);
                Email.SetErrorText('Ya existe el email');
            }
        },
        error: function (error) {
            alert(error.responseJSON.Message);
        }
    });
}
function EmailVerification_Actions(s, e) {
    if (EmailVerification.GetValue() != Email.GetValue()) {
        Email.SetIsValid(false);
        Email.SetErrorText('Los correos electrónicos debe ser iguales');
    }
}
function AcceptConditions_Actions(s, e) {
    if ((AcceptConditions.GetChecked() == true)) {
        btnRegister.SetEnabled(true);
    }
    else {
        btnRegister.SetEnabled(false);
    }
}
function btnSeeTerms_Actions(s, e) {
    var popupWindow = popControl.GetWindowByName('pwUno');
    if (ASPxClientEdit.ValidateGroup('zoneTerm')) {
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbtnSeeTermsInformationMessageResource;
        popupWindow.SetHeaderText(titlebtnSeeTermsInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);
        var w = 500;
        var h = 600;
        var left = (screen.width / 2) - (w / 2);
        var top = (screen.height / 2) - (h / 2);
        newwindow = window.open(window.location.protocol + "//" + window.location.host + '/generated/form/TermsOfUsePopup.aspx', 'name', 'scrollbars=yes,height=' + h + ',width=' + w + ',top=' + top + ', left=' + left);

        popupWindow.popupControl.HideWindow(popupWindow);
    } else
        e.processOnServer = false;
}
function btnRegister_Actions(s, e) {
    var popupWindow = popControl.GetWindowByName('pwUno');
    if (ASPxClientEdit.ValidateGroup(null)) {
        document.getElementById(btnCancel.name).style.visibility = 'hidden';
        document.getElementById(btnConfirm.name).style.visibility = 'hidden';
        document.getElementById(lblMessage.name).innerHTML = msgbtnRegisterInformationMessageResource;
        popupWindow.SetHeaderText(titlebtnRegisterInformationMessageResource);
        popupWindow.popupControl.ShowWindow(popupWindow);
    } else
        e.processOnServer = false;
}
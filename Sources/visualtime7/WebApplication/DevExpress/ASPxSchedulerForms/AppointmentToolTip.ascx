<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AppointmentToolTip.ascx.cs" Inherits="UserForms_AppointmentToolTip" %>
<div runat="server" id="buttonDiv">
    <dxe:ASPxButton ID="btnShowMenu" runat="server" AutoPostBack="False" AllowFocus="False">
        <Border BorderWidth="0px" />
        <Paddings Padding="0px" />
        <FocusRectPaddings Padding="4px" />
        <FocusRectBorder BorderStyle="None" BorderWidth="0px" />
    </dxe:ASPxButton>
</div>    

<script type="text/javascript" id="dxss_ASPxClientAppointmentToolTip">
    ASPxClientAppointmentToolTip = _aspxCreateClass(ASPxClientToolTipBase, {
        Initialize: function() {
            ASPxClientUtils.AttachEventToElement(this.controls.buttonDiv, "click", _aspxCreateDelegate(this.OnButtonDivClick, this));
        },
        OnButtonDivClick: function(s,e) {
            this.ShowAppointmentMenu(s);
        }
    });    
</script>

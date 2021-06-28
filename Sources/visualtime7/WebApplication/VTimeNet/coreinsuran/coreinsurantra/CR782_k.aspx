<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.53.46
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CR782_K")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CR782_K"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.53.46
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
Session("bQuery") = mobjValues.ActionQuery

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/ValFunctions.js"></SCRIPT>	




<SCRIPT LANGUAGE=JavaScript>
    //- Variable para el control de versiones
    document.VssVersion = "$$Revision: 2 $|$$Date: 26/04/06 11:56 $|$$Author: Pgarin $"


    //% DisabledCoverGen: Habilita y desabilita el de cobertura generica si es Vida
    //--------------------------------------------------------------------------------------------
    function DisabledCoverGen(Field) {
        //--------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            if (Field == '40') {
                valCovergen.disabled = false;
                btnvalCovergen.disabled = false;
            }
            else {
                valCovergen.disabled = true;
                btnvalCovergen.disabled = true;
            }
        }
    }


    //% insStateZone: se controla el estado de los campos de la página
    //--------------------------------------------------------------------------------------------
    function insStateZone() {
        //--------------------------------------------------------------------------------------------
        with (self.document.forms[0]) {
            tcdEffecdate.disabled = false;
            btn_tcdEffecdate.disabled = false;
        }
    }

    //% insCancel: se controla la acción Cancelar de la página
    //--------------------------------------------------------------------------------------------
    function insCancel() {
        //--------------------------------------------------------------------------------------------
        return true;
    }

    //% insFinish: se controla la acción Cancelar de la página
    //--------------------------------------------------------------------------------------------
    function insFinish() {
        //--------------------------------------------------------------------------------------------
        return true;
    }


    //% ShowChangeValues: Se cargan los valores de acuerdo a los datos recibidos
    //-------------------------------------------------------------------------------------------
    function ShowChangeValues(lobjOption) {
        //-------------------------------------------------------------------------------------------
        with (document.forms[0]) {
            switch (lobjOption) {
                case "tcnNumber":
                    ShowPopUp("/VTimeNet/CoReinsuran/CoReinsurantra/ShowDefValues.aspx?Field=" + "ShowDefValuesCR782" + "&nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nBranch_rei=" + tcnBranch_rei.value + "&nNumber=" + tcnNumber.value + "&nCovergen=" + valCovergen.value + "&dEffecdate=" + tcdEffecdate.value, 1, 1, "no", "no", 2000, 2000);
                    break;
            }
        }
    }	


</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.setZone(2, "CR782_K", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
	.Write(mobjMenu.MakeMenu("CR782", "CR782_K.aspx", 1, vbNullString))
End With
mobjMenu = Nothing
%>

</HEAD>


<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="CR782" ACTION="ValCoReinsuranTra.aspx?sMode=1">
	<BR>  
	<BR>    

       <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", Today, True, GetLocalResourceObject("tcdEffecdateToolTip"), , , , , True)%></TD>
		</TR>      
	</TABLE>


</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing%>


<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.53.46
Call mobjNetFrameWork.FinishPage("CR782_K")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





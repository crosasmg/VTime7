<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "MAG573"
%>
<HTML>
<HEAD>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	<META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT>
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:34 $"

//% insCancel: Se ejecuta al cancelar la transacción
//-------------------------------------------------------------------------------------------
function insCancel()
//-------------------------------------------------------------------------------------------
{
	return(true)
}

//% insStateZone: Habilita los campos una vez seleccionada una opción
//-------------------------------------------------------------------------------------------
function insStateZone(){
//-------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		cbeInterTyp.disabled = false
		tcdEffecdate.disabled = false
		btn_tcdEffecdate.disabled = false
	}
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	mobjMenu = New eFunctions.Menues
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "MAG573_K.aspx", 1, ""))
	mobjMenu = Nothing
End With
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<BR>
<BR>
<FORM METHOD="POST" ID="FORM" NAME="frmTabLifeComm" ACTION="valMantAgent.aspx?mode=1">
    <TABLE WIDTH="100%">
        <TR>
            <TD><LABEL ID=11750><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today), True, GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
            <TD><LABEL ID=11751><%= GetLocalResourceObject("cbeInterTypCaption") %></LABEL></TD>
			<TD>
			<%
With mobjValues
	.Parameters.Add("nSupervis", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("cbeInterTyp", "tabInter_TypSupervis", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.strNull), True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeInterTypToolTip")))
End With
%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>

<%
mobjValues = Nothing
%>





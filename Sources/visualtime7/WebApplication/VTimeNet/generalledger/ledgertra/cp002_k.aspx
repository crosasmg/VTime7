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

mobjValues.sCodisplPage = "CP002_K"
mobjMenu = New eFunctions.Menues
%>

<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}
//% insStateZone: Se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function insStateZone(nAction){
//------------------------------------------------------------------------------------------
	if(nAction==301)
	{
		with(self.document.forms[0])
		{
			lstrAction = self.document.location.href
			lstraux = lstrAction
			lstrAction = lstrAction.replace(/\?.*/, '') + '?sCodispl=CP002' + '&nMainAction=' + nAction
			self.document.location.href=lstrAction;
		}        
	}
	else
	{
		self.document.forms[0].valAccount.disabled=false;
		self.document.forms[0].btnvalAccount.disabled=false;
	}
}
//% insGetNumber
//------------------------------------------------------------------------------------------
function insGetNumber(){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if(hddAction.value!=301)
        {
            valAccount.Parameters.Param1.sValue=tcnLedCompan.value;
        }
        insDefValues('Led_compan','nLed_compan=' + tcnLedCompan.value ,'/VTimeNet/GeneralLedGer/LedgerTra/')
    }
}
//%	LoadAccount: Condiciona el recargo por el cambio en el patrón de busqueda
//-------------------------------------------------------------------------------------------
function LoadAccount(Field){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0])
    {
		valAux.Parameters.Param1.sValue=tcnLedCompan.value;
		valAux.Parameters.Param2.sValue=Field.value;
    }
}
//-------------------------------------------------------------------------------------------
function LockControl(Field){
//-------------------------------------------------------------------------------------------
    with (self.document.forms[0])
    {
        insDefValues('Locked','sWindow=Header&nLed_compan=' + tcnLedCompan.value + '&sAccount=' + Field ,'/VTimeNet/GeneralLedGer/LedgerTra/')
    }
}
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 15:06 $" 

</SCRIPT>

<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>

<%With Response
	.Write(mobjValues.StyleSheet)
	.Write(mobjMenu.MakeMenu("CP002", "CP002_k.aspx", 1, ""))
End With

mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmAccountUpd" ACTION="ValLedGerTra.aspx?sTime=1">
<BR>
<BR>
	<%Response.Write(mobjValues.ButtonLedCompan("LedCompan", Session("nLedCompan"), GetLocalResourceObject("LedCompanToolTip"), False, "insGetNumber()"))%>
	<TABLE WIDTH=100% COLS=4>
		<TR>
		    <TD><LABEL><%= GetLocalResourceObject("valAccountCaption") %></LABEL></TD>
			<%mobjValues.Parameters.Add("nLed_compan", Session("nLedCompan"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)%>
   			    <TD>
   			    <%Response.Write(mobjValues.PossiblesValues("valAccount", "tabLedger_acc", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("nAccount"), True,  ,  ,  ,  , "LoadAccount(this);LockControl(this.value);", CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 0, 20, GetLocalResourceObject("valAccountToolTip"), eFunctions.Values.eTypeCode.eString, 1,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301))%>
   			    </TD>
			</TD>
			<TD><LABEL ID=11465><%= GetLocalResourceObject("valAuxCaption") %></LABEL></TD>
<TD><%
mobjValues.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.Add("sAccount", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
Response.Write(mobjValues.PossiblesValues("valAux", "tabLedger_accAux", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  ,  , True, 20, GetLocalResourceObject("valAuxToolTip"), eFunctions.Values.eTypeCode.eString, 2,  , CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 301))
Response.Write(mobjValues.HiddenControl("hddAction", Request.QueryString.Item("nMainAction")))
%>
			</TD>
			<TD WIDTH="20%">&nbsp;</TD>
		</TR>
	</TABLE>
</BODY>
</FORM>
</HTML>
<%
mobjValues = Nothing%>






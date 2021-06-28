<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas
Dim mobjMenu As eFunctions.Menues


</script>
<%
Response.Expires = -1

'- Objeto para el manejo del control del Grid
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "CPC002_K"
%>

<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


    
<%With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("CPC002", "CPC002_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>

<SCRIPT>
//% insStateZone: 
//-----------------------
function insStateZone(){
//-----------------------

	with(self.document){
		with(forms[0]){
			tcdInitDate.disabled = false
			valAccount.disabled = false
			valAux.disabled = false
			cbeLedCompan.disabled = false
			//LedCompan.disabled=false
		}
		btn_tcdInitDate.disabled = false
		btnvalAccount.disabled = false
		btnvalAux.disabled = false
	}
}

//--------------------------------
function insCancel(){return true}
//--------------------------------

//% ShowAccount: Asigna los datos necesarios para la búsqueda de la cuenta contable
//---------------------------------------------------------------------------------
function ShowAccount(){
//---------------------------------------------------------------------------------

//+ Parámetro necesario para la búsqueda de la cuenta en el control valAccount (Cuenta Contable)
	self.document.forms[0].valAccount.Parameters.Param1.sValue = self.document.forms[0].cbeLedCompan.value  //+self.document.forms[0].tcnLedCompan.value
}

//% insGetNumber
//------------------------------------------------------------------------------------------
function insGetNumber(){
//------------------------------------------------------------------------------------------
    with (self.document.forms[0]){
        if(top.frames["fraSequence"].plngMainAction!=301)
        {
            valAccount.Parameters.Param1.sValue= cbeLedCompan.value //tcnLedCompan.value;
        }
        insDefValues('Led_compan','nLed_compan=' + cbeLedCompan.value ,'/VTimeNet/GeneralLedGer/LedgerQue/') //tcnLedCompan.value ,'/VTimeNet/GeneralLedGer/LedgerQue/')
    }
}

//% ShowChangeValues: Llama a la página ShowDefValues que ejecuta código necesario
//% para la actualización de los controles de "Header"
//--------------------------------------------------------------------------------
function ShowChangeValues(Field){
//--------------------------------------------------------------------------------

	switch(Field.name){
		case "valAccount":

//+ Parámetros necesarios para la búsqueda de la cuenta auxiliar en el control valAux (Cuenta Auxiliar Contable)
			self.document.forms[0].valAux.Parameters.Param1.sValue = self.document.forms[0].cbeLedCompan.value //self.document.forms[0].tcnLedCompan.value
			self.document.forms[0].valAux.Parameters.Param2.sValue = self.document.forms[0].valAccount.value;
			break;
		case "valAux":
			if (Field.value == '')
			{
				self.document.forms[0].valAux.value = '                    '
			};
	}
}
//+ Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 2 $|$$Date: 24/10/03 19:15 $" 
</SCRIPT>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
	<BR>
 	<FORM METHOD="post" ID="FORM" NAME="CPC002_K" ACTION="valLedgerQue.aspx?sTime=1">
		<TABLE WIDTH="100%">
			<TR> 
		        <TD><LABEL ID=0><%= GetLocalResourceObject("cbeLedCompanCaption") %></LABEL></TD>
				<TD>
				<%
With mobjValues
	'.Parameters.Add "nCompany", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable
	Response.Write(mobjValues.PossiblesValues("cbeLedCompan", "TABLED_COMPANALL_1", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  , "insGetNumber();", True, 30, "", eFunctions.Values.eTypeCode.eString, 1))
End With
%>
            			
				</TD>
			</TR> 

			<TR> 
				<TD><LABEL ID=11288><%= GetLocalResourceObject("tcdInitDateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdInitDate", CStr(DateSerial(Year(Today), Month(Today), 1)),  , GetLocalResourceObject("tcdInitDateToolTip"),  ,  ,  ,  , True)%></TD>
	        </TR>
			<TR>
				<TD><LABEL ID=11282><%= GetLocalResourceObject("valAccountCaption") %></LABEL></TD>
				<TD><%With mobjValues
	.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valAccount", "tabLedger_acc", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("sAccount"), True,  ,  ,  ,  , "ShowChangeValues(this)", True, 20, "", eFunctions.Values.eTypeCode.eString))
End With%></TD>
	        </TR>
	        <TR>
    			<TD><LABEL ID=11465><%= GetLocalResourceObject("valAuxCaption") %></LABEL></TD>
				<TD><%
With mobjValues
	.Parameters.Add("nLed_compan", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("sAccount", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valAux", "tabLedger_accAux", eFunctions.Values.eValuesType.clngWindowType, Request.QueryString.Item("sAux_accoun"), True,  ,  ,  ,  , "ShowChangeValues(this)", True, 20, "", 2))
End With
%></TD>

	        </TR>
	    </TABLE>
	</FORM>
</BODY>
</HTML>

<%
Response.Write("<SCRIPT>ShowAccount()</SCRIPT>")

mobjValues = Nothing
%>





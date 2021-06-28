<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVA645"
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"
</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.ShowWindowsName("MVA645"))
	.Write(mobjValues.WindowsTitle("MVA645"))
End With
%>
	
<SCRIPT LANGUAGE=JavaScript>
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------

}
//% insCloseWindows: Permite cerrar la ventana PopUp invocada. Este evento es llamado desde el botón 
//% ButtonAcceptCancel.
//------------------------------------------------------------------------------------------------
function insCloseWindows(){
//------------------------------------------------------------------------------------------------
	window.close()
}
//% insChangeField: se controla la modificación de los campos de parametros
//--------------------------------------------------------------------------------------------
function insChangeField(vObj){
//--------------------------------------------------------------------------------------------
	var sValue, bNullValue;
	
	sValue = vObj.value;
	bNullValue = (sValue == '');

	with (self.document.forms[0]){
		switch (vObj.name){
			case 'cbeBranch':
				valProduct.Parameters.Param1.sValue=sValue;
				valModulec.Parameters.Param1.sValue=sValue;
				valCover.Parameters.Param2.sValue=sValue;
				valProduct.disabled = btnvalProduct.disabled = bNullValue;
				valCover.value = valModulec.value = '0';
				UpdateDiv('valModulecDesc','','Normal');
				UpdateDiv('valCoverDesc','','Normal');
				break;

			case 'valProduct':
				valModulec.Parameters.Param2.sValue=sValue;
				valCover.Parameters.Param3.sValue=sValue;
				valCover.Parameters.Param4.sValue=0;
				valModulec.disabled = btnvalModulec.disabled = bNullValue;
				valCover.disabled = btnvalCover.disabled = bNullValue;
				break;

			case 'tcdEffecdate':
				valModulec.Parameters.Param3.sValue=sValue;
				valCover.Parameters.Param4.sValue=sValue;
				break;

			case 'valModulec':
				if (sValue == '') sValue = '0';
				valCover.Parameters.Param4.sValue=sValue;
				break;
		}
		if (bNullValue)	valModulec.value = valCover.value = '0';
	}
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">

<FORM METHOD="POST" NAME="MVA645" ACTION="valMantLife.aspx?sCodispl=MVA645&mode=1&WindowType=PopUp&nMainAction=306">
    <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="10%"><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%If CStr(Session("dEffecdate")) = "" Then
	Session("dEffecdate") = Today
End If
Response.Write(mobjValues.DateControl("tcdEffecdate", Session("dEffecdate"), True, GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  , "insChangeField(this)", False))%></TD>
            <TD WIDTH="15%">&nbsp;</TD>
            <TD>&nbsp;</TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valComtabliCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valComtabli", "Tabtab_comlif", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("valComtabliToolTip"))%> </TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valInterm_typCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valInterm_typ", "Tabinter_Typ", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("valInterm_typToolTip"))%> </TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valSellChanelCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valSellChanel", "table5532", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("valSellChanelToolTip"))%> </TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valWay_payCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valWay_pay", "table5002", eFunctions.Values.eValuesType.clngComboType, CStr(eRemoteDB.Constants.intNull),  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("valWay_payToolTip"))%> </TD>
		</TR>
		<TR>
			<TD><LABEL ID=13791><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), CStr(eRemoteDB.Constants.intNull), "valProduct",  ,  ,  , "insChangeField(this)", False))%></TD>
			<TD><LABEL ID=13804><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%
With mobjValues.Parameters
	.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
If CStr(Session("nBranch")) = "" Then
	Session("nBranch") = 0
End If
Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  , eFunctions.Values.eValuesType.clngWindowType, False, CStr(eRemoteDB.Constants.intNull),  ,  ,  , "insChangeField(this)"))%>
			</TD>
		</TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valModulecCaption") %></LABEL></TD>
			<TD><%
If Session("nProduct") = eRemoteDB.Constants.intNull Then
	Session("nProduct") = 0
End If
With mobjValues.Parameters
	.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
If CStr(Session("nModulec")) = "" Then
	Session("nModulec") = 0
End If
Response.Write(mobjValues.PossiblesValues("valModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "insChangeField(this)", False,  , GetLocalResourceObject("valModulecToolTip")))
%> 
			</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valCoverCaption") %></LABEL></TD>
            <TD><%
With mobjValues.Parameters
	.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nModulec", mobjValues.StringToType(Session("nModulec"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nCovernoshow", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nCovermax", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
If CStr(Session("nCover")) = "" Then
	Session("nCover") = 0
End If
Response.Write(mobjValues.PossiblesValues("valCover", "tablife_cover", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "if (this.value=='') this.value='0';", False, 4, GetLocalResourceObject("valCoverToolTip")))
%> 
			</TD>
		</TR>
    </TABLE>
    <P ALIGN=RIGHT>     
             <%=mobjValues.ButtonAcceptCancel( , "CancelErrors(true)",  ,  , eFunctions.Values.eButtonsToShow.All)%> </P>
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing%>

<SCRIPT>
//%CancelErrors: Reactiva menu al cancelar la ventana
//-------------------------------------------------------------------------------
function CancelErrors(bClose) {
//-------------------------------------------------------------------------------    

	top.close();
	
    with (opener.top.fraHeader){
        insHandImage("A390", false);
        insHandImage("A301", false);
	    insHandImage("A302", false);
	    insHandImage("A303", false);
	    insHandImage("A304", false);
	    insHandImage("A401", false);
	    insHandImage("A402", false);
	    insHandImage("A392", true);
	    insHandImage("A393", true);
	    insHandImage("A391", true);
	}
	
     opener.top.fraHeader.setPointer('');
} 
</SCRIPT>





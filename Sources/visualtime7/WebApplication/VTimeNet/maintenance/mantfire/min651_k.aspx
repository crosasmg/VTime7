<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues

'*++ Modificar nombre del objeto. Modificar "Class" por el nombre de la clase con la cual se trabaja
'- Objeto para el manejo particular de los datos de la página
Dim mcolClass As Object


</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues
mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MIN651"
%>
<HTML>
<HEAD>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:03 $|$$Author: Iusr_llanquihue $"
</SCRIPT>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>



<SCRIPT LANGUAGE="JavaScript">

//% insStateZone: Habilitación/Deshabilitación de campos de la forma según la acción a procesar.
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	self.document.forms[0].cbeBranch.disabled=false;
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

//% ChangeBranch: Habilita el campo "Producto" y pasa el valor del campo "Ramo" como parámetro
//--------------------------------------------------------------------------------------------
function ChangeBranch(Field){
//--------------------------------------------------------------------------------------------	
	with (document.forms[0]) {
		if(typeof(valCover)!='undefined'){
			valCover.Parameters.Param1.sValue=Field.value;
		}	
		if(typeof(valModulec)!='undefined') 
			valModulec.Parameters.Param1.sValue=Field.value;	
//+ Se deshabilitan los campos 			
		valModulec.value = "";
		valModulec.disabled = true;
		btnvalModulec.disabled = true;
		UpdateDiv('valModulecDesc', '');		
		valCover.value = "";
		valCover.disabled = true;
		btnvalCover.disabled = true;
		UpdateDiv('valCoverDesc', '');
	}
}

//% ChangeProduct: Habilita los campos y pasa el valor del campo "Producto" como parámetro
//--------------------------------------------------------------------------------------------
function ChangeProduct(Field){
//--------------------------------------------------------------------------------------------
	with (document.forms[0]) {
		if(typeof(valCover)!='undefined'){
			valCover.Parameters.Param2.sValue=Field.value;				
			if(typeof(valModulec)!='undefined') 
				valModulec.Parameters.Param2.sValue=Field.value;		
		}
//+ Se deshabilitan los campos 
		valCover.value = "";
		valCover.disabled = true;
		btnvalCover.disabled = true;
		UpdateDiv('valCoverDesc', '');
		UpdateDiv('valCurrencyDesc', '');
		if ((valProduct.value != "") && (valModulec.value != "")){
			valCover.disabled=false;
			btnvalCover.disabled=false;
		}
		insDefValues('MIN651', 'nBranch=' + cbeBranch.value + '&nProduct=' + valProduct.value, '/VTimeNet/Maintenance/MantFire');	
	}
}

//% ChangeModulec: Habilita los campos y pasa el valor del campo "Módulo" como parámetro
//--------------------------------------------------------------------------------------------
function ChangeModulec(Field){
//--------------------------------------------------------------------------------------------
	with (document.forms[0]) {
		if(typeof(valCover)!='undefined'){
			valCover.Parameters.Param3.sValue=Field.value;				
		    if ((valProduct.value != "") && (valModulec.value != "")){
				valCover.disabled=false;
				btnvalCover.disabled=false;
			}
			valCurrency.value = "";
			valCover.value = "";				
			UpdateDiv('valCoverDesc', '');
			UpdateDiv('valCurrencyDesc', '');
			valCurrency.disabled=false;
			tcdEffecdate.disabled=false;
			btn_tcdEffecdate.disabled=false;
		} 
	}
}

//% ChangeCover: Al cambiar el código de la cobertura.
//--------------------------------------------------------------------------------------------
function ChangeCover(Field){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]) {
		valCurrency.disabled=false;
		tcdEffecdate.disabled=false;
		btn_tcdEffecdate.disabled=false;
		valCurrency.value = valCover_nCurrency.value;
	}
}

</SCRIPT>
<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("MIN651", "MIN651_K.aspx", 1, vbNullString))
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MIN651_K" ACTION="valMantFire.aspx?sMode=2">
<BR><BR>
    <TABLE WIDTH="100%">
		<TR>
			<TD><LABEL><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"),  ,  ,  ,  ,  , "ChangeBranch(this);", True)%></TD>
			<TD><LABEL><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  ,  ,  ,  ,  ,  , "ChangeProduct(this);")%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("valModulecCaption") %></LABEL></TD>
			<TD><%With mobjValues
	.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "ChangeModulec(this)", True, 4, GetLocalResourceObject("valModulecToolTip"),  , 4))
End With
%>
			</TD>
			<TD><LABEL><%= GetLocalResourceObject("valCoverCaption") %></LABEL></TD>
            <%
With mobjValues.Parameters
	.Add("nBranch", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nModulec", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("sCovergen", "2", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.ReturnValue("nCurrency", False, vbNullString, True)
End With
%>
			<TD><%Response.Write(mobjValues.PossiblesValues("valCover", "TabGen_cover3", eFunctions.Values.eValuesType.clngWindowType, CStr(eRemoteDB.Constants.intNull), True,  ,  ,  ,  , "ChangeCover(this);", True,  , GetLocalResourceObject("valCoverToolTip")))%></TD>
        </TR>
        <TR>
			<TD><LABEL><%= GetLocalResourceObject("valCurrencyCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, Session("nCurrency"),  ,  ,  ,  ,  ,  , True, 5, GetLocalResourceObject("valCurrencyToolTip"))%></TD>
			<TD><LABEL><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today()),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True)%></TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>





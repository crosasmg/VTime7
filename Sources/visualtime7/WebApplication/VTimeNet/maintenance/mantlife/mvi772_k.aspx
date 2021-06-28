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
mobjValues.sCodisplPage = "MVI772"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	If Request.QueryString.Item("Type") <> "PopUp" Then
		.Write(mobjMenu.MakeMenu("MVI772", "MVI772_K.aspx", 1, vbNullString))
		.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
		mobjMenu = Nothing
	End If
End With
%>
<SCRIPT LANGUAGE=JavaScript>
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	with(self.document.forms[0]){
		cbeBranch.disabled=false;
		tcdEffecdate.disabled=false;
		btn_tcdEffecdate.disabled=false;
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
//% InsChangeField: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function InsChangeField(vObj, sField){
//--------------------------------------------------------------------------------------------
	var sValue;
	
	sValue = vObj.value;

	with (self.document.forms[0]){
		switch (sField){
			case 'Branch':
				valModulec.Parameters.Param1.sValue=sValue;
				valCover.Parameters.Param2.sValue=sValue;
				valRole.Parameters.Param1.sValue=sValue;
				break;
			case 'Product':
				valModulec.Parameters.Param2.sValue=sValue;
				valCover.Parameters.Param3.sValue=sValue;
				valRole.Parameters.Param2.sValue=sValue;
				if(cbeBranch.value!='' &&
				   valProduct.value!='' &&
				   tcdEffecdate.value!='')
					insDefValues('MVI729', 'nBranch=' + cbeBranch.value + '&nProduct=' + valProduct.value + '&dEffecdate=' + tcdEffecdate.value, '/VTimeNet/Maintenance/MantLife')
				break;
			case 'Effecdate':
				valCover.Parameters.Param5.sValue=sValue;
				valRole.Parameters.Param4.sValue=sValue;
				if(cbeBranch.value!='' &&
				   valProduct.value!='' &&
				   tcdEffecdate.value!='')
					insDefValues('MVI729', 'nBranch=' + cbeBranch.value + '&nProduct=' + valProduct.value + '&dEffecdate=' + tcdEffecdate.value, '/VTimeNet/Maintenance/MantLife')
				break;
			case 'Module':
				valCover.Parameters.Param4.sValue=sValue;
				valRole.Parameters.Param5.sValue=sValue;
				break;
			case 'Cover':
				valRole.Parameters.Param3.sValue=sValue;
		}
		if ((sValue == '' ||
		     sValue == '0') &&
		    sField != 'Module' &&
		    sField != 'Cover'){
			valModulec.disabled = true;
			valCover.disabled = true;
			UpdateDiv('valModulecDesc','');
			UpdateDiv('valCoverDesc','');
		}
		if (cbeBranch.value != '0' && 
		    valProduct.value != '' && 
		    tcdEffecdate.value != ''){
			valCover.disabled = false;
			if(valCover.value!='')
				valRole.disabled = false;
		}
		btnvalCover.disabled = valCover.disabled;
		btnvalModulec.disabled = valModulec.disabled;
		btnvalRole.disabled = valRole.disabled;
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI772_K" ACTION="valMantLife.aspx?sMode=2">
	<BR><BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct",  ,  ,  , "InsChangeField(this, ""Branch"")", True, 1)%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
            <TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), vbNullString, eFunctions.Values.eValuesType.clngWindowType, True, vbNullString,  ,  ,  , "InsChangeField(this,""Product"")", 2)%></TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdEffecdate", vbNullString,  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  , "InsChangeField(this,""Effecdate"")", True)%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valModulecCaption") %></LABEL></TD>
			<TD><%With mobjValues
	.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("valModulec", "tabtab_modul1", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "if (this.value=='') this.value='0';InsChangeField(this,""Module"");", True,  , GetLocalResourceObject("valModulecToolTip")))
%> 
			</TD>
		</TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valCoverCaption") %></LABEL></TD>
			<TD><%With mobjValues
	.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nCovernoshow", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nCovermax", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valCover", "tablife_cover", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "InsChangeField(this,""Cover"")", True, 4, GetLocalResourceObject("valCoverToolTip")))
End With
%>
			</TD>
            <TD><LABEL ID=0><%= GetLocalResourceObject("valRoleCaption") %></LABEL></TD>
            <TD><%
With mobjValues
	.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nCover", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("valRole", "tabTab_covrol3", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valRoleToolTip"),  , 3))
%>
            </TD> 
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>






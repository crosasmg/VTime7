<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

'- Objeto para el manejo particular de los datos de la página

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.ActionQuery = CDbl(IIf(Request.QueryString.Item("nMainAction")="",0,Request.QueryString.Item("nMainAction"))) = 401
mobjValues.sCodisplPage = "MVA606"
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>

<SCRIPT LANGUAGE=JavaScript>
//+ Se controla la versión
    document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 16:06 $"
	
//% insChangeField: se controla la modificación de los campos de parametros
//--------------------------------------------------------------------------------------------
function insChangeField(vObj){
//--------------------------------------------------------------------------------------------
    var sValue, bNullValue;
	
	sValue = vObj.value;
	bNullValue = ((sValue == '0') || (sValue == ''));
	
	with (self.document.forms[0]){
		switch (vObj.name){
			case 'cbeBranch':
				valProduct.Parameters.Param1.sValue=sValue;
				valModulec.Parameters.Param1.sValue=sValue;
				valCover.Parameters.Param2.sValue=sValue;
					
				valProduct.disabled = btnvalProduct.disabled = bNullValue;
				break;

			case 'valProduct':
				insDefValues('MVA606_P','nBranch=' + cbeBranch.value + '&nProduct=' + valProduct.value + '&dEffecdate=' + tcdEffecdate.value,'/VTimeNet/Maintenance/MantLife');
				valModulec.Parameters.Param2.sValue=sValue;
				valCover.Parameters.Param3.sValue=sValue;
				break;
					
			case 'tcdEffecdate':
				valModulec.Parameters.Param3.sValue=sValue;
				valCover.Parameters.Param5.sValue=sValue;
				break;
				
			case 'valModulec':	
				valCover.Parameters.Param4.sValue=sValue;
				break;
			
			case 'valCover':
		        insDefValues('MVA606','nBranch=' + cbeBranch.value + '&nProduct=' + valProduct.value + '&nModulec=' + valModulec.value + '&nCover=' + valCover.value + '&dEffecdate=' + tcdEffecdate.value,'/VTimeNet/Maintenance/MantLife');
				break;
		}
		if (sValue=='') vObj.value='0';	
	}
}

//% insStateZone: se controla el estado de los campos de la página
function insStateZone(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tcdEffecdate.disabled = btn_tcdEffecdate.disabled = false;
		cbeBranch.disabled = false;
		valProduct.disabled = btnvalProduct.disabled = false;
		valCover.disabled = btnvalCover.disabled = false;
		valModulec.disabled = btnvalModulec.disabled = false;
		optTypeTab[0].disabled = false;
		optTypeTab[1].disabled = false;
		chkSmoking.disabled = false;
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
	<%
Response.Write(mobjValues.StyleSheet())
If Request.QueryString.Item("Type") <> "PopUp" Then
	Response.Write(mobjMenu.MakeMenu("MVA606", "MVA606_K.aspx", 1, vbNullString))
	
	mobjMenu = Nothing
	Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End If
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVA606_K" ACTION="valMantLife.aspx?sMode=2">
<BR><BR>
	<TABLE WIDTH="100%">
		<TR>
			<TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD COLSPAN="2"><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today), True, GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  , "insChangeField(this)", True)%></TD>
		</TR>
		<TR>
			<TD><LABEL ID=13791><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD WIDTH="30%"><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType, mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble),  ,  ,  ,  ,  , "insChangeField(this)", True, 4, GetLocalResourceObject("cbeBranchToolTip"))%></TD>
			<TD WIDTH="15%"><LABEL ID=13804><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD COLSPAN="2"><%
With mobjValues.Parameters
	.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "insChangeField(this)", True, 4, GetLocalResourceObject("valProductToolTip")))
%>
			</TD>
		</TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valModulecCaption") %></LABEL></TD>
			<TD><%
With mobjValues.Parameters
	.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("valModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngWindowType, "", True,  ,  ,  ,  , "insChangeField(this)", True,  , GetLocalResourceObject("valModulecToolTip")))
%> 
			</TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valCoverCaption") %></LABEL></TD>
            <TD COLSPAN="2"><%
With mobjValues.Parameters
	.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nModulec", System.DBNull.Value, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("dEffecdate", Today, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nCovernoshow", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nCovermax", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
Response.Write(mobjValues.PossiblesValues("valCover", "tablife_cover", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "insChangeField(this)", True, 4, GetLocalResourceObject("valCoverToolTip")))
%> 
			</TD>
		</TR>
	    <TR>
   			<TD WIDTH="15%"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %>&nbsp;</LABEL></TD>
			<TD><%=mobjValues.DIVControl("valCurrencyDesc",  , "Moneda")%></TD>
	    </TR>
		<TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("Anchor2Caption") %></LABEL></TD>
		    <TD COLSPAN="2"><%
Response.Write(mobjValues.OptionControl(0, "optTypeTab", GetLocalResourceObject("optTypeTab_1Caption"), "1", "1",  , True))
Response.Write(mobjValues.OptionControl(0, "optTypeTab", GetLocalResourceObject("optTypeTab_2Caption"), "2", "2",  , True))
%> 
			</TD>
	        <TD COLSPAN="2"><%=mobjValues.CheckControl("chkSmoking", GetLocalResourceObject("chkSmokingCaption"), "2", "1",  , True)%> </TD>
	    </TR>
	</TABLE>
</FORM> 
</BODY>
</HTML>





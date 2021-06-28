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
mobjValues.sCodisplPage = "MVI729"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>		
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("MVI729", "MVI729_K.aspx", 1, vbNullString))
	.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction</SCRIPT>")
End With
%>
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 20/10/03 12:40 $|$$Author: Nvaplat18 $"

//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		tcdEffecdate.disabled = false;
		btn_tcdEffecdate.disabled = false;
		cbeBranch.disabled = false;
		cbeTyperisk.disabled = false;
	}
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% insFinish: se controla la acción Finalizar de la página
//--------------------------------------------------------------------------------------------
function insFinish(){
//--------------------------------------------------------------------------------------------
    return true;
}

//% InsChangeField: se controla el cambio de valor de los campos de la página
//--------------------------------------------------------------------------------------------
function InsChangeField(sField, sValue){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		switch (sField){
			case 'Branch':
				valProduct.Parameters.Param1.sValue = sValue;			
				valModulec.Parameters.Param1.sValue=sValue;
				valCover.Parameters.Param2.sValue=sValue;								
				tcdEffecdate.value="";
			    valModulec.value="";
			    UpdateDiv('valModulecDesc','');				
				valCover.value="";
				UpdateDiv('valCoverDesc','');
                cbeCurrency.value="";
                chkSmoking.checked=false;
				break;
			case 'Product':
				valModulec.Parameters.Param2.sValue=sValue;
				valCover.Parameters.Param3.sValue=sValue;
			    valModulec.value="";
			    UpdateDiv('valModulecDesc','');				
				valCover.value="";
				UpdateDiv('valCoverDesc','');
                cbeCurrency.value="";				
                chkSmoking.checked=false;
				break;
			case 'Effecdate':
				valModulec.Parameters.Param3.sValue = sValue;
				valCover.Parameters.Param5.sValue=sValue;
				insDefValues('MVI729', 'nBranch=' + cbeBranch.value + '&nProduct=' + valProduct.value + '&dEffecdate=' + tcdEffecdate.value, '/VTimeNet/Maintenance/MantLife');
				break;
			case 'Module':
				valCover.Parameters.Param4.sValue=sValue;
				valCover.value="";
				UpdateDiv('valCoverDesc','');
                cbeCurrency.value="";				
                chkSmoking.checked=false;
				break;
			case 'Cover':
				cbeCurrency.value=valCover_nCurrency.value;
		}
		if (sValue == '' ||
		    sValue == '0'){
			valModulec.disabled = true;
			valCover.disabled = true;
			UpdateDiv('valModulecDesc','');
			UpdateDiv('valCoverDesc','');
		}
	}
	
	with (self.document.forms[0]){
        if (cbeBranch.value>0 && valProduct.value>0 &&
            tcdEffecdate.value !='' && valModulec.value !=''){
		    valCover.disabled = false;
            btnvalCover.disabled = false;
        }
        else{
		      valCover.disabled = true;            
              btnvalCover.disabled = true;
        }          
	}  		
}

</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="MVI729" ACTION="valMantLife.aspx?sMode=2">
	<BR><BR>
    <TABLE WIDTH="100%">
        <TR>
            <TD WIDTH=20%><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD WIDTH=30%><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), vbNullString, "valProduct",  ,  ,  , "InsChangeField(""Branch"",this.value)", True, 1)%></TD>
			<TD WIDTH=15%><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), vbNullString, eFunctions.Values.eValuesType.clngWindowType, True, vbNullString,  ,  ,  , "InsChangeField(""Product"",this.value)", 2)%></TD>
        </TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
			<TD><%=mobjValues.DateControl("tcdEffecdate", "",  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  , "InsChangeField(""Effecdate"",this.value)", True, 3)%></TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valModulecCaption") %></LABEL></TD>
			<TD><%With mobjValues
	.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valModulec", "tabTab_modul", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "InsChangeField(""Module"",this.value)", True, 4, GetLocalResourceObject("valModulecToolTip"),  , 4))
End With
%>
			</TD>
		</TR>
        <TR>                       
			<TD><LABEL ID=0><%= GetLocalResourceObject("valCoverCaption") %></LABEL></TD>
			<TD COLSPAN="3"><%With mobjValues
	.Parameters.Add("sStatregt", "1", eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProduct", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nModulec", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("dEffecdate", eRemoteDB.Constants.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nCovernoshow", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nCovermax", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nCurrency", False, vbNullString, True)
	Response.Write(mobjValues.PossiblesValues("valCover", "tablife_cover", eFunctions.Values.eValuesType.clngWindowType, vbNullString, True,  ,  ,  ,  , "InsChangeField(""Cover"",this.value)", True, 4, GetLocalResourceObject("valCoverToolTip")))
End With
%>
			</TD>
        </TR>
        <TR>
			<TD><LABEL ID=14875><%= GetLocalResourceObject("cbeCurrencyCaption") %></LABEL></TD>
			<TD><%=mobjValues.PossiblesValues("cbeCurrency", "Table11", eFunctions.Values.eValuesType.clngComboType, vbNullString,  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeCurrencyToolTip"))%></TD>
            <TD COLSPAN="2"><%=mobjValues.CheckControl("chkSmoking", GetLocalResourceObject("chkSmokingCaption"), CStr(2), CStr(1),  ,  ,  , GetLocalResourceObject("chkSmokingToolTip"))%></TD>
		</TR>
        <TR>
            <TD WIDTH=20%><LABEL ID=0><%= GetLocalResourceObject("cbeTyperiskCaption") %></LABEL></TD>
            <TD><%With mobjValues
	.BlankPosition = False
	Response.Write(.PossiblesValues("cbeTyperisk", "Table5639", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("cbeTyperiskToolTip")))
End With
%>
			</TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing
%>
	





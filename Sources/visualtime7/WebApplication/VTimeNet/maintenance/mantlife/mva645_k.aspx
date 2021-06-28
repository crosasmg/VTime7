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
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


<%
Response.Write(mobjValues.StyleSheet())
Response.Write(mobjMenu.MakeMenu("MVA645", "MVA645_K.aspx", 1, vbNullString))
mobjMenu = Nothing
Response.Write("<SCRIPT>var nMainAction=top.frames['fraSequence'].plngMainAction;</SCRIPT>")
%>
<SCRIPT LANGUAGE=JavaScript>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:06 $|$$Author: Nvaplat61 $"

    var nProduct = '<%=Session("nProduct")%>';
    var nModulec = '<%=Session("nModulec")%>';
    
//% insStateZone: se controla el estado de los campos de la página
//--------------------------------------------------------------------------------------------
function insStateZone(nAction){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		valComtabli.disabled = false;
		valInterm_typ.disabled = false;
		valSellChanel.disabled = false;
		valWay_pay.disabled = false;
		tcdEffecdate.disabled = btn_tcdEffecdate.disabled = false;
		cbeBranch.disabled = false;
		valProduct.disabled = btnvalProduct.disabled = true;
		
//* Si el ramo tiene valor
		if (cbeBranch.value>0){
		    if (valProduct.value>0){
		        valProduct.disabled = false;
		        btnvalProduct.disabled = valProduct.disabled;
		        valProduct.Parameters.Param1.sValue=cbeBranch.value;
		        $(valProduct).change();
		        nProduct = valProduct.value;
		        if (valModulec.value>0){
		            valModulec.disabled = false;
		            btnvalModulec.disabled = valModulec.disabled;
		            valModulec.Parameters.Param1.sValue=cbeBranch.value;
		            valModulec.Parameters.Param2.sValue=valProduct.value;
		            if (tcdEffecdate.value!='')
		                valModulec.Parameters.Param3.sValue=tcdEffecdate.value;
		            $(valModulec).change();
		            nModulec = valModulec.value;
		            
		            if (valCover.value>0){
		                valCover.disabled = false;
		                btnvalCover.disabled = valCover.disabled;
		            }
		            
		        } else {
		            valCover.value = '';
		        }
		    } else {
		        valModulec.value = '';
		        valCover.value = '';
		    }
		} else {
		    valProduct.value = '';
		    valModulec.value = '';
		    valCover.value = '';
		}
	}	
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
				valCover.value = '';
				valModulec.value = '';
				UpdateDiv('valModulecDesc','','Normal');
				UpdateDiv('valCoverDesc','','Normal');
				break;

			case 'valProduct':
				valModulec.Parameters.Param2.sValue=sValue;
				valCover.Parameters.Param3.sValue=sValue;
				valCover.Parameters.Param4.sValue=0;
				valModulec.disabled = btnvalModulec.disabled = bNullValue;
				valCover.disabled = btnvalCover.disabled = bNullValue;
				if (nProduct!=valProduct.value) {
				    valCover.value = ''; 
				    valModulec.value = '';
				    UpdateDiv('valModulecDesc','','Normal');
				    UpdateDiv('valCoverDesc','','Normal');
				    nProduct = valProduct.value;
				}
				break;

			case 'tcdEffecdate':
				valModulec.Parameters.Param3.sValue=sValue;
				valCover.Parameters.Param4.sValue=sValue;
				break;

			case 'valModulec':
				if (sValue == '') sValue = '0';
				valCover.Parameters.Param4.sValue=sValue;
				if (nModulec!=valModulec.value) {
				    valCover.value = ''; 
				    UpdateDiv('valCoverDesc','','Normal');
				    nModulec = valModulec.value;
				}
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
<FORM METHOD="POST" NAME="MVA645" ACTION="valMantLife.aspx?sMode=2">
    <BR>
    <BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH="10%"><LABEL ID=0><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
            <TD><%If CStr(Session("dEffecdate")) = "" Then
	Session("dEffecdate") = Today
End If
Response.Write(mobjValues.DateControl("tcdEffecdate", Session("dEffecdate"), True, GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  , "insChangeField(this)", True))%></TD>
            <TD WIDTH="15%">&nbsp;</TD>
            <TD>&nbsp;</TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valComtabliCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valComtabli", "Tabtab_comlif", eFunctions.Values.eValuesType.clngComboType, Session("nComtabli"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valComtabliToolTip"))%> </TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valInterm_typCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valInterm_typ", "Tabinter_Typ", eFunctions.Values.eValuesType.clngComboType, Session("nIntertyp"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valInterm_typToolTip"))%> </TD>
		</TR>
		<TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valSellChanelCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valSellChanel", "table5532", eFunctions.Values.eValuesType.clngComboType, Session("nSellChannel"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valSellChanelToolTip"))%> </TD>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valWay_payCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("valWay_pay", "table5002", eFunctions.Values.eValuesType.clngComboType, Session("nWay_pay"),  ,  ,  ,  ,  ,  , True,  , GetLocalResourceObject("valWay_payToolTip"))%> </TD>
		</TR>
		<TR>
			<TD><LABEL ID=13791><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>						
            <TD><%Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), Session("nBranch"), "valProduct",  ,  ,  , "insChangeField(this)", True))%></TD>
			<TD><LABEL ID=13804><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>
			<TD><%
With mobjValues.Parameters
	.Add("nBranch", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
If CStr(Session("nBranch")) = "" Then
	Session("nBranch") = 0
End If
Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  , eFunctions.Values.eValuesType.clngWindowType, True, Session("nProduct"),  ,  ,  , "insChangeField(this)"))%>
			</TD>
		</TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valModulecCaption") %></LABEL></TD>
			<TD><%
If Session("nProduct") = eRemoteDB.Constants.intNull Then
	Session("nProduct") = 0
End If
With mobjValues.Parameters
	.Add("nBranch", mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("nProduct", mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Add("dEffecdate", Session("dEffecdate"), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
End With
If CStr(Session("nModulec")) = "" Then
	Session("nModulec") = 0
End If
Response.Write(mobjValues.PossiblesValues("valModulec", "tabtab_modul", eFunctions.Values.eValuesType.clngWindowType, Session("nModulec"), True,  ,  ,  ,  , "insChangeField(this)", True,  , GetLocalResourceObject("valModulecToolTip")))
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
Response.Write(mobjValues.PossiblesValues("valCover", "tablife_cover", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "if (this.value=='') this.value='0';", True, 4, GetLocalResourceObject("valCoverToolTip")))
%> 
			</TD>
		</TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%
mobjValues = Nothing%>





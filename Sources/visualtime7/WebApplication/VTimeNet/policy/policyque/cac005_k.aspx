<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.27.20
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cac005_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "cac005_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.27.20
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
<META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("CAC005", "CAC005_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing
%>
<SCRIPT>
//- Variable para el control de versiones
	document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:37 $"

//%insCancel : Cancelación de la acción
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}

//%insStateZone : Habilita los campos requeridos
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    var lintIndex = 0;
    for (lintIndex=0; lintIndex < document.forms[0].length; lintIndex++)
         document.forms[0].elements[lintIndex].disabled = false
    for (lintIndex=0; lintIndex < document.images.length; lintIndex++)
         document.images[lintIndex].disabled = false
}

//%ShowBranches : Ejecuta una popup de acurdo a lo seleccionado
//-------------------------------------------------------------------------------------------
function ShowBranches(){
//-------------------------------------------------------------------------------------------
	if (self.document.forms[0].optBranch[0].checked == true)
		self.document.forms[0].sBranchCondition.value = "";
	else
		ShowPopUp("CAC007.aspx","CAC007",400,400,"yes","no",100,100);
}

//%InsChangeFields: Actualiza parametros de la region
//---------------------------------------------------------------------------
function InsChangeFields(vObj){
//---------------------------------------------------------------------------
	with(self.document.forms[0]){
		if (vObj.name == 'valMunicipality'){
			if (valMunicipality.value != ''){
				cbeProvince.value = valMunicipality_nProvince.value;
				valLocal.value = valMunicipality_nLocal.value;
				UpdateDiv('valLocalDesc',valMunicipality_Tabdesc.value);
			}
		}
		else{
			if (vObj.name == 'cbeProvince'){
				valLocal.value='';
				UpdateDiv('valLocalDesc','')
			}
			else
				if (valLocal_nProvince.value != '')
					cbeProvince.value = valLocal_nProvince.value;

			valMunicipality.value = '';
			UpdateDiv('valMunicipalityDesc','')
		}
		valLocal.Parameters.Param1.sValue = cbeProvince.value;
		valMunicipality.Parameters.Param1.sValue = (valLocal.value==''?'0':valLocal.value);
		valMunicipality.Parameters.Param2.sValue = cbeProvince.value;
	}
}
</SCRIPT>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmCAC005" ACTION="valPolicyQue.aspx?Mode=1">
<BR><BR>
<TABLE WIDTH="100%">
	<TR>
	    <TD><LABEL><%= GetLocalResourceObject("cbeProvinceCaption") %><LABEL></TD>
	    <TD><%=mobjValues.PossiblesValues("cbeProvince", "Tab_Province", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "InsChangeFields(this)", True,  , GetLocalResourceObject("cbeProvinceToolTip"),  , 1)%></TD>
		<TD>&nbsp;</TD>
		<TD COLSPAN="2" CLASS="HighLighted"><LABEL ID=40640><A NAME="Ramos"><%= GetLocalResourceObject("AnchorRamosCaption") %></A></LABEL></TD>
	</TR>
	<TR>
		<TD COLSPAN="3"></TD>
		<TD COLSPAN="2" CLASS="Horline"></TD>
	</TR>
	<TR>
	    <TD><LABEL><%= GetLocalResourceObject("valLocalCaption") %></LABEL></TD>
	    <TD>
	    <%
mobjValues.Parameters.Add("nProvince", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
mobjValues.Parameters.ReturnValue("nProvince",  ,  , True)
Response.Write(mobjValues.PossiblesValues("valLocal", "tabTab_locat_a", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "InsChangeFields(this)", True,  , GetLocalResourceObject("valLocalToolTip"),  , 2))
%>
	    </TD>
		<TD>&nbsp;</TD>
		<TD COLSPAN="2"><%=mobjValues.OptionControl(40641, "optBranch", GetLocalResourceObject("optBranch_CStr1Caption"), eFunctions.Values.vbChecked, CStr(1), "ShowBranches()", True, 5, GetLocalResourceObject("optBranch_CStr1ToolTip"))%></TD>
	</TR>
	<TR>
	    <TD><LABEL><%= GetLocalResourceObject("valMunicipalityCaption") %><LABEL></TD>
	    <TD>
	    <%
With mobjValues
	.Parameters.Add("nLocat", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.Add("nProvince", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	.Parameters.ReturnValue("nLocal",  ,  , True)
	.Parameters.ReturnValue("Tabdesc",  ,  , True)
	.Parameters.ReturnValue("nProvince",  ,  , True)
	Response.Write(mobjValues.PossiblesValues("valMunicipality", "tab_municipality_a", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  , "InsChangeFields(this)", True,  , GetLocalResourceObject("valMunicipalityToolTip"),  , 3))
End With
%>
	    </TD>
		<TD>&nbsp;</TD>
		<TD COLSPAN="2"><%=mobjValues.OptionControl(40642, "optBranch", GetLocalResourceObject("optBranch_CStr2Caption"), eFunctions.Values.vbUnChecked, CStr(2), "ShowBranches()", True, 5, GetLocalResourceObject("optBranch_CStr2ToolTip"))%></TD>
	</TR>
	<TR>
	    <TD><LABEL ID=12635><A NAME="Fecha"><%= GetLocalResourceObject("tcdEffecdateCaption") %></A></LABEL></TD>
<TD COLSPAN="2"><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True, 4)%></TD>
	</TR>
</TABLE>
<%=mobjValues.HiddenControl("sBranchCondition", "")%>
</FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.27.20
Call mobjNetFrameWork.FinishPage("cac005_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





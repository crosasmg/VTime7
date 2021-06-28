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

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


	<SCRIPT>
//- Variable para el control de versiones
	     document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16.13 $|$$Author: Nvaplat60 $"
	</SCRIPT>    
<SCRIPT>
//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
}

//% InsStateZone: se controla el estado de los controles de la página
//--------------------------------------------------------------------------------------------
function InsStateZone(){
//--------------------------------------------------------------------------------------------
}

//% InsChangeField: se controla los parámetros del campo producto.
//--------------------------------------------------------------------------------------------
function InsChangeField(sField, sValue){
//--------------------------------------------------------------------------------------------
	with (self.document.forms[0]){
		switch (sField){
			case 'Branch':
				valProduct.Parameters.Param1.sValue=sValue;
				valProduct.disabled = (sValue == '0');
				btnvalProduct.disabled = valProduct.disabled;
				break;
		}
		valProduct.value = '';
		UpdateDiv('valProductDesc','');
	}
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjValues.WindowsTitle("COL626"))
	.Write(mobjMenu.MakeMenu("COL626", "COL626_K.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmCollectAgree" ACTION="valCollectionRep.aspx?mode=1">
	<BR><BR>
	<%Response.Write(mobjValues.ShowWindowsName("COL626"))%>
	<BR><BR>
    <TABLE WIDTH="100%">
        <TR>
		    <TD WIDTH="50%" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
			<TD></TD>
		    <TD WIDTH="50%" CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("cbeInsur_areaCaption") %></LABEL></TD>
		</TR>
		
		<TR>
		    <TD CLASS="HorLine"></TD>
			<TD></TD>
		    <TD CLASS="HorLine"></TD>
		</TR>
		
		<TR>
		     <%mobjValues.BlankPosition = False%> 
			<TD><%=mobjValues.PossiblesValues("cbeInsur_area", "Table5001", eFunctions.Values.eValuesType.clngComboType, "2",  ,  ,  ,  ,  ,  , False,  , GetLocalResourceObject("cbeInsur_areaToolTip"))%> </TD>
		    <TD></TD>
			<TD><%=mobjValues.OptionControl(0, "optTyp_info", GetLocalResourceObject("optTyp_info_1Caption"), "1", "1")%> </TD>
        </TR>
        
		<TR>
		    <TD></TD>
		    <TD></TD>
		    <TD><%=mobjValues.OptionControl(0, "optTyp_info", GetLocalResourceObject("optTyp_info_2Caption"),  , "2")%> </TD>
        </TR>
        
        <TR>
		    <TD></TD>
		    <TD></TD>
		    <TD><%=mobjValues.OptionControl(0, "optTyp_info", GetLocalResourceObject("optTyp_info_3Caption"),  , "3")%> </TD>
        </TR>        
        </TABLE>
        <TABLE WIDTH="100%">
        <TR>
			<TD WIDTH=30%><LABEL ID=0><%= GetLocalResourceObject("valAgreementCaption") %></LABEL></TD>	
			<TD><%With mobjValues
	Response.Write(mobjValues.PossiblesValues("valAgreement", "tabAgreement_sClient", eFunctions.Values.eValuesType.clngWindowType,  , False,  ,  ,  ,  ,  , False, 5, GetLocalResourceObject("valAgreementToolTip")))
End With
%>
			</TD>
        </TR>
        
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  , "InsChangeField(""Branch"",this.value)",  ,  , GetLocalResourceObject("cbeBranchToolTip"))%> </TD>
        </TR>
        
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>	
			<TD><%With mobjValues
	.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster1", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("valProductToolTip")))
End With
%>
			</TD>
        </TR>
        
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("tcdProcessDateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdProcessDate", CStr(Today),  , GetLocalResourceObject("tcdProcessDateToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>

<%
mobjValues = Nothing%>






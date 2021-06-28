<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.47.59
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo del menú
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("col556_k")

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "col556_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>


	<SCRIPT>
//- Variable para el control de versiones
         document.VssVersion="$$Revision: 3 $|$$Date: 11/06/04 13:19 $|$$Author: Nvaplat11 $"
	</SCRIPT>    
<SCRIPT>
//% InsStateZone: se controla el estado de los controles de la página
//--------------------------------------------------------------------------------------------
function InsStateZone(){
//--------------------------------------------------------------------------------------------
}

//% insCancel: se controla la acción Cancelar de la página
//--------------------------------------------------------------------------------------------
function insCancel(){
//--------------------------------------------------------------------------------------------
	return true;
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
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("COL556", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.MakeMenu("COL556", "COL556_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(CShort("1"), Request.QueryString.Item("sCodispl"), "COL556_K.ASPX"))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" NAME="COL556" ACTION="valCollectionRep.aspx?Mode=1">
	<BR><BR>
    <TABLE WIDTH="100%">
        <TR>
			<TD COLSPAN="2"></TD>
		    <TD CLASS="HighLighted"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		</TR>
		
		<TR>
			<TD COLSPAN="2"></TD>
		    <TD CLASS="HorLine"></TD>
		</TR>
		
		<TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("cbeInsur_areaCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeInsur_area", "Table5001", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeInsur_areaToolTip"))%> </TD>
			<TD><%=mobjValues.OptionControl(0, "optProcessTyp", GetLocalResourceObject("optProcessTyp_1Caption"), "1", "1")%> </TD>
        </TR>
        
		<TR>
		    <TD><LABEL ID=0><%= GetLocalResourceObject("tcdOperdateCaption") %></LABEL></TD>
            <TD><%=mobjValues.DateControl("tcdOperdate", CStr(Today),  , GetLocalResourceObject("tcdOperdateToolTip"))%></TD>
		    <TD><%=mobjValues.OptionControl(0, "optProcessTyp", GetLocalResourceObject("optProcessTyp_2Caption"),  , "2")%> </TD>
        </TR>
               
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbeBranch", "Table10", eFunctions.Values.eValuesType.clngComboType,  , False,  ,  ,  ,  , "InsChangeField(""Branch"",this.value)",  ,  , GetLocalResourceObject("cbeBranchToolTip"))%> </TD>
        </TR>
        <TR>
			<TD><LABEL ID=0><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD>	
			<TD WIDTH="50%"><%With mobjValues
	.Parameters.Add("nBranch", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
	Response.Write(mobjValues.PossiblesValues("valProduct", "tabProdmaster4", eFunctions.Values.eValuesType.clngWindowType,  , True,  ,  ,  ,  ,  , True, 4, GetLocalResourceObject("valProductToolTip")))
End With
%>
			</TD>
        </TR>
    </TABLE>
</FORM> 
</BODY>
</HTML>
<%mobjValues = Nothing%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.47.59
Call mobjNetFrameWork.FinishPage("col556_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





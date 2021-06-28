<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las zonas de la página    
Dim mobjMenu As eFunctions.Menues


</script>
<%Response.Expires = -1

mobjMenu = New eFunctions.Menues
mobjValues = New eFunctions.Values

mobjValues.sCodisplPage = "MGSL007"
%>
<HTML>
<HEAD>
	<META NAME="GENERATOR" CONTENT="Personalización VTime">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/valFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>




<SCRIPT>
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 3 $|$$Date: 19/12/03 18:29 $|$$Author: Nvaplat15 $"

//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------
   return true
}

//% insChangeField:
//--------------------------------------------------------------------------------------------
function insChangeField(objField){
//--------------------------------------------------------------------------------------------
//+ Si es Generales, se deshabilita el control chkDL3500 y se habilita cbeTipoInfo
	with(self.document.forms[0]){
		if ((cbeTipoInfo.value == 1)||
		   (cbeTipoInfo.value == 2)){
			optProcessType[0].Checked  = true;
			optProcessType[0].disabled = false;
			optProcessType[1].disabled = false;
			cbeTipoInfo.disabled = false;
		}
		else{
//+ Si es Vida, se habilita el control chkDL3500
			optProcessType[0].Checked  = true;
			optProcessType[0].disabled = true;
			optProcessType[1].disabled = true;
		}
	}
}

//% DisabledControl: 
//--------------------------------------------------------------------------------------------
function DisabledControl(objField){
//--------------------------------------------------------------------------------------------
	self.document.forms[0].cbeTipoInfo.disabled = objField.checked;
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("MGSL007", "MGSL007_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), session("sDesMultiCompany"), session("sSche_code")))
End With
%>    
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="MGSL007" ACTION="ValMarginRep.aspx?sMode=1">
    <BR><BR>
    <%=mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"))%>
    <BR><BR>
    <TABLE WIDTH="100%">
		<TR>
			<TD><LABEL ID="0"><%= GetLocalResourceObject("cbeMonthCaption") %></LABEL></TD>
            <TD><%mobjValues.BlankPosition = False
Response.Write(mobjValues.PossiblesValues("cbeMonth", "table7013", eFunctions.Values.eValuesType.clngComboType, CStr(Month(Today)),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeMonthToolTip")))
%>
            </TD>
            <TD>&nbsp;</TD>
			<TD CLASS="HighLighted" COLSPAN="2"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>			
		</TR>
		<TR>
			<TD COLSPAN="3"></TD>
			<TD CLASS="HorLine" COLSPAN="2"></TD>
        </TR>
        <TR>
			<TD><LABEL ID="0"><%= GetLocalResourceObject("tcnYearCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.NumericControl("tcnYear", 4, CStr(Year(Today)), True, GetLocalResourceObject("tcnYearToolTip"), False)%></TD>
			<TD>&nbsp;</TD>
			<TD COLSPAN="2"><%=mobjValues.OptionControl(0, "optProcessType", GetLocalResourceObject("optProcessType_1Caption"), CStr(1), "1",  , False,  , GetLocalResourceObject("optProcessType_1ToolTip"))%></TD>
        </TR>
        <TR>
        	<TD><LABEL><%= GetLocalResourceObject("cbeTipoInfoCaption") %></LABEL></TD>
			<TD><%With mobjValues
	.BlankPosition = False
	If CStr(session("nInsur_area")) = "1" Then
		.TypeList = CShort("1")
		.List = "1,2"
	End If
	Response.Write(mobjValues.PossiblesValues("cbeTipoInfo", "table5673", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  , "insChangeField(""cbeTipoInfo"")",  ,  , GetLocalResourceObject("cbeTipoInfoToolTip"), eFunctions.Values.eTypeCode.eNumeric))
End With
%>
			</TD>
			<TD>&nbsp;</TD>
			<TD><%=mobjValues.OptionControl(0, "optProcessType", GetLocalResourceObject("optProcessType_2Caption"),  , "2",  , False,  , GetLocalResourceObject("optProcessType_2ToolTip"))%></TD>
        </TR>
    </TABLE>
</FORM>
</BODY>
</HTML>
<%
mobjMenu = Nothing
mobjValues = Nothing
%>





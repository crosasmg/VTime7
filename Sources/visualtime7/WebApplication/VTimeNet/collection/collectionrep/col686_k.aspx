<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.47.59
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
Call mobjNetFrameWork.BeginPage("col686_k")

With Server
	mobjValues = New eFunctions.Values
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
	mobjValues.sSessionID = Session.SessionID
	mobjValues.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
	
	mobjValues.sCodisplPage = "col686_k"
	mobjMenu = New eFunctions.Menues
	'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.47.59
	mobjMenu.sSessionID = Session.SessionID
	mobjMenu.nUsercode = Session("nUsercode")
	'~End Body Block VisualTimer Utility
End With
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tMenu.js"></SCRIPT>



    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT>
//+ Variable para el control de versiones
     document.VssVersion="$$Revision: 2 $|$$Date: 29/09/03 17:41 $|$$Author: Nvaplat15 $"
</SCRIPT>    
<SCRIPT>

//%	ShowDefValues: Condiciona el recargo por el cambio en el patrón de busqueda
//-------------------------------------------------------------------------------------------
function ShowDefValues22(nCollectorType){
//-------------------------------------------------------------------------------------------
    var lintProcess = 0;
    
    switch (nCollectorType){
        case '1':
            lintProcess=33;
            break;
        case '2':
            lintProcess=60;
            break;
        case '3':
            lintProcess=61;
    }
    
    insDefValues("Type_Process","nType_Process=" + lintProcess);
}

//% insCancel: se controla la acción Cancelar de la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}

//% insStateZone: se controla el estado de los campos de la página
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
    //ShowDefValues();  
}

</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjValues.WindowsTitle("COL686", Request.QueryString.Item("sWindowDescript")))
	.Write(mobjMenu.MakeMenu("COL686", "COL686_K.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmModCollect" ACTION="valCollectionRep.aspx?mode=1">
<BR><BR><BR>
    <%Response.Write(mobjValues.ShowWindowsName("COL686", Request.QueryString.Item("sWindowDescript")))%>
    <TABLE WIDTH="100%" BORDER = 0>
    <BR>
        <TR>
            <TD ><LABEL ID=13791><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
            <TD COLSPAN="2">&nbsp;</TD>
        </TR>
        <TR>
            <TD COLSPAN="2" CLASS="HORLINE"></TD>
        </TR>
        <TR>
            <TD COLSPAN="1"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_1Caption"), CStr(1), "1",  , False)%></TD>
            <TD COLSPAN="1"><%=mobjValues.OptionControl(0, "optExecute", GetLocalResourceObject("optExecute_2Caption"),  , "2",  , False)%></TD>
			<TD COLSPAN="2">&nbsp;</TD>
        </TR>
        <TR><TD COLSPAN="4">&nbsp;</TD></TR>
        <TR>
            <TD><LABEL ID="10295"><%= GetLocalResourceObject("cbeInsurAreaCaption") %></LABEL></TD>
            <%mobjValues.BlankPosition = False%>      			
            <TD><%=mobjValues.PossiblesValues("cbeInsurArea", "table5001", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeInsurAreaToolTip"))%></TD>                
            <TD COLSPAN="2">&nbsp;</TD>
		</TR>		
		<TR><TD COLSPAN="4">&nbsp;</TD></TR>
		<TR>
		    <TD><LABEL ID="10528"><%= GetLocalResourceObject("cbeCollectorTypeCaption") %></LABEL></TD>
		    <%mobjValues.BlankPosition = False%>
		    <TD><%=mobjValues.PossiblesValues("cbeCollectorType", "table5551", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  , "ShowDefValues22(this.value);",  ,  , GetLocalResourceObject("cbeCollectorTypeToolTip"))%></TD>
		    <TD COLSPAN="2">&nbsp;</TD>
		</TR>
		<TR><TD COLSPAN="4">&nbsp;</TD></TR>
		<TR>
		    <TD><LABEL ID="10528"><%= GetLocalResourceObject("tcdInitDateCaption") %></LABEL></TD>
		    <TD><%=mobjValues.DateControl("tcdInitDate",  ,  , GetLocalResourceObject("tcdInitDateToolTip"),  ,  ,  ,  , True)%></TD>
		    <TD><LABEL ID="10528"><%= GetLocalResourceObject("tcdFinalDateCaption") %></LABEL></TD>
		    <TD><%=mobjValues.DateControl("tcdFinalDate", CStr(Today),  , GetLocalResourceObject("tcdFinalDateToolTip"),  ,  ,  ,  , False)%></TD>
		</TR>
    </TABLE>    
</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.47.59
Call mobjNetFrameWork.FinishPage("col686_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





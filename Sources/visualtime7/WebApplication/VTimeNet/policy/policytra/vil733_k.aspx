<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.31.39
Dim mobjNetFrameWork As eNetFrameWork.Layout
'- Objeto para el manejo de las funciones generales de carga de valores 
Dim mobjValues As eFunctions.Values
'- Objeto para el manejo del menú 
Dim mobjMenu As eFunctions.Menues

'- Variables de uso de la pagina ramo,producto en Sub "insReaInitial"
Dim mstrOptProc As String
Dim mintBranch As String
Dim mintProduct As String


'-----------------------------------------------------------------------------------------
Private Sub insReaInitial()
	'-----------------------------------------------------------------------------------------
	mstrOptProc = "1"
	If Request.QueryString.Item("mintBranch") <> vbNullString Then
		mintBranch = Request.QueryString.Item("mintBranch")
		Session("nBranch") = mintBranch
	End If
	If Request.QueryString.Item("mintProduct") <> vbNullString Then
		mintProduct = Request.QueryString.Item("mintProduct")
		Session("nProduct") = mintProduct
	End If
	If Request.QueryString.Item("mstrOptProc") <> vbNullString Then
		mstrOptProc = Request.QueryString.Item("mstrOptProc")
	End If
	Session("dEffecdate") = Today
End Sub

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("vil733_k")
'~End Header Block VisualTimer Utility
'- Transacción vil733 : Aniversario de Coberturas 

Response.Cache.SetCacheability(HttpCacheability.NoCache)

mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "vil733_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.31.39
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

'    mobjValues.ActionQuery = Request.QueryString("nMainAction") = 401
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>





<SCRIPT LANGUAGE="JavaScript">  
//- Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:04 $|$$Author: Iusr_llanquihue $"

//% insFinish: Manejo de la acciones de la pagina
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
//% insFinish: Terminar transacción
//-------------------------------------------------------------------------------------------
function insFinish(){
//-------------------------------------------------------------------------------------------
var nAction = new TypeActions();
//+ En modo consulta refresca la página
	if (top.frames["fraSequence"].plngMainAction == nAction.clngActionQuery) {
		insReloadTop(false);
		return false;
	}
	else 
//+ En otro modo ejecuta la validación
		return true;
}

//% insCancel: Anular ingreso
//-------------------------------------------------------------------------------------------
function insCancel(){
//-------------------------------------------------------------------------------------------    
    return true;
}
</SCRIPT>
<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("VIL733", "VIL733_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	.Write(mobjMenu.setZone(1, "VIL733", Request.QueryString.Item("sWindowDescript"), CShort(Request.QueryString.Item("nWindowTy"))))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
    <FORM METHOD="POST" ID="FORM" NAME="VIL733" ACTION="ValPolicyTra.aspx?x=1">
    <BR><BR>
	<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
' Se el proceso ya se jecuto mantiene los valores  
Call insReaInitial()
%>
    <TABLE WIDTH="100%" BORDER=0>
    	<TR>
			<TD><LABEL ID=13937><%= GetLocalResourceObject("cbeBranchCaption") %></LABEL></TD>
			<TD><%=mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip"), CStr(0),  ,  ,  ,  ,  ,  , 1)%></TD>
            <TD CLASS="HIGHLIGHTED"><LABEL ID=0><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>
		</TR>
    	<TR> 
			<TD></TD> 
			<TD></TD> 
			<TD CLASS="HORLINE"></TD> 
		</TR> 
		<TR> 
		    <TD><LABEL ID=13947><%= GetLocalResourceObject("valProductCaption") %></LABEL></TD> 
			<TD><%=mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"), CStr(0),  ,  ,  ,  ,  ,  ,  , 2)%></TD> 
			<TD><%=mobjValues.OptionControl(0, "sOptExecute", GetLocalResourceObject("sOptExecute_1Caption"), "1", "1",  , False, 4, GetLocalResourceObject("sOptExecute_1ToolTip"))%> 
			    <%=mobjValues.OptionControl(0, "sOptExecute", GetLocalResourceObject("sOptExecute_2Caption"), "2", "2",  , False, 5, GetLocalResourceObject("sOptExecute_2ToolTip"))%></TD> 
        </TR> 
		<TR> 
			<TD><LABEL ID=13722><%= GetLocalResourceObject("tcdEffecdateCaption") %></LABEL></TD>
<TD><% %>
<%=mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , False, 3)%></TD>
		</TR>
        <TR>
			<%Response.Write(mobjValues.HiddenControl("sCertype", "2"))%>
        </TR>
        <%mobjValues.ActionQuery = True%>
	</TABLE>
    </FORM>
</BODY>
</HTML>
<%
mobjValues = Nothing
mobjMenu = Nothing
%>
<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.31.39
Call mobjNetFrameWork.FinishPage("vil733_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





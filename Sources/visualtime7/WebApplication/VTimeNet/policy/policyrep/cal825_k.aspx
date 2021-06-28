<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 7/4/03 10.28.03
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
'- Objeto para el manejo de las funciones generales de carga de valores.
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'%insDefineHeader: Permite cargar los campos del encabezado
'-----------------------------------------------------------------------------------------
Private Sub insDefineHeader()
	'-----------------------------------------------------------------------------------------
	Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>&nbsp</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_3Caption"),  , "3"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_1Caption"),  , "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.OptionControl(0, "optType", GetLocalResourceObject("optType_2Caption"), CStr(1), "2"))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>" & vbCrLf)
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>&nbsp</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	    <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("valProductCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.ProductControl("valProduct", GetLocalResourceObject("valProductToolTip"),  ,  ,  ,  ,  ,  ,  , "insEnabledPolicy(this)"))


Response.Write("</TD>" & vbCrLf)
Response.Write("	    </TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnPolicyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnPolicy", 10, vbNullString,  , GetLocalResourceObject("tcnPolicyToolTip"),  ,  ,  ,  ,  , "insShowValues();"))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp;</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdEffecdateCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.DateControl("tcdEffecdate", CStr(Today),  , GetLocalResourceObject("tcdEffecdateToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD>&nbsp</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnLegAmount_oldCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnLegAmount_old", 18, vbNullString,  , GetLocalResourceObject("tcnLegAmount_oldToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD>&nbsp</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcnLegAmountCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.NumericControl("tcnLegAmount", 18, vbNullString,  , GetLocalResourceObject("tcnLegAmountToolTip"),  ,  ,  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("	</TABLE>")

End Sub

</script>
<%Response.Expires = -1
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("cal825_k")
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
mobjValues.sCodisplPage = "cal825_k"
mobjMenu = New eFunctions.Menues
'^Begin Body Block VisualTimer Utility 1.1 7/4/03 10.28.03
mobjMenu.sSessionID = Session.SessionID
mobjMenu.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT>
//%   insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//%   insStateZone: Se controla el estado de los campos de la página.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}
//%   insEnabledPolicy(): Permite habilitar e inhabilitar el campo Póliza.
//------------------------------------------------------------------------------------------
function insEnabledPolicy(lobject){
//------------------------------------------------------------------------------------------
	if (lobject.value) 
		self.document.forms[0].tcnPolicy.disabled=false;
    else
        with(self.document.forms[0]){
			tcnPolicy.disabled=true;
			tcnPolicy.value="";
        }
}
//% Llama a la página ShowValues con el valor "ShowCertif" para habilitar o inhabilitar

//-------------------------------------------------------------------------------------------
function insShowValues(){
//-------------------------------------------------------------------------------------------
var optType_aux;
	with(self.document.forms[0]){
		if (optType[0].checked)
		    optType_aux = 1;
		if (optType[1].checked)
		    optType_aux = 3;
		if (optType[2].checked)
		    optType_aux = 2;
		insDefValues("ShowLeg", "nBranch=" + cbeBranch.value + "&nProduct=" + valProduct.value + "&nPolicy=" + tcnPolicy.value + "&sCertype=" + optType_aux);
	}
}
</SCRIPT>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "CAL825_k.aspx", 1, Request.QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
End With
mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="CAL825" ACTION="valPolicyRep.aspx?Mode=1">
<BR><BR>
<%
Call insDefineHeader()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 7/4/03 10.28.03
Call mobjNetFrameWork.FinishPage("cal825_k")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>






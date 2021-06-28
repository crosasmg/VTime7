<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'**-Objetive: Object for the handling of LOG
'-Objetivo: Objeto para el manejo de LOG
Dim mobjNetFrameWork As eNetFrameWork.Layout

'**-Objetive: Object for the handling of the general functions of load of values
'-Objetivo: Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'**-Objetive: Object for the handling of the generics routines
'-Objetivo: Objeto para el manejo de las rutinas genéricas
Dim mobjMenues As eFunctions.Menues


'**%Objetive: The controls of the page are loaded
'%Objetivo: Se cargan los controles de la página
'----------------------------------------------------------------------------------------------------------------------
Private Sub insPreCOL836()
	'----------------------------------------------------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdDateIniCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")

Response.Write(mobjValues.DateControl("tcdDateIni", CStr(Today),  , GetLocalResourceObject("tcdDateIniToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("			<TD><LABEL ID=0>" & GetLocalResourceObject("tcdDateEndCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdDateEnd", CStr(Today),  , GetLocalResourceObject("tcdDateEndToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeWay_payCaption") & "</LABEL></TD> " & vbCrLf)
Response.Write("            <TD>")

	
	mobjValues.TypeList = 2
	mobjValues.List = "7,5,6,3,8,4"
	Response.Write(mobjValues.PossiblesValues("cbeWay_pay", "table5002", eFunctions.Values.eValuesType.clngComboType, CStr(1),  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeWay_payToolTip")))
Response.Write("</TD> 		" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeBankCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeBank", "table7", eFunctions.Values.eValuesType.clngComboType, "", False, False, "", "",  ,  , False,  , GetLocalResourceObject("cbeBankToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("		<TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeBranchCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.BranchControl("cbeBranch", GetLocalResourceObject("cbeBranchToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("            <TD><LABEL ID=0>" & GetLocalResourceObject("cbeAgencyCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.PossiblesValues("cbeAgency", "table5555", eFunctions.Values.eValuesType.clngWindowType, "", False, False, "", "",  ,  , False,  , GetLocalResourceObject("cbeAgencyToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("		</TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
	
End Sub

</script>
<%
'----------------------------------------------------------------------------------------------------
'**+Objective: GENERAL DESCRIPTION
'**+Version: $$Revision: $
'+Objetivo: DESCRIPCION GENERAL
'+Version: $$Revision: $
'----------------------------------------------------------------------------------------------------
Response.Expires = -1441

mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.BeginPage(Request.QueryString.Item("sCodispl"))

mobjValues = New eFunctions.Values
mobjValues.sSessionID = Session.SessionID
mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
    <META NAME="GENERATOR" CONTENT="Visual TIME Templates">
    <%=mobjValues.StyleSheet()%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT>

//**-Objetive: This line keeps the version coming from VSS
//-Objeto: Esta línea guarda la versión procedente de VSS
//----------------------------------------------------------------------------------------------------------------------
    document.VssVersion="$$Revision: 1 $|$$Date: 09/16/03 1:00p|$$Author: nsoler $"
//----------------------------------------------------------------------------------------------------------------------

//**%Objetive: It allows to cancel the page
//%Objetivo: Manejar los campos de la página de acuerdo a la acción
//-------------------------------------------------------------------------------------------
function insStateZone(nMainAction){
//-------------------------------------------------------------------------------------------
}

//**%Objetive: It allows to cancel the page
//%Objetivo: Permite cancelar la página.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
    return true;
}

//**%Objetive: It allows to finish the page
//%Objetivo: Permite finalizar la página.
//------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------
    return true;
}
</SCRIPT>
<%
With Request
	mobjValues.ActionQuery = (.QueryString.Item("nMainAction") = CStr(eFunctions.Menues.TypeActions.clngActionQuery))
	
	Response.Write("<SCRIPT LANGUAGE=""JavaScript"" SRC=""/VTimeNet/Scripts/tmenu.js""></SCRIPT>")
	mobjMenues = New eFunctions.Menues
	mobjMenues.sSessionID = Session.SessionID
	Response.Write(mobjMenues.MakeMenu(.QueryString.Item("sCodispl"), .QueryString.Item("sCodispl") & "_K.aspx", 1, .QueryString.Item("sWindowDescript"), Session("sDesMultiCompany"), Session("sSche_code")))
	mobjMenues = Nothing
End With
%>
</HEAD>    
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="<%=Request.QueryString.Item("sCodispl")%>" ACTION="valCollectionRep.aspx?sZone=1">
<BR><BR>
<%
Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl"), Request.QueryString.Item("sWindowDescript")))
insPreCOL836()
mobjValues = Nothing

mobjNetFrameWork.FinishPage(Request.QueryString.Item("sCodispl"))
mobjNetFrameWork = Nothing
%>
</FORM>
</BODY>
</HTML>






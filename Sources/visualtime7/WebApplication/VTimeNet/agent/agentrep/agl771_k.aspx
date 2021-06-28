<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues
Dim mclsCtrol_date As eGeneral.Ctrol_date


'%insPreAGL771: Se cargan los controles de la ventana
'----------------------------------------------------------------------------
Private Sub insPreAGL771()
	'----------------------------------------------------------------------------
	
Response.Write("" & vbCrLf)
Response.Write("	<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		<BR><BR>" & vbCrLf)
Response.Write("		")


Response.Write(mobjValues.ShowWindowsName(Request.QueryString.Item("sCodispl")))


Response.Write("" & vbCrLf)
Response.Write("		<BR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD><LABEL ID=100879>" & GetLocalResourceObject("cbeInter_typCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>" & vbCrLf)
Response.Write("				")

	mobjValues.TypeList = 1
	mobjValues.List = "1,3,6,10" '1)Agente directo 3)Corredor 6)Agente compañía 10)Agente libre
	Response.Write(mobjValues.PossiblesValues("cbeInter_typ", "tabInter_typ", eFunctions.Values.eValuesType.clngComboType,  ,  ,  ,  ,  ,  , "insChangeInter_typ()",  ,  , GetLocalResourceObject("cbeInter_typToolTip")))
	
Response.Write("" & vbCrLf)
Response.Write("			</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD WIDTH=""25%""><LABEL ID=11288>" & GetLocalResourceObject("tcdDateIniCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdDateIni",  ,  , GetLocalResourceObject("tcdDateIniToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=100879>" & GetLocalResourceObject("tcdDateEndCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("            <TD>")


Response.Write(mobjValues.DateControl("tcdDateEnd", "",  , GetLocalResourceObject("tcdDateEndToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("	</TABLE>")

	mobjValues = Nothing
	mclsCtrol_date = Nothing
End Sub

</script>
<%Response.Expires = -1
Response.Cache.SetCacheability(HttpCacheability.NoCache)

With Server
	mobjValues = New eFunctions.Values
	mobjMenu = New eFunctions.Menues
	mclsCtrol_date = New eGeneral.Ctrol_date
End With
%>


<SCRIPT LANGUAGE="JavaScript">
//+ Variable para el control de versiones
    document.VssVersion="$$Revision: 1 $|$$Date: 2/09/03 19:01 $"

//%insChangeInter_typ: Se obtiene la fecha de ctrol_date dependiendo del tipo de intermediario seleccionado.
//------------------------------------------------------------------------------
function insChangeInter_typ(){
//------------------------------------------------------------------------------
	
	with(self.document.forms[0]){
	    insDefValues("LastProcess_date", "sValue=AGL771" + "&nInterTyp=" + cbeInter_typ.value, '/VTimeNet/Agent/AgentRep')
	}
}

//%insStateZone: Se habilita/deshabilita los campos de la ventana.
//------------------------------------------------------------------------------
function insStateZone(){
//------------------------------------------------------------------------------
}

//%insCancel: Acciones a efectuar al cancelar la transacción.
//------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------
	return true;
}

//%insFinish: Acciones a efectuar al finalizar la transacción.
//------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------
	return true;
}
</SCRIPT>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>


<%With Response
	.Write(mobjValues.StyleSheet() & vbCrLf)
	.Write(mobjMenu.MakeMenu(Request.QueryString.Item("sCodispl"), "AGL771_K.aspx", 1, ""))
	'+Se agrega zona para dejar des-habilitado el botón aceptar
	.Write(mobjMenu.setZone(1, "AGL771", ""))
End With
mobjMenu = Nothing
%>
</HEAD>
<BR></BR>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="frmRIntermAccount" ACTION="ValAgentRep.aspx?mode=1">
<%
Call insPreAGL771()
%>
</FORM>
</BODY>
</HTML>





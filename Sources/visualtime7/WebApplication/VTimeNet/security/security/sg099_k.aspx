<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'% insPreSG099: Se definen los objetos a ser utilizados y permite realizar el llamado al
'% método de lectura para mostrar la información en la parte de detalle de la página.
'-----------------------------------------------------------------------------------------
Private Sub insPreSG099()
	'-----------------------------------------------------------------------------------------
	Dim lclsUser As eSecurity.User
	
	lclsUser = New eSecurity.User
	
	lclsUser.Find(mobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble))
	
Response.Write("" & vbCrLf)
Response.Write("<BR><BR>" & vbCrLf)
Response.Write("<BR><BR>" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD COLSPAN=2>")

	Response.Write(mobjValues.ShowWindowsName("SG099"))
Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("		<TD><LABEL ID=14962>" & GetLocalResourceObject("tctLoginCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.TextControl("tctLogin", 12, lclsUser.sInitials,  , GetLocalResourceObject("tctLoginToolTip"),  ,  ,  ,  , True))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR> " & vbCrLf)
Response.Write("        <TD>&nbsp;</TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.DIVControl("tctCliename",  , lclsUser.sCliename))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=14959>" & GetLocalResourceObject("tctOldPassCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PasswordControl("tctOldPass", 12, vbNullString,  , GetLocalResourceObject("tctOldPassToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD><LABEL ID=14960>" & GetLocalResourceObject("tctNewPassCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PasswordControl("tctNewPass", 12, vbNullString,  , GetLocalResourceObject("tctNewPassToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>" & vbCrLf)
Response.Write("    <TR>" & vbCrLf)
Response.Write("        <TD WIDTH=""35%""><LABEL ID=14961>" & GetLocalResourceObject("tctRNewPassCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("        <TD>")


Response.Write(mobjValues.PasswordControl("tctRNewPass", 12, vbNullString,  , GetLocalResourceObject("tctRNewPassToolTip")))


Response.Write("</TD>" & vbCrLf)
Response.Write("    </TR>                " & vbCrLf)
Response.Write("</TABLE>")

End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "SG099"

mobjMenu = New eFunctions.Menues
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></script>
<SCRIPT>

//%insCancel: Esta función finaliza la transacción al presionar cancelar.
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
	return true;
}
//% insStateZone: Permite habilitar los objetos y las imágenes en la ventana. En
//% esta página el tratamiento es diferente, ya que posee sólo la acción Entrar 
//% y por lo tanto debería entrar con los objetos ya habilitados.
//------------------------------------------------------------------------------------------
function  insStateZone(){
//------------------------------------------------------------------------------------------
}

//- Esta línea guarda la versión procedente de VSS 
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 17.43 $|$$Author: Nvaplat60 $"

</SCRIPT>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


<%
With Response
	.Write(mobjValues.StyleSheet())
	.Write(mobjMenu.MakeMenu("SG099", "SG099_k.aspx", 1, ""))
	.Write(mobjMenu.setZone(CShort("1"), "SG099", "SG099_K.aspx"))
End With

mobjMenu = Nothing
%>
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="post" ID="FORM" NAME="SG099" ACTION="valSecurity.aspx?Mode=1">
<%
Call insPreSG099()
mobjValues = Nothing
%>
</FORM>
</BODY>
</HTML>





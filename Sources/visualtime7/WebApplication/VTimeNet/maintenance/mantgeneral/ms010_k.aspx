<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim mobjMenu As eFunctions.Menues


'%insPreMS010_K: carga los valores de la página inicial de la secuencia de opciones de instalación
'--------------------------------------------------------------------------------------------
Private Sub insPreMS010_K()
	'--------------------------------------------------------------------------------------------    
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("			<TD WIDTH = ""10%""><LABEL ID=0>" & GetLocalResourceObject("tctPasswordCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("			<TD>")


Response.Write(mobjValues.PasswordControl("tctPassword", 10, vbNullString, True, GetLocalResourceObject("tctPasswordToolTip"), False,  ,  ,  , True, 0))


Response.Write("</TD>" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    </TABLE>")

	
End Sub

</script>
<%Response.Expires = -1

'+ Se crean los objetos propios para el manejo de la página

mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "MS010"
%>
<HTML>
	<HEAD>
		<META NAME = "GENERATOR" Content="Microsoft Visual Studio 6.0">
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/Constantes.js">		</SCRIPT>
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js">	</SCRIPT>
<SCRIPT LANGUAGE = "JavaScript" SRC="/VTimeNet/Scripts/tmenu.js">			</SCRIPT>





<SCRIPT>
//%insStateZone: se encarga de habilitar los controles cuando se selecciona una acción
//-----------------------------------------------------------------------------------------------------
function insStateZone(){
//-----------------------------------------------------------------------------------------------------
	self.document.forms[0].elements["tctPassword"].disabled = false
}

//%insFinish: se activa al finalizar las acciones de la secuencia
//------------------------------------------------------------------------------------------------------
function insFinish(){
//------------------------------------------------------------------------------------------------------
	insReloadTop(false)
}

//%insCancel: se activa al cancelar las acciones de la secuencia
//------------------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------------------
	return true;
}

</SCRIPT>
		<%=mobjValues.StyleSheet()%>
		<%=mobjMenu.MakeMenu("MS010_K", "MS010_K", 1, "")%>
	</HEAD>
	<BODY>
		<P>&nbsp;</P>
		<FORM METHOD="POST" ACTION = "valMantGeneral.aspx?Parameter=1">
			<%insPreMS010_K()%>
		</FORM>
	</BODY>
</HTML>







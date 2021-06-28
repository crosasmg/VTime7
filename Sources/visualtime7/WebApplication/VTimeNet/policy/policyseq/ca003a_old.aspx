<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">


'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'-Objeto para el manejo y evaluación de los procesos de póliza.
Dim mobjRoles As ePolicy.Roleses

'-Indica el nombre del control que llamo la forma
Dim lstrControlName As String
Dim lstrControlClieName As String



'% ShowRecords : Muestra sobre la tabla los registros encontrados.
'--------------------------------------------------------------------------------------------
Sub ShowRecords()
	'--------------------------------------------------------------------------------------------
	Dim lintIndex As Integer
	Dim lclsRoles As Object
	
	'+ Obtiene el nombre del control que llamó a la ventana.
	If Not Request.QueryString.Item("ControlName") = vbNullString Then
		lstrControlName = Request.QueryString.Item("ControlName")
	End If
	
	If Not Request.QueryString.Item("ControlClieName") = vbNullString Then
		lstrControlClieName = Request.QueryString.Item("ControlClieName")
	End If
	
	'+ Se agrega el elemento tanto en la forma como en memoria.    
	With Response
		'For lintIndex = 1 To mobjRoles.Count
        For lintIndex = 0 To mobjRoles.Count -1
			.Write("<TR>")
			.Write("<TD><LABEL>" & mobjValues.TextControl("tctClient", 14, mobjRoles.Item(lintIndex).sClient & "-" & mobjRoles.Item(lintIndex).sDigit,  ,  , True) & "</LABEL></TD>")
			.Write("<TD><LABEL>" & mobjValues.TextControl("tctClieName", CShort("40"), mobjRoles.Item(lintIndex).sCliename,  ,  , True,  , "RecordFound(" & lintIndex - 1 & ",'" & lstrControlName & "','" & lstrControlClieName & "')") & "</LABEL></TD>")
			.Write("</TR>")
			Response.Write("<SCRIPT>insAddRolesClient(""" & mobjRoles.Item(lintIndex).sClient & """,""" & mobjRoles.Item(lintIndex).sCliename & """,""" & mobjRoles.Item(lintIndex).sDigit & """)</" & "Script>")
		Next 
	End With
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjRoles = New ePolicy.Roleses

mobjValues.ActionQuery = Session("bQuery")
%>


<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></script>
<script LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></script>
<script>
//+ Control de la versión 
document.VssVersion="$$Revision: 3 $|$$Date: 15/10/03 18.50 $"

var marrRoles = []
var mintCount = -1

//%	insAddRolesClient: Carga el arreglo con la consulta obtenida.
//-------------------------------------------------------------------------------------------
function insAddRolesClient(sClient, sClieName, sDigit) {
//-------------------------------------------------------------------------------------------
    var lRolesClient = []
    lRolesClient[0] = sClient
    lRolesClient[1] = sClieName
    lRolesClient[2] = sDigit
    marrRoles[++mintCount] = lRolesClient
}
//%	RecordFound: Retorna el código del cliente seleccionado.
//-------------------------------------------------------------------------------------------
function RecordFound(Field,ControlName,ControlClieName) {
//------------------------------------------------------------------------------------------
    var lintIndex = Field
    opener.document.forms[0].elements[ControlName].value = marrRoles[lintIndex][0]
    opener.document.forms[0].elements["tctClient_Digit"].value = marrRoles[lintIndex][2]
    if(ControlClieName!=""){
        opener.document.getElementsByTagName("DIV")[ControlClieName].innerHTML = marrRoles[lintIndex][1]
    }
    window.close();
}
</script>
<html>
    <head>
        <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
        <%=mobjValues.StyleSheet()%>
        <title>Selección de un cliente asociado a la póliza</title>
    </head>
    <body>
        <%Response.Write(mobjValues.ShowWindowsName("CA003A"))%>
        <form METHOD="post" ID="FORM" NAME="frmClientPolicySel" ACTION="CA003A_old.aspx?ControlName=" & Request.QueryString("ControlName") & "&ControlClieName=" &amp; Request.QueryString(" ControlClieName")">
            <table WIDTH="100%" CLASS="GRDDATA">
                <th><label><%= GetLocalResourceObject("AnchorCaption") %></label></th>
                <th><label><%= GetLocalResourceObject("Anchor2Caption") %></label></th>
            <%
'+ Búsqueda de los clientes asociados a la póliza
If mobjRoles.Find_by_Policy(Session("sCertype"), mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("npolicy"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nCertif"), eFunctions.Values.eTypeData.etdDouble), vbNullString, mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
	'+ Verifica la cantidad de registros existentes.
	'+ Sólo se incluye el botón de aceptar si existen datos.
	If mobjRoles.Count > 0 Then ShowRecords()
Else
	With Response
		.Write("<TR>")
		.Write(mobjValues.DataNotFound(6))
		.Write("</TR>")
	End With
End If%>
            </table>
            </p>
            <table WIDTH="100%" BORDER="0">
                <tr>
                    <td COLSPAN="2" CLASS="HORLINE"></td>
                </tr>
                <tr>
                    <td WIDTH="5%"><%=mobjValues.ButtonAbout("CA003A")%></td>
                    <td ALIGN="RIGHT"><%Response.Write(mobjValues.ButtonAcceptCancel( ,  , True,  , eFunctions.Values.eButtonsToShow.OnlyCancel))%></td>
                </tr>
            </table>
<%
mobjValues = Nothing
mobjRoles = Nothing
%>
        </form>
    </body>
</html>





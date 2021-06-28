<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<%@ Import namespace="eRemoteDB" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim lobjPolicySeq As ePolicy.Groups
Dim lobjQuery As eRemoteDB.Query
Dim lobjClass As ePolicy.ValPolicySeq


</script>
<%Response.Expires = 0%>
<script LANGUAGE="JavaScript" SRC="../../Scripts/GenFunctions.js"></script>
<script>

//- Registro actual en el arreglo
	var mlngCurrentIndex

// ShowFields: Muestra los valores de la forma
//-------------------------------------------------------------------------------------------
function ShowFields(Index){
//-------------------------------------------------------------------------------------------
//	Lista de valores que vienen de la ventana madre...
    with(self.document.forms[0]){
		tcnGroup.value = opener.marrCA011[Index][11]
		tctDescript.value = opener.marrCA011[Index][7]
		tcnParticip.value = opener.marrCA011[Index][8]
		cbeGroupStat.value = opener.marrCA011[Index][9]
		nPriorGroupStat.value = opener.marrCA011[Index][9]
    }
    mlngCurrentIndex = Index
}

/* MoveRecord: Se posiciona en el valor del arreglo de la forma
 ------------------------------------------------------------------------------------------- */
function MoveRecord(Option){
/*------------------------------------------------------------------------------------------- */
	var lintIndex = mlngCurrentIndex
	switch (Option){
		case "Back":
			lintIndex--;
			break;
		case "Next":
			lintIndex++;
	}
		
	if (lintIndex >= 0)
		if (lintIndex < opener.marrCA011.length){
			ShowFields(lintIndex);
			mlngCurrentIndex = lintIndex
		}
}

/* ChangeSubmit: Cambia la accion de la forma
/-------------------------------------------------------------------------------------------*/
function ChangeSubmit(Option) {
/*-------------------------------------------------------------------------------------------*/	
	switch (Option) {
		case "Add":
			document.forms[0].action = "valPolicySeq.aspx?sCodispl=CA011&Action=Add&WindowType=PopUp"
			break;
		case "Update":
			document.forms[0].action = "valPolicySeq.aspx?sCodispl=CA011&Action=Update&WindowType=PopUp&ReloadIndex=" + mlngCurrentIndex
	}
}
	
/* LockControl: Habilita/Deshabilita los controles excluyentes de la página
/-------------------------------------------------------------------------------------------*/
function LockControl(){
/*-------------------------------------------------------------------------------------------*/
	document.frmCA011Upd.tcnGroup.disabled=true;
}
</script>

<%mobjValues = New eFunctions.Values
lobjPolicySeq = New ePolicy.Groups
%>

<html>
<head>
	<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
	<link REL="StyleSheet" TYPE="text/css" HREF="../../Common/Custom.css">
	<title>Información de Grupos Asociados a Pólizas [Detalle]</title>
</head>
<body ONUNLOAD="closeWindows();">
<form NAME="frmCA011Upd" METHOD="POST" ACTION="../../Policy/PolicySeq/CA011_Old.aspx">
<%
' + SI LA OPCIÓN QUE ENVÍA LA VENTANA ES PARA ELIMINAR (DELETE) SE MUESTRA EL MENSAJE
' + RESPECTIVO EN LA VENTANA Y LA IMAGEN DEL "CHECK"
If Request.QueryString.Item("Action") = "Delete" Then
	lobjQuery = New eRemoteDB.Query
	lobjClass = New ePolicy.ValPolicySeq
	Response.Write(mobjValues.ShowWindowsName("CA011"))
	'+ Muestra el mensaje de eliminación de registros									    
	Response.Write(mobjValues.ConfirmDelete(False))
	'+ Muestra la imagen para confirmar la eliminación de un registro
	Response.Write(mobjValues.ConfirmDelete(True))
	Response.Write("<SCRIPT>opener.DeleteRecord(" & Request.QueryString.Item("Index") & ")</SCRIPT>")
	'			With lobjQuery
	'				IF .OpenQuery ("Message", "sMessaged", "nErrornum = 3999") then
	'					Response.Write "<B><LABEL ID=40764>"  .FieldToClass("sMessaged")  "</LABEL></B>"
	'					.CloseQuery
	'				End If
	'			End With
	%>
			<!--BR>			<TABLE WIDTH="100%">				<TR ALIGN="Right">					<TD><IMG BORDER="0" SRC="images/Checkmrk.gif" ONCLICK="opener.document.location.reload();self.close();" WIDTH="29" HEIGHT="28"></TD>				</TR>			</TABLE-->
<%	
	With Request
		lobjClass.insPostCA011(.QueryString("Action"), Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), Session("dEffecdate"), .QueryString("nGroup"), .Form.Item("tctDescript"), .Form.Item("tcnParticip"), .Form.Item("cbeGroupStat"), Session("nUserCode"), 0)
		
		Response.Write("<SCRIPT>opener.DeleteRecord(" & .QueryString.Item("Index") & ")</SCRIPT>")
	End With
Else
	' + SI NO, MUESTRA LOS CAMPOS DE LA VENTANA Y LOS BOTONES PARA AÑADIR - ACTUALIZAR - CANCELAR LA OPERACIÓN		
	Response.Write(mobjValues.ShowWindowsName("CA011"))
	%>
			<table WIDTH="100%">
				<tr>
					<td><label ID="40765"><%= GetLocalResourceObject("tcnGroupCaption") %></label></td>
<%	
	If Request.QueryString.Item("Action") = "Add" Then
%>					
						<td><% Response.Write(mobjValues.NumericControl("tcnGroup", 5, Request.QueryString.Item("nPriorGroup"), False, GetLocalResourceObject("tcnGroupToolTip"), False))%></td>
<%		
	Else
%>			        
						<td><% Response.Write(mobjValues.NumericControl("tcnGroup", 5, Request.QueryString.Item("nPriorGroup"), False, GetLocalResourceObject("tcnGroupToolTip"), False))%></td>
<%		
	End If
%>
				</tr>
				<tr>
					<td><label ID="40766"><%= GetLocalResourceObject("tctDescriptCaption") %></label></td>
			        <td><%=mobjValues.TextControl("tctDescript", 30, "", False, GetLocalResourceObject("tctDescriptToolTip"), False)%></td>
				</tr>
				<tr>
					<td><label ID="40767"><%= GetLocalResourceObject("tcnParticipCaption") %></label></td>
			        <td><%=mobjValues.NumericControl("tcnParticip", 4, "", True, GetLocalResourceObject("tcnParticipToolTip"), False, 2)%></td>
				</tr>
				<tr>
					<td><label ID="40768"><%= GetLocalResourceObject("cbeGroupStatCaption") %></label></td>
					<td><%=mobjValues.PossiblesValues("cbeGroupStat", "Table26", 1,  ,  ,  ,  ,  ,  ,  ,  ,  , GetLocalResourceObject("cbeGroupStatToolTip"))%></td>
					<td><%=mobjValues.HiddenControl("nPriorGroupStat", "")%></td>
				</tr>
			</table>
	
			<table WIDTH="100%">
				<tr>
<%	
	If Request.QueryString.Item("Action") = "Update" Then
		With Response
			.Write("<SCRIPT>ShowFields(" & Request.QueryString.Item("Index") & ");")
			' + SI LA OPCIÓN ES "UPDATE" (MODIFICACIÓN O ACTUALIZACIÓN DE REGISTROS), ENTONCES SE
			' + MOSTRARÁN 2 BOTONES CON LOS CUALES SE PODRÁ NAVEGAR ENTRE LOS REGISTROS DE LA TABLA
			.Write("ChangeSubmit(""Update"");")
			.Write("LockControl();</SCRIPT>")
			.Write("<TR>")
			.Write(mobjValues.ButtonBackNext(2))
			.Write("</TR>")
		End With
	End If
	%>			
					<td COLSPAN="3"><hr></td>
				</tr>
				<tr>
					<td><%=mobjValues.CheckControl("chkContinue", GetLocalResourceObject("chkContinueCaption"), "1")%></td>
					<td ALIGN="Right">
						<%=mobjValues.ButtonAcceptCancel("EnabledControl();", "window.close();", True)%>
					</td>
				</tr>
			</table> 
<%	
	If Request.QueryString.Item("Action") = "Add" Then
		Response.Write("<SCRIPT>ChangeSubmit(""Add"");</SCRIPT>")
	End If
End If
'+ UNA VEZ CULMINADA LA FUNCIÓN O EL MÉTODO, SE DEBEN DESTRUIR LAS INSTANCIAS CREADAS 
'+ DE LOS OBJETOS QUE SE ENCUENTRAN EN EL SERVIDOR, PARA ASÍ LIBERAR LA MEMORIA
mobjValues = Nothing
lobjPolicySeq = Nothing
lobjClass = Nothing
lobjQuery = Nothing
%>
</form>
</body>
</html>






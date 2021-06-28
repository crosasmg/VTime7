<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="ePolicy" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.04
Dim mobjNetFrameWork As eNetFrameWork.Layout

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'+ Variables públicas que reciben valor por referencia.
Dim lstrClient As String
Dim lstrCliename As String
Dim lobjClass As ePolicy.Beneficiar


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
mobjNetFrameWork.nUsercode = Session("nUsercode")
Call mobjNetFrameWork.BeginPage("CA023upd")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
%>

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>    
<SCRIPT LANGUAGE="JavaScript">

//- Registro actual en el arreglo
	var mlngCurrentIndex
	
// ShowFields: Muestra los valores de la forma
//-------------------------------------------------------------------------------------------
function ShowFields(Index){
//-------------------------------------------------------------------------------------------
//	Lista de valores que vienen de la ventana madre...	
    self.document.forms[0].elements["tctClient"].value		= opener.marrCA023[Index][5]    
    UpdateDiv("tctCliename",opener.marrCA023[Index][6],'Normal')
    self.document.forms[0].elements["tcnParticip"].value	= opener.marrCA023[Index][8]
    self.document.forms[0].elements["cbeRelation"].value	= opener.marrCA023[Index][9]
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
		if (lintIndex < opener.marrCA023.length){
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
			document.forms[0].action = "valPolicySeq.aspx?sCodispl=CA023&Action=Add&WindowType=PopUp"
			break;
		case "Update":
			document.forms[0].action = "valPolicySeq.aspx?sCodispl=CA023&Action=Update&WindowType=PopUp&ReloadIndex=" + mlngCurrentIndex
	}
}

/* LockControl: Habilita/Deshabilita los controles excluyentes de la página
/-------------------------------------------------------------------------------------------*/
function LockControl(){
/*-------------------------------------------------------------------------------------------*/
	document.frmCA023upd.tctClient.disabled=true;
}

</SCRIPT>

<%mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.04
mobjValues.sSessionID = Session.SessionID
mobjValues.nUsercode = Session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = "CA023upd"
%>

</SCRIPT>
<HTML>
<HEAD>


    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
    <LINK REL="StyleSheet" TYPE="text/css" HREF="/VTimeNet/Common/Custom.css">    
	<TITLE>Beneficiarios identificados por código [Detalle]</TITLE>
	
</HEAD>
<BODY ONUNLOAD="closeWindows();">
<FORM NAME="frmCA023upd" METHOD="POST" ACTION="CA023.aspx">


<%
lstrClient = ""
lstrCliename = ""

' + SI LA OPCIÓN QUE ENVÍA LA VENTANA ES PARA ELIMINAR (DELETE) SE MUESTRA EL MENSAJE
' + RESPECTIVO EN LA VENTANA Y LA IMAGEN DEL "CHECK"
Response.Write(mobjValues.ShowWindowsName("CA023", Request.QueryString.Item("sWindowDescript")))
If Request.QueryString.Item("Action") = "Delete" Then
	lobjClass = New ePolicy.Beneficiar
	
	'		Set lobjQuery = Server.CreateObject("eRemoteDB.Query")
	'		With lobjQuery
	'			IF .OpenQuery ("Message", "sMessaged", "nErrornum = 3999") then
	'				Response.Write "<B><LABEL ID=40944>"  .FieldToClass("sMessaged")  "</LABEL></B>"
	'				.CloseQuery
	'			End If
	'		End With   
	
	With Request
            'lobjClass.insValCA023("CA023", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), .QueryString("sClient"), .QueryString("nParticip"), .QueryString("nRelation"), mobjValues.StringToDate(Session("dEffecdate")), .QueryString("Action"))
		
		'lobjClass.insPostCA023(.QueryString("Action"), "CA023", Session("sCertype"), Session("nBranch"), Session("nProduct"), Session("nPolicy"), Session("nCertif"), .QueryString("sClient"), "", "", mobjValues.StringToDate(Session("dEffecdate")))
            '
		'+ Muestra el mensaje de eliminación de registros									    
		Response.Write(mobjValues.ConfirmDelete(False))
		'+ Muestra la imagen para confirmar la eliminación de un registro
		Response.Write(mobjValues.ConfirmDelete(True))
		Response.Write("<SCRIPT>opener.DeleteRecord(" & .QueryString.Item("Index") & ")</SCRIPT>")
		Response.Write("<SCRIPT>opener.top.frames['fraSequence'].document.location='/VTimeNet/Policy/PolicySeq/Sequence.aspx?nAction=" & Request.QueryString.Item("nMainAction") & "&sGoToNext=No&nOpener=" & Request.QueryString.Item("sCodispl") & "';</SCRIPT>")
	End With
Else
	%>		
			<TABLE WIDTH="100%">
				<TR>
					<TD><LABEL ID=40945><%= GetLocalResourceObject("tctClientCaption") %></LABEL></TD>			        
			        <TD><%=mobjValues.ClientControl("tctClient", lstrClient,  , "",  ,  , "tctCliename", True)%></TD>
				</TR>
				<TR>
					<TD><LABEL ID=40946><%= GetLocalResourceObject("AnchorCaption") %></LABEL></TD>							
			        <TD><DIV ID=tctCliename CLASS=Field>  </DIV></TD>
				</TR>
				<TR>
					<TD><LABEL ID=40947><%= GetLocalResourceObject("tcnParticipCaption") %></LABEL></TD>
			        <TD><%=mobjValues.NumericControl("tcnParticip", 5, "0", False, "", False, 2)%></TD>
				</TR>
				<TR>
					<TD><LABEL ID=40948><%= GetLocalResourceObject("cbeRelationCaption") %></LABEL></TD>
					<TD><%=mobjValues.PossiblesValues("cbeRelation", "Table55", 1)%></TD>
				</TR>
			</TABLE>
	
			<TABLE WIDTH="100%">
				<TR>

<%	
	If Request.QueryString.Item("Action") = "Add" Then
		Response.Write("<SCRIPT>ChangeSubmit(""Add"");</SCRIPT>")
	End If
	
	If Request.QueryString.Item("Action") = "Update" Then
		With Response
			.Write("<SCRIPT>ShowFields(" & Request.QueryString.Item("Index") & ");")
			' + SI LA OPCIÓN ES "UPDATE" (MODIFICACIÓN O ACTUALIZACIÓN DE REGISTROS), ENTONCES SE
			' + MOSTRARÁN 2 BOTONES CON LOS CUALES SE PODRÁ NAVEGAR ENTRE LOS REGISTROS DE LA TABLA
			.Write("ChangeSubmit(""Update"");")
			.Write("LockControl();</SCRIPT>")
			.Write("<TR>")
			.Write(mobjValues.ButtonBackNext(3))
			.Write("</TR>")
		End With
	End If
	%>
		<TD COLSPAN="3"><HR></TD>
				</TR>
				<TR>
					<TD><%=mobjValues.CheckControl("chkContinue", GetLocalResourceObject("chkContinueCaption"), "1")%></TD>
					<TD ALIGN="Right" COLSPAN="2">					
						<%=mobjValues.ButtonAcceptCancel("EnabledControl();", "window.close();", True)%></TD>
				</TR>
			</TABLE> 
			
<%	
	If Request.QueryString.Item("Action") = "Add" Then
		Response.Write("<SCRIPT>ChangeSubmit(""Add"");</SCRIPT>")
	End If
End If

mobjValues = Nothing
lobjClass = Nothing
%>

</FORM>
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.04
Call mobjNetFrameWork.FinishPage("CA023upd")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





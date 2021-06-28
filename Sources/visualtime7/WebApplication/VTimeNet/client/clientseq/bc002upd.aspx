<%@ Page Language="VB" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de los datos a mostrar en la forma
Dim lobjClientSeq As eClient.ClientSeq


'% insLoadValues: Carga los valores para la inserción de los registros.
'--------------------------------------------------------------------------------------------
Private Sub insLoadValues()
	'--------------------------------------------------------------------------------------------
	'+ Mantiene temporalmente el valor de mobjValues.ActionQuery
	Dim lblnActionQuery As Boolean
	
	
Response.Write("" & vbCrLf)
Response.Write("    <TABLE COLS=2 WIDTH=100% CELLSPACING=1 CELLPADDING=1>" & vbCrLf)
Response.Write("        <TR>" & vbCrLf)
Response.Write("            <TD><LABEL ID=40382>" & GetLocalResourceObject("tctClientCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("	        <TD>")

	With Response
		.Write(mobjValues.ClientControl("tctClient", ""))
		.Write("</TD>")
	End With
	
Response.Write("" & vbCrLf)
Response.Write("        </TR>" & vbCrLf)
Response.Write("    	<TR>" & vbCrLf)
Response.Write("    	    <TD><LABEL ID=40383>" & GetLocalResourceObject("cbeRelationCaption") & "</LABEL></TD>" & vbCrLf)
Response.Write("    	    <TD>")


Response.Write(mobjValues.PossiblesValues("cbeRelation", "Table15", 1))


Response.Write("</TD>" & vbCrLf)
Response.Write("    	    <TD>")


Response.Write(mobjValues.HiddenControl("nOriginalRelaship", ""))


Response.Write("</TD>" & vbCrLf)
Response.Write("    	</TR>" & vbCrLf)
Response.Write("    </TABLE>" & vbCrLf)
Response.Write("    <TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("		")

	If Request.QueryString.Item("Action") = "Update" Then
		Response.Write(mobjValues.ButtonBackNext(2))
	End If
Response.Write("" & vbCrLf)
Response.Write("    	<TR>" & vbCrLf)
Response.Write("    	    <TD COLSPAN=""3""><hr></TD>" & vbCrLf)
Response.Write("    	</TR>" & vbCrLf)
Response.Write("    	<TR>" & vbCrLf)
Response.Write("    		<TD>")


Response.Write(mobjValues.CheckControl("chkContinue", GetLocalResourceObject("chkContinueCaption"), "1"))


Response.Write("</TD>" & vbCrLf)
Response.Write("    		<TD ALIGN=""Right"">")

	
	With mobjValues
		If Not .ActionQuery Then
			Response.Write(.ButtonAcceptCancel("EnabledControl()",  , True))
		Else
			lblnActionQuery = .ActionQuery
			.ActionQuery = False
			Response.Write(.ButtonAcceptCancel("EnabledControl()",  ,  ,  , eFunctions.Values.eButtonsToShow.OnlyCancel))
			.ActionQuery = lblnActionQuery
		End If
	End With
Response.Write("</TD>" & vbCrLf)
Response.Write("    	</TR>" & vbCrLf)
Response.Write("    </TABLE> ")

	
End Sub

</script>
<%Response.Expires = 0%>

<SCRIPT LANGUAGE=javascript>
//- Registro actual en el arreglo
	var CurrentIndex
// ChangeSubmit: Cambia la accion de la forma
//-------------------------------------------------------------------------------------------
function ChangeSubmit(Option) {
//-------------------------------------------------------------------------------------------	
		switch (Option) {
			case "Add":
				document.forms[0].action = "valClientSeq.aspx?nZone=2&sCodispl=BC002&Action=Add&WindowType=PopUp&nMainAction=<%=Request.QueryString.Item("nMainAction")%>" 
				break;
			case "Update":
				document.forms[0].action = "valClientSeq.aspx?nZone=2&sCodispl=BC002&Action=Update&Index=<%=Request.QueryString.Item("Index")%>&WindowType=PopUp&nMainAction=<%=Request.QueryString.Item("nMainAction")%>" 
		}
	}

//% ShowFields: Muestra los valores de la forma
//-------------------------------------------------------------------------------------------
function ShowFields(Index){
//-------------------------------------------------------------------------------------------
    //Index--
	with (self.document.forms[0]){
		tctClient.value = opener.marrBC002[Index][1]
		tctClient.disabled = true;
		cbeRelation.value = opener.marrBC002[Index][3]
		nOriginalRelaship.value = opener.marrBC002[Index][3]
    }
}	

// MoveRecord: Se posiciona en el valor del arreglo de la forma
//-------------------------------------------------------------------------------------------
function MoveRecord(Option){
//-------------------------------------------------------------------------------------------
	var lintIndex = CurrentIndex
	switch (Option){
		case "Back":
			lintIndex--;
			break;
		case "Next":
			lintIndex++;
	}
		
	if (lintIndex >= 0)
		if (lintIndex < opener.marrBC002.length){
			ShowFields(lintIndex);
			CurrentIndex = lintIndex
		}
}

</SCRIPT>


<%mobjValues = New eFunctions.Values
lobjClientSeq = New eClient.ClientSeq

%>
<HTML>
    <HEAD>
        <META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
        <%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
        <TITLE>Nexos del cliente.</TITLE>
    </HEAD>
<BODY ONUNLOAD="closeWindows();">
    <FORM NAME="frmBC002Upd" METHOD="POST" ACTION="BC002.ASPX?TIMEINFO=1&sCodispl=BC002">
    <%
Response.Write("<SCRIPT>CurrentIndex=0" & Request.QueryString.Item("Index") & "</SCRIPT>")
If Request.QueryString.Item("Action") = "Delete" Then
	Response.Write(mobjValues.ConfirmDelete)
	With Request
		lobjClientSeq.insPostBC002(.QueryString.Item("Action"), Session("sClient"), .QueryString.Item("sClientr"), .QueryString.Item("sClieName"), .QueryString.Item("nRelaship"), .QueryString.Item("nUserCode"), .QueryString.Item("nRelaship"))
		Response.Write("<SCRIPT>opener.DeleteRecord(" & .QueryString.Item("Index") & ")</SCRIPT>")
	End With
	Response.Write(mobjValues.ConfirmDelete(True))
Else
	Call insLoadValues()
End If

mobjValues = Nothing
lobjClientSeq = Nothing
%>
</BODY>
</HTML>
<%Select Case Request.QueryString.Item("Action")
	Case "Add"
		Response.Write("<SCRIPT>ChangeSubmit(""Add"");</SCRIPT>")
	Case "Update"
		With Response
			.Write("<SCRIPT>ShowFields(" & CDbl(Request.QueryString.Item("Index")) - 1 & ");</SCRIPT>")
			.Write("<SCRIPT>ChangeSubmit(""Update"");</SCRIPT>")
		End With
End Select
%>









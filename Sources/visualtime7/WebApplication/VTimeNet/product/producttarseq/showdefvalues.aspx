<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eTarif" %>
<script language="VB" runat="Server">
Dim InsRefresSequence() As Object

Dim mobjValues As eFunctions.Values
Dim mblnRefresh As Boolean


'% InsValDelete: Verifica si un campo puede ser Borrado
'--------------------------------------------------------------------------------------------
Sub InsValDelete()
	'--------------------------------------------------------------------------------------------
	Dim lclsTarif_tab_col As eTarif.Tarif_tab_col
	lclsTarif_tab_col = New eTarif.Tarif_tab_col
	
	If lclsTarif_tab_col.insExistsTarifValue(mobjValues.StringToType(Request.QueryString.Item("nId_table"), eFunctions.Values.eTypeData.etdLong)) Then
		With Response
			'            .Write "(typeof(top.frames[""fraFolder""].document.forms[0].Sel[" & Request.QueryString("nIndex") &  "])!='undefined')?"
			.Write("top.frames['fraFolder'].document.forms[0].Sel[" & Request.QueryString.Item("nIndex") & "].checked=false;")
			.Write("top.frames['fraFolder'].document.forms[0].Sel.checked=false;")
			.Write("alert(""Err 55893: " & eFunctions.Values.GetMessage(80000) & """);")
		End With
	End If
	
	lclsTarif_tab_col = Nothing
End Sub

</script>
<%Response.Expires = -1
Response.CacheControl = "private"
mblnRefresh = False
mobjValues = New eFunctions.Values

%>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>






	<%=mobjValues.StyleSheet()%>
</HEAD>
<BODY>
<FORM NAME="ShowValues">
</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	
	Case "InsValDelete"
		Call InsValDelete()
		
End Select

Response.Write("setPointer('');")
Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")
mobjValues = Nothing

'+Se valida si se refresca la secuencia
If mblnRefresh Then
End If
%>






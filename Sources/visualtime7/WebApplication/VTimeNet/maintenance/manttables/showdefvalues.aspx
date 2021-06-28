<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'% ValTables: Valida que el registro que se va a borrar no contenga registros asociados
'% en la tabla Client
'--------------------------------------------------------------------------------------------
Sub ValTables()
	'--------------------------------------------------------------------------------------------
	Dim mclsTabGen As eGeneralForm.TabGen
	mclsTabGen = New eGeneralForm.TabGen
	
	If mclsTabGen.InsValTables(Request.QueryString.Item("sTable"), CInt(Request.QueryString.Item("nCodigint"))) Then
		Select Case Request.QueryString.Item("sTable")
			Case "table215"
				Response.Write("alert (""" & "Error: no se puede eliminar, Ordenes de servicio asociadas" & """);")
				Response.Write(" top.frames['fraHeader'].document.location.href='MA1000_K.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=304';")
			Case Else
				Response.Write("alert (""" & "Error: no se puede eliminar, clientes asociados" & """);")
				Response.Write(" top.frames['fraHeader'].document.location.href='MA1000_K.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=304';")
		End Select
	End If
	
	mclsTabGen = Nothing
End Sub
'% ValTablesXXX: Valida que el registro que se va a borrar no contenga registros asociados
'% en la tablaXXX
'--------------------------------------------------------------------------------------------
Sub ValTablesXXX()
	'--------------------------------------------------------------------------------------------
	Dim mclsTabGen As eGeneralForm.TabGen
	mclsTabGen = New eGeneralForm.TabGen
	
	If mclsTabGen.DelValTables(Request.QueryString.Item("sTable"), CInt(Request.QueryString.Item("nCodigint"))) Then
		
		Response.Write("top.frames['fraHeader'].document.forms[0].Sel[" & Request.QueryString.Item("nCount") & "].checked=false;")
		Response.Write("alert (""" & "Error: No se puede eliminar un registro que tiene información relacionada" & """);")
		Response.Write(" top.frames['fraHeader'].document.location.href='MA1000_K.aspx?sCodispl=" & Request.QueryString.Item("sCodispl") & "&nMainAction=304';")
	Else
		Response.Write(" top.frames['fraHeader'].document.forms[0].cmdDelete.disabled = false; ")
	End If
	
	mclsTabGen = Nothing
End Sub

</script>

<%Response.Expires = -1
mobjValues = New eFunctions.Values

%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" CONTENT = "Microsoft Visual Studio 6.0">


</HEAD>
<BODY>
	<FORM NAME="ShowDefValues">
	</FORM>
</BODY>
<BODY>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "ValTables"
		Call ValTables()
	Case "ValTablesXXX"
		Call ValTablesXXX()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>





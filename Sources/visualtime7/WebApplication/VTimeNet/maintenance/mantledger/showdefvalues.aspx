<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLedge" %>
<%@ Import namespace="eGeneral" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'%FindAccount: Valida si la cuenta tiene asientos contables.
'--------------------------------------------------------------------------------------------
Sub FindAccount(ByVal nLed_Compan As Object, ByVal sAccount As String, ByVal nIndex As String)
	'--------------------------------------------------------------------------------------------
	Dim lclsDet_Lines As eLedge.Det_lines
	Dim lclsGeneral As eGeneral.GeneralFunction
	Dim lstrError As String=String.Empty
	
	lclsDet_Lines = New eLedge.Det_lines
	If lclsDet_Lines.FindAccount(nLed_Compan, sAccount, True) Then
		lclsGeneral = New eGeneral.GeneralFunction
		lstrError = lclsGeneral.insLoadMessage(60827)
		Response.Write("alert('Err.60827-" & lstrError & "');")
		
		'+ Este manejo se comento ya que se pueden eliminar las guias que no hayan sido 
		'+ contabilizadas de manera definitiva		
		'		Response.Write "if(top.fraFolder.document.forms[0].hddCount.value>1){"
		'		Response.Write "   top.fraFolder.document.forms[0].Sel[" & nIndex & "].checked=false;"
		'		Response.Write "   top.fraFolder.marrArray[" & nIndex & "].Sel=false;"			
		'		Response.Write "}else{"
		'		Response.Write "   top.fraFolder.document.forms[0].Sel.checked=false;"
		'		Response.Write "   top.fraFolder.marrArray[0].Sel=false;"			
		'		Response.Write "}"
		
	End If
	lclsDet_Lines = Nothing
	lclsGeneral = Nothing
End Sub

</script>
<%
Response.Expires = -1
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


</HEAD>
<BODY>
	<FORM NAME="ShowDefValues">
	</FORM>

</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "ValidateAccount"
		Call FindAccount(Request.QueryString.Item("nLed_Compan"), Request.QueryString.Item("sAccount"), Request.QueryString.Item("Index"))
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>





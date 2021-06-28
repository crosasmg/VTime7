<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eClient" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'% insShowDate: se muestra la fecha de ingreso del cliente
'--------------------------------------------------------------------------------------------
Sub insShowDate()
	'--------------------------------------------------------------------------------------------
	Dim lclsClient As eClient.Client
	Dim lstrClient As String
	
	lclsClient = New eClient.Client
	
	lstrClient = lclsClient.ExpandCode(Request.QueryString.Item("sClient"))
	
	If lclsClient.Find(lstrClient) Then
		Response.Write("top.frames[""fraFolder""].document.forms[0].tcdInputDate.value='" & lclsClient.dInpdate & "';")
	End If
	
	lclsClient = Nothing
End Sub

'% insValDelMS109: Se valida al tratar de eliminar en la transacción MS109
'--------------------------------------------------------------------------------------------
Sub insValDelMS109()
	'--------------------------------------------------------------------------------------------
	Dim lobjError As eGeneral.GeneralFunction
	Dim lclsProvince As eGeneralForm.Province
	
	lobjError = New eGeneral.GeneralFunction
	lclsProvince = New eGeneralForm.Province
	With Response
		If lclsProvince.reaProvince_inTablocat(CInt(Request.QueryString.Item("nProvince"))) Then
			.Write("alert(""Err. 10841: " & lobjError.insLoadMessage(10841) & """);")
			.Write("top.frames[""fraHeader""].document.forms[0].Sel[" & Request.QueryString.Item("nindex") & "].checked=false;")
		Else
			If lclsProvince.reaProvince_inAddress(CInt(Request.QueryString.Item("nProvince"))) Then
				.Write("alert(""Err. 10863: " & lobjError.insLoadMessage(10863) & """);")
				.Write("top.frames[""fraHeader""].document.forms[0].Sel[" & Request.QueryString.Item("nindex") & "].checked=false;")
			End If
		End If
	End With
	
	lobjError = Nothing
	lclsProvince = Nothing
End Sub

'% insShowDate: se muestra la fecha de ingreso del cliente
'--------------------------------------------------------------------------------------------
Sub insShowCompany()
	'--------------------------------------------------------------------------------------------
	
	Dim lclsCompany As eGeneral.Company
	Dim lstrCompany As Object
	
	lclsCompany = New eGeneral.Company
	
	If lclsCompany.Find(CInt(Request.QueryString.Item("nCompany"))) Then
		
		'	Response.Write "alert('"&lclsCompany.sType&"');"
		
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnCompanyType.value='" & lclsCompany.sType & "';")
		Response.Write("top.frames['fraFolder'].document.forms[0].tcnClasific.value=" & lclsCompany.nClassific & ";")
		
	End If
	
	lclsCompany = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "Date"
		Call insShowDate()
	Case "Delete_MS109"
		Call insValDelMS109()
	Case "Company"
		Call insShowCompany()
End Select


Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>





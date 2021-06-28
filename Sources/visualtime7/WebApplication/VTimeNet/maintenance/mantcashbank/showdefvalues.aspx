<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eSecurity" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'% ReaOffice: Obtiene la sucursal asociada al usuario
'--------------------------------------------------------------------------------------------
Sub ReaOffice()
	'--------------------------------------------------------------------------------------------
	Dim lclsUsers As eSecurity.User
	
	lclsUsers = New eSecurity.User
	
	If lclsUsers.Find(mobjValues.StringToType(Request.QueryString.Item("nUser"), eFunctions.Values.eTypeData.etdDouble, True)) Then
		
		With Response
			.Write("with(opener.document.forms[0]){")
			.Write("cbeOffice.value=" & lclsUsers.nOffice & ";")
			
			If lclsUsers.nOfficeagen > 0 Then
				.Write("ValOfficeAgen.value=" & lclsUsers.nOfficeagen & ";")
			End If
			.Write("}")
		End With
	End If
	
	lclsUsers = Nothing
End Sub

'% ReaOffice2: Obtiene la sucursal asociada al usuario
'--------------------------------------------------------------------------------------------
Sub ReaOffice2()
	'--------------------------------------------------------------------------------------------
	Dim lclsUsers As eSecurity.User
	
	lclsUsers = New eSecurity.User
	
	If lclsUsers.Find(mobjValues.StringToType(Request.QueryString.Item("nUser"), eFunctions.Values.eTypeData.etdDouble, True)) Then
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeOffice.value='" & lclsUsers.nOffice & "';")
	End If
	lclsUsers = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
    <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


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
Select Case Request.QueryString.Item("sField")
	Case "MOP634"
		Call ReaOffice()
	Case "MOP633"
		Call ReaOffice2()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing

%>





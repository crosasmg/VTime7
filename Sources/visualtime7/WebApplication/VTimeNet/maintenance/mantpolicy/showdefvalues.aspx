<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eBatch" %>
<%@ Import namespace="eBranches" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'% insShowDataMCA006: Muestra los datos de la transacción de Carga de póliza.
'--------------------------------------------------------------------------------------------
Private Sub insShowDataMCA006()
	'--------------------------------------------------------------------------------------------
	Dim lobjValues As eFunctions.Values
	Dim lclsDocument As eBatch.Group_columns
	Dim lstrTable As String
	Dim lblnOk As Boolean
	
	lobjValues = New eFunctions.Values
	
	lblnOk = False
	Select Case Request.QueryString.Item("sField")
		Case "getData"
			lclsDocument = New eBatch.Group_columns
			
			lstrTable = lclsDocument.getGroupSheet_sTable(lobjValues.StringToType(Request.QueryString.Item("nSheet"), eFunctions.Values.eTypeData.etdDouble))
			If lstrTable <> vbNullString Then
				
				If lstrTable <> "*" Then
					If lclsDocument.find(lobjValues.StringToType(Request.QueryString.Item("nSheet"), eFunctions.Values.eTypeData.etdDouble), "") Then
						lblnOk = True
					End If
				End If
			End If
			'+ Si no se consiguió información se blanquea campo Ramo
			If lblnOk Then
				Response.Write("top.fraHeader.document.forms[0].cbeBranch.value = '" & lclsDocument.nBranch & "';")
				If lobjValues.StringToType(CStr(lclsDocument.nBranch), eFunctions.Values.eTypeData.etdDouble, True) > 0 Then
					Response.Write("top.fraHeader.document.forms[0].optInf[0].checked=true;")
				Else
					Response.Write("top.fraHeader.document.forms[0].optInf[1].checked=true;")
				End If
			Else
				Response.Write("top.fraHeader.document.forms[0].optInf[1].checked=true;")
				Response.Write("top.fraHeader.document.forms[0].cbeBranch.value = '';")
			End If
	End Select
	
	lobjValues = Nothing
	lclsDocument = Nothing
End Sub

'% insShowDataMCA580: Muestra los datos de la transacción de ramos válidos descuento por volumen.
'--------------------------------------------------------------------------------------------
Private Sub insShowDataMCA580()
	'--------------------------------------------------------------------------------------------
	Dim lobjValues As eFunctions.Values
	Dim lclsDocument As eBranches.Tab_branch_quant
	Dim ldtmDate As Object
	Dim lblnOk As Boolean
	
	lobjValues = New eFunctions.Values
	
	lblnOk = False
	Select Case Request.QueryString.Item("sField")
		Case "getDate"
			lclsDocument = New eBranches.Tab_branch_quant
			
			ldtmDate = lclsDocument.Find_Date_Greater()
			If ldtmDate <> vbNullString Then
				Response.Write("top.fraHeader.document.forms[0].tcdEffecdate.value = '" & lobjValues.TypeToString(ldtmDate, eFunctions.Values.eTypeData.etdDate) & "';")
			End If
	End Select
	
	lobjValues = Nothing
	lclsDocument = Nothing
	
End Sub

'% insShowDataMCA581: Muestra los datos de la transacción de descuento por volumen.
'--------------------------------------------------------------------------------------------
Private Sub insShowDataMCA581()
	'--------------------------------------------------------------------------------------------
	Dim lobjValues As eFunctions.Values
	Dim lclsDocument As eBranches.Disc_quantity
	Dim ldtmDate As Object
	Dim lblnOk As Boolean
	
	lobjValues = New eFunctions.Values
	
	lblnOk = False
	Select Case Request.QueryString.Item("sField")
		Case "getDate"
			lclsDocument = New eBranches.Disc_quantity
			
			ldtmDate = lclsDocument.Find_Date_Greater()
			If ldtmDate <> vbNullString Then
				Response.Write("top.fraHeader.document.forms[0].tcdEffecdate.value = '" & lobjValues.TypeToString(ldtmDate, eFunctions.Values.eTypeData.etdDate) & "';")
			End If
	End Select
	
	lobjValues = Nothing
	lclsDocument = Nothing
	
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values

%>
<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>




<SCRIPT>
//+ Variable para el control de versiones 
    document.VssVersion="$$Revision: 2 $|$$Date: 15/10/03 16:15 $|$$Author: Nvaplat61 $"
</SCRIPT>  
</HEAD>
<BODY>
<FORM NAME="ShowValues">
</FORM>
</BODY>
</HTML>

<%Response.Write("<SCRIPT>")

Select Case Request.QueryString.Item("Field")
	Case "ShowDataMCA006"
		Call insShowDataMCA006()
	Case "ShowDataMCA580"
		Call insShowDataMCA580()
	Case "ShowDataMCA581"
		Call insShowDataMCA581()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>





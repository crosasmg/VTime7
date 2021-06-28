<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eTarif" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'% insShowDataMCA581: Muestra los datos de la transacción de descuento por volumen.
'--------------------------------------------------------------------------------------------
Private Sub insShowDataMDP8003()
	'--------------------------------------------------------------------------------------------
	Dim lobjValues As eFunctions.Values
	Dim lobjtarif As eTarif.tarif_column
	Dim lclsDocument As Object
	Dim ldtmDate As Object
	Dim lblnOk As Object
	
	If Request.QueryString.Item("Action") <> "Update" Then
		lobjtarif = New eTarif.tarif_column
		lobjValues = New eFunctions.Values
		If Request.QueryString.Item("tctTable") <> vbNullString And Request.QueryString.Item("tctColumn") <> vbNullString Then
			If lobjtarif.Find_columns(Request.QueryString.Item("tctTable"), Request.QueryString.Item("tctColumn")) Then
				Response.Write("top.frames[""fraFolder""].document.forms[0].tctdata_type.value = '" & lobjtarif.sData_type & "';")
				Response.Write("top.frames[""fraFolder""].document.forms[0].tctsize.value = '" & lobjValues.TypeToString(lobjtarif.nSize, eFunctions.Values.eTypeData.etdLong) & "';")
				Response.Write("top.frames[""fraFolder""].document.forms[0].tctdecimal.value = '" & lobjValues.TypeToString(lobjtarif.nDecimal, eFunctions.Values.eTypeData.etdLong) & "';")
				Response.Write("top.frames[""fraFolder""].document.forms[0].hdddata_type.value = '" & lobjValues.TypeToString(lobjtarif.nData_type, eFunctions.Values.eTypeData.etdLong) & "';")
				Response.Write("top.frames[""fraFolder""].document.forms[0].tctTablefk.value = '" & lobjtarif.sTablefk & "';")
			End If
		End If
		lobjtarif = Nothing
		lobjValues = Nothing
	End If
	
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
	Case "ShowDataMDP8003"
		Call insShowDataMDP8003()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>





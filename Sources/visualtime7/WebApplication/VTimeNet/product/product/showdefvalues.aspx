<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneral" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

Dim mclsValues As eFunctions.Values


'% insCancel: acción Cancelar de la secuencia
'%			  Se utiliza cuando se presiona el botón cancelar de la ventana principal (DP003_K)
'--------------------------------------------------------------------------------------------
Sub insDeleteDP001()
	'--------------------------------------------------------------------------------------------
	Dim lclsBranches As eProduct.Branches
	Dim lclsGeneral As eGeneral.GeneralFunction
	Dim lstrMessage As String
	
	lclsGeneral = New eGeneral.GeneralFunction
	lclsBranches = New eProduct.Branches
	
	If lclsBranches.valExistAssoPolicy(CInt(Request.QueryString.Item("nBranch"))) Then
		lstrMessage = lclsGeneral.insLoadMessage(11213)
		With Response
			.Write("(typeof(top.frames[""fraHeader""].document.forms[0].Sel[" & Request.QueryString.Item("nIndex") & "])!='undefined')?")
			.Write("top.frames[""fraHeader""].document.forms[0].Sel[" & Request.QueryString.Item("nIndex") & "].checked=false:")
			.Write("top.frames[""fraHeader""].document.forms[0].Sel.checked=false;")
			.Write("alert(""Err 11213:  " & lstrMessage & """);")
		End With
	Else
		If lclsBranches.valExistProduc(CInt(Request.QueryString.Item("nBranch"))) Then
			lstrMessage = lclsGeneral.insLoadMessage(11338)
			With Response
				.Write("(typeof(top.frames[""fraHeader""].document.forms[0].Sel[" & Request.QueryString.Item("nIndex") & "])!='undefined')?")
				.Write("top.frames[""fraHeader""].document.forms[0].Sel[" & Request.QueryString.Item("nIndex") & "].checked=false:")
				.Write("top.frames[""fraHeader""].document.forms[0].Sel.checked=false;")
				.Write("alert(""Err 11338:  " & lstrMessage & """);")
			End With
		End If
	End If
	
	Response.Write("top.frames[""fraHeader""].document.cmdDelete.disabled = false;")
	
	lclsGeneral = Nothing
	lclsBranches = Nothing
End Sub

</script>
<%Response.Expires = -1
mclsValues = New eFunctions.Values
%>
<HTML>
<HEAD>


</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "DeleteDP001"
		Call insDeleteDP001()
End Select

Response.Write(mclsValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mclsValues = Nothing
%>






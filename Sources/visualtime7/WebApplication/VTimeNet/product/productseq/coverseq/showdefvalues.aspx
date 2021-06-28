<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">

Dim mobjValues As eFunctions.Values


'% Update_sStatregt: acción Finalizar de la secuencia
'%					 Se utiliza cuando se presiona el botón Finalizar de la ventana principal
'%					 correspondiente a la subsecuencia de coberturas (DP034_K)
'--------------------------------------------------------------------------------------------
Sub Update_sStatregt()
	'--------------------------------------------------------------------------------------------
	Dim lclsGen_cover As eProduct.Gen_cover
	lclsGen_cover = New eProduct.Gen_cover
	
	With lclsGen_cover
		If .Update_Status(Session("sBrancht"), Session("nBranch"), Session("nProduct"), Session("nModulec"), Session("nCover"), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate), "1") Then
			With Response
				.Write("var lstrHref = '/VTimeNet/Product/ProductSeq/DP033.aspx?sOnSeq=1&sCodispl=DP033&nMainAction=302&nModulec=" & Session("nModulec") & "';")
				.Write("top.opener.top.frames['fraFolder'].location.href=lstrHref;")
			End With
		End If
	End With
	lclsGen_cover = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>


</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "Finish"
		Call Update_sStatregt()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>





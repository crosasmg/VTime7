<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCashBank" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores	
Dim mobjValues As eFunctions.Values

'- Variable auxiliar para manejo de moneda
Dim mintCurrency As Object


'% insShownCashnum: se muestran el número de caja asociada al usuario en tratamiento
'--------------------------------------------------------------------------------------------
Sub insShownCashnum()
	'--------------------------------------------------------------------------------------------
	Dim lclsUser_cashnum As eCashBank.User_cashnum
	Dim lobjValues As eFunctions.Values
	
	With Server
		lclsUser_cashnum = New eCashBank.User_cashnum
		lobjValues = New eFunctions.Values
	End With
	
	If lclsUser_cashnum.Find_nUser(lobjValues.StringToType(Session("nUsercode"), eFunctions.Values.eTypeData.etdDouble)) Then
		Response.Write("opener.document.forms[0].tcnCashnum.value=" & lclsUser_cashnum.nCashnum & ";")
	Else
		Response.Write("opener.document.forms[0].tcnCashnum.value=''")
	End If
	
	lclsUser_cashnum = Nothing
	lobjValues = Nothing
	
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
%>
<HTML>
<HEAD>
    <%=mobjValues.StyleSheet()%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>


</HEAD>
<BODY>
	<FORM NAME="ShowValues">
	</FORM>
</BODY>
</HTML>
<%
Response.Write("<SCRIPT>")
Select Case Request.QueryString.Item("Field")
	Case "nCashnum"
		Call insShownCashnum()
End Select

Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")

mobjValues = Nothing
%>





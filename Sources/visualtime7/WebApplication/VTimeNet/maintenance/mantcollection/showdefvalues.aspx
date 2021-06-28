<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eCollection" %>
<script language="VB" runat="Server">
Dim mobjValues As eFunctions.Values


'% insShowDescrip: se muestra las descripciones del codigo de  la tabla
'--------------------------------------------------------------------------------------------
Sub insShowDescrip()
	'--------------------------------------------------------------------------------------------
	Dim lclsCollect_comm As eCollection.Collect_comm
	lclsCollect_comm = New eCollection.Collect_comm
	
	If CBool(lclsCollect_comm.FindDescript(CInt(Request.QueryString.Item("nCode"))).ToOADate) Then
		Response.Write("top.frames['fraHeader'].document.forms[0].tctDescript.value='" & lclsCollect_comm.sDescript & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].tctShort_des.value='" & lclsCollect_comm.sShort_des & "';")
		Response.Write("top.frames['fraHeader'].document.forms[0].cbeStatregt.value='" & lclsCollect_comm.sStatregt & "';")
	End If
	
	lclsCollect_comm = Nothing
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
	Case "Descript"
		Call insShowDescrip()
End Select
Response.Write(mobjValues.CloseShowDefValues(Request.QueryString.Item("sFrameCaller")))
Response.Write("</SCRIPT>")
mobjValues = Nothing
%>







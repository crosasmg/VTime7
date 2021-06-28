<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eLetter" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 09/05/2003 10:49:56 a.m.
Dim mobjNetFrameWork As eNetFrameWork.Layout
'~End Header Block VisualTimer Utility
Dim a As eFunctions.Values



'%insGetVariables
'-----------------------------------
Private Function insFindLetters() As Object
	'dim NumNull As Short
	'-----------------------------------    
	Dim lcolTabLetter As eLetter.Letters
	lcolTabLetter = New eLetter.Letters
	If lcolTabLetter.FindTab_Letters(CShort(Request.QueryString.Item("nLetterNum"))) Then
		Response.Write("<SCRIPT>")
		Response.Write("opener.document.forms[0].tctDescript.value='" & lcolTabLetter.Item(1).sDescript & "';")
		Response.Write("opener.document.forms[0].tctDescript.disabled = true;")
		
		If lcolTabLetter.Item(1).sCtroLettInd = "1" Then
			Response.Write("opener.document.forms[0].chkCtroLettInd.checked=true;")
		Else
			Response.Write("opener.document.forms[0].chkCtroLettInd.checked=false;")
		End If
		
		Response.Write("opener.document.forms[0].chkCtroLettInd.disabled = true;")
		
		
		If lcolTabLetter.Item(1).nMinTimeAns = intNull Or IsNothing(lcolTabLetter.Item(1).nMinTimeAns) Then
			Response.Write("opener.document.forms[0].tcnMinTimeAns.value='';")
		Else
			Response.Write("opener.document.forms[0].tcnMinTimeAns.value=" & lcolTabLetter.Item(1).nMinTimeAns & ";")
		End If
		
		Response.Write("opener.document.forms[0].tcnMinTimeAns.disabled = true;")
		
		If lcolTabLetter.Item(1).sDelivInvalidind = "1" Then
			Response.Write("opener.document.forms[0].chksDelivInvalidind.checked = true;")
		Else
			Response.Write("opener.document.forms[0].chksDelivInvalidind.checked = false;")
		End If
		
		Response.Write("opener.document.forms[0].chksDelivInvalidind.disabled = true;")
		
		Response.Write("</" & "Script>")
	Else
		Response.Write("<SCRIPT>")
		Response.Write("opener.document.forms[0].tctDescript.value='';")
		Response.Write("opener.document.forms[0].tctDescript.disabled = false;")
		Response.Write("opener.document.forms[0].chkCtroLettInd.checked=false;")
		Response.Write("opener.document.forms[0].chkCtroLettInd.disabled = false;")
		Response.Write("opener.document.forms[0].tcnMinTimeAns.disabled = true;")
		Response.Write("opener.document.forms[0].tcnMinTimeAns.value='';")
		Response.Write("opener.document.forms[0].chksDelivInvalidind.disabled = false;")
		Response.Write("opener.document.forms[0].chksDelivInvalidind.value = false;")
		Response.Write("</" & "Script>")
	End If
	lcolTabLetter = Nothing
End Function

</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = Session.SessionID
Call mobjNetFrameWork.BeginPage("DefValuesLett")
a = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 09/05/2003 10:49:57 a.m.
a.sSessionID = Session.SessionID
'~End Body Block VisualTimer Utility

a.sCodisplPage = "DefValuesLett"
%>
<HTML>
<HEAD>
	

<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
	

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Constantes.aspx" -->

	

<!-- #INCLUDE VIRTUAL="~/VTimeNet/Includes/Collection.aspx" -->
		
</HEAD>
<BODY>
<FORM id=form1 name=form1>    
<%
Select Case Request.QueryString.Item("Field")
	Case "LT001"
		insFindLetters()
		Response.Write("<SCRIPT>window.close();</script>")
End Select
%>
</FORM>    
</BODY>
</HTML>
<%'^Begin Footer Block VisualTimer Utility 1.1 09/05/2003 10:49:57 a.m.
Call mobjNetFrameWork.FinishPage("DefValuesLett")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>








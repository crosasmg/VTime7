<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values

'- Objeto para el manejo de las rutinas genéricas

Dim mobjMenu As eFunctions.Menues

    Dim mintNull As String


'----------------------------------------------------------------------------
Private Sub insLoadOP008_k()
	'----------------------------------------------------------------------------
	Call insOldValues()
	
Response.Write("" & vbCrLf)
Response.Write("<TABLE WIDTH=""100%"">" & vbCrLf)
Response.Write("	<TR>" & vbCrLf)
Response.Write("	    ")

	           If mintNull = "1" Then
	               Response.Write("" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.OptionControl(40070, "optNull", GetLocalResourceObject("optNull_CStr1Caption"), CStr(1), CStr(1), "LoadFields(this)", Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.OptionControl(40071, "optNull", GetLocalResourceObject("optNull_CStr2Caption"), CStr(0), CStr(2), "LoadFields(this)", Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.OptionControl(40072, "optNull", GetLocalResourceObject("optNull_CStr3Caption"), CStr(0), CStr(3), "LoadFields(this)", Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        ")

	           ElseIf mintNull = "2" Then
	               Response.Write("" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.OptionControl(40073, "optNull", GetLocalResourceObject("optNull_CStr1Caption"), CStr(0), CStr(1), "LoadFields(this)", Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.OptionControl(40074, "optNull", GetLocalResourceObject("optNull_CStr2Caption"), CStr(1), CStr(2), "LoadFields(this)", Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.OptionControl(40075, "optNull", GetLocalResourceObject("optNull_CStr3Caption"), CStr(0), CStr(3), "LoadFields(this)", Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        ")

	           ElseIf mintNull = "3" Then
	               Response.Write("" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.OptionControl(40073, "optNull", GetLocalResourceObject("optNull_CStr1Caption"), CStr(0), CStr(1), "LoadFields(this)", Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.OptionControl(40074, "optNull", GetLocalResourceObject("optNull_CStr2Caption"), CStr(0), CStr(2), "LoadFields(this)", Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.OptionControl(40075, "optNull", GetLocalResourceObject("optNull_CStr3Caption"), CStr(1), CStr(3), "LoadFields(this)", Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        ")

	           Else
	               Response.Write("" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.OptionControl(40073, "optNull", GetLocalResourceObject("optNull_CStr1Caption"), CStr(0), CStr(1), "LoadFields(this)", Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.OptionControl(40074, "optNull", GetLocalResourceObject("optNull_CStr2Caption"), CStr(1), CStr(2), "LoadFields(this)", Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.OptionControl(40075, "optNull", GetLocalResourceObject("optNull_CStr3Caption"), CStr(0), CStr(3), "LoadFields(this)", Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        ")

	           End If
	           Response.Write("" & vbCrLf)
	           Response.Write("" & vbCrLf)
	           Response.Write("        <TD><LABEL ID=8664>" & GetLocalResourceObject("gmnCheNumCaption") & "</LABEL></TD>" & vbCrLf)
	           Response.Write("        ")

	           If mintNull = "1" Then
	               Response.Write("" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.NumericControl("gmnCheNum", 10, CStr(0), , GetLocalResourceObject("gmnCheNumToolTip"), False, 0, , , , , Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        ")

	           ElseIf mintNull = "2" Then
	               Response.Write("" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.TextControl("gmtCheque", 10, "", , GetLocalResourceObject("gmtChequeToolTip"), , , , , Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        ")

	           ElseIf mintNull = "3" Then
	               Response.Write("" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.NumericControl("gmnBordereaux", 10, CStr(0), , GetLocalResourceObject("gmnBordereauxToolTip"), False, 0, , , , , Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        ")

	           Else
	               Response.Write("" & vbCrLf)
	               Response.Write("        <TD>")


	               Response.Write(mobjValues.TextControl("gmtCheque", 10, "", , GetLocalResourceObject("gmtChequeToolTip"), , , , , Request.QueryString.Item("mintNull") = vbNullString))


	               Response.Write("</TD>" & vbCrLf)
	               Response.Write("        ")

	           End If
	           Response.Write("" & vbCrLf)
	           Response.Write("        " & vbCrLf)
	           Response.Write("        " & vbCrLf)
	           Response.Write("	</TR>" & vbCrLf)
	           Response.Write("</TABLE>		")

	
	       End Sub
	       '-----------------------------------------------------------------------------------------
	       Private Sub insOldValues()
	           '-----------------------------------------------------------------------------------------
	           If mintNull <> "" Then
	               With Response
	                   .Write("<SCRIPT>")
	                   .Write("var mintNull = " & CStr(mintNull) & ";")
	                   .Write("</" & "Script>")
	               End With
	           Else
	               With Response
	                   .Write("<SCRIPT>")
	                   .Write("var mintNull = 0;")
	                   .Write("</" & "Script>")
	               End With
	           End If
	       End Sub
	       '------------------------------------------------------------------------------------------------------------------
	       Private Function insReaInitial() As Object
	           '------------------------------------------------------------------------------------------------------------------
	
	           If Not Request.QueryString.Item("mintNull") = vbNullString Then
	               mintNull = Request.QueryString.Item("mintNull")
	           End If
	
	       End Function

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjMenu = New eFunctions.Menues

mobjValues.sCodisplPage = "op008_k"
%>

<SCRIPT>
//% insCancel: Ejecuta rutinas necesarias en el momento de cancelar la página
//------------------------------------------------------------------------------------------
function insCancel(){
//------------------------------------------------------------------------------------------
   return (true);
}
//-------------------------------------------------------------------------------------------
function LoadFields(Field){
//-------------------------------------------------------------------------------------------
	if (mintNull != Field.value){
	    self.document.location.href="/VTimeNet/CashBank/CashBank/OP008_k.aspx?sCodispl=OP008&mintNull=" + Field.value + "&sField=" + Field.name
	}
}

</SCRIPT>

<HTML>
<HEAD>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/tmenu.js"></SCRIPT>
	<SCRIPT> 
		function insStateZone(){
			with (self.document.forms[0])
			{
				optNull[0].disabled = false;
				optNull[1].disabled = false;
				optNull[2].disabled = false;
				if (optNull[0].checked) gmnCheNum.disabled = false;
				if (optNull[1].checked) gmtCheque.disabled = false;
				if (optNull[2].checked) gmnBordereaux.disabled = false;
			}
		}
        document.VssVersion="$$Revision: 2 $|$$Date: 9/03/04 11:53 $"
	</SCRIPT>

<%With Response
	.Write(mobjValues.StyleSheet)
	.Write(mobjMenu.MakeMenu("OP008", "OP008_k.aspx", 1, ""))
End With
mobjMenu = Nothing
%>
</HEAD>

<BODY ONUNLOAD="closeWindows();">
<FORM METHOD="POST" ID="FORM" NAME="frmTransBank" ACTION="ValCashBank.aspx?X=1">
<BR><BR>
<%
Call insReaInitial()
Call insLoadOP008_k()
mobjValues = Nothing
%>
</BODY>
</FORM>
</HTML>






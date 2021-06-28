<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eNetFrameWork" %>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eGeneralForm" %>
<script language="VB" runat="Server">
'^Begin Header Block VisualTimer Utility 1.1 31/3/03 19.42.05
Dim mobjNetFrameWork As eNetFrameWork.Layout

Dim mobjValues As eFunctions.Values
Dim lobjPhone As eGeneralForm.Phone


</script>
<%Response.Expires = -1441
mobjNetFrameWork = New eNetFrameWork.Layout
mobjNetFrameWork.sSessionID = session.SessionID
mobjNetFrameWork.nUsercode = session("nUsercode")
Call mobjNetFrameWork.BeginPage("SCA001Upd")
'~End Header Block VisualTimer Utility
Response.CacheControl = "private"
mobjValues = New eFunctions.Values
'^Begin Body Block VisualTimer Utility 1.1 31/3/03 19.42.05
mobjValues.sSessionID = session.SessionID
mobjValues.nUsercode = session("nUsercode")
'~End Body Block VisualTimer Utility

mobjValues.sCodisplPage = Request.QueryString.Item("sCodispl")
%>
<HTML>
<HEAD>
<META NAME="GENERATOR" CONTENT="Microsoft Visual Studio 6.0"/>
    <%=mobjValues.StyleSheet()%>
<TITLE>Página de Actualización de teléfonos</TITLE>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<%="<SCRIPT>"%>
    var CurrentIndex 
    function ShowFields(Index){
        var lintIndex=0
        with (self.document.forms[0]){
            elements[0].value = opener.marrSCA001Phones[Index][1]
			elements[1].value = opener.marrSCA001Phones[Index][2]
			elements[2].value = opener.marrSCA001Phones[Index][3]
			elements[3].value = opener.marrSCA001Phones[Index][4]
			elements[4].value = opener.marrSCA001Phones[Index][5]
			elements[5].value = opener.marrSCA001Phones[Index][6]
		}
    }
// MoveRecord: Se posiciona en el valor del arreglo de la forma
//-------------------------------------------------------------------------------------------
	function MoveRecord(Option){
//-------------------------------------------------------------------------------------------
		var lintIndex = CurrentIndex
		switch (Option){
			case "Back":
				lintIndex--;
				break;
			case "Next":
				lintIndex++;
		}
		
		if (lintIndex >= 0)
			if (lintIndex < opener.marrSCA001Phones.length){
				ShowFields(lintIndex);
				CurrentIndex = lintIndex
			}
	}    
	
// ChangeSubmit: Cambia la accion de la forma
//-------------------------------------------------------------------------------------------
function ChangeSubmit(Option) {
//-------------------------------------------------------------------------------------------	
		switch (Option) {
			case "Add":
				document.forms[0].action = "valGeneralForm.aspx?sCodispl=<%=Request.QueryString.Item("sCodispl")%>&Action=Add&WindowType=PopUp&nRecowner=<%=Request.QueryString.Item("nRecowner")%>&sKeyAddress=<%=Request.QueryString.Item("sKeyAddress")%>&nMainAction=<%=Request.QueryString.Item("nMainAction")%>&sOnSeq=<%=Request.QueryString.Item("sOnSeq")%>&sRecType=<%=Request.QueryString.Item("sRecType")%>"  
				break;
			case "Update":
				document.forms[0].action = "valGeneralForm.aspx?sCodispl=<%=Request.QueryString.Item("sCodispl")%>&Action=Update&WindowType=PopUp&ReloadIndex=" + CurrentIndex + "&nRecowner=<%=Request.QueryString.Item("nRecowner")%>&sKeyAddress=<%=Request.QueryString.Item("sKeyAddress")%>&nMainAction=<%=Request.QueryString.Item("nMainAction")%>&sOnSeq=<%=Request.QueryString.Item("sOnSeq")%>&sRecType=<%=Request.QueryString.Item("sRecType")%>" 
		}
}
	
// ChangeSubmit: Habilita/Deshabilita los controles excluyentes de la página
//-------------------------------------------------------------------------------------------
function LockControl(Field, Value){
//-------------------------------------------------------------------------------------------
}	
</SCRIPT>
</HEAD>
<BODY>
<FORM ID=form1 NAME=form1 METHOD="POST" ACTION="SCA001.aspx">
	<%Response.Write("<SCRIPT>CurrentIndex=0" & Request.QueryString.Item("Index") & "</SCRIPT>")
If Request.QueryString.Item("Action") = "Delete" Then
	Response.Write(mobjValues.ConfirmDelete)
	lobjPhone = New eGeneralForm.Phone
	With Request
		lobjPhone.Find(Request.QueryString.Item("sKeyAddress"), CInt(Request.QueryString.Item("nKeyPhones")), CShort(Request.QueryString.Item("nRecowner")), session("SCA101_dEffecDate"))
	End With
	lobjPhone.Delete()
	lobjPhone = Nothing
	Response.Write(mobjValues.ConfirmDelete(True))
Else
	%>
    <TABLE COLS=2>
        <TR>
            <TD><LABEL ID=41051><%= GetLocalResourceObject("tcnOrderCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnOrder", 5, "", True, "", False, 0)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=41052><%= GetLocalResourceObject("cbePhoneTypeCaption") %> </LABEL></TD>
            <TD><%=mobjValues.PossiblesValues("cbePhoneType", "Table564", 1, "")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=41053><%= GetLocalResourceObject("tcnAreaCaption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnArea", 5, "", True, "", False, 0)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=41054><%= GetLocalResourceObject("tctPhoneCaption") %></LABEL></TD>
            <TD><%=mobjValues.TextControl("tctPhone", 10, "", True, "")%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=41055><%= GetLocalResourceObject("tcnExtensi1Caption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnExtensi1", 3, "", True, "", False, 0)%></TD>
        </TR>
        <TR>
            <TD><LABEL ID=41056><%= GetLocalResourceObject("tcnExtensi2Caption") %></LABEL></TD>
            <TD><%=mobjValues.NumericControl("tcnExtensi2", 3, "", True, "", False, 0)%></TD>
        </TR>
    </TABLE>
	<table WIDTH="100%">
		<%	If Request.QueryString.Item("Action") = "Update" Then
		Response.Write(mobjValues.ButtonBackNext(2))
	End If
	%>
		<TR>
			<td COLSPAN="3"><hr></td>
		</TR>
		<TR>
			<TD><%=mobjValues.CheckControl("chkContinue", GetLocalResourceObject("chkContinueCaption"), "1")%></TD>
			<TD ALIGN="Right"><%=mobjValues.ButtonAcceptCancel()%></TD>
		</TR>
	</table> 
    <%End If%>
</FORM>
<%Select Case Request.QueryString.Item("Action")
	Case "Add"
		Response.Write("<SCRIPT>ChangeSubmit(""Add"");</SCRIPT>")
	Case "Update"
		With Response
			.Write("<SCRIPT>ShowFields(" & Request.QueryString.Item("Index") & ");")
			.Write("ChangeSubmit(""Update"");</SCRIPT>")
			'				.Write "LockControl(this,""Update"");" 
		End With
	Case "Delete"
		
End Select

mobjValues = Nothing
%>
</BODY>
</HTML>

<%'^Begin Footer Block VisualTimer Utility 1.1 31/3/03 19.42.05
Call mobjNetFrameWork.FinishPage("SCA001Upd")
mobjNetFrameWork = Nothing
'^End Footer Block VisualTimer%>





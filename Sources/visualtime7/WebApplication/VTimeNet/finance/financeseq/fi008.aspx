<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eFinance" %>
<script language="VB" runat="Server">

'- Objeto para el manejo de las funciones generales de carga de valores
Dim mobjValues As eFunctions.Values

Dim lblnActionQuery As Boolean
Dim lblnEnabledcboWaitCode As Boolean
Dim lstrTextMessage As String
Dim lintcboWaitCode_Index As Byte


'--------------------------------------------------------------------------------------------
'%insLoadFI008 : Rutina que carga la información del proceso en la ventana.
'--------------------------------------------------------------------------------------------
Private Sub insLoadFI008()
	Dim lclsFinanc_win As eFinance.FinanceWin
	lclsFinanc_win = New eFinance.FinanceWin
	lstrTextMessage = vbNullString
	
	With mobjValues
		If lclsFinanc_win.IsPageRequired(.StringToType(Session("nContrat"), eFunctions.Values.eTypeData.etdDouble), .StringToType(Session("deffecdate"), eFunctions.Values.eTypeData.etdDate), .StringToType(Session("nTransaction"), eFunctions.Values.eTypeData.etdDouble)) Then
			lstrTextMessage = lclsFinanc_win.insConcatMessage(lstrTextMessage, 21134)
			lintcboWaitCode_Index = 1
			lblnEnabledcboWaitCode = False
		Else
			lstrTextMessage = lclsFinanc_win.insConcatMessage(lstrTextMessage, 4327)
			lintcboWaitCode_Index = 0
			lblnEnabledcboWaitCode = True
		End If
	End With
	lstrTextMessage = lstrTextMessage & lclsFinanc_win.insConcatMessage(lstrTextMessage, 21135)
	lstrTextMessage = lstrTextMessage & lclsFinanc_win.insConcatMessage(lstrTextMessage, 3909)
	lclsFinanc_win = Nothing
End Sub

</script>
<%Response.Expires = -1

mobjValues = New eFunctions.Values
mobjValues.ActionQuery = Session("bQuery")

mobjValues.sCodisplPage = "fi008"

Call insLoadFI008()
%>


<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/Constantes.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
    <HEAD>
        <%=mobjValues.WindowsTitle("FI008")%>
        <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">
        <%=mobjValues.StyleSheet()%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmFI008" ACTION="valFinanceSeq.aspx?nAction=392">
            <TABLE BORDER=1 CELLPADDING=5 BGCOLOR=WHITE>
                <TR>
                    <TD WIDTH="100%"><%
With mobjValues
	lblnActionQuery = .ActionQuery
	.ActionQuery = True
	Response.Write(mobjValues.TextAreaControl("txtMessage", 5, 40, lstrTextMessage))
	.ActionQuery = lblnActionQuery
End With%>
                    </TD>
                </TR>
            </TABLE>
            <TABLE WIDTH="100%">
                <TR><TD></TD></TR>
                <TR>
                    <TD><%
Response.Write(mobjValues.PossiblesValues("cboWaitCode", "table256", eFunctions.Values.eValuesType.clngComboType, CStr(CShort(lintcboWaitCode_Index)),  ,  ,  ,  ,  ,  , Not CBool(lblnEnabledcboWaitCode),  , GetLocalResourceObject("cboWaitCodeToolTip")))
%>
					</TD>
                    <TD></TD>
                    <TD></TD>
                    <TD></TD>
                </TR>

                <TR>
                    <TD><BR></TD>
                </TR>
                <TR>
                    <TD><%=mobjValues.ButtonAcceptCancel("EnabledControl()",  , True, 2)%></TD>
                </TR>
            </TABLE>
        </FORM>
    </BODY>
</HTML>
<%
mobjValues = Nothing
%>





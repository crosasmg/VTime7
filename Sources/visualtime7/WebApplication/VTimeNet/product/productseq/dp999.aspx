<%@ Page Language="VB" explicit="true" Inherits="InMotionGIT.Web.Page.BackOfficeCommon" EnableViewState="false"%>
<%@ Import namespace="eFunctions" %>
<%@ Import namespace="eProduct" %>
<script language="VB" runat="Server">
'- Objeto para el manejo de las funciones generales de carga de valores

Dim mobjValues As eFunctions.Values
Dim msStatregt As String
Dim mblnInformation As Boolean
Dim mblnVisibleInformation As Boolean
Dim mintInformation As Byte
Dim mblnDisabledStatProduct As Boolean


'% insPreDP999 : Establece el estado inicial de la página
'-------------------------------------------------------------------------------------------
Private Sub insPreDP999()
	'-------------------------------------------------------------------------------------------
	Dim lclsProdWin As eProduct.Prod_win
	Dim lclsProduct As eProduct.Product
	lclsProdWin = New eProduct.Prod_win
	lclsProduct = New eProduct.Product
	If Not lclsProdWin.insValSequence(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("dEffecdate"), eFunctions.Values.eTypeData.etdDate)) Then
		msStatregt = "2"
		mblnInformation = False
	Else
		If lclsProduct.FindProdMasterActive(mobjValues.StringToType(Session("nBranch"), eFunctions.Values.eTypeData.etdDouble), mobjValues.StringToType(Session("nProduct"), eFunctions.Values.eTypeData.etdDouble)) Then
			If msStatregt = "2" Or msStatregt = vbNullString Then
				msStatregt = "1"
			End If
			mblnInformation = True
		End If
	End If
	If mblnInformation Then
		mblnVisibleInformation = False
		mblnDisabledStatProduct = False
	Else
		mblnVisibleInformation = True
		mintInformation = 1
		mblnDisabledStatProduct = True
	End If
	lclsProdWin = Nothing
	lclsProduct = Nothing
End Sub

</script>
<%Response.Expires = -1
mobjValues = New eFunctions.Values
mobjValues.sCodisplPage = "DP999"

Call insPreDP999()
%>
<SCRIPT LANGUAGE="JavaScript" SRC="/VTimeNet/Scripts/GenFunctions.js"></SCRIPT>
<HTML>
    <HEAD>
        <%=mobjValues.WindowsTitle("DP999")%>
        <META NAME = "GENERATOR" Content = "Microsoft Visual Studio 6.0">


        <%=mobjValues.StyleSheet()%>
    </HEAD>
    <BODY ONUNLOAD="closeWindows();">
        <FORM METHOD="post" ID="FORM" NAME="frmProductEnd" ACTION="valProductSeq.aspx?sCodispl=<%=Request.QueryString.Item("sCodispl")%>&nAction=<%=Request.QueryString.Item("nAction")%>">
            <TABLE WIDTH=100% BORDER="1" CELLPADDING=5 BGCOLOR="white">
                <TR>
                    <TD>
                        <LABEL ID=14937><%= GetLocalResourceObject("cboStatProductCaption") %></LABEL>
                        <%=mobjValues.PossiblesValues("cboStatProduct", "Table26", eFunctions.Values.eValuesType.clngComboType, CStr(msStatregt),  ,  ,  ,  ,  ,  , CBool(mblnDisabledStatProduct),  , "")%>
                    </TD>
                </TR>
            </TABLE>
            <TABLE>
                <TR><%If mblnVisibleInformation Then%>
                    <TD><%=mobjValues.CheckControl("chkInformation", GetLocalResourceObject("chkInformationCaption"), CStr(mintInformation),  ,  , True)%></TD>
                    <%End If%>
                </TR>
                <TR>
                    <TD><%=mobjValues.ButtonAcceptCancel("self.document.forms[0].cboStatProduct.disabled=false",  ,  ,  , eFunctions.Values.eButtonsToShow.All)%></TD>
                </TR>
            </TABLE>
        </FORM>
<%
mobjValues = Nothing%>
    </BODY>
</HTML>




